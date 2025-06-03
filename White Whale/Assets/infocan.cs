using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public class infocan : MonoBehaviour
{
    public GameObject camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);
    }
}
