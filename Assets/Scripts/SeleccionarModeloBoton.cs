using UnityEngine;

public class SeleccionarModeloBoton : MonoBehaviour
{
    public AdministradorPaginas administradorPaginas;
    public MostrarImagenes mostrarImagenes;

    public RenderTexture texturaSoporte;
    public RenderTexture texturaMuro;
    

    public void SeleccionarSoporte()
    {
        string nombre = TiposPerlin.nombrePerlinSeleccionado;
        SeleccionModelo.modelo = $"Modelo (Perlin {nombre}, Soportes)";

        if (GestorTexturasPerlin.soporteDict.TryGetValue(nombre, out var textura))
        {
            ModeloSeleccionado.texturaModelo = textura;
        }
        

        administradorPaginas.MostrarPagina(2);
        mostrarImagenes.CargarImagenes();
    }

    public void SeleccionarMuro()
    {
        string nombre = TiposPerlin.nombrePerlinSeleccionado;
        SeleccionModelo.modelo = $"Modelo (Perlin {nombre}, Muro)";

        if (GestorTexturasPerlin.muroDict.TryGetValue(nombre, out var textura))
        {
            ModeloSeleccionado.texturaModelo = textura;
        }

        administradorPaginas.MostrarPagina(2);
        mostrarImagenes.CargarImagenes();
    }

}
