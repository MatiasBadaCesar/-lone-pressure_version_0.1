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
    private float TiempoTranscurrido;
    private float TiempoMaxJuggerIn = 10f;
    private float TiempoRandomIn = 0f;
    
    private int lifeSub = 3;
    public Text lifesSubText;
    private bool generatingEnemy = false;
    //private float secondEnemy = 5f;

    


//Voy a definir los estados del juego como si fuera una Máquina de Estados
public enum EstadosJuego {
Entrada, //El juego comienza
Idle, //El submarino esta en StandBy y Compite contra objetivos desde la derecha
JuggerIngresa, //El Jugger ingresa a la scena
JuggerIdle,
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
        
    }

    // Update is called once per frame
    void Update()
    {
        lifesSubText.text = lifeSub.ToString(); 

        switch (EstadosJuegoManager)
        {
            case EstadosJuego.Entrada:
                    TiempoTranscurrido = Time.time; //Pongo el contador a cero
                    TiempoRandomIn = Random.Range(TiempoMaxJuggerIn-5, TiempoMaxJuggerIn); //Pongo una variable float para definir un random entre un tiempo - 5s y el tiempo maximo
            break;

            case EstadosJuego.Idle:
                    if(generatingEnemy == false)StartCoroutine(EnemyGenerator()); 
            break;

            case EstadosJuego.SubPierdeVida:
                //Realizo la acción pertinente y vuelvo al estado en el que estaba
                lifeSub--;

                Debug.Log("Las Vidas Son: " + lifeSub);

                switch(lifeSub)
                {
                    case 2:
                        Tank1.GetComponent<Rigidbody>().useGravity = true;
                        Tank1.GetComponent<Rigidbody>().isKinematic = false;
                        EstadosJuegoManager = auxEstadosJuegos;
                    break;

                    case 1:
                        Tank2.GetComponent<Rigidbody>().useGravity = true;
                        Tank2.GetComponent<Rigidbody>().isKinematic = false;
                        EstadosJuegoManager = auxEstadosJuegos;
                    break;

                    case 0:
                        EstadosJuegoManager = EstadosJuego.SubMuere;
                    break;
                }
                
            break;

            case EstadosJuego.SubMuere:
               //Debug.Log("He morido");
            break;
            
        }
    }

//Mientras el submarino está en Idle, buscamos y determinamos un tiempo para que el Jugger Aparezca
    private void BuscarTiempoParaJugger()
    {
        
        if(Time.time - TiempoTranscurrido > TiempoRandomIn )
        {
            Debug.Log("El tiempo es: " + TiempoRandomIn);
            EstadosJuegoManager = EstadosJuego.JuggerIngresa;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator EnemyGenerator()
    {
        GameObject newEnemy;
        float secondEnemy = Random.Range(1f,2f);
        float velocityEnemy = Random.Range(5f,10f);
        float positionYEnemy = Random.Range(-6f,6f);
        generatingEnemy = true; //Bandera para anunciar que se está por generar un objeto que no genere otro mientras tanto
        yield return new WaitForSeconds(secondEnemy);
        //Instanciamos a los enemigos
        newEnemy = Object.Instantiate(enemyGO, new Vector3(50f,positionYEnemy,-5f), enemyGO.transform.rotation); 
        newEnemy.GetComponent<Rigidbody>().velocity = newEnemy.transform.right * -1f * velocityEnemy ; 
        generatingEnemy = false;
    }
}
