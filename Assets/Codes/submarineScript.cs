using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class submarineScript : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject shieldSub;
    public float speedMove = 0.5f;

    //Creamos el Joystick
    public Joystick joystickYAxes;
    Transform tr;
    GameObject submarine;
    public float velMovSubmarine = 0;
    private float maxXmoveSubEntrada = 15; 
    private float maxXmoveSubJuggerIn = 24; 
    private float limInferiorScene = -7f;
    private float limSuperiorScene = 7f;
    private levelMgrScript.EstadosJuego Call2EstadosSubm;

    private bool flagSound;

    void Start()
    {
        tr = GetComponent<Transform>();
        shieldSub.SetActive(false);
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Tomamos el estado del Juego desde el levelMgr
        Call2EstadosSubm = levelMgrScript.EstadosJuegoManager;

        //Si el usuario mueve el joystick verticalmente movemos el player, pero solo hasta cierto lugar para que no sobresalga dle mapa y para no tener que utilizar colliders
        if(tr.position.y >= limInferiorScene && tr.position.y <= limSuperiorScene )tr.position = tr.position + new Vector3(0f,joystickYAxes.Vertical * speedMove * 1,0f); 
        
        //Solo se le dará límites mientras el submarino no haya muerto
        if(Call2EstadosSubm != levelMgrScript.EstadosJuego.PLAYER_DIE)
        {
            if(tr.position.y < limInferiorScene)tr.position = new Vector3(tr.position.x,limInferiorScene,tr.position.z);
            if(tr.position.y > limSuperiorScene)tr.position = new Vector3(tr.position.x,limSuperiorScene,tr.position.z);
        }

        switch(Call2EstadosSubm)
        {
            case levelMgrScript.EstadosJuego.ENTRADA:

                    //Ubicamos al submarino en el centro de la escena
                 if(tr.position.x < (maxXmoveSubEntrada - 0.1f))
                {       
                    tr.position = tr.position + new Vector3(velMovSubmarine,0f,0f); 
                }
                else
                {
                     if(tr.position.x > (maxXmoveSubEntrada + 0.1f))
                    {       
                        tr.position = tr.position + new Vector3(velMovSubmarine * -1f,0f,0f); 
                    }
                    else
                    {
                        levelMgrScript.EstadosJuegoManager = levelMgrScript.EstadosJuego.PLAYER_IDLE; //Si ya llegó a la posición entonces paso al nuevo estado
                    }
                }     
            break;

            case levelMgrScript.EstadosJuego.JUGGER_IN:
                
                if(tr.position.x < maxXmoveSubJuggerIn)
                {       
                    tr.position = tr.position + new Vector3(0.25f,0f,0f); 
                } //Lo movemos al medio, tiene que ser rápido

            break;

            case levelMgrScript.EstadosJuego.PLAYER_DIE:

            break;

            case levelMgrScript.EstadosJuego.PLAYER_LOST_LIFE:
                
            break;

        }

    }

    void OnCollisionEnter(Collision collision)
    {
        //Si me pega algo me hago invulnerable
        StartCoroutine(invulnerabilidadSub());

        if(collision.gameObject.name == "Enemigo(Clone)") //Si me colisiona un enemigo
        {
            //Destruyo el enemigo
            Object.Destroy(collision.gameObject);
            //Primero guardo el estado de juego en el que estoy
            levelMgrScript.auxEstadosJuegos = levelMgrScript.EstadosJuegoManager;
            //Me voy al estaod en el que el Sub ha sido golpeado
            levelMgrScript.EstadosJuegoManager = levelMgrScript.EstadosJuego.PLAYER_LOST_LIFE;
            
        }

        
        if(collision.gameObject.name == "Juggernaut Variant(Clone)") //Si me colisiona el Jugger
        {
            levelMgrScript.auxEstadosJuegos = levelMgrScript.EstadosJuegoManager;
            levelMgrScript.EstadosJuegoManager = levelMgrScript.EstadosJuego.PLAYER_LOST_LIFE;           
        }

    }

       //Hace invulnerable al submarino por un cierto tiempo
    IEnumerator invulnerabilidadSub()
    {
        //Muestro el "ESCUDO"
        shieldSub.SetActive(true);
        //Lo hago invulnerable a los colliders
        gameObject.GetComponent<SphereCollider>().isTrigger = true;
        //Espero una cantidad de tiempo
        yield return new WaitForSeconds(levelMgrScript.TIME_SUBMARINE_INVULNERABLE);
        //Le regreso el collider
        gameObject.GetComponent<SphereCollider>().isTrigger = false;
        //Borro el "ESCUDO"
        shieldSub.SetActive(false);

    }

    
}
