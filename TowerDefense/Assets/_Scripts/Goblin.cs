using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Goblin : EnemigoBase
{
    private void Awake()
    {
        vida = 15;
        _dano = 5;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        referenciaAdminJuego.enemigosBaseDerrotados++;
    }
}
