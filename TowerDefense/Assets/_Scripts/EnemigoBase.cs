using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/**
 * Clase base para configurar parametros, animaciones y comportamientos de los enemigos. \n
 * Esta clase lo heredan los enemigos _Vampire, Demon, Goblin_ y _Boss_
 */
public class EnemigoBase : MonoBehaviour, IAtacante, IAtacable
{
    public GameObject objetivo;
    public int vida = 100;
    /**
     * Variable que se usa para realizar dano al objetivo. \n
     * El valor base es de 5, dicho valor cambia en la clase \n
     * de cada enemigo en especifico.
     */
    public int _dano = 5;
    public int recursosGanados = 100;

    /**
     * Variable que se usa para hacer referencia al _Animator Controller_ \n
     * de cada enemigo.
     */
    public Animator Anim;
    public SpawnerEnemigos referenciaSpawner;
    public AdminJuego referenciaAdminJuego;


    private void OnEnable()
    {
        objetivo = GameObject.Find("Objetivo");
        referenciaAdminJuego = GameObject.Find("AdminJuego").GetComponent<AdminJuego>();
        referenciaSpawner = GameObject.Find("SpawnerEnemigos").GetComponent<SpawnerEnemigos>();
        objetivo.GetComponent<Objetivo>().EnObjetivoDestruido += Detener;
    }

    private void OnDisable()
    {
        objetivo.GetComponent<Objetivo>().EnObjetivoDestruido -= Detener;
    }

    // Start is called before the first frame update

    void Start()
    {
        GetComponent<NavMeshAgent>().SetDestination(objetivo.transform.position);
        Anim = GetComponent<Animator>();
        Anim.SetBool("IsMoving", true);
    }

    // Update is called once per frame
    void Update()
    {
        if (vida <= 0)
        {
            Anim.SetTrigger("OnDead");
            GetComponent<NavMeshAgent>().SetDestination(transform.position);
            Destroy(gameObject, 3);
        }
    }
    /**
     * Funcion para otorgar recursos que vale el enemigo al \n
     * jugador y eliminar al enemigo de la lista _EnemigosGenerados_.
     */
    public virtual void OnDestroy()
    {
        referenciaAdminJuego.ModificarRecursos(recursosGanados);
        referenciaSpawner.EnemigosGenerados.Remove(this.gameObject);
    }
    /**
     * Funcion para detener al enemigo en cuanto colisione \n
     * con el objetivo.
     * 
     * _Parametros_
     * 
     * __Collision__ : _el objeto con el que se ha detectado una colision en el evento OnCollisionEnter_.
     */
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Objetivo")
        {
            Anim.SetBool("IsMoving", false);
            Anim.SetTrigger("OnObjectiveReached");
        }
    }

    public void Danar(int dano)
    {
        if (dano == 0) dano = _dano;
        objetivo?.GetComponent<Objetivo>().RecibirDano(_dano);
    }

    public void RecibirDano(int dano = 5)
    {
        vida -= dano;
    }


    private void Detener()
    {
        Anim.SetTrigger("OnObjectiveDestroyed");
        GetComponent<NavMeshAgent>().SetDestination(transform.position);
    }
}
