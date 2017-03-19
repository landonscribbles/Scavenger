using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class District : MonoBehaviour {

    [SerializeField]
    private string districtName;
    public string DistrictName {
        get {
            return districtName;
        }
    }

    [SerializeField]
    private int[] metalAmountRange;
    private int metalAmount;
    public int MetalAmount {
        get {
            return metalAmount;
        }

        set {
            metalAmount = value;
        }
    }

    [SerializeField]
    private int[] fabricAmountRange;
    private int fabricAmount;
    public int FabricAmount {
        get {
            return fabricAmount;
        }

        set {
            fabricAmount = value;
        }
    }

    [SerializeField]
    private int[] flammableAmountRange;
    private int flammableAmount;
    public int FlammableAmount {
        get {
            return flammableAmount;
        }

        set {
            flammableAmount = value;
        }
    }

    [SerializeField]
    private int[] electronicsAmountRange;
    private int electronicsAmount;
    public int ElectronicsAmount {
        get {
            return electronicsAmount;
        }

        set {
            electronicsAmount = value;
        }
    }

    [SerializeField]
    private int[] foodWaterAmountRange;
    private int foodWaterAmount;
    public int FoodWaterAmount {
        get {
            return foodWaterAmount;
        }

        set {
            foodWaterAmount = value;
        }
    }

    void Awake() {
        generateResources();
    }

    private void generateResources() {
        metalAmount = generateResourceValue(metalAmountRange);
        fabricAmount = generateResourceValue(fabricAmountRange);
        flammableAmount = generateResourceValue(flammableAmountRange);
        electronicsAmount = generateResourceValue(electronicsAmountRange);
        foodWaterAmount = generateResourceValue(foodWaterAmountRange);
    }

    private int generateResourceValue(int[] resourceRange) {
        int resourceValue = 0;
        switch (resourceRange.Length) {
            case 0:
                Debug.LogError("Missing resource values for: " + gameObject.name);
                break;
            case 1:
                Debug.LogError("Missing max resource values for: " + gameObject.name);
                resourceValue = resourceRange[1];
                break;
            case 2:
                resourceValue = Random.Range(resourceRange[0], resourceRange[1] + 1);
                break;
            default:
                Debug.LogError("More than two resource values were defined for: " + gameObject.name);
                resourceValue = Random.Range(resourceRange[0], resourceRange[1] + 1);
                break;
        }
        return resourceValue;
    }

}
