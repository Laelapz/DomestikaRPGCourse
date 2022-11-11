using Cinemachine;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("Dependencies")]
    public PlayerPathSO playerPath;
    public GameObject playerPrefab;
    public CinemachineVirtualCamera followCamera;
    public GameObject playerParent;

    public void InstantiatePlayerOnLevel()
    {
        GameObject player = GetPlayer();
        Transform entrance = GetLevelEntrance(playerPath.levelEntrance);

        player.transform.position = entrance.position;
        player.transform.parent = playerParent.transform;
        this.followCamera.Follow = player.transform;

        playerPath.levelEntrance = null;
    }

    private GameObject GetPlayer()
    {
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if(playerObject == null)
        {
            playerObject = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
        }

        return playerObject;
    }

    private Transform GetLevelEntrance(LevelEntranceSO playerEntrance)
    {
        if(playerEntrance == null)
        {
            return this.transform.GetChild(0).transform;
        }

        var levelEntrances = GameObject.FindObjectsOfType<LevelEntrance>();

        foreach(LevelEntrance levelEntrance in levelEntrances)
        {
            if( levelEntrance.entrance == playerEntrance)
            {
                return levelEntrance.gameObject.transform;
            }
        }

        return this.transform.GetChild(0).transform;
    }
}
