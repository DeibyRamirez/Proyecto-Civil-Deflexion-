using UnityEngine;

public class ControlModelo : MonoBehaviour
{
    public Transform modelo;
    public float velocidadRotacion = 0.2f;
    public float zoomSpeed = 5f;
    public Camera camara;

    private Vector3 ultimaPosicion;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - ultimaPosicion;

            // ✅ Solo rotar sobre su propio eje Y (sin moverse)
            modelo.Rotate(0, -delta.x * velocidadRotacion, 0, Space.Self);
        }

        // Zoom con scroll (si quieres mantenerlo)
        float zoom = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        camara.transform.Translate(0, 0, zoom);

        ultimaPosicion = Input.mousePosition;
    }
}
