using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScript : MonoBehaviour
{
    public GameObject rockWall_1;
    public GameObject rockWall_2;
    public GameObject rockPrefab;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Movemos el fondo hacia la izquierda
        rockWall_1.transform.position = rockWall_1.transform.position + new Vector3(-0.2f,0f,0f);  
        rockWall_2.transform.position = rockWall_2.transform.position + new Vector3(-0.2f,0f,0f); 

        //A cierta posición en X cambiamos de GameObject destruimos uno y creamos uno nuevo para
        //que se vea como si el fondo fuera continuo...
        if(rockWall_2.transform.position.x < 20.0f)
        { 
            Object.Destroy(rockWall_1); //Destruimos el muro que salió de la pantalla
            rockWall_1 = rockWall_2; //Cambiamos las instancias
            rockWall_2 = Object.Instantiate(rockPrefab, new Vector3(58f,0.63f,6.2f), rockPrefab.transform.rotation); //Creamos un muro nuevo
        }
    
    }
}
