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
    private string modeloSeleccionado;

    void Start()
    {
        modeloSeleccionado = SeleccionModelo.modelo;
        Debug.Log($"Modelo seleccionado: {modeloSeleccionado}");

        // Inicializar botones de casos
        for (int i = 0; i < botonesCasos.Length; i++)
        {
            int caso = i + 1;
            botonesCasos[i].onClick.AddListener(() => SeleccionarCaso(caso));
        }

        // Verificar referencias
        if (materialManager == null)
            Debug.LogError("MaterialManager no asignado en el inspector");
        if (usosManager == null)
            Debug.LogError("UsosManager no asignado en el inspector");
    }

    void SeleccionarCaso(int caso)
    {
        casoSeleccionado = caso;
        Debug.Log($"Caso {casoSeleccionado} seleccionado");
    }

    public void CalcularDeflexion()
    {
        Debug.Log("=== INICIO DE CÁLCULO ===");

        // Validaciones iniciales
        if (!ValidarEntradas())
            return;

        // Obtener valores con verificaciones
        float L = ObtenerValor(inputL, "Longitud L");
        float P = ObtenerValor(inputP, "Carga P", true);
        float w = ObtenerValor(inputW, "Carga distribuida w", true);
        float M = ObtenerValor(inputM, "Momento M", true);

        // E en Pa (conversión desde MPa)
        float E = materialManager.moduloSeleccionado * 1_000_000f;
        Debug.Log($"Módulo de elasticidad E: {E} Pa");

        // Deflexión límite: a = L / uso
        float deflexionLimite = L / usosManager.moduloSeleccionado;
        Debug.Log($"Deflexión límite calculada: {deflexionLimite}m (L/{usosManager.moduloSeleccionado})");

        float I_m4 = 0f;
        float ymax = 0f;
        string formulaUsada = "";

        if (modeloSeleccionado == "Modelo (Perlin Rectangular, Muro)" || modeloSeleccionado == "Modelo (Perlin Cuadrado, Muro)" || modeloSeleccionado == "Modelo (Perlin Circular, Muro)" || modeloSeleccionado == "Modelo (Perlin H, Muro)" || modeloSeleccionado == "Modelo (Perlin I, Muro)")
        {
            Debug.Log("Modelo: Perlin Rectangular (Muro)");

            switch (casoSeleccionado)
            {
                case 1:
                    formulaUsada = "I = (P*L³)/(3*E*a) | ymax = (P*L³)/(3*E*I)";
                    I_m4 = SafeDivide((P * Mathf.Pow(L, 3)), (3 * E * deflexionLimite));
                    ymax = SafeDivide((P * Mathf.Pow(L, 3)), (3 * E * I_m4));
                    break;
                case 2:
                    formulaUsada = "I = (w*L⁴)/(8*E*a) | ymax = (w*L⁴)/(8*E*I)";
                    I_m4 = SafeDivide((w * Mathf.Pow(L, 4)), (8 * E * deflexionLimite));
                    ymax = SafeDivide((w * Mathf.Pow(L, 4)), (8 * E * I_m4));
                    break;
                case 3:
                    formulaUsada = "I = (M*L²)/(2*E*a) | ymax = (M*L²)/(2*E*I)";
                    I_m4 = SafeDivide((M * Mathf.Pow(L, 2)), (2 * E * deflexionLimite));
                    ymax = SafeDivide((M * Mathf.Pow(L, 2)), (2 * E * I_m4));
                    break;
                case 4:
                    formulaUsada = "I = (5*P*L³)/(48*E*a) | ymax = (5*P*L³)/(48*E*I)";
                    I_m4 = SafeDivide((5 * P * Mathf.Pow(L, 3)), (48 * E * deflexionLimite));
                    ymax = SafeDivide((5 * P * Mathf.Pow(L, 3)), (48 * E * I_m4));
                    break;
                default:
                    resultadoTexto.text = "Selecciona un caso válido.";
                    Debug.LogWarning("Caso no válido seleccionado");
                    return;
            }
        }
        else if (modeloSeleccionado == "Modelo (Perlin Rectangular, Soportes)" || modeloSeleccionado == "Modelo (Perlin Cuadrado, Soportes)" || modeloSeleccionado == "Modelo (Perlin Circular, Soportes)" || modeloSeleccionado == "Modelo (Perlin H, Soportes)" || modeloSeleccionado == "Modelo (Perlin I, Soportes)")
        {
            Debug.Log("Modelo: Perlin Rectangular (Soportes)");

            switch (casoSeleccionado)
            {
                case 1:
                    formulaUsada = "I = (P*L³)/(48*E*a) | ymax = (P*L³)/(48*E*I)";
                    I_m4 = SafeDivide((P * Mathf.Pow(L, 3)), (48 * E * deflexionLimite));
                    ymax = SafeDivide((P * Mathf.Pow(L, 3)), (48 * E * I_m4));
                    break;
                case 2:
                    formulaUsada = "I = (5*w*L⁴)/(384*E*a) | ymax = (5*w*L⁴)/(384*E*I)";
                    I_m4 = SafeDivide((5 * w * Mathf.Pow(L, 4)), (384 * E * deflexionLimite));
                    ymax = SafeDivide((5 * w * Mathf.Pow(L, 4)), (384 * E * I_m4));
                    break;
                case 3:
                    formulaUsada = "I = (M*L²)/(9*√3*E*a) | ymax = (M*L²)/(9*√3*E*I)";
                    I_m4 = SafeDivide((M * Mathf.Pow(L, 2)), (9 * Mathf.Sqrt(3) * E * deflexionLimite));
                    ymax = SafeDivide((M * Mathf.Pow(L, 2)), (9 * Mathf.Sqrt(3) * E * I_m4));
                    break;
                case 4:
                    formulaUsada = "I = 0.00652*w*L⁴/(E*a) | ymax = 0.00652*w*L⁴/(E*I)";
                    I_m4 = SafeDivide((0.00652f * w * Mathf.Pow(L, 4)), (E * deflexionLimite));
                    ymax = SafeDivide((0.00652f * w * Mathf.Pow(L, 4)), (E * I_m4));
                    break;
                default:
                    resultadoTexto.text = "Selecciona un caso válido.";
                    Debug.LogWarning("Caso no válido seleccionado");
                    return;
            }
        }
        else
        {
            resultadoTexto.text = "Modelo no válido.";
            Debug.LogError($"Modelo no válido: {modeloSeleccionado}");
            return;
        }

        Debug.Log($"Fórmula utilizada: {formulaUsada}");
        Debug.Log($"Resultados intermedios - I: {I_m4}m⁴, ymax: {ymax}m");

        // Conversión de momento de inercia de m^4 a cm^4 (1 m^4 = 10^8 cm^4)
        float I_cm4 = I_m4 * 100_000_000f;

        resultadoTexto.text = $"Deflexión máxima: {ymax:F4} m\nMomento de inercia I: {I_cm4:F4} cm⁴";
        resultadoDeflexionLimiteTexto.text = $"Deflexión límite: {deflexionLimite:F4} m";

        if (float.IsNaN(ymax) || float.IsInfinity(ymax))
        {
            Debug.LogError("Resultado inválido (NaN o Infinito) - Revise las entradas");
            resultadoTexto.text += "\nERROR: Resultado no válido - Revise los valores de entrada";
        }
        else if (Mathf.Abs(ymax) > deflexionLimite)
        {
            resultadoTexto.text += "\n¡Atención! La deflexión máxima excede el límite permitido.";
            Debug.LogWarning("Deflexión excede el límite permitido");
        }
        else
        {
            resultadoTexto.text += "\nLa deflexión máxima está dentro del límite permitido.";
            Debug.Log("Deflexión dentro de límites aceptables");
        }

        Debug.Log("=== FIN DE CÁLCULO ===");
        
    }

    private bool ValidarEntradas()
    {
        if (materialManager == null)
        {
            Debug.LogError("MaterialManager no asignado");
            resultadoTexto.text = "Error: Falta configurar materiales";
            return false;
        }

        if (usosManager == null)
        {
            Debug.LogError("UsosManager no asignado");
            resultadoTexto.text = "Error: Falta configurar usos";
            return false;
        }

        if (Mathf.Approximately(materialManager.moduloSeleccionado, 0f))
        {
            Debug.LogError("No se ha seleccionado material o módulo es 0");
            resultadoTexto.text = "Error: Selecciona un material válido";
            return false;
        }

        if (Mathf.Approximately(usosManager.moduloSeleccionado, 0f))
        {
            Debug.LogError("No se ha seleccionado uso o límite es 0");
            resultadoTexto.text = "Error: Selecciona un uso válido";
            return false;
        }

        if (string.IsNullOrEmpty(inputL.text))
        {
            Debug.LogError("Longitud L no especificada");
            resultadoTexto.text = "Error: Ingresa la longitud L";
            return false;
        }

        return true;
    }

    private float ObtenerValor(TMP_InputField field, string nombre, bool opcional = false)
    {
        if (string.IsNullOrEmpty(field.text))
        {
            if (!opcional)
            {
                Debug.LogError($"{nombre} no especificado");
                resultadoTexto.text = $"Error: {nombre} requerido";
            }
            return 0f;
        }

        if (!float.TryParse(field.text, out float valor))
        {
            Debug.LogError($"{nombre} no es un número válido");
            resultadoTexto.text = $"Error: {nombre} inválido";
            return 0f;
        }

        return valor;
    }

    private float SafeDivide(float numerator, float denominator)
    {
        if (Mathf.Approximately(denominator, 0f))
        {
            Debug.LogWarning("División por cero evitada, retornando 0");
            return 0f;
        }
        return numerator / denominator;
    }
}