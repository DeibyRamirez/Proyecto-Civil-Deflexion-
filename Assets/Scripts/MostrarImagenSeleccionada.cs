using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MostrarImagenSeleccionada : MonoBehaviour
{
    [Header("Referencias UI")]
    [Tooltip("Imagen superior donde se mostrará el sprite")]
    public Image imagenSuperior;
    
    [Tooltip("RawImage para mostrar la textura del modelo (primera vista)")]
    public RawImage rawImageModelo;
    
    [Tooltip("RawImage para mostrar la textura del modelo (segunda vista)")]
    public RawImage rawImageModelo2;

    [Header("Configuración")]
    [Tooltip("¿Mostrar advertencias si no hay selección?")]
    public bool mostrarAdvertencias = true;

    private void OnValidate()
    {
        // Validación en el editor
        if (imagenSuperior == null) Debug.LogError("imagenSuperior no asignada en el inspector!", this);
        if (rawImageModelo == null) Debug.LogError("rawImageModelo no asignada en el inspector!", this);
        if (rawImageModelo2 == null) Debug.LogError("rawImageModelo2 no asignada en el inspector!", this);
    }

    public void ActualizarImagen()
    {
        if (!isActiveAndEnabled)
        {
            Debug.LogWarning($"{name} está desactivado. No se actualizarán imágenes.", this);
            return;
        }

        // Manejo de la imagen seleccionada
        try
        {
            if (ImagenSeleccionada.imagenSeleccionada != null)
            {
                if (imagenSuperior != null)
                {
                    imagenSuperior.sprite = ImagenSeleccionada.imagenSeleccionada.sprite;
                    imagenSuperior.enabled = true;
                }
                else if (mostrarAdvertencias)
                {
                    Debug.LogWarning("imagenSuperior no asignada. No se puede mostrar sprite.", this);
                }
            }
            else if (mostrarAdvertencias)
            {
                Debug.LogWarning("No se seleccionó ninguna imagen.", this);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error al cargar imagen seleccionada: {e.Message}", this);
        }

        // Manejo de la textura del modelo
        try
        {
            if (ModeloSeleccionado.texturaModelo != null)
            {
                if (rawImageModelo != null)
                {
                    rawImageModelo.texture = ModeloSeleccionado.texturaModelo;
                    rawImageModelo.enabled = true;
                }
                else if (mostrarAdvertencias)
                {
                    Debug.LogWarning("rawImageModelo no asignado. No se puede mostrar textura.", this);
                }

                if (rawImageModelo2 != null)
                {
                    rawImageModelo2.texture = ModeloSeleccionado.texturaModelo;
                    rawImageModelo2.enabled = true;
                }
                else if (mostrarAdvertencias)
                {
                    Debug.LogWarning("rawImageModelo2 no asignado. No se puede mostrar textura.", this);
                }
            }
            else if (mostrarAdvertencias)
            {
                Debug.LogWarning("No se seleccionó ninguna textura de modelo.", this);
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error al cargar textura del modelo: {e.Message}", this);
        }
    }
}