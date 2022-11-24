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

    [SerializeField] private TextMeshProUGUI _playerName;
    [SerializeField] private Slider _playerHP;
    [SerializeField] private TextMeshProUGUI _enemyName;
    [SerializeField] private Slider _enemyHP;

    private CombatUnitSO _player;
    private CombatUnitSO _enemy;

    public void SetupHUD(CombatUnitSO player, CombatUnitSO enemy, int level, int gold)
    {
        this._player = player;
        this._enemy = enemy;

        this._levelText.text = "Level: " + level.ToString();
        this._goldText.text = "Gold: " + gold.ToString();

        this._playerName.text = _player.unitName;
        this._playerHP.minValue = 0;
        this._playerHP.maxValue = _player.maxHP;

        this._enemyName.text = _enemy.unitName;
        this._enemyHP.minValue = 0;
        this._enemyHP.maxValue = _enemy.maxHP;

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
    }
}
