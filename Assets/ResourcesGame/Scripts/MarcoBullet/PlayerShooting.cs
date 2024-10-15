using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public float range = 100f; // Rango del disparo
    public LayerMask targetLayer; // Capa de los objetivos
    public GameObject particlePrefab; // Prefab del sistema de part�culas
    public Transform firePoint; // Pivote del arma

    private GameObject currentParticle; // Instancia actual de la part�cula

    void Update()
    {
        if (Input.GetButton("Fire1")) // Mantener presionado el bot�n izquierdo del rat�n o Ctrl
        {
            if (currentParticle == null)
            {
                // Instanciar la part�cula y empezar a disparar
                currentParticle = Instantiate(particlePrefab, firePoint.position, firePoint.rotation);
                ParticleSystem ps = currentParticle.GetComponent<ParticleSystem>();
                ps.Play(); // Iniciar el sistema de part�culas
            }

            // Realizar el raycast
            RaycastHit hit;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range, targetLayer))
            {
                Debug.Log("Disparo exitoso a: " + hit.transform.name);
                // Puedes a�adir l�gica para da�ar al objetivo
            }
        }
        else if (currentParticle != null)
        {
            // Detener y destruir la part�cula al soltar el bot�n
            ParticleSystem ps = currentParticle.GetComponent<ParticleSystem>();
            ps.Stop(); // Detener el sistema de part�culas
            Destroy(currentParticle, ps.main.duration); // Esperar a que termine y luego destruir
            currentParticle = null; // Resetear la referencia
        }
    }
}



