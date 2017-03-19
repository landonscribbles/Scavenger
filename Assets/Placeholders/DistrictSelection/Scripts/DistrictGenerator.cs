using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrictGenerator : MonoBehaviour {

    [SerializeField]
    private GameObject cityPlane;

    [SerializeField]
    private GameObject[] metalDistricPrefabs;
    [SerializeField]
    private GameObject[] fabricDistricPrefabs;
    [SerializeField]
    private GameObject[] flammableDistricPrefabs;
    [SerializeField]
    private GameObject[] electronicsDistricPrefabs;
    [SerializeField]
    private GameObject[] foodWaterDistricPrefabs;
    [SerializeField]
    private GameObject[] mixedDistricPrefabs;

    [SerializeField]
    private GameObject[] districtLocations;

    private List<GameObject> createdDistricts;
    private Dictionary<string, int> totalDistrictResources;

    [SerializeField]
    private int minFlammableTravelCost;
    [SerializeField]
    private int maxFlammableTravelCost;
    [SerializeField]
    private int minFoodWaterTravelCost;
    [SerializeField]
    private int maxFoodWaterTravelCost;

    private int flammableTravelCost;
    private int foodWaterTravelCost;

    private DisplayUIResources displayUIResources;

    void Start() {
        totalDistrictResources = new Dictionary<string, int>();
        createdDistricts = new List<GameObject>();
        displayUIResources = GameObject.Find("UI").GetComponent<DisplayUIResources>();
        generateDistrics(0, 0);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.G)) {
            for (int i = 0; i < createdDistricts.Count; i++) {
                Destroy(createdDistricts[i]);
            }
            generateDistrics(0, 0);
        }
    }

    public void generateDistrics(int minimumFoodWater, int minimumFlammable) {
        createTravelCost();
        GameObject[] prefabDistrics = createRandomDistricts();
        createdDistricts.Clear();
        for (int i = 0; i < districtLocations.Length; i++) {
            GameObject newDistrict = Instantiate(prefabDistrics[i], districtLocations[i].transform.position, cityPlane.transform.rotation) as GameObject;
            createdDistricts.Add(newDistrict);
        }
        calculateDistrictTotals();
        ensureResourcesForTravelExist();
        debugShowDistictTotals();
    }

    private GameObject[] createRandomDistricts() {
        GameObject[][] allDistricTypePrefabArrays = new GameObject[6][] {
            metalDistricPrefabs,
            fabricDistricPrefabs,
            flammableDistricPrefabs,
            electronicsDistricPrefabs,
            foodWaterDistricPrefabs,
            mixedDistricPrefabs
        };
        GameObject[] districtsToCreate = new GameObject[districtLocations.Length];
        for (int i = 0; i < districtLocations.Length; i++) {
            int districtPrefabType = Random.Range(0, allDistricTypePrefabArrays.Length);
            int specificDistricIdx = Random.Range(0, allDistricTypePrefabArrays[districtPrefabType].Length);
            districtsToCreate[i] = allDistricTypePrefabArrays[districtPrefabType][specificDistricIdx];
        }
        return districtsToCreate;
    }

    private void createTravelCost() {
        flammableTravelCost = Random.Range(minFlammableTravelCost, maxFlammableTravelCost + 1);
        foodWaterTravelCost = Random.Range(minFoodWaterTravelCost, maxFoodWaterTravelCost + 1);
        updateUITravelCosts();
    }

    private void updateUITravelCosts() {
        displayUIResources.DisplayTravelCosts(flammableTravelCost, foodWaterTravelCost);
    }

    private void calculateDistrictTotals() {
        initializeTotalsDict();
        for (int i = 0; i < createdDistricts.Count; i++) {
            District districtData = createdDistricts[i].GetComponent<District>();
            totalDistrictResources["metals"] += districtData.MetalAmount;
            totalDistrictResources["fabrics"] += districtData.FabricAmount;
            totalDistrictResources["flammables"] += districtData.FlammableAmount;
            totalDistrictResources["electronics"] += districtData.ElectronicsAmount;
            totalDistrictResources["foodWater"] += districtData.FoodWaterAmount;
        }
    }

    private void ensureResourcesForTravelExist() {
        while ((flammableTravelCost > totalDistrictResources["flammables"]) || (foodWaterTravelCost > totalDistrictResources["foodWater"])) {
            for (int i = 0; i < createdDistricts.Count; i++) {
                if (flammableTravelCost > totalDistrictResources["flammables"]) {
                    Debug.Log("Adding a flammable district");
                    District districtData = createdDistricts[i].GetComponent<District>();
                    if (districtData.FlammableAmount == 0) {
                        if (districtData.FoodWaterAmount == 0) {
                            int flammablePrefabIdx = Random.Range(0, flammableDistricPrefabs.Length);
                            GameObject newDistrict = Instantiate(flammableDistricPrefabs[flammablePrefabIdx], districtLocations[i].transform.position, cityPlane.transform.rotation) as GameObject;
                            GameObject oldDistrict = createdDistricts[i];
                            createdDistricts[i] = newDistrict;
                            Destroy(oldDistrict);
                            calculateDistrictTotals();
                            continue;
                        } else {
                            // Check to see that removing a foodWater district won't put foodWater
                            // below the traveling threshold
                            if ((totalDistrictResources["foodWater"] - districtData.FoodWaterAmount) >= foodWaterTravelCost) {
                                int flammablePrefabIdx = Random.Range(0, flammableDistricPrefabs.Length);
                                GameObject newDistrict = Instantiate(flammableDistricPrefabs[flammablePrefabIdx], districtLocations[i].transform.position, cityPlane.transform.rotation) as GameObject;
                                GameObject oldDistrict = createdDistricts[i];
                                createdDistricts[i] = newDistrict;
                                Destroy(oldDistrict);
                                calculateDistrictTotals();
                                continue;
                            }
                        }
                    } 
                }
                if (foodWaterTravelCost > totalDistrictResources["foodWater"]) {
                    Debug.Log("Adding a foodWater district");
                    District districtData = createdDistricts[i].GetComponent<District>();
                    if (districtData.FoodWaterAmount == 0) {
                        if (districtData.FlammableAmount == 0) {
                            int foodWaterPrefabIdx = Random.Range(0, foodWaterDistricPrefabs.Length);
                            GameObject newDistrict = Instantiate(foodWaterDistricPrefabs[foodWaterPrefabIdx], districtLocations[i].transform.position, cityPlane.transform.rotation) as GameObject;
                            GameObject oldDistrict = createdDistricts[i];
                            createdDistricts[i] = newDistrict;
                            Destroy(oldDistrict);
                            calculateDistrictTotals();
                            continue;
                        } else {
                            // Check to see that removing a foodWater district won't put foodWater
                            // below the traveling threshold
                            if ((totalDistrictResources["flammable"] - districtData.FlammableAmount) >= flammableTravelCost) {
                                int foodWaterPrefabIdx = Random.Range(0, foodWaterDistricPrefabs.Length);
                                GameObject newDistrict = Instantiate(foodWaterDistricPrefabs[foodWaterPrefabIdx], districtLocations[i].transform.position, cityPlane.transform.rotation) as GameObject;
                                GameObject oldDistrict = createdDistricts[i];
                                createdDistricts[i] = newDistrict;
                                Destroy(oldDistrict);
                                calculateDistrictTotals();
                                continue;
                            }
                        }
                    }
                }

            }
        }
        /*
        if (foodWaterTravelCost > totalDistrictResources["foodWater"]) {
            for (int i = 0; i < createdDistricts.Count; i++) {
                District districtData = createdDistricts[i].GetComponent<District>();
                if (districtData.FoodWaterAmount == 0) {
                    if (districtData.FlammableAmount == 0) {
                        GameObject oldDistrict = createdDistricts[i];
                        Destroy(oldDistrict);
                        int foodWaterPrefabIdx = Random.Range(0, foodWaterDistricPrefabs.Length);
                        GameObject newDistrict = Instantiate(flammableDistricPrefabs[foodWaterPrefabIdx], districtLocations[i].transform.position, cityPlane.transform.rotation) as GameObject;
                        createdDistricts[i] = newDistrict;
                        calculateDistrictTotals();
                        ensureResourcesForTravelExist();
                    } else {
                        if ((totalDistrictResources["flammables"] - districtData.FlammableAmount) < flammableTravelCost) {
                            continue;
                        } else {
                            GameObject oldDistrict = createdDistricts[i];
                            Destroy(oldDistrict);
                            int foodWaterPrefabIdx = Random.Range(0, foodWaterDistricPrefabs.Length);
                            GameObject newDistrict = Instantiate(flammableDistricPrefabs[foodWaterPrefabIdx], districtLocations[i].transform.position, cityPlane.transform.rotation) as GameObject;
                            createdDistricts[i] = newDistrict;
                            calculateDistrictTotals();
                            ensureResourcesForTravelExist();
                        }
                    }
                }
            }
        } */
    }


    private void initializeTotalsDict() {
        totalDistrictResources["metals"] = 0;
        totalDistrictResources["fabrics"] = 0;
        totalDistrictResources["flammables"] = 0;
        totalDistrictResources["electronics"] = 0;
        totalDistrictResources["foodWater"] = 0;
    }

    private void debugShowDistictTotals() {
        displayUIResources.DebugSetDistrictTotals(
            totalDistrictResources["metals"], totalDistrictResources["fabrics"], totalDistrictResources["flammables"],
            totalDistrictResources["electronics"], totalDistrictResources["foodWater"]
        );
    }

    
}
