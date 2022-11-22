using UnityEngine;

public class CombatTrigger : MonoBehaviour
{
    [Header("Configurations")]
    
    [SerializeField] private CombatUnitSO player;
    [SerializeField] private Transform playerPosition;
    [SerializeField] private CombatUnitSO[] enemies;
    [SerializeField] private Transform enemyPosition;

    //Emitir evento do combat request
    //[Header("Broadcasting Events")]
    //public CombatRequestGameEvent combatRequestEvent;

    public void TriggerCombat()
    {
        var combatRequest = new CombatRequest(player, playerPosition, enemies, enemyPosition);

        //this.combatRequestEvent.Raise(combatRequest);
    }
}
