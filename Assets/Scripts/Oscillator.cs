using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Oscillator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector;
    [SerializeField] LoopType loopType;
    [SerializeField] Ease easetype;

    Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalMove(movementVector, 1f, false).SetLoops(-1, loopType).SetEase(easetype).SetRelative();
    }

    // Update is called once per frame
    void Update()
    {        
        
    }
}
