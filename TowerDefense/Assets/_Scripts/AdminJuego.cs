using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminJuego : MonoBehaviour
{
    public int enemigosBaseDerrotados;
    public int enemigosJefeDerrotados;
    public int recursos;
    public AdministradorUI referenciaAdminUI;

    public delegate void RecursosModificados();
    public event RecursosModificados EnRecursosModificados;


    public void ModificarRecursos(int modificacion)
    {
        recursos += modificacion;
        referenciaAdminUI.textoRecursos.text = $"Recursos: {referenciaAdminUI.referenciaAdminJuego.recursos}";
        if (EnRecursosModificados != null)
        {
            EnRecursosModificados();
        }
    }

    public void ResetValores()
    {
        enemigosBaseDerrotados = 0;
        enemigosJefeDerrotados = 0;
    }
}
