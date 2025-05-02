using UnityEngine;
using UnityEngine.UI;

public class SeleccionarImagenBoton : MonoBehaviour
{
    public Image imagenAsociada;

    public AdministradorPaginas adminPaginas;
    public int indicePaginaDatos;

    public void Seleccionar()
{
    if (imagenAsociada != null)
    {
        ImagenSeleccionada.imagenSeleccionada = imagenAsociada;
        
        adminPaginas.MostrarPagina(indicePaginaDatos);

        // Llamar manualmente al script en la página nueva
        GameObject paginaDestino = adminPaginas.paginas[indicePaginaDatos];
        MostrarImagenSeleccionada script = paginaDestino.GetComponentInChildren<MostrarImagenSeleccionada>();

        if (script != null)
        {
            script.ActualizarImagen();
        }
    }
    else
    {
        Debug.LogError("Imagen asociada no asignada en el botón: " + gameObject.name);
    }
}

}
