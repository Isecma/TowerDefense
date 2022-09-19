using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreMago : TorreBase
{
    public GameObject prefabMagia;
    public int _dano = 15;

    private void Update()
    {
        if (enemigo != null)
        {
            Apuntar();
        }
    }

    public override void Disparar()
    {
        var tempMagia = Instantiate<GameObject>(prefabMagia, puntasCanon[0].transform.position, Quaternion.identity);
        tempMagia.GetComponent<Bala>().destino = enemigo.transform.position;
    }

}
