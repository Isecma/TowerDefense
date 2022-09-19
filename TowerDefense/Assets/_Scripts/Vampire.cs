using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Vampire : EnemigoBase
{
    private void Awake()
    {
        vida = 25;
        _dano = 15;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        referenciaAdminJuego.enemigosBaseDerrotados++;
    }
}
