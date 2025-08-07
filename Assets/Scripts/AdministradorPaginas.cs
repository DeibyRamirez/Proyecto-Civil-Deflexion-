using UnityEngine;
using System.Collections; // Necesario para usar IEnumerator

public class AdministradorPaginas : MonoBehaviour
{
    public GameObject[] paginas; // Se asigna manualmente en el Inspector
    private int paginaActual = 6;

    void Start()
    {
        if (paginas.Length == 0)
        {
            Debug.LogError("🚨 ERROR: No se han asignado las páginas en el array.");
            return;
        }

        MostrarPagina(paginaActual);
        StartCoroutine(CambiarPaginaDespuesDeTiempo(5f)); // Espera 5 segundos y cambia a la página 1
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

    // Corutina para cambiar de página después de cierto tiempo
    private IEnumerator CambiarPaginaDespuesDeTiempo(float segundos)
    {
        yield return new WaitForSeconds(segundos);
        MostrarPagina(0); // Cambia a la página con índice 1
    }
}
