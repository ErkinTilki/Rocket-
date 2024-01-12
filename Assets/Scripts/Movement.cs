using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb; 

    [SerializeField] float thrustPower = 1000f ;
    [SerializeField] float rotateForce = 200f ;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
            if(Input.GetKey(KeyCode.Space))
            {
                rb.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime);
            }
    }

    void ProcessRotation()
    {
            if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotateForce);
        }
        else if(Input.GetKey(KeyCode.D))
        {
                ApplyRotation(-rotateForce);
        }
    }

     void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
 