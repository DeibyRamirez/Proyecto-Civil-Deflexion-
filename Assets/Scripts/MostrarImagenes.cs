using UnityEngine;
using UnityEngine.UI;

public class MostrarImagenes : MonoBehaviour
{
    public Image[] contenedoresImagenes;
    public Sprite[] soporteSprites;
    public Sprite[] muroSprites;

    public GameObject[] botones; // Los botones deben tener el script SeleccionarImagenBoton
    public AdministradorPaginas adminPaginas;

    void Start()
    {
        CargarImagenes(); // ✅ Aquí llamamos al método al iniciar el objeto
    }

    public void CargarImagenes()
    {
        Sprite[] sprites;

        if (SeleccionModelo.modelo == "Modelo (Perlin Rectangular, Soportes)" || SeleccionModelo.modelo == "Modelo (Perlin Cuadrado, Soportes)" || SeleccionModelo.modelo == "Modelo (Perlin Circular, Soportes)" || SeleccionModelo.modelo == "Modelo (Perlin H, Soportes)" || SeleccionModelo.modelo == "Modelo (Perlin I, Soportes)")
        {
            sprites = soporteSprites;
        }
        else if (SeleccionModelo.modelo == "Modelo (Perlin Rectangular, Muro)" || SeleccionModelo.modelo == "Modelo (Perlin Cuadrado, Muro)" || SeleccionModelo.modelo == "Modelo (Perlin Circular, Muro)" || SeleccionModelo.modelo == "Modelo (Perlin H, Muro)" || SeleccionModelo.modelo == "Modelo (Perlin I, Muro)" )
        {
            sprites = muroSprites;
        }
        else
        {
            Debug.LogError("Modelo no reconocido: " + SeleccionModelo.modelo);
            return;
        }

        for (int i = 0; i < contenedoresImagenes.Length; i++)
        {
            if (i < sprites.Length)
            {
                contenedoresImagenes[i].sprite = sprites[i];

                SeleccionarImagenBoton script = botones[i].GetComponent<SeleccionarImagenBoton>();
                script.imagenAsociada = contenedoresImagenes[i]; // ✅ Aquí va la imagen, no el sprite
                script.adminPaginas = adminPaginas;
                script.indicePaginaDatos = 3;
            }
        }
    }
}
