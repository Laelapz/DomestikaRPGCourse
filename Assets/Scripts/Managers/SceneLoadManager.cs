using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadManager : MonoBehaviour
{
    [Header("Dependencies")]
    public LoadingScreenUI loadingScreenUI;

    private LoadSceneRequest _pendingRequest;

    public void OnLoadMenuRequest(LoadSceneRequest request)
    {
        if(IsSceneAlreadyLoaded(request.scene) == false)
        {
            SceneManager.LoadScene(request.scene.SceneName);
        }
    }

    public void OnLoadLevelRequest(LoadSceneRequest request)
    {
        if (IsSceneAlreadyLoaded(request.scene))
        {
            ActivateLevel(request);
        }
        else
        {
            if (request.loadingScreen)
            {
                this._pendingRequest = request;
                this.loadingScreenUI.ToggleScreen(true);
            }
            else
            {
                StartCoroutine(ProcessLevelLoading(request));
            }
        }
    }

    public void OnLoadingScreenToggle(bool enabled)
    {
        if (enabled && this._pendingRequest != null)
        {
            StartCoroutine(ProcessLevelLoading(this._pendingRequest));
        }
    }

    private bool IsSceneAlreadyLoaded(SceneSO scene)
    {
        Scene loadedScene = SceneManager.GetSceneByName(scene.SceneName);

        if (loadedScene != null & loadedScene.isLoaded == true) return true;

        return false;
    }

    IEnumerator ProcessLevelLoading(LoadSceneRequest request)
    {
        print(request.scene);
        if (request.scene != null)
        {
            var currentLoadedLevel = SceneManager.GetActiveScene();
            SceneManager.UnloadSceneAsync(currentLoadedLevel);

            AsyncOperation loadSceneProcess = SceneManager.LoadSceneAsync(request.scene.name, LoadSceneMode.Additive);

            while(!loadSceneProcess.isDone)
            {
                yield return null;
            }
            ActivateLevel(request);
        }
    }

    private void ActivateLevel(LoadSceneRequest request)
    {
        var loadedLevel = SceneManager.GetSceneByName(request.scene.SceneName);
        print(loadedLevel.name);
        SceneManager.SetActiveScene(loadedLevel);

        if (request.loadingScreen)
        {
            this.loadingScreenUI.ToggleScreen(false);
        }

        this._pendingRequest = null;
    }
}
