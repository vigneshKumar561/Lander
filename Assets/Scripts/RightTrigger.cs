using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightTrigger : MonoBehaviour
{

    [SerializeField] Oscillator oscillator;
    [SerializeField] LeftTrigger leftTrigger;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            oscillator.EndTrigger(true);
            GetComponent<Collider>().enabled = false;
            leftTrigger.GetComponent<Collider>().enabled = false;
        }
    }
}
