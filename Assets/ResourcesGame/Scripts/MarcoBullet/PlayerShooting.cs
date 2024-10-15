using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float range = 100f; // Rango del disparo
    public LayerMask targetLayer; // Capa de los objetivos
    public GameObject particlePrefab; // Prefab del sistema de partículas
    public Transform firePoint; // Pivote del arma

    private GameObject currentParticle; // Instancia actual de la partícula

    void Update()
    {
        if (Input.GetButton("Fire1")) // Mantener presionado el botón izquierdo del ratón o Ctrl
        {
            if (currentParticle == null)
            {
                // Instanciar la partícula y empezar a disparar
                currentParticle = Instantiate(particlePrefab, firePoint.position, firePoint.rotation);
                ParticleSystem ps = currentParticle.GetComponent<ParticleSystem>();
                ps.Play(); // Iniciar el sistema de partículas
            }

            // Realizar el raycast
            RaycastHit hit;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range, targetLayer))
            {
                Debug.Log("Disparo exitoso a: " + hit.transform.name);
                // Puedes añadir lógica para dañar al objetivo
            }
        }
        else if (currentParticle != null)
        {
            // Detener y destruir la partícula al soltar el botón
            ParticleSystem ps = currentParticle.GetComponent<ParticleSystem>();
            ps.Stop(); // Detener el sistema de partículas
            Destroy(currentParticle, ps.main.duration); // Esperar a que termine y luego destruir
            currentParticle = null; // Resetear la referencia
        }
    }
}



