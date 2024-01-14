using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{

    [SerializeField] float thrustPower = 1000f ;
    [SerializeField] float rotateForce = 200f ;
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem leftRocketParticles;
    [SerializeField] ParticleSystem middleRocketParticles1;
    [SerializeField] ParticleSystem middleRocketParticles2;
    [SerializeField] ParticleSystem rightRocketParticles;

    Rigidbody rb; 
    AudioSource myAudioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        myAudioSource = GetComponent<AudioSource>();
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
            StartThrusting();
        }
        else
            {
                myAudioSource.Stop();
            }
    }

    void ProcessRotation()
    {
            if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
    }

    void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrustPower * Time.deltaTime);
        
        StartAllRocketParticles();

        if (!myAudioSource.isPlaying)
        {
            myAudioSource.PlayOneShot(mainEngine);
        }
    }


    void RotateLeft()
    {
        rightRocketParticles.Play();
        leftRocketParticles.Stop();
        ApplyRotation(rotateForce);
    }

    void RotateRight()
    {
        leftRocketParticles.Play();
        rightRocketParticles.Stop();
        ApplyRotation(-rotateForce);
    }
    
    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }

    void StartAllRocketParticles()
    {
        leftRocketParticles.Play();
        middleRocketParticles1.Play();
        middleRocketParticles2.Play();
        rightRocketParticles.Play();
    }
}
 