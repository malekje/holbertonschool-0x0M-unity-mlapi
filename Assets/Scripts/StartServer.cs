using UnityEngine;
using UnityEngine.UI;
using MLAPI;
using MLAPI.SceneManagement;

public class ServerManager : NetworkBehaviour
{
    [SerializeField] private GameObject arenaPrefab;

    private void Start()
    {
        // Disable the button if not the host
        if (!NetworkManager.Singleton.IsHost)
        {
            GetComponent<Button>().interactable = false;
        }
    }

    public void StartServer()
    {
        if (!NetworkManager.Singleton.IsServer)
        {
            // Start the server and load the arena scene
            NetworkManager.Singleton.StartHost();
            NetworkSceneManager.SwitchScene("ArenaScene");
        }

        // Spawn the arena on the server
        SpawnArena();
    }

    private void SpawnArena()
    {
        if (IsServer)
        {
            // Spawn the arena prefab
            Instantiate(arenaPrefab, Vector3.zero, Quaternion.identity);
        }
    }
}
