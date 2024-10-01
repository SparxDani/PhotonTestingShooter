using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;

public class NickName : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;  // Referencia al TextMeshPro UGUI (interfaz de usuario)

    void Start()
    {
        // Verificar que TextMeshPro esté asignado
        if (textMeshPro != null)
        {
            // Obtener el nombre del GameObject y asignarlo al TextMeshPro
            textMeshPro.text = SandController.instance.UINickName.text;
        }
        else
        {
            UnityEngine.Debug.LogWarning("TextMeshPro no está asignado.");
        }
    }
}