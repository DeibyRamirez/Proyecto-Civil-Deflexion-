using TMPro;
using UnityEngine;

public class Objeto : MonoBehaviour
{
    
    [SerializeField] TextMeshProUGUI codigoObjeto;
    [SerializeField] TextMeshProUGUI iyObjeto;
    [SerializeField] TextMeshProUGUI wyObjeto;
    [SerializeField] TextMeshProUGUI wpl_yObjeto;



    public void CrearObjeto(PlantillaObjeto datosObjeto)
    {

        codigoObjeto.text = "Referencia :" + datosObjeto.codigo;
        iyObjeto.text = "Iy :" + datosObjeto.iy;
        wyObjeto.text = "Wy :" + datosObjeto.wy;
        wpl_yObjeto.text = "Wpl_y :" + datosObjeto.wpl_y;
    }
    
}
