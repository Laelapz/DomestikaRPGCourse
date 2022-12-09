using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private GameObject _combatMenu;
    [SerializeField] private GameObject _wonMenu;
    [SerializeField] private GameObject _lostMenu;

    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _goldText;
    [SerializeField] private TextMeshProUGUI _earnedGoldText;

    [SerializeField] private Image firstWeaponImage;
    [SerializeField] private Image secondWeaponImage;

    [SerializeField] private Image firstConsumableImage;
    [SerializeField] private TextMeshProUGUI firstConsumableAmountText;
    [SerializeField] private Image secondConsumableImage;
    [SerializeField] private TextMeshProUGUI secondConsumableAmountText;

    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private Slider _playerHP;
    [SerializeField] private TextMeshProUGUI _enemyName;
    [SerializeField] private Slider _enemyHP;

    private CombatUnitSO _player;
    private CombatUnitSO _enemy;
    private InventorySO _playerInventory;

    private bool _attacking;
    private bool _returning;
    private Vector3 _initialPos;
    private Transform _attacker;
    private Transform _target;

    public void SetupHUD(CombatUnitSO player, CombatUnitSO enemy, InventorySO playerInventory, int level, int gold)
    {
        this._player = player;
        this._enemy = enemy;

        this._levelText.text = "Level: " + level.ToString();
        this._goldText.text = "Gold: " + gold.ToString();
        this._playerInventory = playerInventory;

        this._playerName.text = _player.unitName;
        this._playerHP.minValue = 0;
        this._playerHP.maxValue = _player.maxHP;

        this._enemyName.text = _enemy.unitName;
        this._enemyHP.minValue = 0;
        this._enemyHP.maxValue = _enemy.maxHP;

        if(this._playerInventory.weapons.Count > 0)
        {
            var firstWeapon = playerInventory.weapons[0];
            this.firstWeaponImage.sprite = firstWeapon.icon;
            this.firstWeaponImage.color = Color.white;
        }
        else
        {
            this.firstWeaponImage.sprite = null;
            this.firstWeaponImage.color = Color.clear;
        }

        if (this._playerInventory.weapons.Count > 1)
        {
            var secondWeapon = playerInventory.weapons[1];
            this.secondWeaponImage.sprite = secondWeapon.icon;
            this.secondWeaponImage.color = Color.white;
        }
        else
        {
            this.secondWeaponImage.sprite = null;
            this.secondWeaponImage.color = Color.clear;
        }

    }

    public void SetInfoText(string infoText)
    {
        this._infoText.text = infoText;
    }

    public void ShowCombatMenu()
    {
        this._wonMenu.SetActive(false);
        this._lostMenu.SetActive(false);
        this._combatMenu.SetActive(true);
    }

    public void ShowWonMenu(int earnedGold)
    {
        this._earnedGoldText.text = earnedGold.ToString();
        this._wonMenu.SetActive(true);
        this._lostMenu.SetActive(false);
        this._combatMenu.SetActive(false);
    }

    public void ShowLostMenu()
    {
        this._wonMenu.SetActive(false);
        this._lostMenu.SetActive(true);
        this._combatMenu.SetActive(false);
    }

    public void ResetHUD()
    {
        this._player = null;
        this._enemy = null;

        this._levelText.text = "";
        this._goldText.text = "";

        this._playerName.text = "";
        this._playerHP.minValue = 0;
        this._playerHP.maxValue = 0;
        this._playerHP.value = 0;

        this._enemyName.text = "";
        this._enemyHP.minValue = 0;
        this._enemyHP.maxValue = 0;
        this._enemyHP.value = 0;
    }

    void Update()
    {
        if(this._player != null)
        {
            this._playerHP.value = this._player.currentHP;
        }

        if (this._enemy != null)
        {
            this._enemyHP.value = this._enemy.currentHP;
        }

        if (this._playerInventory != null)
        {
            if (this._playerInventory.consumables.Count > 0)
            {
                var firstConsumable = this._playerInventory.consumables[0];
                this.firstConsumableImage.sprite = firstConsumable.item.icon;
                this.firstConsumableImage.color = Color.white;
                this.firstConsumableAmountText.text = firstConsumable.amount.ToString();
            }
            else
            {
                this.firstConsumableImage.sprite = null;
                this.firstConsumableImage.color = Color.clear;
                this.firstConsumableAmountText.text = "";
            }

            if (this._playerInventory.consumables.Count > 1)
            {
                var secondConsumable = this._playerInventory.consumables[1];
                this.secondConsumableImage.sprite = secondConsumable.item.icon;
                this.secondConsumableImage.color = Color.white;
                this.secondConsumableAmountText.text = secondConsumable.amount.ToString();
            }
            else
            {
                this.secondConsumableImage.sprite = null;
                this.secondConsumableImage.color = Color.clear;
                this.secondConsumableAmountText.text = "";
            }
        }
    }

    public IEnumerator SpawnPlayerAnimation(Transform player)
    {
        player.localScale = Vector3.zero;

        while (player.localScale.x < 1)
        {
            player.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            yield return new WaitForSeconds(0.05f);
        }

    }

    public IEnumerator SpawnEnemyAnimation(Transform enemy)
    {
        enemy.localScale = Vector3.zero;

        while (enemy.localScale.x < 1)
        {
            enemy.localScale += new Vector3(0.1f, 0.1f, 0.1f);
            yield return new WaitForSeconds(0.05f);
        }

    }

    public IEnumerator AttackAnimation(Transform attacker, Transform target)
    {
        var initialPos = attacker.position;

        while (Vector3.Distance(attacker.position, target.position) > 0.5f)
        {
            attacker.position = Vector3.MoveTowards(attacker.position, target.position, 20 * Time.deltaTime);
            yield return null;
        }

        while (Vector3.Distance(attacker.position, initialPos) > 0.5f)
        {
            attacker.position = Vector3.MoveTowards(attacker.position, initialPos, 20 * Time.deltaTime);
            yield return null;
        }

        yield return true;
    }
}
