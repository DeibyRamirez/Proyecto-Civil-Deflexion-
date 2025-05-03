using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FormulasDeflexion : MonoBehaviour
{
    public TMP_InputField inputL, inputP, inputW, inputM;
    public Button[] botonesCasos;
    public TMP_Text resultadoTexto;
    public TMP_Text resultadoDeflexionLimiteTexto;

    [Header("Referencias externas")]
    public MaterialManager materialManager;
    public UsosManager usosManager;

    private int casoSeleccionado;

    void Start()
    {
        for (int i = 0; i < botonesCasos.Length; i++)
        {
            int caso = i + 1;
            botonesCasos[i].onClick.AddListener(() => SeleccionarCaso(caso));
        }
    }

    void SeleccionarCaso(int caso)
    {
        casoSeleccionado = caso;
    }

    public void CalcularDeflexion()
    {
        float L = float.Parse(inputL.text);
        float P = string.IsNullOrEmpty(inputP.text) ? 0 : float.Parse(inputP.text);
        float w = string.IsNullOrEmpty(inputW.text) ? 0 : float.Parse(inputW.text);
        float M = string.IsNullOrEmpty(inputM.text) ? 0 : float.Parse(inputM.text);

        float E = materialManager.moduloSeleccionado;
        float a = L / usosManager.moduloSeleccionado;

        float I = 0f;
        float ymax = 0f;

        if (SeleccionModelo.modelo == "Modelo (Perlin Rectangular, Muro)")
        {
            switch (casoSeleccionado)
            {
                case 1:
                    I = -(P * Mathf.Pow(L, 3)) / (3 * E * a);
                    ymax = -(P * Mathf.Pow(L, 3)) / (3 * E * I);
                    break;
                case 2:
                    I = -(w * Mathf.Pow(L, 4)) / (8 * E * a);
                    ymax = -(w * Mathf.Pow(L, 4)) / (8 * E * I);
                    break;
                case 3:
                    I = -(M * Mathf.Pow(L, 2)) / (2 * E * a);
                    ymax = -(M * Mathf.Pow(L, 2)) / (2 * E * I);
                    break;
                case 4:
                    I = -(5 * P * Mathf.Pow(L, 3)) / (48 * E * a);
                    ymax = -(5 * P * Mathf.Pow(L, 3)) / (48 * E * I);
                    break;
                default:
                    resultadoTexto.text = "Selecciona un caso válido.";
                    return;
            }
        }
        else if (SeleccionModelo.modelo == "Modelo (Perlin Rectangular, Soportes)")
        {
            switch (casoSeleccionado)
            {
                case 1:
                    I = -(P * Mathf.Pow(L, 3)) / (48 * E * a);
                    ymax = -(P * Mathf.Pow(L, 3)) / (48 * E * I);
                    break;
                case 2:
                    I = -(5 * w * Mathf.Pow(L, 4)) / (384 * E * a);
                    ymax = -(5 * w * Mathf.Pow(L, 4)) / (384 * E * I);
                    break;
                case 3:
                    I = -(M * Mathf.Pow(L, 2)) / (9 * Mathf.Sqrt(3) * E * a);
                    ymax = -(M * Mathf.Pow(L, 2)) / (9 * Mathf.Sqrt(3) * E * I);
                    break;
                case 4:
                    I = -0.00652f * w * Mathf.Pow(L, 4) / (E * a);
                    ymax = -0.00652f * w * Mathf.Pow(L, 4) / (E * I);
                    break;
                default:
                    resultadoTexto.text = "Selecciona un caso válido.";
                    return;
            }
        }
        else
        {
            resultadoTexto.text = "Modelo no válido.";
            return;
        }

        resultadoTexto.text = $"Deflexión máxima: {ymax:F4} m\nMomento de inercia I: {I:F4} cm⁴";

        float deflexionLimite = a; // ya fue calculado como L / uso
        resultadoDeflexionLimiteTexto.text = $"Deflexión límite: {deflexionLimite:F4} m";

        if (Mathf.Abs(ymax) > deflexionLimite)
            resultadoTexto.text += "\n¡Atención! La deflexión máxima excede el límite permitido.";
        else
            resultadoTexto.text += "\nLa deflexión máxima está dentro del límite permitido.";
    }
}
