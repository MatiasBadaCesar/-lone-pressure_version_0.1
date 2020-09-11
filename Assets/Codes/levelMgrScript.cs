using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelMgrScript : MonoBehaviour
{
    public GameObject Jugger; 
    public GameObject Tank1;
    public GameObject Tank2;

    private int lifeSub = 3;
    public Text lifesSubText;
      
    //Acá irán todos los timers necesarion en el juego ------------------------------------------------------------------------------------------
    public static float TIME_ENEMY_CREATION = 3f; // Acá se setea cada cuanto queremos que el enemigo se genere
    private static float TIME_JUGGER_APARITION = 15f; //Tiempo que transcurre desde que el submarino comienza a esquivar enemigos
    private static float TIME_JUGGER_POSITIONING = 10F; //Tiempo en que pasa de aparece a posicionarse
    private static float TIME_JUGGER_ATACK = 5f; //Tiempo desde que se posiciona hasta que realiza el ataque cualquiera fuera
    public static float TIME_JUGGER_ANIMATION_ATACK_1 = 2.3f; //Tiempo que tardo para volver a poner al Jugger en animación IDLE mientras atacan los enemigos  
    public static float TIME_JUGGER_ANIMATION_ATACK_2_1 = 5f; //Tiempo que tardo para volver a poner al Jugger en animación ATACK_2 mientras atacan los enemigos 
    public static float TIME_JUGGER_ANIMATION_ATACK_2_2 = 2f; //Tiempo que tardo para volver a poner al Jugger en animación IDLE mientras atacan los enemigos 
    //--------------------------------------------------------------------------------------------------------------------------------------------

    

//Voy a definir los estados del juego como si fuera una Máquina de Estados
public enum EstadosJuego {
    ENTRADA, //El juego comienza
    PLAYER_IDLE, //El submarino esta en StandBy y Compite contra objetivos desde la derecha
    JUGGER_IN, //El Jugger ingresa a la scena
    JUGGER_IDLE,
    JUGGER_2_ATACK,
    JUGGER_ATACK_TIMER,
    JUGGER_ATACK_1, //El JuggerAtaca de forma 1
    JUGGER_ATACK_2, //El JuggerAtaca de forma 2
    JUGGER_OUT, //El jugger se va
    PLAYER_LOST_LIFE, //El submarino pierde una vida
    PLAYER_DIE, //El submarino muere para siempre nunca jamás
    GO_OUT_APLICATION //Salir de la aplicación
}

public static EstadosJuego EstadosJuegoManager;
public static EstadosJuego auxEstadosJuegos;

    void Start()
    {
        //Inciamos la corutina que va a darnos las funcionalidades temporales
        StartCoroutine("timeManager");
    }

    // Update is called once per frame, y solo manejará acciones que no dependan del tiempo
    void Update()
    {
        lifesSubText.text = lifeSub.ToString(); 

        switch (EstadosJuegoManager)
        {
            case EstadosJuego.PLAYER_LOST_LIFE:
                //Realizo la acción pertinente y vuelvo al estado en el que estaba
                lifeSub--;

                Debug.Log("Las Vidas Son: " + lifeSub);

                switch(lifeSub) //Manejo de las vidas
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
                        EstadosJuegoManager = EstadosJuego.PLAYER_DIE;
                    break;
                }
                
            break;

            //No interesa en cual de los estados se encuentre

            case EstadosJuego.PLAYER_DIE:
               //Debug.Log("He morido");
            break;
            
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator timeManager() //CORUTINA QUE MANEJA LOS TIEMPOS DEL JUEGO
    {

            for(;;) //Se ejecuta por siempre
            {
                switch(EstadosJuegoManager)
                {
                    case EstadosJuego.PLAYER_IDLE:
                       
                        yield return new WaitForSeconds(TIME_JUGGER_APARITION);
                        EstadosJuegoManager = EstadosJuego.JUGGER_IN;

                    break;

                    case EstadosJuego.JUGGER_IN:
                        
                        GameObject newJugger;
                        newJugger = Object.Instantiate(Jugger, new Vector3(-10f,-6.19f,-5f), Jugger.transform.rotation);
                        newJugger.GetComponent<Animator>().SetInteger("jugComp",1);
                        yield return new WaitForSeconds(1f);
                        newJugger.GetComponent<Transform>().position = new Vector3(12f,-2,-5);
                        levelMgrScript.EstadosJuegoManager = levelMgrScript.EstadosJuego.JUGGER_IDLE;

                    break;

                    case EstadosJuego.JUGGER_IDLE:
                       
                        yield return new WaitForSeconds(TIME_JUGGER_POSITIONING);
                        EstadosJuegoManager = EstadosJuego.JUGGER_2_ATACK;     

                    break;

                    case EstadosJuego.JUGGER_ATACK_TIMER:
                       
                        yield return new WaitForSeconds(TIME_JUGGER_ATACK);
                        int tipoAtaque = Random.Range(0,2); //Ataco de una u otra manera
                        if(tipoAtaque == 0)EstadosJuegoManager = EstadosJuego.JUGGER_ATACK_1;
                        if(tipoAtaque == 1)EstadosJuegoManager = EstadosJuego.JUGGER_ATACK_2;

                    break;

                    case EstadosJuego.PLAYER_DIE:
                       
                        StopAllCoroutines();
                   
                    break;
                
                
                }

                yield return null;
            }


    }
 
}
