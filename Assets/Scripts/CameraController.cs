using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform playerTransform;
    Vector3 offset;
   
    void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        offset = transform.position - playerTransform.position;    
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position + offset;
        
    }
}
