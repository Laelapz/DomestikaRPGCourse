using UnityEngine;

[System.Serializable]
public class CombatRequest
{
    [SerializeField] private CombatUnitSO player;
    [SerializeField] private Transform playerPosition;


    [SerializeField] private CombatUnitSO[] enemies;
    [SerializeField] private Transform enemyPosition;

    public CombatRequest(CombatUnitSO player, Transform playerPosition, CombatUnitSO[] enemies, Transform enemyPosition)
    {
        this.player = player;
        this.playerPosition = playerPosition;
        this.enemies = enemies;
        this.enemyPosition = enemyPosition;
    }
}
