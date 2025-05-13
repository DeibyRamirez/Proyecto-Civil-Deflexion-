using System.Collections.Generic;
using UnityEngine;

public class GestorTexturasPerlin : MonoBehaviour
{
    [Header("Nombres de cada tipo de Perlin")]
    public List<string> nombresPerlin = new List<string> { "Rectangular", "Cuadrado", "Circular", "H", "I" };

    [Header("Texturas para soporte")]
    public List<RenderTexture> texturasSoporte;

    [Header("Texturas para muro")]
    public List<RenderTexture> texturasMuro;

    public static Dictionary<string, RenderTexture> soporteDict = new();
    public static Dictionary<string, RenderTexture> muroDict = new();

    void Awake()
    {
        for (int i = 0; i < nombresPerlin.Count; i++)
        {
            soporteDict[nombresPerlin[i]] = texturasSoporte[i];
            muroDict[nombresPerlin[i]] = texturasMuro[i];
        }
    }
}
