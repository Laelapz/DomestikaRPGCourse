using ScriptableObjectArchitecture;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [Header("Configuration")]
    public SceneSO scene;
    public LevelEntranceSO levelEntrance;
    public bool loadingScreen;

    [Header("Player Path")]
    public PlayerPathSO playerPath;

    [Header("Broadcasting events")]
    public LoadSceneRequestGameEvent loadSceneEvent;

    public void LoadScene()
    {
        if (this.levelEntrance != null && this.playerPath != null) this.playerPath.levelEntrance = this.levelEntrance;

        var request = new LoadSceneRequest(scene, loadingScreen);

        this.loadSceneEvent.Raise(request);
    }
}
