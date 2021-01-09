using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector;
    [Range(0,1)] [SerializeField] float movementFactor;
    [SerializeField] float period = 2f;

    Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        startingPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float cycles = Time.time * period;
        const float tau = Mathf.PI / 2;
        float rawSinwave = Mathf.Sin(cycles * tau);
        movementFactor = rawSinwave / 2f + 0.5f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPos + offset;
    }
}
