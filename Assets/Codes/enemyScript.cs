using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destruirLater());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator destruirLater()
    {
        yield return new WaitForSeconds(5f);
        Object.Destroy(gameObject);
    }
}
