using System.Collections;
using System.Collections.Generic;
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
    [Header("Combat Messages Configuration")]
    public string combatStartedInfoText = "An enemy appeared...";
    public string playerTurnInfoText = "Player's turn...";
    public string wonInfoText = "Enemy defeated!";
    public string enemyTurnInfoText = "Enemy's turn...";
    public string enemyAttackedInfoText = "Enemy attacked";

    [Header("Dependencies")]
    public CombatUI combatUI;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
