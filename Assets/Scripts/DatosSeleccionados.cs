using UnityEngine;
using System;

public static class DatosSeleccionados
{
    public static event Action OnDatosActualizados;
    
    private static string _codigo;
    private static string _iy;
    private static string _wy;
    private static string _wpl_y;

    public static string Codigo {
        get => _codigo;
        set { _codigo = value; NotificarCambios(); }
    }
    
    public static string Iy {
        get => _iy;
        set { _iy = value; NotificarCambios(); }
    }
    
    public static string Wy {
        get => _wy;
        set { _wy = value; NotificarCambios(); }
    }
    
    public static string Wpl_y {
        get => _wpl_y;
        set { _wpl_y = value; NotificarCambios(); }
    }

    private static void NotificarCambios()
    {
        OnDatosActualizados?.Invoke();
        Debug.Log($"Datos actualizados: Código={_codigo}, Iy={_iy}, Wy={_wy}, Wpl_y={_wpl_y}");
    }
}