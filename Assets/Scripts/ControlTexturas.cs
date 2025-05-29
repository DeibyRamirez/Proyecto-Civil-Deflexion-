using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MostrarTexturaSeleccionada : MonoBehaviour
{
    [Header("Referencias UI")]
    [Tooltip("Primer destino para mostrar la textura")]
    public RawImage rawImageDestino;
    
    [Tooltip("Segundo destino para mostrar la textura")]
    public RawImage rawImageDestino2;

    [Header("Configuración")]
    [Tooltip("¿Mostrar advertencias si no hay selección?")]
    public bool mostrarAdvertencias = true;

    private void OnValidate()
    {
        // Validación en el editor
        if (rawImageDestino == null) Debug.LogError("rawImageDestino no asignado en el inspector!", this);
        if (rawImageDestino2 == null) Debug.LogError("rawImageDestino2 no asignado en el inspector!", this);
    }

    void Start()
    {
        if (!isActiveAndEnabled)
        {
            Debug.LogWarning($"{name} está desactivado. No se cargarán texturas.", this);
            return;
        }

        try
        {
            if (ModeloSeleccionado.texturaModelo != null)
            {
                if (rawImageDestino != null)
                {
                    rawImageDestino.texture = ModeloSeleccionado.texturaModelo;
                    rawImageDestino.enabled = true;
                }
                else if (mostrarAdvertencias)
                {
                    Debug.LogWarning("rawImageDestino no asignado. No se puede mostrar textura.", this);
                }

                if (rawImageDestino2 != null)
                {
                    rawImageDestino2.texture = ModeloSeleccionado.texturaModelo;
                    rawImageDestino2.enabled = true;
                }
                else if (mostrarAdvertencias)
                {
                    Debug.LogWarning("rawImageDestino2 no asignado. No se puede mostrar textura.", this);
                }
            }
            else if (mostrarAdvertencias)
            {
                Debug.LogWarning("No hay textura seleccionada.", this);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error al cargar textura seleccionada: {e.Message}", this);
        }
    }
}