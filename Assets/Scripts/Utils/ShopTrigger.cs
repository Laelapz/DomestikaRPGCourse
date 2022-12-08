using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [Header("Configuration")]
    public InventorySO shopInventory;

    //[Header("Broadcasting Events")]
    //public InventorySOGameEvent shopRequestEvent;


    public void TriggerShop()
    {
        //this.shopRequestEvent.Raise(this.shopInventory);
    }
}
