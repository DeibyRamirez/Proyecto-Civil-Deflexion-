using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class UsosData
{
    public string nombre;
    public float deflexionLimite;
}

public class UsosManager : MonoBehaviour
{
    public List<UsosData> usos = new List<UsosData>();
    public TMP_Dropdown dropdown;
    public float moduloSeleccionado;

    void Start()
    {
        Inicializar();
        // Forzar selección inicial
        if (dropdown.options.Count > 0)
        {
            CambiarUso(dropdown.value);
        }
    }
    
    public void Inicializar()
    {
        usos.Clear();
        usos.Add(new UsosData { nombre = "Voladizos", deflexionLimite = 180f });
        usos.Add(new UsosData { nombre = "Vigas con yeso", deflexionLimite = 360f });
        usos.Add(new UsosData { nombre = "Solo carga muerta", deflexionLimite = 200f });
        usos.Add(new UsosData { nombre = "Combinación crítica", deflexionLimite = 100f });
        usos.Add(new UsosData { nombre = "Vidrio simple", deflexionLimite = 175f });
        usos.Add(new UsosData { nombre = "Vidrio doble", deflexionLimite = 250f });
        usos.Add(new UsosData { nombre = "Cima de columnas", deflexionLimite = 300f });

        dropdown.ClearOptions();
        List<string> nombres = new List<string>();
        foreach (UsosData uso in usos)
        {
            nombres.Add(uso.nombre);
        }
        dropdown.AddOptions(nombres);

        dropdown.onValueChanged.RemoveAllListeners();
        dropdown.onValueChanged.AddListener(CambiarUso);

        Debug.Log("UsosManager inicializado correctamente");
    }

    void CambiarUso(int index)
    {   
        if (index < 0 || index >= usos.Count)
        {
            Debug.LogError("Índice de uso fuera de rango");
            return;
        }

        moduloSeleccionado = usos[index].deflexionLimite;
        Debug.Log($"Uso cambiado a: {usos[index].nombre}, Límite: L/{moduloSeleccionado}");
    }
}