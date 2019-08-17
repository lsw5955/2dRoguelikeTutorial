using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start1");
        StartCoroutine(Print());
        Debug.Log("Start2");
        StartCoroutine(Print());
        Debug.Log("Start3");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Print()
    {
        Debug.Log("Print1");
        yield return new WaitForSeconds(3);
        Debug.Log("Print2");        
    }
}
