using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplorableSection : MonoBehaviour {

    public enum RoomType { hallway, room, intersection };

    [SerializeField]
    private RoomType sectionRoomType;
    public RoomType SectionRoomType {
        get {
            return sectionRoomType;
        }
    }

    [SerializeField]
    private GameObject[] exits;
    public GameObject[] Exits {
        get {
            return exits;
        }
    }

    [SerializeField]
    private GameObject[] lootableContainers;
    public GameObject[] LootableContainers {
        get {
            return lootableContainers;
        }
    }

    private GameObject externalAttachedExitLocation;
    public GameObject ExternalAttachedExitLocation {
        get {
            return externalAttachedExitLocation;
        }
    }

    private GameObject roomAttachedExitLocation;
    public GameObject RoomAttachedExitLocation {
        get {
            return roomAttachedExitLocation;
        }
    }

    public void AttachToExit(GameObject otherRoomExitLocation) {
        externalAttachedExitLocation = otherRoomExitLocation;
        roomAttachedExitLocation = exits[Random.Range(0, exits.Length)];
        rotateToAttach(otherRoomExitLocation, roomAttachedExitLocation);
        transform.position = externalAttachedExitLocation.transform.position + (transform.position - roomAttachedExitLocation.transform.position);
    }

    private void rotateToAttach(GameObject exitToAttachTo, GameObject thisSectionExit) {
        ExplorableSectionExit toAttachExit = exitToAttachTo.GetComponent<ExplorableSectionExit>();
        ExplorableSectionExit ownedExit = thisSectionExit.GetComponent<ExplorableSectionExit>();
        ExplorableSectionExit.ExitDirection directionToRotateTo = getDirectionOpposite(toAttachExit.ExitOrientation);
        int numberOfRotations = getNumberOfRotationsRequired(ownedExit.ExitOrientation, directionToRotateTo);
        transform.Rotate(0, 0, numberOfRotations * 90);
        updateExitOrientations(numberOfRotations);
    }

    private ExplorableSectionExit.ExitDirection getDirectionOpposite(ExplorableSectionExit.ExitDirection inputDirection) {
        // Opposites are all spaced two apart in the enum
        int oppositeDirection = (int)inputDirection;
        int lengthOfDirections = ((ExplorableSectionExit.ExitDirection[])System.Enum.GetValues(typeof(ExplorableSectionExit.ExitDirection))).Length;
        for (int i = 0; i < 2; i++) {
            oppositeDirection += 1;
            if (oppositeDirection >= lengthOfDirections) {
                oppositeDirection -= lengthOfDirections;
            }
        }
        return ((ExplorableSectionExit.ExitDirection[])System.Enum.GetValues(typeof(ExplorableSectionExit.ExitDirection)))[oppositeDirection];
    }

    private int getNumberOfRotationsRequired(ExplorableSectionExit.ExitDirection currentDirection, ExplorableSectionExit.ExitDirection directionToMatch) {
        ExplorableSectionExit.ExitDirection[] exitDirections = (ExplorableSectionExit.ExitDirection[])System.Enum.GetValues(typeof(ExplorableSectionExit.ExitDirection));

        int startingDirectionIdx = 0;
        for (int i = 0; i < exitDirections.Length; i++) {
            if (currentDirection == exitDirections[i]) {
                startingDirectionIdx = i;
                break;
            }
        }

        int targetDirectionIdx = 0;
        for (int i = 0; i < exitDirections.Length; i++) {
            if (directionToMatch == exitDirections[i]) {
                targetDirectionIdx = i;
                break;
            }
        }

        int numberOfStepsToMatchDirection = 0;
        while (startingDirectionIdx != targetDirectionIdx) {
            startingDirectionIdx += 1;
            if (startingDirectionIdx >= exitDirections.Length) {
                startingDirectionIdx = startingDirectionIdx - exitDirections.Length;
            }
            numberOfStepsToMatchDirection += 1;
        }

        return numberOfStepsToMatchDirection;
    }

    private void updateExitOrientations(int numberOfRotations) {
        for (int i = 0; i < exits.Length; i++) {
            ExplorableSectionExit currentExit = exits[i].GetComponent<ExplorableSectionExit>();
            currentExit.ExitOrientation = getNewDirectionFromRotations(numberOfRotations, currentExit.ExitOrientation);
        }
    }

    private ExplorableSectionExit.ExitDirection getNewDirectionFromRotations(int numberOfRotations, ExplorableSectionExit.ExitDirection startingOrientation) {
        ExplorableSectionExit.ExitDirection[] exitDirections = (ExplorableSectionExit.ExitDirection[])System.Enum.GetValues(typeof(ExplorableSectionExit.ExitDirection));
        int orientationIdx = 0;
        for (int i = 0; i < exitDirections.Length; i++) {
            if (startingOrientation == exitDirections[i]) {
                orientationIdx = i;
                break;
            }
        }

        ExplorableSectionExit.ExitDirection newDirection;
        for (int i = 0; i < numberOfRotations; i++) {
            orientationIdx += 1;
            if (orientationIdx >= exitDirections.Length) {
                orientationIdx = orientationIdx - exitDirections.Length;
            }
        }
        newDirection = exitDirections[orientationIdx];

        return newDirection;
    }

}
