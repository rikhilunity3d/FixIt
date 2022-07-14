using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class UpToDownAnimation : MonoBehaviour
{
    
    [SerializeField]
    private float startPositionY;
    [SerializeField]
    private float endPositionY;
    [SerializeField]
    private float animDuration;
    [SerializeField]
    private Ease animEase;

    [SerializeField]
    private bool isConfetti;
    [SerializeField]
    GameObject [] confetti;
    
    void Awake()
    {
        if(startPositionY!=0)
        {
            Vector3 temp =this.gameObject.transform.position;
            temp.y = startPositionY;
            this.gameObject.transform.position = temp;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.transform.DOMoveY(endPositionY,animDuration).SetEase(animEase).SetLoops(4,LoopType.Yoyo);

        if(isConfetti)
        {
            for(int i=0; i < confetti.Length; i++)
            {
                confetti[i].GetComponent<ParticleSystem>().Play();
            }
        }
    }
}
