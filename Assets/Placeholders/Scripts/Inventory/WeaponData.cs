using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : MonoBehaviour {

    [SerializeField]
    private float damage;
    public float Damage {
        get {
            return damage;
        }
    }

    [SerializeField]
    private int projectilesPerShot;
    public int ProjectilesPerShot {
        get {
            return projectilesPerShot;
        }
    }

    [SerializeField]
    private float shotsPerSecond;
    public float ShotsPerSecond {
        get {
            return shotsPerSecond;
        }
    }

    [SerializeField]
    private int coneOfFireDegrees;
    public int ConeOfFireDegrees {
        get {
            return coneOfFireDegrees;
        }
    }

}
