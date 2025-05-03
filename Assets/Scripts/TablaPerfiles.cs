using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TablaPerfiles : MonoBehaviour {
    public GameObject filaPrefab;
    public Transform contenedorTabla;

    private List<PerfilEstructural> perfiles = new List<PerfilEstructural>() {
        new PerfilEstructural { nombre = "IPE 100", altura = 100f, anchoAla = 55f, area = 8.1f, Iy = 135f, Wy = 27.1f, Wpl_y = 29.3f },
        new PerfilEstructural { nombre = "IPE 200", altura = 200f, anchoAla = 100f, area = 21.4f, Iy = 858f, Wy = 85.8f, Wpl_y = 98.2f },
        new PerfilEstructural { nombre = "IPE 300", altura = 300f, anchoAla = 150f, area = 45.9f, Iy = 4680f, Wy = 312f, Wpl_y = 356f }
        // Agrega más perfiles si los necesitas
    };

    void Start() {
        foreach (var perfil in perfiles) {
            GameObject fila = Instantiate(filaPrefab, contenedorTabla);
            Text[] columnas = fila.GetComponentsInChildren<Text>();
            columnas[0].text = perfil.nombre;
            columnas[1].text = perfil.altura.ToString("F1");
            columnas[2].text = perfil.anchoAla.ToString("F1");
            columnas[3].text = perfil.area.ToString("F2");
            columnas[4].text = perfil.Iy.ToString("F1");
            columnas[5].text = perfil.Wy.ToString("F1");
            columnas[6].text = perfil.Wpl_y.ToString("F1");
        }
    }
}
