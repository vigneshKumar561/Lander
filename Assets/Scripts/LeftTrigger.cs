using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftTrigger : MonoBehaviour
{

    [SerializeField] Oscillator oscillator;
    [SerializeField] RightTrigger rightTrigger;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            oscillator.EndTrigger(false);
            GetComponent<Collider>().enabled = false;
            rightTrigger.GetComponent<Collider>().enabled = false;
        }
    }
}
