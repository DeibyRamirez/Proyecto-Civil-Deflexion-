using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Objeto : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI codigoObjeto;
    [SerializeField] private TextMeshProUGUI iyObjeto;
    [SerializeField] private TextMeshProUGUI wyObjeto;
    [SerializeField] private TextMeshProUGUI wpl_yObjeto;

    private PlantillaObjeto datosObjeto;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(SeleccionarObjeto);
    }

    public void CrearObjeto(PlantillaObjeto datos)
    {
        datosObjeto = datos;
        codigoObjeto.text = "Ref: " + datos.codigo;
        iyObjeto.text = "Iy: " + datos.iy;
        wyObjeto.text = "Wy: " + datos.wy;
        wpl_yObjeto.text = "Wpl: " + datos.wpl_y;
    }

    private void SeleccionarObjeto()
    {
        if(datosObjeto == null) return;
        
        DatosSeleccionados.Codigo = datosObjeto.codigo;
        DatosSeleccionados.Iy = datosObjeto.iy;
        DatosSeleccionados.Wy = datosObjeto.wy;
        DatosSeleccionados.Wpl_y = datosObjeto.wpl_y;
        
        // Corregido método obsoleto:
        var admin = FindFirstObjectByType<AdministradorPaginas>();
        if(admin != null) admin.MostrarPagina(5);
        else Debug.LogError("No se encontró AdministradorPaginas");
    }
}