using TMPro;
using UnityEngine;

public class AnimacionController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI txtCodigo;
    [SerializeField] private TextMeshProUGUI txtIy;
    [SerializeField] private TextMeshProUGUI txtWy;
    [SerializeField] private TextMeshProUGUI txtWpl;

    private void OnEnable()
    {
        DatosSeleccionados.OnDatosActualizados += ActualizarUI;
        ActualizarUI(); // Forzar actualización al activarse
    }

    private void OnDisable()
    {
        DatosSeleccionados.OnDatosActualizados -= ActualizarUI;
    }

    private void ActualizarUI()
    {
        try
        {
            txtCodigo.text = $"Ref: {DatosSeleccionados.Codigo ?? "N/A"}";
            txtIy.text = $"Iy: {DatosSeleccionados.Iy ?? "0"} cm⁴";
            txtWy.text = $"Wy: {DatosSeleccionados.Wy ?? "0"} cm³";
            txtWpl.text = $"Wpl: {DatosSeleccionados.Wpl_y ?? "0"} cm³";
            
            Debug.Log("UI actualizada correctamente");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error al actualizar UI: {e.Message}");
        }
    }
}