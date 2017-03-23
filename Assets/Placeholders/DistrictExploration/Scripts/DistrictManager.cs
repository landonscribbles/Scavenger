using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrictManager : MonoBehaviour {

    private bool initialized = false;
    public bool Initialized {
        get {
            return initialized;
        }
    }

    private int metals;
    private int fabrics;
    private int fuel;
    private int electronics;
    private int provisions;

    private LevelGeneration levelGeneration;

    private List<GameObject> districtRooms;

    private List<LootableContainer> lootableContainers;

    private List<InventoryItemInterface> itemsInDistrict;

    void Start() {
        lootableContainers = new List<LootableContainer>();
        levelGeneration = GameObject.Find("LevelGeneration").GetComponent<LevelGeneration>();
        districtRooms = levelGeneration.BuildRooms();

        // TESTING REMOVE ME LATER
        Initialize(10, 9, 8, 7, 6, districtRooms);
        //
    }

    public void Initialize(int _metals, int _fabrics, int _fuel, int _electronics, int _provisions, List<GameObject> _districtRooms, List<InventoryItemInterface> _itemsInDistrict) {
        initialized = true;

        metals = _metals;
        fabrics = _fabrics;
        fuel = _fuel;
        electronics = _electronics;
        provisions = _provisions;

        itemsInDistrict = _itemsInDistrict;

        districtRooms = _districtRooms;

        PlaceItemsInDistrict();
    }

    public void Initialize(int _metals, int _fabrics, int _fuel, int _electronics, int _provisions, List<GameObject> _districtRooms) {
        initialized = true;

        metals = _metals;
        fabrics = _fabrics;
        fuel = _fuel;
        electronics = _electronics;
        provisions = _provisions;

        itemsInDistrict = new List<InventoryItemInterface>();

        districtRooms = _districtRooms;

        PlaceItemsInDistrict();
    }

    private void PlaceItemsInDistrict() {
        FindAllLootableContainers();

        for (int i = metals; i > 0; i--) {
            PickRandomLootableContainer().Metals += 1;
        }
        
        for (int i = fabrics; i > 0; i--) {
            PickRandomLootableContainer().Fabrics += 1;
        }

        for (int i = fuel; i > 0; i--) {
            PickRandomLootableContainer().Fuel += 1;
        }

        for (int i = electronics; i > 0; i--) {
            PickRandomLootableContainer().Electronics += 1;
        }

        for (int i = provisions; i > 0; i--) {
            PickRandomLootableContainer().Provisions += 1;
        }

        for (int i = 0; i < itemsInDistrict.Count; i++) {
            PickRandomLootableContainer().AddDisplayItem(itemsInDistrict[i]);
        }

    }

    private LootableContainer PickRandomLootableContainer() {
        int lootableIdx = Random.Range(0, lootableContainers.Count);
        return lootableContainers[lootableIdx];
    }

    private void FindAllLootableContainers() {
        for (int i = 0; i < districtRooms.Count; i++) {
            ExplorableSection explorableSection = districtRooms[i].GetComponent<ExplorableSection>();
            for (int j = 0; j < explorableSection.LootableContainers.Length; j++) {
                lootableContainers.Add(explorableSection.LootableContainers[j].GetComponent<LootableContainer>());
            }
        }
    }

}
