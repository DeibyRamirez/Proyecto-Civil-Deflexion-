using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro

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
}


    void CambiarUso(int index)
    {   
        Debug.Log("Seleccionaste: " + usos[index].nombre);
        // Aquí podrías usar usos[index].deflexionLimite si quieres
        moduloSeleccionado = usos[index].deflexionLimite;
    }


    
}
