using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class submarineScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float speedMove = 1f;

    //Creamos el Joystick
    public Joystick joystickYAxes;
    Transform tr;
    GameObject submarine;
    public float velMovSubmarine = 0;
    private float maxXmoveSubEntrada = 10; 
    private float maxXmoveSubJuggerIn = 24; 
    private levelMgrScript.EstadosJuego Call2EstadosSubm;
    void Start()
    {
        tr = GetComponent<Transform>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Tomamos el estado del Juego desde el levelMgr
        Call2EstadosSubm = levelMgrScript.EstadosJuegoManager;
        //Si el usuario mueve el joystick verticalmente
        tr.position = tr.position + new Vector3(0f,joystickYAxes.Vertical * speedMove * 1,0f); 
        
        switch(Call2EstadosSubm)
        {
            case levelMgrScript.EstadosJuego.Entrada:
                    //Ubicamos al submarino en el centro de la scena
                 if(tr.position.x < maxXmoveSubEntrada)
                {       
                    tr.position = tr.position + new Vector3(velMovSubmarine,0f,0f); 
                }
                else
                {
                    levelMgrScript.EstadosJuegoManager = levelMgrScript.EstadosJuego.Idle; //Si ya llegó a la posición entonces paso al nuevo estado
                }     
            break;

            case levelMgrScript.EstadosJuego.JuggerIngresa:
                
                if(tr.position.x < maxXmoveSubJuggerIn)
                {       
                    tr.position = tr.position + new Vector3(0.25f,0f,0f); 
                } //Lo movemos al medio, tiene que ser rápido

            break;

            case levelMgrScript.EstadosJuego.SubMuere:
                gameObject.GetComponent<Rigidbody>().useGravity = true; //Dejamos que actúe la gravedad
                gameObject.GetComponent<Rigidbody>().isKinematic = false; //Le damos acción a las físicas del RigidBody
            break;

        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Enemigo(Clone)") //Si me colisiona un enemigo
        {
            //Destruyo el enemigo
            Object.Destroy(collision.gameObject);
            //Le pongo el IsTrigger para que no colisione con nada mas
            //gameObject.GetComponent<Collider>().isTrigger = true;
            //Primero guardo el estado de juego en el que estoy
            levelMgrScript.auxEstadosJuegos = levelMgrScript.EstadosJuegoManager;
            //Me voy al estaod en el que el Sub ha sido golpeado
            levelMgrScript.EstadosJuegoManager = levelMgrScript.EstadosJuego.SubPierdeVida;
            
        }
        
    }

    
}
