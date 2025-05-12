using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TablaPerfiles : MonoBehaviour
{
    public GameObject filaPrefab;
    public Transform contenedorTabla;

    private List<PerfilEstructural> perfiles = new List<PerfilEstructural>()
    {
        new PerfilEstructural { nombre = "IPE 100", altura = 100f, anchoAla = 55f, area = 8.1f, Iy = 135f, Wy = 27.1f, Wpl_y = 29.3f },
        new PerfilEstructural { nombre = "IPE 200", altura = 200f, anchoAla = 100f, area = 21.4f, Iy = 858f, Wy = 85.8f, Wpl_y = 98.2f },
        new PerfilEstructural { nombre = "IPE 300", altura = 300f, anchoAla = 150f, area = 45.9f, Iy = 4680f, Wy = 312f, Wpl_y = 356f },
        // Agrega más perfiles si quieres
    };

    void Start()
    {
        foreach (var perfil in perfiles)
        {
            GameObject fila = Instantiate(filaPrefab, contenedorTabla);
            Text[] columnas = fila.GetComponentsInChildren<Text>();

            if (columnas.Length >= 7)
            {
                fila.transform.Find("NombreText").GetComponent<Text>().text = perfil.nombre;
                fila.transform.Find("AlturaText").GetComponent<Text>().text = perfil.altura.ToString("F1") + " mm";
                fila.transform.Find("AnchoAlaText").GetComponent<Text>().text = perfil.anchoAla.ToString("F1") + " mm";
                fila.transform.Find("AreaText").GetComponent<Text>().text = perfil.area.ToString("F2") + " cm²";
                fila.transform.Find("IyText").GetComponent<Text>().text = perfil.Iy.ToString("F1") + " cm⁴";
                fila.transform.Find("WyText").GetComponent<Text>().text = perfil.Wy.ToString("F1") + " cm³";
                fila.transform.Find("WplYText").GetComponent<Text>().text = perfil.Wpl_y.ToString("F1") + " cm³";

            }
            else
            {
                Debug.LogWarning("La fila no tiene suficientes columnas.");
            }
        }
    }
}
