using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class juggernautScript : MonoBehaviour
{

    // Start is called before the first frame update

    //Tomamos el transform para correr el Jugger hasta que comience el ataque



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(levelMgrScript.EstadosJuegoManager == levelMgrScript.EstadosJuego.Idle )Debug.Log("Estoy en Idle");
        if(levelMgrScript.EstadosJuegoManager == levelMgrScript.EstadosJuego.JuggerIngresa )Debug.Log("Estoy en Ingresa");
    }
}
