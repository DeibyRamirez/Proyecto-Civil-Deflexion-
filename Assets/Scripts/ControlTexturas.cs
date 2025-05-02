using UnityEngine;
using UnityEngine.UI;

public class MostrarTexturaSeleccionada : MonoBehaviour
{
    public RawImage rawImageDestino;

    void Start()
    {
        if (ModeloSeleccionado.texturaModelo != null)
        {
            rawImageDestino.texture = ModeloSeleccionado.texturaModelo;
        }
        else
        {
            Debug.LogWarning("No hay textura seleccionada.");
        }
    }
}
