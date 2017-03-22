using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistrictManager : MonoBehaviour {

    private bool initialized = false;

    private int metals;
    private int fabrics;
    private int fuel;
    private int electronics;
    private int provisions;

    List<GameObject> districtRooms;

    public void Initialize(int _metals, int _fabrics, int _fuel, int _electronics, int _provisions, List<GameObject> _districtRooms) {
        initialized = true;

        metals = _metals;
        fabrics = _fabrics;
        fuel = _fuel;
        electronics = _electronics;
        provisions = _provisions;

        districtRooms = _districtRooms;
    }

}
