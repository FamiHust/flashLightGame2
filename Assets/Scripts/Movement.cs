using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class MovementReceiver : MonoBehaviour
{
    public float moveSpeed = 5f;
    private UdpClient udpClient;
    private string movementCommand = "Centered";

    void Start()
    {
        // Start UDP server
        udpClient = new UdpClient(12345); // Same port as Python
        udpClient.BeginReceive(OnReceive, null);
    }

    void OnReceive(IAsyncResult result)
    {
        try
        {
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 12345);
            byte[] receivedData = udpClient.EndReceive(result, ref remoteEndPoint);
            movementCommand = Encoding.UTF8.GetString(receivedData);
            Debug.Log($"Received Movement Command: {movementCommand}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error receiving UDP data: {e}");
        }
        finally
        {
            udpClient.BeginReceive(OnReceive, null); // Continue receiving
        }
    }

    void Update()
    {
        Vector3 movement = Vector3.zero;

        // Map commands to movement
        switch (movementCommand)
        {
            case "Up":
                movement = Vector3.right; // Move left when going up
                break;
            case "Down":
                movement = Vector3.left; // Move right when going down
                break;
            case "Left":
                movement = Vector3.up; // Move down when going left
                break;
            case "Right":
                movement = Vector3.down; // Move up when going right
                break;
            case "UpLeft":
                movement = new Vector3(1, 1, 0).normalized; // Diagonal adjustment
                break;
            case "UpRight":
                movement = new Vector3(1, 1, 0).normalized; // Diagonal adjustment
                break;
            case "DownLeft":
                movement = new Vector3(1, 1, 0).normalized; // Diagonal adjustment
                break;
            case "DownRight":
                movement = new Vector3(1, 1, 0).normalized; // Diagonal adjustment
                break;
            case "Centered":
                movement = Vector3.zero;
                break;
        }

        // Move the character
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }

    void OnDestroy()
    {
        udpClient.Close();
    }
}
