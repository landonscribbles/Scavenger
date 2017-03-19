using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlePlayerInput : MonoBehaviour {

    private DisplayUIResources displayDistrictResources;

    void Start() {
        displayDistrictResources = GameObject.Find("UI").GetComponent<DisplayUIResources>();
    }
	
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                if (hit.transform.gameObject.tag == "District") {
                    displayDistrictResources.SetDisplayedDistrict(hit.transform.gameObject);
                }
            }
        }
	}
}
