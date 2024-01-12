using UnityEngine;
using MLAPI;
using MLAPI.Messaging;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 180f;

    private void Update()
    {
        if (IsLocalPlayer)
        {
            // Only allow local player to control movement
            HandleMovementInput();
        }
    }

    private void HandleMovementInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 moveDirection = transform.TransformDirection(movement);

        // Move the player locally
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);

        // Rotate the player locally
        transform.Rotate(Vector3.up, horizontal * rotationSpeed * Time.deltaTime);

        // Send movement data to the server to be synchronized with other clients
        SendMovementToServer(moveDirection, transform.rotation);
    }

    [ServerRPC]
    private void SendMovementToServer(Vector3 moveDirection, Quaternion rotation)
    {
        // Update server-side position and rotation
        UpdateServerMovement(moveDirection, rotation);

        // Send the movement to all clients
        SendMovementToClients(moveDirection, rotation);
    }

    [Server]
    private void UpdateServerMovement(Vector3 moveDirection, Quaternion rotation)
    {
        // Update the server-side position and rotation
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        transform.rotation = rotation;
    }

    [ClientRPC]
    private void SendMovementToClients(Vector3 moveDirection, Quaternion rotation)
    {
        // Update the client-side position and rotation
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime, Space.World);
        transform.rotation = rotation;
    }
}
