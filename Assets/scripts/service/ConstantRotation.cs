using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    private float rotationSpeed = 200f;


    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed*Time.deltaTime);
    }
}
