using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayUIResources : MonoBehaviour {

    [SerializeField]
    private Text districtName;
    [SerializeField]
    private Text metalText;
    [SerializeField]
    private Text fabricText;
    [SerializeField]
    private Text flammableText;
    [SerializeField]
    private Text electronicsText;
    [SerializeField]
    private Text foodWaterText;

    private string districtNameDefaultText;
    private string metalDefaultText;
    private string fabricDefaultText;
    private string flammableDefaultText;
    private string electronicsDefaultText;
    private string foodWaterDefaultText;

    private GameObject displayedDistrict;

    [SerializeField]
    private Text debugMetalText;
    [SerializeField]
    private Text debugFabricText;
    [SerializeField]
    private Text debugFlammableText;
    [SerializeField]
    private Text debugElectronicsText;
    [SerializeField]
    private Text debugFoodWaterText;

    [SerializeField]
    private Text flammableTravelCost;
    [SerializeField]
    private Text foodWaterTravelCost;

    [SerializeField]
    private string flammableTravelCostDefaultString;
    [SerializeField]
    private string foodWaterTravelCostDefaultString;

    void Start() {
        districtNameDefaultText = "-";
        metalDefaultText = metalText.text;
        fabricDefaultText = fabricText.text;
        flammableDefaultText = flammableText.text;
        electronicsDefaultText = electronicsText.text;
        foodWaterDefaultText = foodWaterText.text;
    }

    void Update() {
        if (displayedDistrict == null) {
            districtName.text = districtNameDefaultText;
            metalText.text = metalDefaultText;
            fabricText.text = fabricDefaultText;
            flammableText.text = flammableDefaultText;
            electronicsText.text = electronicsDefaultText;
            foodWaterText.text = foodWaterDefaultText;
        }
    }

    public void SetDisplayedDistrict(GameObject district) {
        displayedDistrict = district;
        District districtInfo = displayedDistrict.GetComponent<District>();
        districtName.text = districtInfo.DistrictName;
        metalText.text = metalDefaultText + districtInfo.MetalAmount;
        fabricText.text = fabricDefaultText + districtInfo.FabricAmount;
        flammableText.text = flammableDefaultText + districtInfo.FlammableAmount;
        electronicsText.text = electronicsDefaultText + districtInfo.ElectronicsAmount;
        foodWaterText.text = foodWaterDefaultText + districtInfo.FoodWaterAmount;
    }

    public void DisplayTravelCosts(int flammableCost, int foodWaterCost) {
        flammableTravelCost.text = flammableTravelCostDefaultString + flammableCost;
        foodWaterTravelCost.text = foodWaterTravelCostDefaultString + foodWaterCost;
    }

    public void DebugSetDistrictTotals(int metals, int fabrics, int flammables, int electronics, int foodWater) {
        debugMetalText.text = "Total metals: " + metals;
        debugFabricText.text = "Total fabrics: " + fabrics;
        debugFlammableText.text = "Total flammables: " + flammables;
        debugElectronicsText.text = "Total electronics: " + electronics;
        debugFoodWaterText.text = "Total food/water: " + foodWater;
    }

}
