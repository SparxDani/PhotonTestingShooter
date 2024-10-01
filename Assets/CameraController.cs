using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;  // Referencia a la Cinemachine FreeLook
    public float rotationSpeed = 100f;  // Velocidad de rotaci�n de la c�mara

    void Update()
    {
        // Leer las entradas del teclado num�rico
        float horizontalInput = 0f;
        float verticalInput = 0f;

        // Teclas 4 y 6 para rotaci�n horizontal
        if (Input.GetKey(KeyCode.Keypad4))
        {
            horizontalInput = -1f;  // Rotaci�n a la izquierda
        }
        else if (Input.GetKey(KeyCode.Keypad6))
        {
            horizontalInput = 1f;   // Rotaci�n a la derecha
        }

        // Teclas 8 y 2 para rotaci�n vertical
        if (Input.GetKey(KeyCode.Keypad8))
        {
            verticalInput = 1f;   // Rotaci�n hacia arriba
        }
        else if (Input.GetKey(KeyCode.Keypad2))
        {
            verticalInput = -1f;  // Rotaci�n hacia abajo
        }

        // Rotar la c�mara alrededor del jugador en el eje Y (horizontal)
        if (horizontalInput != 0)
        {
            freeLookCamera.m_XAxis.Value += horizontalInput * rotationSpeed * Time.deltaTime;
        }

        // Ajustar la rotaci�n vertical de la c�mara (eje X)
        if (verticalInput != 0)
        {
            freeLookCamera.m_YAxis.Value += verticalInput * Time.deltaTime;
        }
    }
}
