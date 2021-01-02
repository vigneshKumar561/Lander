using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody rb;
    AudioSource thrustAudioSource;
    [SerializeField] float thrustValue = 100f;
    [SerializeField] float rotationValue = 100f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        thrustAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessInput();
    }

    void ProcessInput()
    {
        Thrust();
        Rotate();
    }

    private void Rotate()
    {
        rb.freezeRotation = true;
        float rotationMultiplier = rotationValue * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))  //Rotate left only
        {
            transform.Rotate(Vector3.forward*rotationMultiplier);
        }

        else if (Input.GetKey(KeyCode.D))  //Rotate right only
        {
            transform.Rotate(-Vector3.forward*rotationMultiplier);
        }

        rb.freezeRotation = false;
    }

    private void Thrust()
    {
        float thrustMultiplier = thrustValue * Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))  //Thrust while rotating
        {
            rb.AddRelativeForce(Vector3.up * thrustMultiplier);

            if (!thrustAudioSource.isPlaying)
            {
                thrustAudioSource.Play();
            }

        }
        else
        {
            thrustAudioSource.Stop();
        }
    }
}
