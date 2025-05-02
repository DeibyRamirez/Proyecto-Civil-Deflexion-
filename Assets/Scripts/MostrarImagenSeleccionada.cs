using UnityEngine;
using UnityEngine.UI;

public class MostrarImagenSeleccionada : MonoBehaviour
{
    public Image imagenSuperior;
    public RawImage rawImageModelo;

    public void ActualizarImagen()
    {
        if (ImagenSeleccionada.imagenSeleccionada != null)
        {
            imagenSuperior.sprite = ImagenSeleccionada.imagenSeleccionada.sprite; // ✅ Obtenemos solo el sprite
        }
        else
        {
            Debug.LogWarning("No se seleccionó ninguna imagen.");
        }
        if (ModeloSeleccionado.texturaModelo != null)
        {
            rawImageModelo.texture = ModeloSeleccionado.texturaModelo;
        }
        else
        {
            Debug.LogWarning("No se seleccionó ninguna textura de modelo.");
        }
    }
    
}
