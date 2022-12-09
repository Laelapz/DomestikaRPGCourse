using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class ShopManager : MonoBehaviour
{
    [Header("Dependencies")]
    public ShopUI shopUI;
    public InventorySO playerInventory;

    [Header("Shop Configuration")]
    public List<int> weaponPrices = new List<int>(2);
    public List<int> consumablePrices = new List<int>(2);

    [Header("Action Events")]
    public UnityEvent onShopOpened;
    public UnityEvent onShopClosed;

    private InventorySO _shopInventory;

    public void OpenShop(InventorySO shopInventory)
    {

        if (this._shopInventory != null)
            return;

        this._shopInventory = shopInventory;

        this.shopUI.SetupHUD(this._shopInventory, this.weaponPrices, this.consumablePrices, this.playerInventory);

        if (this.onShopOpened != null)
            onShopOpened.Invoke();
    }


    public void CloseShop()
    {
        this._shopInventory = null;

        if (this.onShopClosed != null)
            this.onShopClosed.Invoke();
    }

    public void BuyItem(int itemId)
    {
        if(itemId == 0 || itemId == 1)
        {
            var itemPrice = this.weaponPrices[itemId];
            var item = this._shopInventory.weapons[itemId];

            if (this.playerInventory.gold < itemPrice || this.playerInventory.weapons.Contains(item))
                return;

            this.playerInventory.AddWeapon(item);
            this.playerInventory.RemoveGold(itemPrice);
        }
        else if(itemId == 2 || itemId == 3)
        {
            var consumableIndex = itemId % 2;
            var itemPrice = this.consumablePrices[consumableIndex];
            var shopItem = this._shopInventory.consumables[consumableIndex];

            if (this.playerInventory.gold < itemPrice || this.playerInventory.consumables.Contains(shopItem))
                return;

            this.playerInventory.AddConsumable(shopItem.item);
            this.playerInventory.RemoveGold(itemPrice);
            this._shopInventory.RemoveConsumable(shopItem.item);
        }
    }
}
