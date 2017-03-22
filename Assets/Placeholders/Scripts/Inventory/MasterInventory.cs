using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterInventory : MonoBehaviour {

    // Basic resource materials

    private int metalsOwned = 0;
    public int MetalsOwned {
        get {
            return metalsOwned;
        }

        set {
            metalsOwned = value;
        }
    }

    private int fabricOwned = 0;
    public int FabricOwned {
        get {
            return fabricOwned;
        }

        set {
            fabricOwned = value;
        }
    }

    private int fuelOwned = 0;
    public int FuelOwned {
        get {
            return fuelOwned;
        }

        set {
            fuelOwned = value;
        }
    }

    private int electronicsOwned = 0;
    public int ElectronicsOwned {
        get {
            return electronicsOwned;
        }

        set {
            electronicsOwned = value;
        }
    }

    private int provisionsOwned = 0;
    public int ProvisionsOwned {
        get {
            return provisionsOwned;
        }

        set {
            provisionsOwned = value;
        }
    }

    // Equipment

    private int ammo = 0;
    public int Ammo {
        get {
            return ammo;
        }

        set {
            ammo = value;
        }
    }

    private List<WeaponData> ownedWeapons;

    void Awake() {
        ownedWeapons = new List<WeaponData>();
    }

    public void AddWeaponToOwned(WeaponData newWeapon) {
        ownedWeapons.Add(newWeapon);
    }
}
