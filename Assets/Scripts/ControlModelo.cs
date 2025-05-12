using UnityEngine;
using System.Collections.Generic;

public class ControlModelo : MonoBehaviour
{
    public List<Transform> modelos;            // Lista de modelos 3D a rotar
    public float velocidadRotacion = 0.2f;
    public float zoomSpeed = 0.1f;
    public List<Camera> camaras;               // Lista de cámaras que miran a los modelos

    private Vector3 ultimaPosicion;
    private float distanciaInicial = 0f;
    private List<float> zoomIniciales = new List<float>();

    void Start()
    {
        // Guardamos el zoom inicial de cada cámara
        foreach (var cam in camaras)
        {
            zoomIniciales.Add(cam.transform.localPosition.z);
        }
    }

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

        // ✌️ ZOOM - Pinch con dos dedos
        if (Input.touchCount == 2)
        {
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);

            float distanciaActual = Vector2.Distance(t0.position, t1.position);

            if (t0.phase == TouchPhase.Began || t1.phase == TouchPhase.Began)
            {
                distanciaInicial = distanciaActual;
                zoomIniciales.Clear();
                foreach (var cam in camaras)
                {
                    zoomIniciales.Add(cam.transform.localPosition.z);
                }
            }

            float diferencia = distanciaInicial - distanciaActual;
            for (int i = 0; i < camaras.Count; i++)
            {
                Vector3 nuevaPos = camaras[i].transform.localPosition;
                nuevaPos.z = Mathf.Clamp(zoomIniciales[i] + diferencia * zoomSpeed, -10f, -2f);
                camaras[i].transform.localPosition = nuevaPos;
            }
        }

        // 🖱️ ZOOM - Rueda del ratón
        float zoomRueda = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed * 100f;
        if (zoomRueda != 0f)
        {
            foreach (var cam in camaras)
            {
                Vector3 nuevaPos = cam.transform.localPosition;
                nuevaPos.z = Mathf.Clamp(nuevaPos.z + zoomRueda, -10f, -2f);
                cam.transform.localPosition = nuevaPos;
            }
        }

        ultimaPosicion = Input.mousePosition;
    }
}
