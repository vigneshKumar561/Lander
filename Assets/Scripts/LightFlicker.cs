using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class LightFlicker : MonoBehaviour
{
    [SerializeField] LoopType loopType;
    [SerializeField] Ease easetype;
    [SerializeField] Color color;

    // Start is called before the first frame update
    void Start()
    {
        Material material = GetComponent<Renderer>().material;
        material.DOColor(color * 15f, 1f).SetLoops(-1, loopType).SetEase(easetype).SetRelative();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
