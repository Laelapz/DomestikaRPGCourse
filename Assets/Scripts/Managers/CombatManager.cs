using System.Collections;
using UnityEngine;

public enum CombatStates
{
    NONE,
    START,
    PLAYERTURN,
    ENEMYTURN,
    WON,
    LOST
}

public class CombatManager : MonoBehaviour
{
    [Header("Combat Configurations")]
    public int baseGoldPrize = 10;
    public float prizeIncrementPerLevel = 0.2f;
    public float difficultyIncrementPerLevel = 0.2f;
    public float timeBetweenActions = 1.5f;

    [Header("Combat Messages Configuration")]
    public string combatStartedInfoText = "An enemy appeared...";
    public string playerTurnInfoText = "Player's turn...";
    public string wonInfoText = "Enemy defeated!";
    public string enemyTurnInfoText = "Enemy's turn...";
    public string enemyAttackedInfoText = "Enemy attacked";

    [Header("Dependencies")]
    public CombatUI combatUI;


    private CombatStates _currentState = CombatStates.NONE;
    private int _currentLevel = 1;
    private int _gold = 0;

    private CombatRequest _request;
    private CombatUnitSO _currentEnemy;
    private GameObject _currentEnemyGO;

    public void SetupCombat(CombatRequest request)
    {
        this._request = request;

        this._request.player.ResetHP();


        GameObject playerGO = Instantiate(this._request.player.unitPrefab, this._request.playerPosition.position, Quaternion.identity);
        playerGO.transform.parent = request.playerPosition;

        StartCoroutine(this.combatUI.SpawnPlayerAnimation(this._request.playerPosition));
        StartCoroutine(StartCombat());
    }

    public void NextCombat()
    {
        this._currentLevel += 1;
        StartCoroutine(StartCombat());
    }

    public void ResetCombat()
    {
        this._currentLevel = 1;
        this._gold = 0;
        StartCoroutine(StartCombat());
    }

    public void OnPlayerAttack(int inventoryItemId)
    {
        if (_currentState != CombatStates.PLAYERTURN) return;

        if(inventoryItemId == 0 || inventoryItemId == 1)
        {
            if (this._request.player.inventory.weapons.Count > inventoryItemId)
            {
                var usedWeapon = this._request.player.inventory.weapons[inventoryItemId];
                StartCoroutine(this.combatUI.AttackAnimation(this._request.playerPosition, this._request.enemyPosition));

                this._request.player.AttackUnit(_currentEnemy, usedWeapon);
                StartCoroutine(this._currentEnemy.FlashHit());

                if (this._currentEnemy.currentHP <= 0)
                {
                    StartCoroutine(CombatWon());
                }
                else
                {
                    StartCoroutine(EnemyTurn());
                }
            }
        }
        else if (inventoryItemId == 2 || inventoryItemId == 3)
        {
            inventoryItemId = inventoryItemId % 2;
            
            if(this._request.player.inventory.consumables.Count > inventoryItemId)
            {
                var usedConsumable = this._request.player.inventory.consumables[inventoryItemId];
                this._request.player.Heal(usedConsumable.item.healPower);
                this._request.player.inventory.RemoveConsumable(usedConsumable.item);
            }

            StartCoroutine(EnemyTurn());
        }

    }

    private IEnumerator StartCombat()
    {
        this._currentState = CombatStates.START;

        int randomNumber = Random.Range(0, this._request.enemies.Length);
        this._currentEnemy = this._request.enemies[randomNumber];

        if(this._currentLevel > 1)
        {
            this._currentEnemy.maxHP *= (1 + difficultyIncrementPerLevel);
        }

        this._currentEnemy.currentHP = this._currentEnemy.maxHP;

        this._currentEnemyGO = Instantiate(this._currentEnemy.unitPrefab, this._request.enemyPosition.position, Quaternion.identity);
        this._currentEnemyGO.transform.parent = this._request.enemyPosition;

        StartCoroutine(this.combatUI.SpawnEnemyAnimation(this._request.enemyPosition));
        this.combatUI.ResetHUD();
        this.combatUI.ShowCombatMenu();
        this.combatUI.SetupHUD(this._request.player, this._currentEnemy, this._request.player.inventory, this._currentLevel, this._gold);
        this.combatUI.SetInfoText(combatStartedInfoText);

        yield return new WaitForSeconds(timeBetweenActions);

        PlayerTurn();
    }

    private void PlayerTurn()
    {
        this._currentState = CombatStates.PLAYERTURN;
        this.combatUI.SetInfoText(playerTurnInfoText);
    }

    private IEnumerator EnemyTurn()
    {
        this._currentState = CombatStates.ENEMYTURN;
        this.combatUI.SetInfoText(enemyTurnInfoText);

        yield return new WaitForSeconds(timeBetweenActions + 0.5f);

        this.combatUI.SetInfoText(enemyAttackedInfoText);

        var usedWeapon = _currentEnemy.inventory.weapons[0];
        StartCoroutine(this.combatUI.AttackAnimation(this._request.enemyPosition, this._request.playerPosition));

        this._currentEnemy.AttackUnit(this._request.player, usedWeapon);
        StartCoroutine(this._request.player.FlashHit());

        yield return new WaitForSeconds(timeBetweenActions);

        if (this._request.player.currentHP <= 0)
        {
            CombatLost();
        }
        else
        {
            PlayerTurn();
        }
    }

    private IEnumerator CombatWon()
    {
        this._currentState = CombatStates.WON;
        this.combatUI.SetInfoText(wonInfoText);

        var earnedGold = (int)(this.baseGoldPrize * (1 + this.prizeIncrementPerLevel * this._currentLevel));
        this._gold += earnedGold;

        yield return new WaitForSeconds(timeBetweenActions);

        this.combatUI.ShowWonMenu(this._gold);
        this.ResetEnemyHPToBase();
        Destroy(this._currentEnemyGO);
        this._currentEnemy = null;
    }
    
    private void CombatLost()
    {
        this._currentState = CombatStates.LOST;
        this.combatUI.ShowLostMenu();

        this.ResetEnemyHPToBase();
    }

    public void FinishCombat()
    {
        this._request.player.inventory.AddGold(this._gold);
    }

    private void ResetEnemyHPToBase()
    {
        this._currentEnemy.maxHP = this._currentEnemy.baseHP;
    }
}
