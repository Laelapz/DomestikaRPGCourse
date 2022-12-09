using ScriptableObjectArchitecture;
using UnityEngine;

public class ShopTrigger : MonoBehaviour
{
    [Header("Configuration")]
    public InventorySO shopInventory;

    [Header("Broadcasting Events")]
    public InventorySOGameEvent shopRequestEvent;


    public void TriggerShop()
    {
        print("Trigger");
        this.shopRequestEvent.Raise(this.shopInventory);
    }
}
