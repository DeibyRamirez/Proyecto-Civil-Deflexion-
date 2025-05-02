using UnityEngine;
using UnityEngine.UI;

public class SeleccionarModeloBoton : MonoBehaviour
{
    public AdministradorPaginas administradorPaginas;
    public MostrarImagenes mostrarImagenes;

    public RenderTexture texturaSoporte;
    public RenderTexture texturaMuro;

public void SeleccionarSoporte()
{
    SeleccionModelo.modelo = "Modelo (Perlin Rectangular, Soportes)";
    ModeloSeleccionado.texturaModelo = texturaSoporte; // Guardamos la textura
    administradorPaginas.MostrarPagina(2);
    mostrarImagenes.CargarImagenes();
    
   

}

public void SeleccionarMuro()
{
    SeleccionModelo.modelo = "Modelo (Perlin Rectangular, Muro)";
    ModeloSeleccionado.texturaModelo = texturaMuro; // Guardamos la textura
    administradorPaginas.MostrarPagina(2);
    mostrarImagenes.CargarImagenes();
    
    

}
}