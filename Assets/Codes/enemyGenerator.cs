using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyGenerator : MonoBehaviour
{
     public GameObject enemyGenerate;
     public GameObject flechaEnemigo;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("enemyGeneratorByTime");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator enemyGeneratorByTime()
    {
            for(;;)
            {
                switch(levelMgrScript.EstadosJuegoManager)
                {
                    case levelMgrScript.EstadosJuego.PLAYER_IDLE: //Si estoy en la etapa de generación genero los enemigos
                        GameObject newEnemy;
                        //float secondEnemy = Random.Range(1f,2f);
                        float velocityEnemy = Random.Range(7f,13f);
                        float positionYEnemy = Random.Range(-6f,6f);
                        yield return new WaitForSeconds(levelMgrScript.TIME_ENEMY_CREATION);
                        //Instanciamos a los enemigos
                        newEnemy = Object.Instantiate(enemyGenerate, new Vector3(50f,positionYEnemy,-5f), enemyGenerate.transform.rotation); 
                        newEnemy.GetComponent<Rigidbody>().velocity = newEnemy.transform.right * -1f * velocityEnemy ; 
                    break;

                    //Vamos a crear lo enemigos que vienen desde la izquierda y dejan un espacio para que el Submarino no sea golpeado
                    // - Este espacio debe ser random en cada ataque
                    case levelMgrScript.EstadosJuego.JUGGER_ATACK_2:

                        

                        if(juggernautScript.sincronicedEnemy == true) //Solo largamos el ataque si la animación está en 3 y si ya no la largué
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
                            GameObject flecha;

                            cantEnemigos = Mathf.RoundToInt(((limSuperiorEnemigo-limInferiorEnemigo)/pasoEntreEnemigos)); //Calculo cuántos tienen que ser
                            enemigoOUT = Random.Range(0,cantEnemigos); //Elijo cuales saco de la hilera
                            
                            for(cant = 0 ; cant < cantEnemigos ; cant++)
                            {
                                
                                if(cant < enemigoOUT - 1  || cant > enemigoOUT + 1)
                                {
                                    enemyHilera = Object.Instantiate(enemyGenerate, new Vector3(0f,(cant*pasoEntreEnemigos)-limSuperiorEnemigo,-4.8f), enemyGenerate.transform.rotation); 
                                    enemyHilera.GetComponent<Rigidbody>().velocity = enemyHilera.transform.right * velocityEnemyHilera; 
                                }
                                
                            }

                            //Creamos y pocisionamos la flecha
                            flecha = Object.Instantiate(flechaEnemigo, new Vector3(37.5f,limInferiorEnemigo + (enemigoOUT*pasoEntreEnemigos),-9f), flechaEnemigo.transform.rotation); 
                            yield return new WaitForSeconds(levelMgrScript.TIME_FLECHA_ANIMADA);
                            Object.Destroy(flecha);
                            juggernautScript.sincronicedEnemy = false;
                        }

                    break;
                }

            
                
                yield return null;
            }

    }

}
