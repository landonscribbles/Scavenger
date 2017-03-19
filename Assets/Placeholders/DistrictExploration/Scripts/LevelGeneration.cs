using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System;

public class LevelGeneration : MonoBehaviour {

    [SerializeField]
    private int buildIterations;

    [SerializeField]
    private GameObject startingRoomPrefab;

    [SerializeField]
    private GameObject[] rooms;
    private Dictionary<GameObject, int> createdRoomToRoomIdx;

    private Dictionary<ExplorableSection.RoomType, List<GameObject>> instantiatedRooms;

    private List<GameObject> createdRooms;

    private GameObject startingRoom;
    public GameObject StartingRoom {
        get {
            return startingRoom;
        }
    }

    private GameObject playerStartPoint;
    public GameObject PlayerStartPoint {
        get {
            return playerStartPoint;
        }
    }

    void Awake() {
        createdRooms = new List<GameObject>();
        createRoomBuildingPool();
        startingRoom = Instantiate(startingRoomPrefab, transform.position, Quaternion.identity);
        ExplorableSectionExit.ExitDirection startingPlayerExit = ExplorableSectionExit.ExitDirection.west;
       GameObject[] startingRoomExits = startingRoom.GetComponent<ExplorableSection>().Exits;
        for (int i = 0; i < startingRoomExits.Length; i++) {
            ExplorableSectionExit startingRoomExit = startingRoomExits[i].GetComponent<ExplorableSectionExit>();
            if (startingRoomExit.ExitOrientation == startingPlayerExit) {
                playerStartPoint = startingRoomExits[i];
            }
        }
        buildRooms();
        removeRoomBuildingPool();

    }

    private void createRoomBuildingPool() {
        createdRoomToRoomIdx = new Dictionary<GameObject, int>();
        instantiatedRooms = new Dictionary<ExplorableSection.RoomType, List<GameObject>>();
        for (int i = 0; i < rooms.Length; i++) {
            GameObject newRoom = Instantiate(rooms[i], transform.position, Quaternion.identity);
            ExplorableSection newRoomExplorableSection = newRoom.GetComponent<ExplorableSection>();
            if (!instantiatedRooms.ContainsKey(newRoomExplorableSection.SectionRoomType)) {
                instantiatedRooms[newRoomExplorableSection.SectionRoomType] = new List<GameObject>();
            }
            instantiatedRooms[newRoomExplorableSection.SectionRoomType].Add(newRoom);
            createdRoomToRoomIdx[newRoom] = i;
        }
    }

    private void replenishRoomPool(GameObject usedRoom, ExplorableSection.RoomType connectingType, int connectingTypeIdx) {
        GameObject replacementRoom = Instantiate(rooms[createdRoomToRoomIdx[usedRoom]], transform.position, Quaternion.identity);
        createdRoomToRoomIdx[replacementRoom] = createdRoomToRoomIdx[usedRoom];
        createdRoomToRoomIdx.Remove(usedRoom);
        instantiatedRooms[connectingType][connectingTypeIdx] = replacementRoom;
    }

    private void removeRoomBuildingPool() {
        ExplorableSection.RoomType[] roomTypes = (ExplorableSection.RoomType[])System.Enum.GetValues(typeof(ExplorableSection.RoomType));

        for (int i = 0; i < roomTypes.Length; i++) {
            if (instantiatedRooms[roomTypes[i]] != null) {
                for (int j = 0; j < instantiatedRooms[roomTypes[i]].Count; j++) {
                    Destroy(instantiatedRooms[roomTypes[i]][j]);
                }
                instantiatedRooms[roomTypes[i]].Clear();
            }
        }
    }


    private void buildRooms() {
        // playerStartPoint is the side of the room that the player starts in, currently it should only support
        // east or west (as that is what the hallway is by default). Later this should be changed to allow the starting
        // room type to be rotated as the game call for it
        ExplorableSection currentRoomExplorable = startingRoom.GetComponent<ExplorableSection>();
        List<GameObject> pendingExits = new List<GameObject>();
        for (int i = 0; i < currentRoomExplorable.Exits.Length; i++) {
            if (currentRoomExplorable.Exits[i] != playerStartPoint) {
                pendingExits.Add(currentRoomExplorable.Exits[i]);
            }
        }
        
        createdRooms.Add(startingRoom);

        for (int i = 0; i < buildIterations; i++) {
            List<GameObject> newExits = new List<GameObject>();
            for (int j = 0; j < pendingExits.Count; j++) {
                ExplorableSectionExit sectionExit = pendingExits[j].GetComponent<ExplorableSectionExit>();
                ExplorableSection.RoomType connectingType = sectionExit.ConnectableRoomTypes[Random.Range(0, sectionExit.ConnectableRoomTypes.Length)];
                currentRoomExplorable = placeRoom(connectingType, pendingExits[j]);
                if (currentRoomExplorable != null) {
                    for (int k = 0; k < currentRoomExplorable.Exits.Length; k++) {
                        if (currentRoomExplorable.RoomAttachedExitLocation != currentRoomExplorable.Exits[k]) {
                            newExits.Add(currentRoomExplorable.Exits[k]);
                        }
                    }
                }
            }
            pendingExits = newExits;
        }
    }

    private ExplorableSection placeRoom(ExplorableSection.RoomType connectingType, GameObject exitToConnectTo) {
        GameObject connectableRoom = null;
        ExplorableSection connectableRoomExplorable = null;
        List<int> roomIdxToAttempt = getRandomRoomIdx(connectingType);

        for (int i = 0; i < roomIdxToAttempt.Count; i++) {
            connectableRoom = instantiatedRooms[connectingType][roomIdxToAttempt[i]];
            connectableRoomExplorable = connectableRoom.GetComponent<ExplorableSection>();
            connectableRoomExplorable.AttachToExit(exitToConnectTo);
            replenishRoomPool(connectableRoom, connectingType, roomIdxToAttempt[i]);

            if (doesRoomOverlap(connectableRoom)) {
                Destroy(connectableRoom);
                connectableRoom = null;
                connectableRoomExplorable = null;
            } else {
                createdRooms.Add(connectableRoom);
                break;
            }
            
        }
        return connectableRoomExplorable;
    }

    private bool doesRoomOverlap(GameObject newRoom) {
        BoxCollider newRoomCollider = newRoom.GetComponent<BoxCollider>();
        for (int i = 0; i < createdRooms.Count; i++) {
            BoxCollider existingRoomCollider = createdRooms[i].GetComponent<BoxCollider>();
            if (newRoomCollider.bounds.Intersects(existingRoomCollider.bounds)) {
                return true;
            }
        }
        return false;
    }

    private List<int> getRandomRoomIdx(ExplorableSection.RoomType roomType) {
        List<int> randomizedRoomIdx = new List<int>();
        for (int i = 0; i < instantiatedRooms[roomType].Count; i++) {
            randomizedRoomIdx.Add(i);
        }
        int roomIdxCount = randomizedRoomIdx.Count;
        while (roomIdxCount > 1) {
            roomIdxCount--;
            int i = Random.Range(0, roomIdxCount);
            int val = randomizedRoomIdx[i];
            randomizedRoomIdx[i] = randomizedRoomIdx[roomIdxCount];
            randomizedRoomIdx[roomIdxCount] = val;
        }
        return randomizedRoomIdx;
    }

}
