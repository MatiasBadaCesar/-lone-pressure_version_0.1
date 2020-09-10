using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenMove_Paralax : MonoBehaviour
{

    //Velocidad de las pantallas para crear el efecto Paralax
    public float velocidadScreen;
    Renderer Rnd;
    // Start is called before the first frame update
    private levelMgrScript.EstadosJuego Call2EstadosScreen;
    void Start()
    {
        //Tomamos el material del Quad
        Rnd = GetComponent<Renderer>();
              
    }

    // Update is called once per frame
    void Update()
    {
        Call2EstadosScreen = levelMgrScript.EstadosJuegoManager; //Tomamos el estado actual del Juego

        switch(Call2EstadosScreen)
        {
            case levelMgrScript.EstadosJuego.ENTRADA:
                    
            break;

            default:
                Rnd.material.mainTextureOffset = new Vector2(Time.time * velocidadScreen, 0);
            break;
        }
    }
}
