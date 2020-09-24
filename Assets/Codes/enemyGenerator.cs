using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGenerator : MonoBehaviour
{
     public GameObject enemyGenerate;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("enemyGeneratorByTime");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool flagDontDo = false;
    IEnumerator enemyGeneratorByTime()
    {
            for(;;)
            {
                switch(levelMgrScript.EstadosJuegoManager)
                {
                    case levelMgrScript.EstadosJuego.PLAYER_IDLE: //Si estoy en la etapa de generación genero los enemigos
                        GameObject newEnemy;
                        float secondEnemy = Random.Range(1f,2f);
                        float velocityEnemy = Random.Range(5f,10f);
                        float positionYEnemy = Random.Range(-6f,6f);
                        yield return new WaitForSeconds(levelMgrScript.TIME_ENEMY_CREATION);
                        //Instanciamos a los enemigos
                        newEnemy = Object.Instantiate(enemyGenerate, new Vector3(50f,positionYEnemy,-5f), enemyGenerate.transform.rotation); 
                        newEnemy.GetComponent<Rigidbody>().velocity = newEnemy.transform.right * -1f * velocityEnemy ; 
                    break;

                    //Vamos a crear lo enemigos que vienen desde la izquierda y dejan un espacio para que el Submarino no sea golpeado
                    // - Este espacio debe ser random en cada ataque
                    case levelMgrScript.EstadosJuego.JUGGER_ATACK_2:

                        if(flagDontDo == false) //Solo largamos el ataque si la animación está en 3 y si ya no la largué
                        {
                            const int limInferiorEnemigo = -9; //Hasta donde generamos la hilera de enemigos
                            const int limSuperiorEnemigo = 9; //Desde donde generamos la hilera de enemigos
                            const float pasoEntreEnemigos = 2f; //Cada cuanto generamos un enemigos
                            const float velocityEnemyHilera = 12f;
                            int cantEnemigos;
                            int cant;
                            int enemigoOUT; //Enemigos que no estarán presentes
                            //Generamos la ilera de enemigos:
                            GameObject enemyHilera;

                            cantEnemigos = Mathf.RoundToInt(((limSuperiorEnemigo-limInferiorEnemigo)/pasoEntreEnemigos));
                            enemigoOUT = Random.Range(0,cantEnemigos); //Elijo cuales saco de la hilera
                            
                            Debug.Log("La cantidad de enemigos a generar es: " + cantEnemigos);
                            Debug.Log("Los enemigos afuera son: " + enemigoOUT);

                            for(cant = 0 ; cant < cantEnemigos ; cant++)
                            {
                                
                                if(cant < enemigoOUT - 2  || cant > enemigoOUT + 2)
                                {
                                    Debug.Log("Estoy generando el enemigo: " + cant);
                                    enemyHilera = Object.Instantiate(enemyGenerate, new Vector3(0f,(cant*pasoEntreEnemigos)-limSuperiorEnemigo,-4.8f), enemyGenerate.transform.rotation); 
                                    enemyHilera.GetComponent<Rigidbody>().velocity = enemyHilera.transform.right * velocityEnemyHilera; 
                                }
                                
                                yield return null; //Para salir del bucle en cada update
                            }


                            flagDontDo = true;
                        }

                        Debug.Log("ATAQUE FINALIZADO...");
                        flagDontDo = false;
                        yield return new WaitForSeconds(levelMgrScript.TIME_JUGGER_ANIMATION_ATACK_2_1 + levelMgrScript.TIME_JUGGER_ANIMATION_ATACK_2_2); //Cuando el ataque haya finalizado
                        


                    break;
                }

            
                
                yield return null;
            }

    }

}
