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
            case "Left":
                movement = Vector3.left;
                break;
            case "Right":
                movement = Vector3.right;
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
