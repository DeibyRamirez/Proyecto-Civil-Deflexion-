using UnityEngine;
using System.Collections.Generic;

public class ModelViewer : MonoBehaviour
{
    [Header("Lista de modelos 3D con Animator")]
    public List<GameObject> modelos;

    [Header("Lista de modelos sin animación")]
    public List<GameObject> modelos_normales;

    [Header("Referencia al script que carga los sprites")]
    public MostrarImagenes mostrarImagenes;

    [Header("Imagen seleccionada desde botón")]
    public UnityEngine.UI.Image imagenSeleccionada;

    private int animacionActual = 1;
    private bool animacionReproduciendose = false;

    private void Start()
    {
        OcultarModelosAnimados();
        MostrarModelosNormales();
        PausarTodosLosModelos();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) ToggleAnimacion();
        if (Input.GetKeyDown(KeyCode.R)) RetornarAVistaNormal();
    }

    /// <summary>
    /// Detecta tipo y sprite y selecciona la animación correcta
    /// </summary>
    public void VisualizarAnimacionSegunSeleccion()
    {
        if (mostrarImagenes == null || imagenSeleccionada == null)
        {
            Debug.LogWarning("Faltan referencias a MostrarImagenes o imagenSeleccionada.");
            return;
        }

        string modelo = SeleccionModelo.modelo;
        Sprite sprite = imagenSeleccionada.sprite;

        if (modelo == null || sprite == null)
        {
            Debug.LogWarning("Modelo o imagen seleccionada no asignados.");
            return;
        }

        string tipo = modelo.Contains("Soportes") ? "Soportes" : "Muro";
        int spriteIndex = -1;

        Sprite[] spritesArray = tipo == "Soportes" ? mostrarImagenes.soporteSprites : mostrarImagenes.muroSprites;

        for (int i = 0; i < spritesArray.Length; i++)
        {
            if (spritesArray[i] == sprite)
            {
                spriteIndex = i;
                break;
            }
        }

        if (spriteIndex == -1)
        {
            Debug.LogWarning("No se encontró el índice del sprite seleccionado.");
            return;
        }

        // Selección de animación según la tabla que mencionaste
        if (tipo == "Muro")
        {
            if (spriteIndex == 0 || spriteIndex == 1) animacionActual = 1;
            else if (spriteIndex == 2) animacionActual = 3;
            else if (spriteIndex == 3) animacionActual = 2;
        }
        else // Soportes
        {
            if (spriteIndex == 0 || spriteIndex == 1) animacionActual = 1;
            else if (spriteIndex == 2) animacionActual = 3;
            else if (spriteIndex == 3) animacionActual = 2;
        }

        Debug.Log($"Tipo: {tipo}, SpriteIndex: {spriteIndex}, Animación: {animacionActual}");

        ReproducirAnimacion(animacionActual);
    }

    public void ReproducirAnimacion(int numero)
    {
        MostrarModelosAnimados();

        foreach (GameObject modelo in modelos)
        {
            Animator anim = modelo.GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetInteger("Animacion-index", numero);
                anim.speed = 1;
            }
        }

        animacionReproduciendose = true;
        Debug.Log("Animación activada en todos los modelos: " + numero);
    }

    public void PausarAnimacion()
    {
        foreach (GameObject modelo in modelos)
        {
            Animator anim = modelo.GetComponent<Animator>();
            if (anim != null)
            {
                anim.speed = 0;
            }
        }

        animacionReproduciendose = false;
        Debug.Log("Animación pausada.");
    }

    public void ToggleAnimacion()
    {
        if (animacionReproduciendose)
        {
            PausarAnimacion();
        }
        else
        {
            VisualizarAnimacionSegunSeleccion();
        }
    }

    public void RetornarAVistaNormal()
    {
        MostrarModelosNormales();
        PausarTodosLosModelos();
        animacionReproduciendose = false;
        Debug.Log("Vista normal restaurada.");
    }

    private void MostrarModelosAnimados()
    {
        foreach (GameObject modelo in modelos)
            modelo.SetActive(true);

        foreach (GameObject normal in modelos_normales)
            normal.SetActive(false);
    }

    private void MostrarModelosNormales()
    {
        foreach (GameObject modelo in modelos)
            modelo.SetActive(false);

        foreach (GameObject normal in modelos_normales)
            normal.SetActive(true);
    }

    private void OcultarModelosAnimados()
    {
        foreach (GameObject modelo in modelos)
            modelo.SetActive(false);
    }

    private void PausarTodosLosModelos()
    {
        foreach (GameObject modelo in modelos)
        {
            Animator anim = modelo.GetComponent<Animator>();
            if (anim != null)
            {
                anim.speed = 0;
                anim.SetInteger("Animacion-index", 0);
            }
        }
    }
}
