using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    [Header("Dependencies")]
    public Button shopFirstWeaponButton;
    public Image shopFirstWeaponImage;
    public TextMeshProUGUI shopFirstWeaponName;
    public TextMeshProUGUI shopFirstWeaponCost;

    public Button shopSecondWeaponButton;
    public Image shopSecondWeaponImage;
    public TextMeshProUGUI shopSecondWeaponName;
    public TextMeshProUGUI shopSecondWeaponCost;

    public Button shopFirstConsumableButton;
    public Image shopFirstConsumableImage;
    public TextMeshProUGUI shopFirstConsumableName;
    public TextMeshProUGUI shopFirstConsumableCost;
    public TextMeshProUGUI shopFirstConsumableAmount;

    public Button shopSecondConsumableButton;
    public Image shopSecondConsumableImage;
    public TextMeshProUGUI shopSecondConsumableName;
    public TextMeshProUGUI shopSecondConsumableCost;
    public TextMeshProUGUI shopSecondConsumableAmount;

    //Player Inventory
    public Image playerFirstWeaponImage;
    public Image playerSecondWeaponImage;
    public Image playerFirstConsumableImage;
    public TextMeshProUGUI playerFirstConsumableAmount;
    public Image playerSecondConsumableImage;
    public TextMeshProUGUI playerSecondConsumableAmount;
    public TextMeshProUGUI playerGoldText;

    private InventorySO _shopInventory;
    private List<int> _weaponPrices;
    private List<int> _consumablePrices;
    private InventorySO _playerInventory;

    public void SetupHUD(InventorySO shopInventory, List<int> weaponPrices, List<int> consumablePrices, InventorySO playerInventory)
    {
        this.ResetShopHUD();
        this.ResetPlayerHUD();

        this._shopInventory = shopInventory;
        this._playerInventory = playerInventory;
        this._weaponPrices = weaponPrices;
        this._consumablePrices = consumablePrices;

        this.shopFirstWeaponButton.interactable = false;
        this.shopSecondWeaponButton.interactable = false;
        this.shopFirstConsumableButton.interactable = false;
        this.shopSecondConsumableButton.interactable = false;

        this.ConfigureShopWeapons();
        this.ConfigureShopConsumables();

        this.ConfigurePlayerWeapons();
        this.ConfigurePlayerConsumables();
        this.ConfigurePlayerGold();
    }

    private void Update()
    {
        if (this._shopInventory == null || this._playerInventory == null)
            return;

        this.ConfigurePlayerWeapons();
        this.ConfigurePlayerConsumables();
        this.ConfigurePlayerGold();
    }

    private void ConfigureShopWeapons()
    {
        for(int weaponIndex = 0; weaponIndex < this._shopInventory.weapons.Count; weaponIndex++)
        {
            var weaponItem = this._shopInventory.weapons[weaponIndex];
            var weaponPrice = this._weaponPrices[weaponIndex];

            if( weaponIndex == 0 )
            {
                this.shopFirstWeaponImage.sprite = weaponItem.icon;
                this.shopFirstWeaponImage.color = Color.white;
                this.shopFirstWeaponName.text = weaponItem.itemName;
                this.shopFirstWeaponCost.text = weaponPrice.ToString();
                this.shopFirstWeaponButton.interactable = true;
            }
            else if( weaponIndex == 1 )
            {
                this.shopSecondWeaponImage.sprite = weaponItem.icon;
                this.shopSecondWeaponImage.color = Color.white;
                this.shopSecondWeaponName.text = weaponItem.itemName;
                this.shopSecondWeaponCost.text = weaponPrice.ToString();
                this.shopSecondWeaponButton.interactable = true;
            }
        }
    }

    private void ConfigureShopConsumables()
    {
        for (int consumableIndex = 0; consumableIndex < this._shopInventory.consumables.Count; consumableIndex++)
        {
            var consumableItem = this._shopInventory.consumables[consumableIndex];
            var consumablePrice = this._consumablePrices[consumableIndex];

            if ( consumableIndex == 0 )
            {
                this.shopFirstConsumableImage.sprite = consumableItem.item.icon;
                this.shopFirstConsumableImage.color = Color.white;
                this.shopFirstConsumableName.text = consumableItem.item.itemName;
                this.shopFirstConsumableCost.text = consumablePrice.ToString();
                this.shopFirstConsumableAmount.text = consumableItem.amount.ToString();
                this.shopFirstConsumableButton.interactable = true;
            }
            else if ( consumableIndex == 1 )
            {
                this.shopSecondConsumableImage.sprite = consumableItem.item.icon;
                this.shopSecondConsumableImage.color = Color.white;
                this.shopSecondConsumableName.text = consumableItem.item.itemName;
                this.shopSecondConsumableCost.text = consumablePrice.ToString();
                this.shopSecondConsumableAmount.text = consumableItem.amount.ToString();
                this.shopSecondConsumableButton.interactable = true;
            }
        }
    }

    private void ConfigurePlayerWeapons()
    {
        if(this._playerInventory.weapons.Count > 0)
        {
            var weaponItem = this._playerInventory.weapons[0];
            this.playerFirstWeaponImage.sprite = weaponItem.icon;
            this.playerFirstWeaponImage.color = Color.white;
        }
        else
        {
            this.playerFirstWeaponImage.sprite = null;
            this.playerFirstWeaponImage.color = Color.clear;
        }

        if (this._playerInventory.weapons.Count > 1)
        {
            var weaponItem = this._playerInventory.weapons[1];
            this.playerSecondWeaponImage.sprite = weaponItem.icon;
            this.playerSecondWeaponImage.color = Color.white;
        }
        else
        {
            this.playerSecondWeaponImage.sprite = null;
            this.playerSecondWeaponImage.color = Color.clear;
        }
    }

    private void ConfigurePlayerConsumables()
    {
        if(this._playerInventory.consumables.Count > 0)
        {
            var consumableItem = this._playerInventory.consumables[0];
            this.playerFirstConsumableImage.sprite = consumableItem.item.icon;
            this.playerFirstConsumableImage.color = Color.white;
            this.playerFirstConsumableAmount.text = consumableItem.amount.ToString();
        }
        else
        {
            this.playerFirstConsumableImage.sprite = null;
            this.playerFirstConsumableImage.color = Color.clear;
            this.playerFirstConsumableAmount.text = null;

        }

        if (this._playerInventory.consumables.Count > 1)
        {
            var consumableItem = this._playerInventory.consumables[1];
            this.playerSecondConsumableImage.sprite = consumableItem.item.icon;
            this.playerSecondConsumableImage.color = Color.white;
            this.playerSecondConsumableAmount.text = consumableItem.amount.ToString();
        }
        else
        {
            this.playerSecondConsumableImage.sprite = null;
            this.playerSecondConsumableImage.color = Color.clear;
            this.playerSecondConsumableAmount.text = null;

        }
    }

    private void ConfigurePlayerGold()
    {
        this.playerGoldText.text = this._playerInventory.gold.ToString();
    }

    private void ResetShopHUD()
    {
        this.shopFirstWeaponImage.sprite = null;
        this.shopFirstWeaponImage.color = Color.clear;
        this.shopFirstWeaponName.text = "";
        this.shopFirstWeaponCost.text = "";
        this.shopFirstWeaponButton.interactable = false;


        this.shopSecondWeaponImage.sprite = null;
        this.shopSecondWeaponImage.color = Color.clear;
        this.shopSecondWeaponName.text = "";
        this.shopSecondWeaponCost.text = "";
        this.shopSecondWeaponButton.interactable = false;

        this.shopFirstConsumableImage.sprite = null;
        this.shopFirstConsumableImage.color = Color.clear;
        this.shopFirstConsumableName.text = "";
        this.shopFirstConsumableCost.text = "";
        this.shopFirstConsumableButton.interactable = false;


        this.shopSecondConsumableImage.sprite = null;
        this.shopSecondConsumableImage.color = Color.clear;
        this.shopSecondConsumableName.text = "";
        this.shopSecondConsumableCost.text = "";
        this.shopSecondConsumableButton.interactable = false;
    }

    private void ResetPlayerHUD()
    {
        this.shopFirstWeaponImage.sprite = null;
        this.shopFirstWeaponImage.color = Color.clear;

        this.shopSecondWeaponImage.sprite = null;
        this.shopSecondWeaponImage.color = Color.clear;
        
        this.playerFirstConsumableImage.sprite = null;
        this.playerFirstConsumableImage.color = Color.clear;
        this.playerFirstConsumableAmount.text = "";

        this.playerSecondConsumableImage.sprite = null;
        this.playerSecondConsumableImage.color = Color.clear;
        this.playerSecondConsumableAmount.text = "";
    }
}
