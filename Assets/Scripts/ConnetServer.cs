using UnityEngine;
using UnityEngine.UI;
using MLAPI;

public class ClientManager : NetworkBehaviour
{
    [SerializeField] private GameObject playerPrefab;

    private void Start()
    {
        // Disable the button if already connected
        if (NetworkManager.Singleton.IsConnectedClient)
        {
            GetComponent<Button>().interactable = false;
        }
    }

    public void ConnectToServer()
    {
        if (!NetworkManager.Singleton.IsConnectedClient)
        {
            // Connect to the server
            NetworkManager.Singleton.StartClient();
        }
    }

    private void OnConnectedToServer()
    {
        // Instantiate player prefab on the client side
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        // Spawn the player prefab on the client and synchronize it across the network
        Instantiate(playerPrefab, GetSpawnPosition(), Quaternion.identity);
    }

    private Vector3 GetSpawnPosition()
    {
        // Set the spawn position based on your game requirements
        return Vector3.zero;
    }
}
