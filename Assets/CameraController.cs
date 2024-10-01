using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineFreeLook freeLookCamera;  // Referencia a la Cinemachine FreeLook
    public float rotationSpeed = 100f;  // Velocidad de rotación de la cámara

    void Update()
    {
        // Leer las entradas del teclado numérico
        float horizontalInput = 0f;
        float verticalInput = 0f;

        // Teclas 4 y 6 para rotación horizontal
        if (Input.GetKey(KeyCode.Keypad4))
        {
            horizontalInput = -1f;  // Rotación a la izquierda
        }
        else if (Input.GetKey(KeyCode.Keypad6))
        {
            horizontalInput = 1f;   // Rotación a la derecha
        }

        // Teclas 8 y 2 para rotación vertical
        if (Input.GetKey(KeyCode.Keypad8))
        {
            verticalInput = 1f;   // Rotación hacia arriba
        }
        else if (Input.GetKey(KeyCode.Keypad2))
        {
            verticalInput = -1f;  // Rotación hacia abajo
        }

        // Rotar la cámara alrededor del jugador en el eje Y (horizontal)
        if (horizontalInput != 0)
        {
            freeLookCamera.m_XAxis.Value += horizontalInput * rotationSpeed * Time.deltaTime;
        }

        // Ajustar la rotación vertical de la cámara (eje X)
        if (verticalInput != 0)
        {
            freeLookCamera.m_YAxis.Value += verticalInput * Time.deltaTime;
        }
    }
}
