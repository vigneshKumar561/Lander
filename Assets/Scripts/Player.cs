using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] float thrustValue = 100f;
    [SerializeField] float rotationValue = 100f;
    [SerializeField] AudioClip thrustSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip winSound;
    [SerializeField] ParticleSystem thrustParticle;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem deathParticle;
    enum State { Alive, Transcending, Dying };
    State state = State.Alive;

    //debug
    bool areCollisionsDisabled = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();

        if(Debug.isDebugBuild)
        {
            ProcessDebugMode();
        }
        
    }

    private void ProcessDebugMode()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            LoadNextScene();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            areCollisionsDisabled = true;
        }
    }

    void ProcessInput()
    {
        if(state == State.Alive)
        {
            Thrust();
            Rotate();
        }
        
    }


    private void Thrust()
    {
        float thrustMultiplier = thrustValue * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))  //Thrust while rotating
        {
            ApplyThrust(thrustMultiplier);
            thrustParticle.Play();
        }
        else
        {
            audioSource.Stop();
            thrustParticle.Stop();
        }
    }
    private void Rotate()
    {
        rb.freezeRotation = true;
        float rotationMultiplier = rotationValue * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))  //Rotate left only
        {
            transform.Rotate(Vector3.forward * rotationMultiplier);
        }

        else if (Input.GetKey(KeyCode.D))  //Rotate right only
        {
            transform.Rotate(-Vector3.forward * rotationMultiplier);
        }

        rb.freezeRotation = false;
    }

    

    private void ApplyThrust(float thrustMultiplier)
    {
        rb.AddRelativeForce(Vector3.up * thrustMultiplier);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(thrustSound, 0.6f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if(state != State.Alive || areCollisionsDisabled)
        {            
            return ;
        }
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                print("Friendly");
                break;

            case "Finish":
                StartWinSequence();
                break;

            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartWinSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(winSound, 2f);
        successParticle.Play();
        Invoke("LoadNextScene", 1f);
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound);
        deathParticle.Play();
        Invoke("LoadFirstScene", 1f);
    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    private void LoadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }


}
