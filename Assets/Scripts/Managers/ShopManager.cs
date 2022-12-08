using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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
        this._shopInventory = shopInventory;

        if (this._shopInventory == null)
            return;

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

            if (this.playerInventory.gold < itemPrice)
                return;

            var item = this._shopInventory.weapons[itemId];
            this.playerInventory.AddWeapon(item);
            this.playerInventory.RemoveGold(itemPrice);
        }
        else if(itemId == 2 || itemId == 3)
        {
            var consumableIndex = itemId % 2;
            var itemPrice = this.consumablePrices[consumableIndex];

            if (this.playerInventory.gold < itemPrice)
                return;

            var shopItem = this._shopInventory.consumables[consumableIndex];
            this.playerInventory.AddConsumable(shopItem.item);
            this.playerInventory.RemoveGold(itemPrice);
            this._shopInventory.RemoveConsumable(shopItem.item);
        }
    }
}
