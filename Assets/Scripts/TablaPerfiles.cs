using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class TablaPerfiles : MonoBehaviour
{
    [Header("Referencias")]
    public GameObject filaPrefab;
    public Transform contenedorTabla;
    public GameObject panelTabla; // Referencia al panel contenedor de la tabla

    [Header("Datos de Perfiles")]
    public List<PerfilEstructural> perfiles = new List<PerfilEstructural>()
    {
        new PerfilEstructural { 
            nombre = "IPE 100", 
            altura = 100f, 
            anchoAla = 55f, 
            area = 8.1f, 
            Iy = 135f, 
            Wy = 27.1f, 
            Wpl_y = 29.3f 
        },
        // Agrega más perfiles aquí...
    };

    void Awake()
    {
        // Desactivar la tabla al inicio si hay referencia
        if(panelTabla != null) 
        {
            panelTabla.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Panel de tabla no asignado en el inspector");
        }
    }

    public void GenerarTabla()
    {
        // Activar el panel si está asignado
        if(panelTabla != null)
        {
            panelTabla.SetActive(true);
        }

        // Limpiar tabla existente
        LimpiarTabla();

        // Generar filas con los perfiles predefinidos
        foreach (var perfil in perfiles)
        {
            CrearFilaPerfil(perfil);
        }
    }

    public void GenerarTabla(List<PerfilEstructural> perfilesFiltrados)
    {
        // Activar el panel si está asignado
        if(panelTabla != null)
        {
            panelTabla.SetActive(true);
        }

        // Limpiar tabla existente
        LimpiarTabla();

        // Generar filas con los perfiles filtrados
        foreach (var perfil in perfilesFiltrados)
        {
            CrearFilaPerfil(perfil);
        }
    }

    private void LimpiarTabla()
    {
        // Destruir todas las filas existentes
        foreach (Transform child in contenedorTabla)
        {
            Destroy(child.gameObject);
        }
    }

    private void CrearFilaPerfil(PerfilEstructural perfil)
    {
        // Instanciar nueva fila
        GameObject nuevaFila = Instantiate(filaPrefab, contenedorTabla);
        
        // Configurar textos
        SetTextIfExists(nuevaFila, "NombreText", perfil.nombre);
        SetTextIfExists(nuevaFila, "AlturaText", $"{perfil.altura:F1} mm");
        SetTextIfExists(nuevaFila, "AnchoAlaText", $"{perfil.anchoAla:F1} mm");
        SetTextIfExists(nuevaFila, "AreaText", $"{perfil.area:F2} cm²");
        SetTextIfExists(nuevaFila, "IyText", $"{perfil.Iy:F1} cm⁴");
        SetTextIfExists(nuevaFila, "WyText", $"{perfil.Wy:F1} cm³");
        SetTextIfExists(nuevaFila, "WplYText", $"{perfil.Wpl_y:F1} cm³");
    }

    private void SetTextIfExists(GameObject parent, string childName, string value)
    {
        Transform child = parent.transform.Find(childName);
        if (child != null)
        {
            TMP_Text textComponent = child.GetComponent<TMP_Text>();
            if (textComponent != null)
            {
                textComponent.text = value;
            }
            else
            {
                Debug.LogError($"No se encontró componente TMP_Text en {childName}");
            }
        }
        else
        {
            Debug.LogError($"No se encontró objeto hijo llamado {childName}");
        }
    }
}