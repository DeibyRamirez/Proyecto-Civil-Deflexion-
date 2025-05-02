using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FormulasDeflexion : MonoBehaviour
{
    public TMP_InputField inputL, inputP, inputW, inputI, inputM;
    public Button[] botonesCasos; // Array de botones (4 botones para cada modelo)
    public TMP_Text resultadoTexto;
    public TMP_Text resultadoDeflexionLimiteTexto; 

    private int casoSeleccionado; // Valor del caso seleccionado

    [Header("Referencias externas")]
    public MaterialManager materialManager; // Referencia al script MaterialManager
    public UsosManager usosManager; // Referencia al script UsosManager

    void Start()
    {
        // Asignar un listener a cada botón para capturar el caso seleccionado
        for (int i = 0; i < botonesCasos.Length; i++)
        {
            int caso = i + 1; // Caso 1 al 4
            botonesCasos[i].onClick.AddListener(() => SeleccionarCaso(caso));
        }
    }

    void SeleccionarCaso(int caso)
    {
        casoSeleccionado = caso; // Guardar el caso seleccionado
    }

    public void CalcularDeflexion()
    {
        float L = float.Parse(inputL.text);
        float P = string.IsNullOrEmpty(inputP.text) ? 0 : float.Parse(inputP.text);
        float w = string.IsNullOrEmpty(inputW.text) ? 0 : float.Parse(inputW.text);
        float E = materialManager.moduloSeleccionado; // Obtener el módulo de elasticidad del material seleccionado
        float I = float.Parse(inputI.text);
        float M = string.IsNullOrEmpty(inputM.text) ? 0 : float.Parse(inputM.text);

        float ymax = 0f;

        // Determinar el modelo actual y ejecutar el switch correspondiente
        if (SeleccionModelo.modelo == "Modelo (Perlin Rectangular, Muro)")
        {
            switch (casoSeleccionado)
            {
                case 1:
                    ymax = -(P * Mathf.Pow(L, 3)) / (3 * E * I);
                    break;
                case 2:
                    ymax = -(w * Mathf.Pow(L, 4)) / (8 * E * I);
                    break;
                case 3:
                    ymax = -(M * Mathf.Pow(L, 2)) / (2 * E * I);
                    break;
                case 4:
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
                    ymax = -(P * Mathf.Pow(L, 3)) / (48 * E * I);
                    break;
                case 2:
                    ymax = -(5 * w * Mathf.Pow(L, 4)) / (384 * E * I);
                    break;
                case 3:
                    ymax = -(M * Mathf.Pow(L, 2)) / (9 * Mathf.Sqrt(3) * E * I);
                    break;
                case 4:
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

        resultadoTexto.text = $"Deflexión máxima: {ymax:F4} m";

        float deflexionLimite = L / usosManager.moduloSeleccionado; // Deflexión límite según el uso seleccionado
        resultadoDeflexionLimiteTexto.text = $"Deflexión límite: {deflexionLimite:F4} m";

        if (Mathf.Abs(ymax) > deflexionLimite)
        {
            resultadoTexto.text += "\n¡Atención! La deflexión máxima excede el límite permitido.";
        }
        else
        {
            resultadoTexto.text += "\nLa deflexión máxima está dentro del límite permitido.";
        }
        {
            
        }
    }
}
