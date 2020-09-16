using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backgroundScript : MonoBehaviour
{
    public GameObject rockWall_1;
    public GameObject rockWall_2;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        rockWall_1.GetComponent<Transform>().position = rockWall_1.GetComponent<Transform>().position + new Vector3(-0.2f,0f,0f);  
        rockWall_2.GetComponent<Transform>().position = rockWall_2.GetComponent<Transform>().position + new Vector3(-0.2f,0f,0f); 
    }
}
