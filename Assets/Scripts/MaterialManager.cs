using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro

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

    public GameObject[] modelos; // Array de modelos a los que se les cambiará la textura
    private Renderer[] perlinRenderers; // Array para los Renderers de los modelos

    public Material texturaConcreto;
    public Material texturaAcero;
    public Material texturaAluminio;
    public Material texturaMadera;
    public Material texturaGuadua;

    void Start()
    {
        Inicializar(); // Llamar a la función de inicialización al inicio
    }

    public void Inicializar()
    {
        // Inicializar los Renderers de todos los modelos
        perlinRenderers = new Renderer[modelos.Length];
        
        for (int i = 0; i < modelos.Length; i++)
        {
            GameObject modeloActual = modelos[i];
            if (modeloActual == null)
            {
                Debug.LogError("No se encontró el modelo en la escena.");
                return;
            }

            // Buscar dentro del modelo el objeto "Perlin Rectangulo"
            string nombrePerlin = "Perlin Rectangular"; // Nombre común para los modelos

            Transform perlinTransform = modeloActual.transform.Find(nombrePerlin);
            if (perlinTransform != null)
                perlinRenderers[i] = perlinTransform.GetComponent<Renderer>();
            else
            {
                Debug.LogError($"No se encontró el objeto '{nombrePerlin}' en el modelo.");
                return;
            }
        }

        // Crear y asignar las opciones de materiales
        materiales.Clear();
        materiales.Add(new MaterialData { nombre = "Concreto", moduloElasticidad = 21538f, textura = texturaConcreto });
        materiales.Add(new MaterialData { nombre = "Acero", moduloElasticidad = 200000f, textura = texturaAcero });
        materiales.Add(new MaterialData { nombre = "Aluminio", moduloElasticidad = 65000f, textura = texturaAluminio });
        materiales.Add(new MaterialData { nombre = "Madera", moduloElasticidad = 12000f, textura = texturaMadera });
        materiales.Add(new MaterialData { nombre = "Guadua", moduloElasticidad = 10000f, textura = texturaGuadua });

        // Cargar nombres al Dropdown
        dropdown.ClearOptions();
        List<string> nombres = new List<string>();
        foreach (MaterialData material in materiales)
        {
            nombres.Add(material.nombre);
        }
        dropdown.AddOptions(nombres);

        // Asignar el evento al Dropdown
        dropdown.onValueChanged.RemoveAllListeners(); // Asegurarse de no duplicar eventos
        dropdown.onValueChanged.AddListener(CambiarMaterial);

        Debug.Log("MaterialManager inicializado correctamente.");
    }

    void CambiarMaterial(int index)
    {
        if (perlinRenderers.Length == 0)
        {
            Debug.LogError("No se encontraron los Renderers para cambiar el material.");
            return;
        }

        MaterialData materialSeleccionado = materiales[index];
        for (int i = 0; i < perlinRenderers.Length; i++)
        {
            if (perlinRenderers[i] != null)
            {
                perlinRenderers[i].material = materialSeleccionado.textura;
            }
        }

        moduloSeleccionado = materialSeleccionado.moduloElasticidad;

        Debug.Log($"Material cambiado a: {materialSeleccionado.nombre}, Módulo de Elasticidad: {moduloSeleccionado} MPa");
    }
}
