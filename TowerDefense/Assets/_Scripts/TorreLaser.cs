using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorreLaser : TorreBase, IAtacante
{
    public float divisionRayo = 10;
    public LineRenderer LRRayo;
    public List<Vector3> puntos;
    public int potenciaRayo;

    public void Start()
    {
        LRRayo = GetComponent<LineRenderer>();
    }

    private void FixedUpdate()
    {
        if (enemigo != null)
        {
            Disparar();
            Danar(potenciaRayo);
        }
        else
        {
            LRRayo.positionCount = 0;
        }
    }

    public override void Disparar()
    {
        puntos = ObtenerPuntos();
        puntos.Insert(0, puntasCanon[0].transform.position);
        var posEnemigo = enemigo.transform.position;
        posEnemigo.y += 0.15f;
        puntos.Add(posEnemigo);
        LRRayo.positionCount = puntos.Count;
        LRRayo.SetPositions(puntos.ToArray());
    }
    private List<Vector3> ObtenerPuntos()
    {
        List<Vector3> puntosTemporales = new List<Vector3>();
        float divisor = 1f / divisionRayo;
        float linear = 0f;
        bool esPositivo = false;

        if (divisionRayo == 0)
        {
            Debug.LogError("No podemos dividir entre cero por favor ingresa otro valor en el prefab");
            return null;
        }

        if (divisionRayo == 1)
        {
            var punto = Vector3.Lerp(puntasCanon[0].transform.position, enemigo.transform.position, 0.5f); //Linear interpolation = Lerp
            puntosTemporales.Add(punto);
            return puntosTemporales;
        }
        for (int i = 0; i < divisionRayo; i++)
        {
            if (i == 0)
            {
                linear = divisor / 2;
            }
            else
            {
                linear += divisor;
            }
            var punto = Vector3.Lerp(puntasCanon[0].transform.position, enemigo.transform.position, linear);
            if (esPositivo)
            {
                punto.x += Random.value * 2;
                esPositivo = false;
            }
            else
            {
                punto.x -= Random.value * 2;
                esPositivo =true;
            }
            puntosTemporales.Add(punto);
        }

        return puntosTemporales;
    }

    public void Danar(int dano = 0)
    {
        enemigo.GetComponent<EnemigoBase>().RecibirDano(dano);
    }
}
