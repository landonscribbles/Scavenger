using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootableContainer : MonoBehaviour {

    private int metals = 0;
    public int Metals {
        get {
            return metals;
        }
        set {
            metals = value;
        }
    }

    private int fabrics = 0;
    public int Fabrics {
        get {
            return fabrics;
        }
        set {
            fabrics = value;
        }
    }

    private int fuel = 0;
    public int Fuel {
        get {
            return fuel;
        }
        set {
            fuel = value;
        }
    }

    private int electronics = 0;
    public int Electronics {
        get {
            return electronics;
        }
        set {
            electronics = value;
        }
    }

    private int provisions = 0;
    public int Provisions {
        get {
            return provisions;
        }
        set {
            provisions = value;
        }
    }

    private List<InventoryItemInterface> displayableItems;

    public void AddDisplayItem(InventoryItemInterface newItem) {
        displayableItems.Add(newItem);
    }

    public void RemoveDisplayItem(InventoryItemInterface itemToRemove) {
        displayableItems.Remove(itemToRemove);
    }

}
