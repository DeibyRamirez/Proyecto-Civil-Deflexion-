using UnityEngine;

public class AdministradorPaginas : MonoBehaviour
{
    public GameObject[] paginas; // Ahora el array se asigna manualmente en el Inspector
    private int paginaActual = 0;

    void Start()
    {
        if (paginas.Length == 0)
        {
            Debug.LogError("🚨 ERROR: No se han asignado las páginas en el array.");
            return;
        }
        MostrarPagina(paginaActual);
    }

    public void MostrarPagina(int pagina)
    {
        if (pagina < 0 || pagina >= paginas.Length)
        {
            Debug.LogError("🚨 ERROR: Índice de página fuera de rango.");
            return;
        }

        for (int i = 0; i < paginas.Length; i++)
        {
            paginas[i].SetActive(i == pagina);
        }
        paginaActual = pagina;
    }

    public void SiguientePagina()
    {
        paginaActual = (paginaActual + 1) % paginas.Length;
        MostrarPagina(paginaActual);
    }

    public void AnteriorPagina()
    {
        paginaActual = (paginaActual - 1 + paginas.Length) % paginas.Length;
        MostrarPagina(paginaActual);
    }

    
}