
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Cinemachine;


public class Player : MonoBehaviour
{

    

    Rigidbody rb;
    AudioSource audioSource;
    [SerializeField] SceneLoader sceneLoader;
    [SerializeField] float thrustValue = 100f;
    [SerializeField] float rotationValue = 100f;
    [SerializeField] AudioClip thrustSound;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip winSound;
    [SerializeField] ParticleSystem thrustParticle;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] CinemachineVirtualCamera cinemachineVirtualCamera;
    

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
        print(tag);

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
            audioSource.PlayOneShot(thrustSound, 0.3f);
        }
        if(!thrustParticle.isPlaying)
        {
            thrustParticle.Play();
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
        thrustParticle.Stop();
        audioSource.PlayOneShot(winSound, 1.6f);
        successParticle.Play();
        Invoke("LoadNextScene", 1f);
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        tag = "NoPlayer";
        ProcessCamera();
        audioSource.Stop();
        thrustParticle.Stop();
        audioSource.PlayOneShot(deathSound, .4f);
        Invoke("DisableShake", .5f);
        Disassemble();
        Invoke("LoadFirstScene", 2f);
    }

    private void ProcessCamera()
    {
        //GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        rb.constraints = RigidbodyConstraints.FreezeAll;
            
    }

    private void DisableShake()
    {
        cinemachineVirtualCamera.GetComponent<CinemachineImpulseListener>().enabled = false;
    }

    private void Disassemble()
    {
        foreach(Transform child in this.transform)
        {
            Rigidbody crb =  child.gameObject.AddComponent<Rigidbody>();
            crb.useGravity = false;            
        }
    }

    private void LoadFirstScene()
    {
        SceneManager.LoadScene(1);
    }

    private void LoadNextScene()
    {
        sceneLoader.LoadNextScene();
    }

}
