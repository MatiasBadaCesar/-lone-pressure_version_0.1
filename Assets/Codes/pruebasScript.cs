using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class pruebasScript : MonoBehaviour
{
    // Start is called before the first frame update
     private UnityAction m_MyFirstAction;

    void Start()
    {
        m_MyFirstAction+=firstFuntion;
        m_MyFirstAction+=secondFunction;

        m_MyFirstAction();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void firstFuntion()
    {
        Debug.Log("Soy la primera función");
    }

    void secondFunction()
    {
        Debug.Log("Soy la segunda función");
    }
}
