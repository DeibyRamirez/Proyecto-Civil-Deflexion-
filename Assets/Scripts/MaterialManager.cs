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
    private List<Renderer> perlinRenderers = new List<Renderer>();

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
        // Limpiar lista de renderers
        perlinRenderers.Clear();
        
        // Buscar todos los posibles Perlines en cada modelo
        string[] posiblesPerlines = {
            "Perlin Rectangular",
            "Perlin Cuadrado",
            "Perlin Circular",
            "Perlin H",
            "Perlin I"
        };

        foreach (GameObject modelo in modelos)
        {
            if (modelo == null)
            {
                Debug.LogError("Modelo no asignado en el inspector");
                continue;
            }

            bool encontrado = false;
            foreach (string perlinName in posiblesPerlines)
            {
                Transform perlinTransform = modelo.transform.Find(perlinName);
                if (perlinTransform != null)
                {
                    Renderer renderer = perlinTransform.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        perlinRenderers.Add(renderer);
                        encontrado = true;
                        break; // Salir del bucle si encontramos un Perlin
                    }
                }
            }

            if (!encontrado)
            {
                Debug.LogError($"No se encontró ningún Perlin conocido en el modelo {modelo.name}");
            }
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

        // Aplicar textura a todos los Perlines encontrados
        foreach (Renderer renderer in perlinRenderers)
        {
            if (renderer != null)
            {
                renderer.material = materialSeleccionado.textura;
            }
        }

        Debug.Log($"Material cambiado a: {materialSeleccionado.nombre}, Módulo: {moduloSeleccionado} MPa");
    }
}