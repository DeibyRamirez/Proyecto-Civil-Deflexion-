using System.Collections.Generic;
using TMPro;
using UnityEngine;


[System.Serializable]

public class Perlines
{
    public string nombre;

}
public class TiposPerlin : MonoBehaviour
{
    public List<Perlines> perlines = new List<Perlines>();
    public TMP_Dropdown dropdown;

    public List<RenderTexture> renderTexturesSoporte; // 5 elementos
    public List<RenderTexture> renderTexturesMuro;    // 5 elementos

    public UnityEngine.UI.RawImage rawImageSoporte;
    public UnityEngine.UI.RawImage rawImageMuro;

    public float moduloSeleccionado;

    void Start()
    {
        Inicializar();
    }

    public void Inicializar()
    {
        perlines.Clear();
        perlines.Add(new Perlines { nombre = "Perlin Rectangular"});
        perlines.Add(new Perlines { nombre = "Perlin Cuadrado"});
        perlines.Add(new Perlines { nombre = "Perlin Circular"});
        perlines.Add(new Perlines { nombre = "Perlin H"});
        perlines.Add(new Perlines { nombre = "Perlin I"});

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
        // Verifica que hay suficientes render textures
        if (index >= 0 && index < renderTexturesSoporte.Count && index < renderTexturesMuro.Count)
        {
            rawImageSoporte.texture = renderTexturesSoporte[index];
            rawImageMuro.texture = renderTexturesMuro[index];
        }
    }
}
