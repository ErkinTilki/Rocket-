using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{   
    [SerializeField] float crashDelay = 2f;
    [SerializeField] float finishDelay = 1f;
    [SerializeField] AudioClip crashSFXMain, finishSFXMain;
    [SerializeField] ParticleSystem crashParticles;

    Movement movement;
    AudioSource myAudioSource;
    CapsuleCollider myCapsuleCollider;

    bool isTransitioning = false;
    bool collisionDisable = false;

    void Start()
    {
        movement = GetComponent<Movement>();
        myAudioSource = GetComponent<AudioSource>();
        myCapsuleCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        DebugKeys();
    }

    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || collisionDisable){return;}else
        {

        switch (other.gameObject.tag)
            {
            case "Friendly":
                Debug.Log("You Have Landed.");
                break;
            case "Finish":
                StartFinishSequence();
                break;
            case "Fuel":
                Debug.Log("You Have Fuel.");
                break;
            default:
                StartCrashSequence();
                break;
            }
        }
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        myAudioSource.Stop();
        movement.enabled = false;
        myAudioSource.PlayOneShot(crashSFXMain);
        crashParticles.Play();
        Invoke("ReloadLevel",crashDelay);
    }

    void StartFinishSequence()
    {
        isTransitioning = true;
        myAudioSource.Stop();
        movement.enabled = false;
        myAudioSource.PlayOneShot(finishSFXMain);
        Invoke("LoadNextLevel",finishDelay);
    }

    void ReloadLevel()
    {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex);
    }

        void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
            if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
            {
                nextSceneIndex = 0;
            }
        SceneManager.LoadScene(nextSceneIndex);
            
    }

    void DebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
            
        }else if(Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable;
        }
    }

    
}
