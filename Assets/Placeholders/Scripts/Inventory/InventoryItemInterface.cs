using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface InventoryItemInterface {

    string GetFullItemDescription();

    string GetDisplayItemDescription();

    Image GetDisplayImage();
}
