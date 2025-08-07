
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Alerta : MonoBehaviour
{
    public Image panelAlerta;
    public TextMeshProUGUI textMensaje;

    public void MostrarAlerta(string mensaje)
    {
        textMensaje.text = "Faltan datos requeridos por el caso seleccionado.";
        panelAlerta.gameObject.SetActive(true);

    }

    public void OcultarAlerta()
    {
        panelAlerta.gameObject.SetActive(false);
    }
}
