using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;  // Velocidad de movimiento
    public float jumpForce = 7f;  // Fuerza de salto
    public float smoothMoveTime = 0.1f;  // Suavizado del movimiento
    public LayerMask groundLayer;  // Capa del suelo para detectar si está en tierra firme

    private Vector3 moveDirection;  // Dirección de movimiento
    private Vector3 currentVelocity;  // Velocidad actual para suavizado (CAMBIADO a Vector3)
    private Rigidbody rb;  // Referencia al componente Rigidbody
    private bool isGrounded;  // Verifica si el personaje está en el suelo

    void Start()
    {
        rb = GetComponent<Rigidbody>();  // Obtener el Rigidbody del jugador
    }

    void Update()
    {
        // Obtener entradas de movimiento de los ejes horizontales y verticales (X y Z)
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Establecer la dirección de movimiento en el plano XZ y aplicar suavizado
        Vector3 targetDirection = new Vector3(moveX, 0f, moveZ).normalized;
        moveDirection = Vector3.SmoothDamp(moveDirection, targetDirection, ref currentVelocity, smoothMoveTime);

        // Verificar si está en el suelo antes de permitir el salto
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);

        // Lógica de salto
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
        // Calcular la nueva posición basándonos en la dirección de movimiento y la velocidad
        Vector3 movement = moveDirection * speed * Time.fixedDeltaTime;

        // Mover el Rigidbody en XZ (el eje Y es manejado por la física del salto)
        rb.MovePosition(rb.position + movement);
    }

    void Jump()
    {
        // Aplicar fuerza de salto en el eje Y
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
}
