using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelMgrScript : MonoBehaviour
{
    private GameObject submarine;
    public float velMovSubmarine = 0;
    private float maxXmoveSub = 24; 

    private GameObject jugger;

    private float velScreens;
    private float TiempoTranscurrido;
    private float TiempoMaxJuggerIn = 10f;


//Voy a definir los estados del juego como si fuera una Máquina de Estados
public enum EstadosJuego {
Entrada, //El juego comienza
Idle, //El submarino esta en StandBy
JuggerIngresa, //El Jugger ingresa a la scena
JuggerAtaca1, //El JuggerAtaca de forma 1
JuggerAtaca2, //El JuggerAtaca de forma 2
JuggerSale, //El jugger se va
SubPierdeVida, //El submarino pierde una vida
SubMuere //El submarino muere para siempre nunca jamás
}

public static EstadosJuego EstadosJuegoManager;

    void Start()
    {
        //Instanciamos al Submarino
        submarine = GameObject.FindWithTag("Submarino");
        //Instanciamos al Jugger
        jugger = GameObject.FindWithTag("Jugger");
        
    }

    // Update is called once per frame
    void Update()
    {

        switch (EstadosJuegoManager)
        {
            case EstadosJuego.Entrada:
                //Ubicamos al submarino en el centro de la scena
                if(submarine.transform.position.x < maxXmoveSub)
                {       
                    submarine.transform.position = submarine.transform.position + new Vector3(velMovSubmarine,0f,0f); 
                }
                else
                {
                    TiempoTranscurrido = Time.time;
                    EstadosJuegoManager = EstadosJuego.Idle;
                }      

            break;

            case EstadosJuego.Idle:
                BuscarTiempoParaJugger();
            break;
        }
    }

    private void BuscarTiempoParaJugger()
    {
        
        if(Time.time - TiempoTranscurrido > TiempoMaxJuggerIn )
        {
            EstadosJuegoManager = EstadosJuego.JuggerIngresa;
        }
    }
}
