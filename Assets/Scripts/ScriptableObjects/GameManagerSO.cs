using ScriptableObjectArchitecture;
using UnityEngine;

[CreateAssetMenu(fileName = "GameManager", menuName = "Scriptable Objects/Game Manager")]
public class GameManagerSO : ScriptableObject
{
    public GameStateSO currentState;

    [Header("Broadcasting Events")]
    public GameStateSOGameEvent gameStateChanged;

    private GameStateSO _previousState;

    public void SetGameState(GameStateSO gameState)
    {
        if (this.currentState != null)
        {
            this._previousState = currentState;
        }

        this.currentState = gameState;

        if(this.currentState != null)
        {
            gameStateChanged.Raise(gameState);
        }
    }

    public void RestorePreviousState()
    {
        this.SetGameState(this._previousState);
    }
}
