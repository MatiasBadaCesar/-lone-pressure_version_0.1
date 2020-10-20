using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class levelMgrScript : MonoBehaviour
{
    public GameObject Jugger; 
    private GameObject juggerInstanciate;
    public GameObject Tank1;
    public GameObject Tank2;



    private int lifeSub = 3;
    public Text lifesSubText; //Muestra la cantidad de vidas restantes

    public Text timeScoreText; //Muestra el tiempo que el jugador lleva jugando desde que comenzó
    public GameObject gameOverText; //Texto de GAME OVER

    //---------------------------- SONIDOS --------------------------------------------------------

    [Header("Sonidos del Juego")]
    public GameObject[] soundsOfGame;

    public enum soundsNames {
        juggerIn = 0,
        juggerOut,
        fondoNormal,
        fondoJugger,
        mordiscoJugger,
        invocacionJugger,
        golpeNave


    }

    //---------------------------------------------------------------------------------------------
      
    //Acá irán todos los timers necesarion en el juego ------------------------------------------------------------------------------------------
    public static float TIME_ENEMY_CREATION = 1f; // Acá se setea cada cuanto queremos que el enemigo se genere
    private static float TIME_JUGGER_APARITION = 15f; //Tiempo que transcurre desde que el submarino comienza a esquivar enemigos
    private static float TIME_JUGGER_POSITIONING = 10F; //Tiempo en que pasa de aparece a posicionarse
    private static float TIME_JUGGER_ATACK = 3f; //Tiempo desde que se posiciona hasta que realiza el ataque cualquiera fuera
    public static float TIME_JUGGER_ANIMATION_ATACK_1 = 2.3f; //Tiempo que tardo para volver a poner al Jugger en animación IDLE mientras atacan los enemigos  
    public static float TIME_JUGGER_ANIMATION_ATACK_2_1 = 5f; //Tiempo que tardo para volver a poner al Jugger en animación ATACK_2 mientras atacan los enemigos 
    public static float TIME_JUGGER_ANIMATION_ATACK_2_2 = 2f; //Tiempo que tardo para volver a poner al Jugger en animación IDLE mientras atacan los enemigos 
    public static float TIME_FLECHA_ANIMADA = 3f; //Tiempo que la flecha animada se va a ver en la escena
    public static float TIME_SUBMARINE_INVULNERABLE = 5f; //Tiempo en que el submarino es invulnerable
    //--------------------------------------------------------------------------------------------------------------------------------------------

    


//Voy a definir los estados del juego como si fuera una Máquina de Estados
public enum EstadosJuego {
    ENTRADA, //El juego comienza
    PLAYER_IDLE, //El submarino esta en StandBy y Compite contra objetivos desde la derecha
    JUGGER_IN, //El Jugger ingresa a la scena
    JUGGER_IDLE, //El jugger esta por un tiempo en idle siguiendo al submarino
    JUGGER_2_ATACK, //El jugger se posiciona para atacar
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
        //Apago el Texto de Game Over
        gameOverText.SetActive(false);

        //Inciamos la corutina que va a darnos las funcionalidades temporales
        StartCoroutine("timeManager");
        StartCoroutine("timeCounter");

        //Definimos estados iniciales para cuando se recargue el juego
        EstadosJuegoManager = EstadosJuego.ENTRADA;
        //tiempoTransc = Time.realtimeSinceStartup;  //#### DESECHADO DESDE QUE SE DESCUBRE QUE HAY UNA VARIABLE QUE TE LARGA LOS SEGUNDOS DESDE LA RECARGA ####

        //Comenzamos con el sonido de fondo normal
        repSonidos(soundsNames.fondoNormal, true, false, 0f);

    }

    //Flags para que no se vuelvan a repetir las acciones...
    private bool flagExecute = false;

    // Update is called once per frame, y solo manejará acciones que no dependan del tiempo
    void FixedUpdate()
    {
        lifesSubText.text = lifeSub.ToString(); 

        switch (EstadosJuegoManager)
        {

            case EstadosJuego.JUGGER_ATACK_1:


                if(juggerInstanciate.GetComponent<Animator>().GetInteger("jugComp") == 2 && flagExecute == false) //Significa que lanza el ataque;
                {
                    repSonidos(soundsNames.mordiscoJugger, false, true, 2f); //Iniciamos el sonido de Jugger de entrada por 3s
                    flagExecute = true;
                }
                else
                {
                    flagExecute = false;
                }

            break;

            case EstadosJuego.JUGGER_ATACK_2:
                
                if(juggerInstanciate.GetComponent<Animator>().GetInteger("jugComp") == 3 && flagExecute == false) //Significa que lanza el ataque;
                {
                    repSonidos(soundsNames.invocacionJugger, false, true, 3.5f); //Iniciamos el sonido de Jugger de entrada por 3s
                    flagExecute = true;
                }
                else
                {
                    flagExecute = false;
                }

            break;

            case EstadosJuego.JUGGER_OUT:
               
                if(juggernautScript.sincroniceOut == true)
                {
                        repSonidos(soundsNames.fondoNormal, true, false, 0f); //Detenemos el sonido normal
                        repSonidos(soundsNames.fondoJugger, false, false, 0f); //Iniciamos el sonido de Jugger de fondo
                        repSonidos(soundsNames.juggerOut, false, true, 5f); //Iniciamos el sonido de Jugger de entrada por 3s
                        juggernautScript.sincroniceOut = false;
                }

            break;

            case EstadosJuego.PLAYER_LOST_LIFE:

                //Realizo la acción pertinente y vuelvo al estado en el que estaba
                lifeSub--;

                repSonidos(soundsNames.golpeNave, false, true, 2f); //Iniciamos el sonido de Jugger de entrada por 3s

                switch(lifeSub) //Manejo de las vidas
                {
                    case 2:
                        Tank1.GetComponent<Rigidbody>().useGravity = true; //Activo Grabedad para que se caiga el tanque
                        Tank1.GetComponent<Rigidbody>().isKinematic = false; //Le saco el kinematic para que le afecte la gravedad
                        Object.Destroy(Tank1, 3f);
                        EstadosJuegoManager = auxEstadosJuegos; //Volvemos al estado en el que estaba
                    break;

                    case 1:
                        Tank2.GetComponent<Rigidbody>().useGravity = true; //Activo Grabedad para que se caiga el tanque
                        Tank2.GetComponent<Rigidbody>().isKinematic = false; //Le saco el kinematic para que le afecte la gravedad
                        Object.Destroy(Tank2, 3f);
                        EstadosJuegoManager = auxEstadosJuegos; //Volvemos al estado en el que estaba
                    break;

                    case 0:
                        EstadosJuegoManager = EstadosJuego.PLAYER_DIE;
                    break;
                }
                
            break;

            //No interesa en cual de los estados se encuentre

            case EstadosJuego.PLAYER_DIE:

               Time.timeScale = 0.0f;
               gameOverText.SetActive(true);

            break;
            
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    //private float tiempoTransc = 0f; //#### OBSOLETO ####
    public void rechargeLevel()
    {
        SceneManager.LoadScene("Nivel1");
        Time.timeScale = 1.0f;
    }

    private int CantAtaks = 3;
    private int CantAtaks_Actual = 0;
    private IEnumerator timeManager() //CORUTINA QUE MANEJA LOS TIEMPOS DEL JUEGO
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
                        
                        while(GameObject.Find("Enemigo(Clone)")!=null) //Esperamos a que todas los enemigos mueran
                        {
                            yield return new WaitForSeconds(0.5f);
                        }  


                        repSonidos(soundsNames.fondoNormal, false, false, 0f); //Detenemos el sonido normal
                        repSonidos(soundsNames.fondoJugger, true, false, 0f); //Iniciamos el sonido de Jugger de fondo
                        repSonidos(soundsNames.juggerIn, false, true, 3.5f); //Iniciamos el sonido de Jugger de entrada por 3s

                        
                        juggerInstanciate = Object.Instantiate(Jugger, new Vector3(-10f,-6.19f,-5f), Jugger.transform.rotation);
                        juggerInstanciate.GetComponent<Animator>().SetInteger("jugComp",1);
                        yield return new WaitForSeconds(1f);
                        juggerInstanciate.GetComponent<Transform>().position = new Vector3(12f,-2,-5);
                        levelMgrScript.EstadosJuegoManager = levelMgrScript.EstadosJuego.JUGGER_IDLE;

                    break;

                    case EstadosJuego.JUGGER_IDLE:
                       
                        //Tengo que sacar el KINEMATIC para que golpee a la nave (Para que funcione el collider)
                        //##### CUIDADO !!! si el jugger golpea un enemigo se va a mover!!! #####
                        juggerInstanciate.GetComponent<Collider>().enabled = true;
                        juggerInstanciate.GetComponent<Rigidbody>().isKinematic = false;
                        juggerInstanciate.GetComponent<Animator>().SetInteger("jugComp",2); //Genero que mientras me persigue me mordisquee
                        
                        yield return new WaitForSeconds(TIME_JUGGER_POSITIONING); //Lo persigo por un tiempo
                        
                        //Tengo que poner el KINEMATIC para que no golpee nada (Para que no funcione el collider)
                        juggerInstanciate.GetComponent<Rigidbody>().isKinematic = true;
                        juggerInstanciate.GetComponent<Collider>().enabled = false;
                        
                        CantAtaks_Actual = 0; //Volvemos a 0 la variable de ataques para que la cantidad sea la misma en cada escena
                        EstadosJuegoManager = EstadosJuego.JUGGER_2_ATACK;     

                    break;

                    case EstadosJuego.JUGGER_ATACK_TIMER:

                        if(CantAtaks_Actual != CantAtaks)
                        {
                            CantAtaks_Actual++;
                            yield return new WaitForSeconds(TIME_JUGGER_ATACK);
                            int tipoAtaque = Random.Range(0,2); //Ataco de una u otra manera
                            if(tipoAtaque == 0)EstadosJuegoManager = EstadosJuego.JUGGER_ATACK_1;
                            if(tipoAtaque == 1)EstadosJuegoManager = EstadosJuego.JUGGER_ATACK_2;
                        }
                        else
                        {
                            EstadosJuegoManager = EstadosJuego.JUGGER_OUT; 
                        }

                    break;

                    case EstadosJuego.PLAYER_DIE:
                       
                    break;
                
                
                }

                yield return null;
            }


    }

    private IEnumerator timeCounter() //Calcula el Score dado por el tiempo
    {
            for(;;)
            {
                timeScoreText.text = CalcularScore(Time.timeSinceLevelLoad);
                yield return new WaitForSeconds(1f);
            }


    }

    private string CalcularScore(float seconds)
    {
        
        return Mathf.RoundToInt(seconds).ToString(); //Sacamos decimales y convertimos en string
    }

    private string CalcularTiempo(float t_segundos) //Función para convertir segundos en Horas, Minutos y Segundos. #### QUEDA OBSOLETA ####
    {
        int horas = Mathf.FloorToInt((t_segundos / 3600));
        int minutos = Mathf.FloorToInt(((t_segundos-horas*3600)/60));
        float segundos = t_segundos-(horas*3600+minutos*60);
        return horas.ToString() + ":" + minutos.ToString() + ":" + segundos.ToString();
    }

    public void repSonidos(soundsNames soundGO , bool activateSound,bool oneShot, float timeToSound) {
        
        if(oneShot == true) //Si solo quiero que suene una vez y por un tiempo
        {
            StartCoroutine(repSoundByTime(soundsOfGame[(int)soundGO].GetComponent<AudioSource>(), timeToSound));
        }
        else   //Si se va a reproducir por siempre 
        {
            if(activateSound == true)soundsOfGame[(int)soundGO].GetComponent<AudioSource>().enabled = true;
            else soundsOfGame[(int)soundGO].GetComponent<AudioSource>().enabled = false;
        }

    }

//Reproducimos el audio solo por un tiempo
    private IEnumerator repSoundByTime(AudioSource audioSource, float timeToRep)
    {
        audioSource.enabled = true;
        yield return new WaitForSeconds(timeToRep);
        audioSource.enabled = false;
    }

 
 
}
