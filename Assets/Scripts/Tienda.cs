using UnityEngine;

public class Tienda : MonoBehaviour
{
    [SerializeField] GameObject prefabObjetoTienda;
    
    [SerializeField] PlantillaObjeto[] listaRectangular;
    [SerializeField] PlantillaObjeto[] listaCuadrada;
    [SerializeField] PlantillaObjeto[] listaCircular;
    [SerializeField] PlantillaObjeto[] listaH;
    [SerializeField] PlantillaObjeto[] listaI;

    private GameObject parent;
    private GameObject[] objetosTiendaActuales;

    private void Awake()
    {
        parent = GameObject.FindGameObjectWithTag("Perlines");
        if (parent == null)
        {
            Debug.LogError("No se encontró objeto con tag 'Perlines'");
        }
    }

    public void ActualizarTienda()
    {
        // Limpiar objetos de tienda anteriores
        LimpiarTiendaActual();

        PlantillaObjeto[] listaSeleccionada = ObtenerListaSegunModelo();

        if (listaSeleccionada == null || listaSeleccionada.Length == 0)
        {
            Debug.LogError("Lista de tienda vacía o modelo no reconocido");
            return;
        }

        objetosTiendaActuales = new GameObject[listaSeleccionada.Length];

        for (int i = 0; i < listaSeleccionada.Length; i++)
        {
            GameObject tienda = Instantiate(prefabObjetoTienda, Vector2.zero, Quaternion.identity, parent.transform);
            Objeto objeto = tienda.GetComponent<Objeto>();

            if (objeto != null)
            {
                objeto.CrearObjeto(listaSeleccionada[i]);
                objetosTiendaActuales[i] = tienda;
            }
            else
            {
                Debug.LogError("Prefab no tiene componente Objeto");
            }
        }
    }

    private void LimpiarTiendaActual()
    {
        if (objetosTiendaActuales != null && objetosTiendaActuales.Length > 0)
        {
            foreach (GameObject obj in objetosTiendaActuales)
            {
                if (obj != null)
                {
                    Destroy(obj);
                }
            }
        }
    }

    private PlantillaObjeto[] ObtenerListaSegunModelo()
    {
        string modelo = TiposPerlin.nombrePerlinSeleccionado;

        switch (modelo)
        {
            case "Rectangular":
                return listaRectangular;
            case "Cuadrado":
                return listaCuadrada;
            case "Circular":
                return listaCircular;
            case "H":
                return listaH;
            case "I":
                return listaI;
            default:
                Debug.LogWarning("Modelo no reconocido: " + modelo);
                return null;
        }
    }
}