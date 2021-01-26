using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Rotator : MonoBehaviour
{

    [SerializeField] Vector3 angle;
    [SerializeField] float spinTime;
    [SerializeField] LoopType loopType;
    [SerializeField] Ease easetype;
    // Start is called before the first frame update
    void Start()
    {
        transform.DOLocalRotate(angle, spinTime, RotateMode.Fast).SetLoops(-1, loopType).SetEase(easetype);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
