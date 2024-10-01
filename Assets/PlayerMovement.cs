using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;  // Velocidad de movimiento
    public float jumpForce = 7f;  // Fuerza de salto
    public float smoothMoveTime = 0.1f;  // Suavizado del movimiento
    public LayerMask groundLayer;  // Capa del suelo para detectar si est� en tierra firme

    private Vector3 moveDirection;  // Direcci�n de movimiento
    private Vector3 currentVelocity;  // Velocidad actual para suavizado (CAMBIADO a Vector3)
    private Rigidbody rb;  // Referencia al componente Rigidbody
    private bool isGrounded;  // Verifica si el personaje est� en el suelo

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Obtener el Rigidbody del jugador
    }

    void Update()
    {
        // Obtener entradas de movimiento de los ejes horizontales y verticales (X y Z)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Establecer la direcci�n de movimiento en el plano XZ y aplicar suavizado
        Vector3 targetDirection = new Vector3(moveX, 0f, moveZ).normalized;
        moveDirection = Vector3.SmoothDamp(moveDirection, targetDirection, ref currentVelocity, smoothMoveTime);

        // Verificar si est� en el suelo antes de permitir el salto
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);

        // L�gica de salto
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // Aplicar movimiento suave en el plano XZ
        MovePlayer();
    }

    void MovePlayer()
    {
        // Calcular la nueva posici�n bas�ndonos en la direcci�n de movimiento y la velocidad
        Vector3 movement = moveDirection * speed * Time.fixedDeltaTime;

        // Mover el Rigidbody en XZ (el eje Y es manejado por la f�sica del salto)
        rb.MovePosition(rb.position + movement);
    }

    void Jump()
    {
        // Aplicar fuerza de salto en el eje Y
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
