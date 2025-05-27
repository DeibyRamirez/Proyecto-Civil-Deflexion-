using UnityEngine;
using System.Collections.Generic;

public class Tienda : MonoBehaviour
{
    [SerializeField] GameObject prefabObjetoTienda;
    
    [SerializeField] List<PlantillaObjeto> listaRectangular;
    [SerializeField] List<PlantillaObjeto> listaCuadrada;
    [SerializeField] List<PlantillaObjeto> listaCircular;
    [SerializeField] List<PlantillaObjeto> listaH;
    [SerializeField] List<PlantillaObjeto> listaI;

    private GameObject parent;
    private List<GameObject> objetosTiendaActuales = new List<GameObject>();

    private void Awake()
    {
        parent = GameObject.FindGameObjectWithTag("Perlines");
    }

    public void ActualizarTienda()
    {
        LimpiarTiendaActual();

        // Obtener el I requerido desde FormulasDeflexion
        float iRequerido = DatosCompartidosTabla.MomentoInerciaRequerido;
        
        // Obtener la lista CORRECTA según el modelo seleccionado
        List<PlantillaObjeto> listaFiltrada = FiltrarObjetos(iRequerido);

        if (listaFiltrada.Count == 0)
        {
            Debug.LogWarning($"No hay objetos con Iy >= {iRequerido} cm⁴");
            return;
        }

        // Mostrar solo los objetos filtrados
        foreach (var objeto in listaFiltrada)
        {
            GameObject nuevoObjeto = Instantiate(prefabObjetoTienda, parent.transform);
            nuevoObjeto.GetComponent<Objeto>().CrearObjeto(objeto);
            objetosTiendaActuales.Add(nuevoObjeto);
        }
    }

    private List<PlantillaObjeto> FiltrarObjetos(float iRequerido)
    {
        // 1. Seleccionar la lista correcta según el modelo
        List<PlantillaObjeto> listaSeleccionada;
        
        switch (TiposPerlin.nombrePerlinSeleccionado)
        {
            case "Rectangular": listaSeleccionada = listaRectangular; break;
            case "Cuadrado":   listaSeleccionada = listaCuadrada;   break;
            case "Circular":    listaSeleccionada = listaCircular;   break;
            case "H":           listaSeleccionada = listaH;          break;
            case "I":           listaSeleccionada = listaI;          break;
            default:            return new List<PlantillaObjeto>();  // Lista vacía si no hay modelo
        }

        // 2. Filtrar SOLO los objetos con Iy >= iRequerido
        List<PlantillaObjeto> listaFiltrada = new List<PlantillaObjeto>();

        foreach (var obj in listaSeleccionada)
        {
            // Extraer el número de la cadena "iy" (ejemplo: "12.5 cm⁴" → 12.5f)
            float iyObjeto = ExtraerNumero(obj.iy);

            if (iyObjeto >= iRequerido)
            {
                listaFiltrada.Add(obj);
            }
        }

        return listaFiltrada;
    }

    // Método para extraer el valor numérico de "iy" (ejemplo: "8.25 cm⁴" → 8.25f)
    private float ExtraerNumero(string texto)
    {
        string numeroStr = System.Text.RegularExpressions.Regex.Match(texto, @"[\d\.]+").Value;
        return float.TryParse(numeroStr, out float numero) ? numero : 0f;
    }

    private void LimpiarTiendaActual()
    {
        foreach (var obj in objetosTiendaActuales)
        {
            if (obj != null) Destroy(obj);
        }
        objetosTiendaActuales.Clear();
    }
}