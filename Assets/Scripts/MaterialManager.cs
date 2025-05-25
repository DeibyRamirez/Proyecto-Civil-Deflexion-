using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class MaterialData
{
    public string nombre;
    public float moduloElasticidad;
    public Material textura;
}

public class MaterialManager : MonoBehaviour
{
    public List<MaterialData> materiales = new List<MaterialData>();
    public TMP_Dropdown dropdown;
    public float moduloSeleccionado;

    public GameObject[] modelos;
    private Renderer[] perlinRenderers;

    public Material texturaConcreto;
    public Material texturaAcero;
    public Material texturaAluminio;
    public Material texturaMadera;
    public Material texturaGuadua;

    void Start()
    {
        Inicializar();
        // Forzar selección inicial
        if (dropdown.options.Count > 0)
        {
            CambiarMaterial(dropdown.value);
        }
    }

    public void Inicializar()
    {
        // Inicializar renderers
        perlinRenderers = new Renderer[modelos.Length];
        
        for (int i = 0; i < modelos.Length; i++)
        {
            if (modelos[i] == null)
            {
                Debug.LogError("Modelo no asignado en el inspector");
                continue;
            }

            Transform perlinTransform = modelos[i].transform.Find("Perlin Rectangular");
            if (perlinTransform != null)
                perlinRenderers[i] = perlinTransform.GetComponent<Renderer>();
            else
                Debug.LogError("No se encontró 'Perlin Rectangular' en el modelo");
        }

        // Configurar materiales
        materiales.Clear();
        materiales.Add(new MaterialData { nombre = "Concreto", moduloElasticidad = 21538f, textura = texturaConcreto });
        materiales.Add(new MaterialData { nombre = "Acero", moduloElasticidad = 200000f, textura = texturaAcero });
        materiales.Add(new MaterialData { nombre = "Aluminio", moduloElasticidad = 65000f, textura = texturaAluminio });
        materiales.Add(new MaterialData { nombre = "Madera", moduloElasticidad = 12000f, textura = texturaMadera });
        materiales.Add(new MaterialData { nombre = "Guadua", moduloElasticidad = 10000f, textura = texturaGuadua });

        // Configurar dropdown
        dropdown.ClearOptions();
        List<string> nombres = new List<string>();
        foreach (MaterialData material in materiales)
        {
            nombres.Add(material.nombre);
        }
        dropdown.AddOptions(nombres);

        dropdown.onValueChanged.RemoveAllListeners();
        dropdown.onValueChanged.AddListener(CambiarMaterial);

        Debug.Log("MaterialManager inicializado correctamente");
    }

    void CambiarMaterial(int index)
    {
        if (index < 0 || index >= materiales.Count)
        {
            Debug.LogError("Índice de material fuera de rango");
            return;
        }

        MaterialData materialSeleccionado = materiales[index];
        moduloSeleccionado = materialSeleccionado.moduloElasticidad;

        // Aplicar textura
        for (int i = 0; i < perlinRenderers.Length; i++)
        {
            if (perlinRenderers[i] != null)
            {
                perlinRenderers[i].material = materialSeleccionado.textura;
            }
        }

        Debug.Log($"Material cambiado a: {materialSeleccionado.nombre}, Módulo: {moduloSeleccionado} MPa");
    }
}