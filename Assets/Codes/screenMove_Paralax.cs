using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenMove_Paralax : MonoBehaviour
{

    //Velocidad de las pantallas para crear el efecto Paralax
    public float velocidadScreen;
    Renderer Rnd;
    // Start is called before the first frame update
    void Start()
    {
        //Tomamos el material del Quad
        Rnd = GetComponent<Renderer>();
              
    }

    // Update is called once per frame
    void Update()
    {
        Rnd.material.mainTextureOffset = new Vector2(Time.time * velocidadScreen, 0);
        
    }
}
