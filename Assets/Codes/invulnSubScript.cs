using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invulnSubScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

   void FixedUpdate()
   {

       //ATENCIÓN, ESTA RUTINA CARGA AL PROCESADOR!!! Si es necesarios reemplazar 
        gameObject.transform.Rotate(0f,0f,2f,Space.Self);


   }

}
