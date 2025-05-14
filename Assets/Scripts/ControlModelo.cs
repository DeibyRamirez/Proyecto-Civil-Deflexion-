using UnityEngine;
using System.Collections.Generic;

public class ControlModelo : MonoBehaviour
{
    public List<Transform> modelos;            // Lista de modelos 3D a rotar
    public float velocidadRotacion = 0.2f;
    public List<Camera> camaras;               // Lista de cámaras que miran a los modelos

    private Vector3 ultimaPosicion;

    void Update()
    {
        // ☝️ ROTACIÓN - Toque simple o mouse
        if (Input.touchCount == 1)
        {
            Touch toque = Input.GetTouch(0);
            if (toque.phase == TouchPhase.Moved)
            {
                Vector2 delta = toque.deltaPosition;
                foreach (var modelo in modelos)
                {
                    modelo.Rotate(0, -delta.x * velocidadRotacion, 0, Space.Self);
                }
            }
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 delta = Input.mousePosition - ultimaPosicion;
            foreach (var modelo in modelos)
            {
                modelo.Rotate(0, -delta.x * velocidadRotacion, 0, Space.Self);
            }
        }

        ultimaPosicion = Input.mousePosition;
    }
}
