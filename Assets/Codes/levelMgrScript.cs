using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelMgrScript : MonoBehaviour
{
    private GameObject submarine;
    private GameObject jugger;
    public GameObject enemyGO;

    public GameObject Tank1;
    public GameObject Tank2;
    public GameObject Tamk3;



    private float velScreens;

    
    private int lifeSub = 3;
    public Text lifesSubText;
    private bool flagGeneratingEnemy = false;
    private bool flagTimerToJuggerStart;
    private bool flagJuggerIdle2Positioning;
    private static int cantAtack1 = 3;
    private int cantAtack1Aux = 0;
    private float timeToAtack1 = 3f;
    private bool flagTimerToJuggerAtack1;
    private bool flagTimerToJuggerAtack2;
    private float time2JuggerStart = 10f; //Seteo el tiempo en que quiero que el Jugger aparezca luego de la fase de los enemigos
    
    private bool flagJuggerIdle2Atack1;



//Voy a definir los estados del juego como si fuera una Máquina de Estados
public enum EstadosJuego {
Entrada, //El juego comienza
Idle, //El submarino esta en StandBy y Compite contra objetivos desde la derecha
JuggerIngresa, //El Jugger ingresa a la scena
JuggerIdle,
JuggerPositioning2Atack,
JuggerAtacaTimer,
JuggerAtaca1, //El JuggerAtaca de forma 1
JuggerAtaca2, //El JuggerAtaca de forma 2
JuggerSale, //El jugger se va
SubPierdeVida, //El submarino pierde una vida
SubMuere, //El submarino muere para siempre nunca jamás
SalirDelJuego //Salir de la aplicación
}

public static EstadosJuego EstadosJuegoManager;
public static EstadosJuego auxEstadosJuegos;

    void Start()
    {
        cantAtack1Aux = cantAtack1;
    }

    // Update is called once per frame
    void Update()
    {
        lifesSubText.text = lifeSub.ToString(); 

        switch (EstadosJuegoManager)
        {
            case EstadosJuego.Entrada:

            break;

            case EstadosJuego.Idle:
                    if(!flagGeneratingEnemy)StartCoroutine(EnemyGenerator()); 
                    if(!flagTimerToJuggerStart)StartCoroutine(timerJuggerAparition()); //Solo deseo que se ejecute una vez
            break;

            case EstadosJuego.JuggerIdle:
                    if(!flagJuggerIdle2Positioning)StartCoroutine(timerToJuggerPositioning());
            break;

            case EstadosJuego.JuggerAtacaTimer:
                    if(!flagJuggerIdle2Atack1)StartCoroutine(timerToJuggerAtack1());
            break;


            case EstadosJuego.SubPierdeVida:
                //Realizo la acción pertinente y vuelvo al estado en el que estaba
                lifeSub--;

                Debug.Log("Las Vidas Son: " + lifeSub);

                switch(lifeSub)
                {
                    case 2:
                        Tank1.GetComponent<Rigidbody>().useGravity = true; //Activo Grabedad para que se caiga el tanque
                        Tank1.GetComponent<Rigidbody>().isKinematic = false; //Le saco el kinematic para que le afecte la gravedad
                        EstadosJuegoManager = auxEstadosJuegos; //Volvemos al estado en el que estaba
                    break;

                    case 1:
                        Tank2.GetComponent<Rigidbody>().useGravity = true; //Activo Grabedad para que se caiga el tanque
                        Tank2.GetComponent<Rigidbody>().isKinematic = false; //Le saco el kinematic para que le afecte la gravedad
                        EstadosJuegoManager = auxEstadosJuegos; //Volvemos al estado en el que estaba
                    break;

                    case 0:
                        EstadosJuegoManager = EstadosJuego.SubMuere;
                    break;
                }
                
            break;

            //No interesa en cual de los estados se encuentre

            case EstadosJuego.SubMuere:
               //Debug.Log("He morido");
            break;
            
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    //Función que genera los enemigos que vienen desde la derecha
    IEnumerator EnemyGenerator()
    {
        GameObject newEnemy;
        float secondEnemy = Random.Range(1f,2f);
        float velocityEnemy = Random.Range(5f,10f);
        float positionYEnemy = Random.Range(-6f,6f);
        flagGeneratingEnemy = true; //Bandera para anunciar que se está por generar un objeto que no genere otro mientras tanto
        yield return new WaitForSeconds(secondEnemy);
        //Instanciamos a los enemigos
        newEnemy = Object.Instantiate(enemyGO, new Vector3(50f,positionYEnemy,-5f), enemyGO.transform.rotation); 
        newEnemy.GetComponent<Rigidbody>().velocity = newEnemy.transform.right * -1f * velocityEnemy ; 
        flagGeneratingEnemy = false;
    }

    //Función que va contabilizar el tiempo antes de que el jugger parezca
    IEnumerator timerJuggerAparition()
    {
        flagTimerToJuggerStart = true; //Bandera para que no se repita la acción (Buscar mejores formas)
        yield return new WaitForSeconds(time2JuggerStart);
        EstadosJuegoManager = EstadosJuego.JuggerIngresa;
        flagTimerToJuggerStart = false;

    }

    //Funcion que va a contar el tiempo antes de que el Jugger realice su primer ataque
    IEnumerator timerToJuggerPositioning()
    {
        flagJuggerIdle2Positioning = true;   //Bandera para que no se vuelva a ejecutar la acción
        yield return new WaitForSeconds(time2JuggerStart);
        EstadosJuegoManager = EstadosJuego.JuggerPositioning2Atack;
        flagJuggerIdle2Positioning = false;

    }

    //Función que realiza el Ataque número 1 del Jugger
    IEnumerator timerToJuggerAtack1()
    {
        int tipoAtaque = 0;
        flagJuggerIdle2Atack1 = true; //Evitamos que ingrese nuevamente
        yield return new WaitForSeconds(timeToAtack1);
        tipoAtaque = Random.Range(0,2); //Ataco de una u otra manera
        if(tipoAtaque == 0)EstadosJuegoManager = EstadosJuego.JuggerAtaca1;
        if(tipoAtaque == 1)EstadosJuegoManager = EstadosJuego.JuggerAtaca2;
        flagJuggerIdle2Atack1 = false; //Evitamos que ingrese nuevamente
    }

}
