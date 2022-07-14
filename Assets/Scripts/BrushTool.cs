using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using System;

public class BrushTool : ToolClass
{

    [SerializeField]
    private float animDuration;
    [SerializeField]
    private Ease animEase;
    [SerializeField]
    int setLoop;

    [SerializeField]
    GameObject correctSign;
    [SerializeField]
    GameObject wrongSign;
    

    [SerializeField]
    private bool isConfetti;
    [SerializeField]
    GameObject[] confetti;
    

    private void OnEnable()
    {
        CorrectAnimation();
    }

    public override void CorrectAnimation()
    {
        base.CorrectAnimation();

        EventHandler.Instance.InvokeOnAnimationDoMove(this.gameObject,correctSign);
        //PlayAnimation(correctSign);
    }
    public override void WrongAnimation()
    {
        base.WrongAnimation();
    }

    void PlayAnimation(GameObject correctSign)
    {
        Debug.Log("Play Animation");

        SetParent(correctSign);

        
        if (isConfetti)
        {
            for (int i = 0; i < confetti.Length; i++)
            {
                confetti[i].GetComponent<ParticleSystem>().Play();
            }
        }
    }

    public void OnCompleteAnimation()
    {
        correctSign.gameObject.SetActive(false);
    }


    public void SetParent(GameObject correctSign)
    {
        //Makes the GameObject "newParent" the parent of the GameObject "player".
        correctSign.transform.parent = this.transform;

        //Display the parent's name in the console.
        Debug.Log("Player's Parent: " + correctSign.transform.parent.name);

        // Check if the new parent has a parent GameObject.
        if (this.transform.parent != null)
        {
            //Display the name of the grand parent of the player.
            Debug.Log(correctSign.gameObject.name +"'s" + correctSign.transform.parent.parent.name);
        }
    }

}
