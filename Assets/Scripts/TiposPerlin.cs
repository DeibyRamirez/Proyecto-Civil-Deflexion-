using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Perlines
{
    public string nombre;
}

public class TiposPerlin : MonoBehaviour
{
    public List<Perlines> perlines = new List<Perlines>();
    public TMP_Dropdown dropdown;

    public List<RenderTexture> renderTexturesSoporte;
    public List<RenderTexture> renderTexturesMuro;

    public RawImage rawImageSoporte;
    public RawImage rawImageMuro;

    public float moduloSeleccionado;

    public static string nombrePerlinSeleccionado;

    [SerializeField] private Tienda tienda; // Ahora lo asignamos desde el Inspector

    void Start()
    {
        Inicializar();
    }

    public void Inicializar()
    {
        perlines.Clear();
        perlines.Add(new Perlines { nombre = "Rectangular" });
        perlines.Add(new Perlines { nombre = "Cuadrado" });
        perlines.Add(new Perlines { nombre = "Circular" });
        perlines.Add(new Perlines { nombre = "H" });
        perlines.Add(new Perlines { nombre = "I" });

        dropdown.ClearOptions();
        List<string> nombres = new List<string>();
        foreach (Perlines perlin in perlines)
        {
            nombres.Add(perlin.nombre);
        }
        dropdown.AddOptions(nombres);

        dropdown.onValueChanged.RemoveAllListeners();
        dropdown.onValueChanged.AddListener(OnDropdownValueChanged);

        // Mostrar el primero por defecto
        OnDropdownValueChanged(0);
    }

    private void OnDropdownValueChanged(int index)
    {
        if (index >= 0 && index < renderTexturesSoporte.Count && index < renderTexturesMuro.Count)
        {
            rawImageSoporte.texture = renderTexturesSoporte[index];
            rawImageMuro.texture = renderTexturesMuro[index];

            nombrePerlinSeleccionado = perlines[index].nombre;

            // Actualizar la tienda si está asignada
            if (tienda != null)
            {
                tienda.ActualizarTienda();
            }
            else
            {
                Debug.LogWarning("No se ha asignado la referencia a Tienda en el Inspector");
            }
        }
    }
}