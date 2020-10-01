using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class juggernautScript : MonoBehaviour
{

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
        StartCoroutine(juggerByTime());
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

               
        }

    }

    private static int CANT_ATAQUES_JUGGER = 3; //Cantidad de ataque que va a realizar el jugger para pasar a la siguiente etapa
    private static float JUGGER_OUT_SCREEN = -10; //Posición en X en donde el Jugger ya no se ve
    private float velocityGoOut = 0.4f; //Velocidad con la que el Jugger se va de la pantalla
    public static bool sincronicedEnemy = false; 
    public static bool sincroniceOut = false;
    IEnumerator juggerByTime()
    {
        for(;;)
        {
            switch(Call2EstadosJugger)
            {

                case levelMgrScript.EstadosJuego.JUGGER_ATACK_1:
                   
                    int cantAtacks_1;

                    //Tengo que sacar el KINEMATIC para que golpee a la nave (Para que funcione el collider)
                    GetComponent<Collider>().enabled = true;
                    GetComponent<Rigidbody>().isKinematic = false;

                    for(cantAtacks_1 = 0; cantAtacks_1 < CANT_ATAQUES_JUGGER; cantAtacks_1++)
                    {
                        
                        JuggerAnim.SetInteger("jugComp" , 2);
                        yield return new WaitForSeconds(levelMgrScript.TIME_JUGGER_ANIMATION_ATACK_1);
                        JuggerAnim.SetInteger("jugComp" , 1);
                        yield return new WaitForSeconds(levelMgrScript.TIME_JUGGER_ANIMATION_ATACK_1);
                        

                    }

                    //Tengo que poner el KINEMATIC para que no golpee nada (Para que no funcione el collider)
                    GetComponent<Rigidbody>().isKinematic = true;
                    GetComponent<Collider>().enabled = false;
                    levelMgrScript.EstadosJuegoManager = levelMgrScript.EstadosJuego.JUGGER_OUT;

                break;


                case levelMgrScript.EstadosJuego.JUGGER_ATACK_2:
                   
                    int cantAtacks_2;

                    for(cantAtacks_2 = 0; cantAtacks_2 < CANT_ATAQUES_JUGGER; cantAtacks_2++)
                    {
                        
                        JuggerAnim.SetInteger("jugComp" , 3);
                        yield return new WaitForSeconds(levelMgrScript.TIME_JUGGER_ANIMATION_ATACK_2_2);
                        sincronicedEnemy = true; //Le aviso al enemiGenerator que estoy haciendo el primer ataque;
                        JuggerAnim.SetInteger("jugComp" , 1);
                        yield return new WaitForSeconds(levelMgrScript.TIME_JUGGER_ANIMATION_ATACK_2_1);
                    
                    }

                    levelMgrScript.EstadosJuegoManager = levelMgrScript.EstadosJuego.JUGGER_OUT;

                break;

                case levelMgrScript.EstadosJuego.JUGGER_OUT:
                    
                    JuggerAnim.SetInteger("jugComp" , 4);

                    sincroniceOut = true; //Le avisamos al levelMgr que ya puede reproducir los sonidos necesarios 

                    yield return new WaitForSeconds(2f);

                    while(JuggerTransf.position.x > JUGGER_OUT_SCREEN)
                    {
                        JuggerTransf.position = JuggerTransf.position + new Vector3(-1f * velocityGoOut ,0f,0f);
                        yield return null;
                    }

                    
                    
                    yield return new WaitForSeconds(3f);
                   
                    levelMgrScript.EstadosJuegoManager = levelMgrScript.EstadosJuego.ENTRADA;

                    Object.Destroy(this.gameObject);

                break;

            }

            yield return null;
        }

    }

    
}
