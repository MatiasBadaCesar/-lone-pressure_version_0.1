using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class juggernautScript : MonoBehaviour
{

    // Start is called before the first frame update
private levelMgrScript.EstadosJuego Call2EstadosJugger; //Variable para tomar los estados del juego, que debe ser del mismo tipo del otro script
    //Tomamos el transform para correr el Jugger hasta que comience el ataque
private Animator JuggerAnim;
private Transform JuggerTransf;
private float timeAux;
private Transform trSubmar;
public float velPursuit;
private float distSubm;
private float juggerInMiddle = -2f;
private float velMovJuggerAtack1 = 0.1f;

    void Start()
    {
        JuggerTransf = GetComponent<Transform>();
        JuggerAnim = GetComponent<Animator>();
        trSubmar = GameObject.FindWithTag("Submarino").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Lo primero que hacemos siempre es tomar el estado actual del juego que me brinda le manejador de la scena
        Call2EstadosJugger = levelMgrScript.EstadosJuegoManager;

        //Hacemos el switch que va a definir las acciones según los estados
        switch(Call2EstadosJugger)
        {
                //Cuando el levelMgr nos da el Ok, el jugger ingresa al scenario
                case levelMgrScript.EstadosJuego.JUGGER_IN:
                  
                break;     

                case levelMgrScript.EstadosJuego.JUGGER_IDLE:
                        distSubm = JuggerTransf.position.y - trSubmar.position.y + 2;
                        if(Mathf.Abs(distSubm) > 0.1f)JuggerTransf.position = JuggerTransf.position + new Vector3(0f,distSubm*velPursuit/2*-1,0f); //Perseguimos a la nave
                break;

                case levelMgrScript.EstadosJuego.JUGGER_2_ATACK:   //Posiciono al Jugger en el centro para que esté listo para atacar
                    
                    //Primero que nada averiguamos si el Jugger esta por encima o debajo del medio
                    float juggerDir;
                    if(JuggerTransf.position.y > juggerInMiddle )juggerDir = -1;
                    else juggerDir = 1;

                    //Luego lo llevamos a la posición correcta en el centro
                    if(JuggerTransf.position.y > juggerInMiddle + 0.15 || JuggerTransf.position.y < juggerInMiddle - 0.15 ) JuggerTransf.position = JuggerTransf.position + new Vector3(0f,velMovJuggerAtack1 * juggerDir,0f); 
                    else levelMgrScript.EstadosJuegoManager = levelMgrScript.EstadosJuego.JUGGER_ATACK_TIMER;
                    
                break;

                case levelMgrScript.EstadosJuego.JUGGER_ATACK_1:
                    JuggerAnim.SetInteger("jugComp", 2);
                break;

                case levelMgrScript.EstadosJuego.JUGGER_ATACK_2:
                    JuggerAnim.SetInteger("jugComp", 3);
                break;

        }

    }

    
}
