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
                }
                
                yield return null;
            }

    }

}
