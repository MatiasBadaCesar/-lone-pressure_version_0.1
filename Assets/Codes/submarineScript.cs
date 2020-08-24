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
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //Si el usuario mueve el joystick verticalmente
        tr.position = tr.position + new Vector3(0f,joystickYAxes.Vertical * speedMove * 1,0f); 
        
    }

    
}
