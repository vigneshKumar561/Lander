using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Oscillator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector;
    [SerializeField] LoopType loopType;
    [SerializeField] Ease easetype;
    [SerializeField] float duration = 1f;
    [SerializeField] bool twist = false;

    Vector3 startingPos;
    // Start is called before the first frame update
    void Start()
    {
        if(twist == false)
        {
            transform.DOLocalMove(movementVector, duration, false).SetLoops(-1, loopType).SetEase(easetype).SetRelative();
        }
        
    }

    // Update is called once per frame
    void Update()
    {        
        
    }

    public void EndTrigger()
    {
        
            transform.DOLocalMove(movementVector, duration, false).SetEase(easetype).SetRelative();
        
    }
}
