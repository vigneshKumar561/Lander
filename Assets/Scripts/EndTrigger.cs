using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{

    [SerializeField] Oscillator oscillator;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            oscillator.EndTrigger();
            GetComponent<Collider>().enabled = false;
        }
    }
}
