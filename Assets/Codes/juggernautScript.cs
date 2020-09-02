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
float distSubm;


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
                case levelMgrScript.EstadosJuego.JuggerIngresa:
                                       
                        JuggerAnim.SetInteger("jugComp", 1); //Le damos la animación requerida
                        timeAux += Time.deltaTime;
                        
                        if(timeAux > 1f)//Le damos un retardo para que no se vean quilombos
                        {
                            timeAux = 0;
                            JuggerTransf.position = new Vector3(8.5f,-2,-5); //Pocisionamos al Jugger pero luego de un tiempo para que no haga un mal efecto
                            levelMgrScript.EstadosJuegoManager = levelMgrScript.EstadosJuego.JuggerIdle; //Pasamos al siguiente estado
                        }
                break;     

                case levelMgrScript.EstadosJuego.JuggerIdle:
                        distSubm = JuggerTransf.position.y - trSubmar.position.y + 2;
                        if(Mathf.Abs(distSubm) > 0.1f)JuggerTransf.position = JuggerTransf.position + new Vector3(0f,distSubm*velPursuit/2*-1,0f); //Perseguimos a la nave
                break;

        }

    }
}
