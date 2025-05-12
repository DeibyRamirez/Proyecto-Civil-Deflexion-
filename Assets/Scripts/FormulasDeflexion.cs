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

        // E en Pa (conversión desde MPa)
        float E = materialManager.moduloSeleccionado * 1_000_000f;

        // Deflexión límite: a = L / uso
        float a = L / usosManager.moduloSeleccionado;

        float I_m4 = 0f;
        float ymax = 0f;

        if (SeleccionModelo.modelo == "Modelo (Perlin Rectangular, Muro)")
        {
            switch (casoSeleccionado)
            {
                case 1:
                    I_m4 = (P * Mathf.Pow(L, 3)) / (3 * E * a);
                    ymax = (P * Mathf.Pow(L, 3)) / (3 * E * I_m4);
                    break;
                case 2:
                    I_m4 = (w * Mathf.Pow(L, 4)) / (8 * E * a);
                    ymax = (w * Mathf.Pow(L, 4)) / (8 * E * I_m4);
                    break;
                case 3:
                    I_m4 = (M * Mathf.Pow(L, 2)) / (2 * E * a);
                    ymax = (M * Mathf.Pow(L, 2)) / (2 * E * I_m4);
                    break;
                case 4:
                    I_m4 = (5 * P * Mathf.Pow(L, 3)) / (48 * E * a);
                    ymax = (5 * P * Mathf.Pow(L, 3)) / (48 * E * I_m4);
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
                    I_m4 = (P * Mathf.Pow(L, 3)) / (48 * E * a);
                    ymax = (P * Mathf.Pow(L, 3)) / (48 * E * I_m4);
                    break;
                case 2:
                    I_m4 = (5 * w * Mathf.Pow(L, 4)) / (384 * E * a);
                    ymax = (5 * w * Mathf.Pow(L, 4)) / (384 * E * I_m4);
                    break;
                case 3:
                    I_m4 = (M * Mathf.Pow(L, 2)) / (9 * Mathf.Sqrt(3) * E * a);
                    ymax = (M * Mathf.Pow(L, 2)) / (9 * Mathf.Sqrt(3) * E * I_m4);
                    break;
                case 4:
                    I_m4 = 0.00652f * w * Mathf.Pow(L, 4) / (E * a);
                    ymax = 0.00652f * w * Mathf.Pow(L, 4) / (E * I_m4);
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

        // Conversión de momento de inercia de m^4 a cm^4 (1 m^4 = 10^8 cm^4)
        float I_cm4 = I_m4 * 100_000_000f;

        resultadoTexto.text = $"Deflexión máxima: {ymax:F4} m\nMomento de inercia I: {I_cm4:F4} cm⁴";

        float deflexionLimite = a;
        resultadoDeflexionLimiteTexto.text = $"Deflexión límite: {deflexionLimite:F4} m";

        if (Mathf.Abs(ymax) > deflexionLimite)
            resultadoTexto.text += "\n¡Atención! La deflexión máxima excede el límite permitido.";
        else
            resultadoTexto.text += "\nLa deflexión máxima está dentro del límite permitido.";
    }
}
