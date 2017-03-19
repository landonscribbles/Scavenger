using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrictLevelLoadHelper : MonoBehaviour {

    // the SerializeFields below are only in place for debugging and can be remove later if needed
    [SerializeField]
    private string districtName = "";
    public string DistrictName {
        get {
            return districtName;
        }

        set {
            districtName = value;
        }
    }

    [SerializeField]
    private int MetalAmount = 0;
    public int metalAmount {
        get {
            return metalAmount;
        }

        set {
            metalAmount = value;
        }
    }

    [SerializeField]
    private int fabricAmount = 0;
    public int FabricAmount {
        get {
            return fabricAmount;
        }

        set {
            fabricAmount = value;
        }
    }

    [SerializeField]
    private int flammableAmount = 0;
    public int FlammableAmount {
        get {
            return flammableAmount;
        }

        set {
            flammableAmount = value;
        }
    }

    [SerializeField]
    private int electronicsAmount = 0;
    public int ElectronicsAmount {
        get {
            return electronicsAmount;
        }

        set {
            electronicsAmount = value;
        }
    }

    [SerializeField]
    private int foodWaterAmount = 0;
    public int FoodWaterAmount {
        get {
            return foodWaterAmount;
        }

        set {
            foodWaterAmount = value;
        }
    }

    [SerializeField]

    void Awake() {
        DontDestroyOnLoad(gameObject);
    }

}
