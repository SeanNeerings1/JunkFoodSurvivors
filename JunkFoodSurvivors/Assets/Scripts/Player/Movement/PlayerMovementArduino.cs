using System.IO.Ports;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovementArduino : MonoBehaviour
{
    [SerializeField] private string _portName = "COM8";
    private SerialPort stream;

    public float moveSpeed = 5f;
    [HideInInspector] public Vector2 moveDirection = Vector2.zero;
    [HideInInspector] public Vector2 lastMoveDirection = Vector2.right;
    private bool _isArduinoConnected = false;

    void Start()
    {
        if (IsPortAvailable(_portName))
        {
            stream = new SerialPort(_portName, 9600);
            stream.ReadTimeout = 10; 

            try
            {
                stream.Open();
                _isArduinoConnected = true;
                Debug.Log(" Succesfully connected to arduino port " + _portName);
            }
            catch (System.Exception e)
            {
                Debug.LogError("Could'nt open poort " + _portName + ": " + e.Message);
                DisconnectArduino();
            }
        }
        else
        {
            Debug.LogWarning(_portName + " Not found. Arduino-control skipped.");
            DisconnectArduino();
        }
    }

    void Update()
    {
        if (!_isArduinoConnected || stream == null) return;

        try
        {
            if (stream.IsOpen)
            {
                string value = stream.ReadLine().Trim();

                if (!string.IsNullOrEmpty(value))
                {
                    char command = value[0];
                    ProcessInput(command);
                }
            }
        }
        catch (System.TimeoutException)
        {
        
        }
        catch (System.Exception e)
        {
            Debug.LogWarning("Arduino disconnected: " + e.Message);
            DisconnectArduino();
        }
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    void ProcessInput(char cmd)
    {
        switch (cmd)
        {
            case 'A'://pin 2 -> left
                moveDirection = Vector2.left;
                lastMoveDirection = moveDirection;
                break;
            case 'W': //pin 3 -> up
                moveDirection = Vector2.up;
                lastMoveDirection = moveDirection;
                break;
            case 'S': // Pin 4 -> down
                moveDirection = Vector2.down;
                lastMoveDirection = moveDirection;
                break;
            case 'D': // Pin 5 -> right
                moveDirection = Vector2.right;
                lastMoveDirection = moveDirection;
                break;
            case 'X': // no button pressed stop moving
                moveDirection = Vector2.zero;
                break;

            // --- ACTIONS (button 5 and 6) ---
            case 'B': // Pin 6 -> Accepted
                Debug.LogWarning(" [INPUT] Accept-button Pressed (Works mechanical, here comes the menu code and the accept upgrade)");
                break;

            case 'K': // Pin 7 -> Pause
                Debug.LogWarning(" [INPUT] Pauze-Button pressed (Input works the menu does'nt)");
                break;
        }
    }
    private bool IsPortAvailable(string port)
    {
        string[] availablePorts = SerialPort.GetPortNames();
        foreach (string p in availablePorts)
        {
            if (p == port) return true;
        }
        return false;
    }
    private void DisconnectArduino()
    {
        _isArduinoConnected = false;
        moveDirection = Vector2.zero; 

        try
        {
            if (stream != null)
            {
                if (stream.IsOpen) stream.Close();
                stream.Dispose();
            }
        }
        catch { }

        stream = null;
    }

    void OnApplicationQuit()
    {
        DisconnectArduino();
        Debug.Log(" Poort disconnected.");
    }
}