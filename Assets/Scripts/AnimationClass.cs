using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class AnimationClass: MonoBehaviour
{
    //Data part needs to be warp somewhere else.
    [SerializeField]
    private float animDuration;
    [SerializeField]
    private Ease animEase;
    [SerializeField]
    int setLoop;

    [SerializeField]
    Tween fadeTween;

    //All type of Animation has to be done here
    private void OnEnable()
    {
        Debug.Log("Animation OnEnable");
        EventHandler.OnAnimationDoMove += DoMoveAnimation;
        EventHandler.ToolMoveAnimation += ToolMoveAnimation;
        EventHandler.OnImageSwapAnimation += ImageSwapAnimation;
        EventHandler.OnRotateOnZaxis += RotateOnZaxis;
    }

    private void OnDisable()
    {
        Debug.Log("Animation OnDisable");
        EventHandler.OnAnimationDoMove -= DoMoveAnimation;
        EventHandler.ToolMoveAnimation -= ToolMoveAnimation;
        EventHandler.OnImageSwapAnimation -= ImageSwapAnimation;
        EventHandler.OnRotateOnZaxis -= RotateOnZaxis;
    }

    private void Fade(GameObject go,float endValue, float duration, TweenCallback onEnd)
    {
        // checking the fadeTween is not equals to null if that is true, its mean
        // that another tween is in process and we need to terminate it before
        // start the new one.
        if(fadeTween!= null)
        {
            fadeTween.Kill(false);
        }
        fadeTween = go.GetComponent<SpriteRenderer>().DOFade(endValue, duration);

    }

    public void DoMoveAnimation(GameObject parentGO, GameObject childGO)
    {
        //print(parentGO.name + "max " + parentGO.gameObject.GetComponent<SpriteRenderer>().bounds.max);
        //print(parentGO.name + "min " + parentGO.gameObject.GetComponent<SpriteRenderer>().bounds.min);

        if(childGO.activeSelf == false)
        {
            childGO.SetActive(true);
            Vector3 temp= new Vector3(0.0f,0f,0.0f);
            childGO.transform.localPosition = temp;
            // Same temp variable used to asign end position of animation.
            float endPositionY = 0.5f;
            childGO.transform.DOLocalMoveY(endPositionY, animDuration).
                SetEase(animEase).
                SetLoops(setLoop, LoopType.Yoyo).
                OnComplete(() =>
                {
                    OnCompleteAnimation(childGO);
                });
        }
        
    }

    public void ToolMoveAnimation(GameObject parentGO, GameObject childGO)
    {
        //print(parentGO.name + "max " + parentGO.gameObject.GetComponent<SpriteRenderer>().bounds.max);
        //print(parentGO.name + "min " + parentGO.gameObject.GetComponent<SpriteRenderer>().bounds.min);

            childGO.SetActive(true);
            Vector3 temp = new Vector3(0.0f, 0f, 0.0f);
            childGO.transform.localPosition = temp;
            // Same temp variable used to asign end position of animation.
            float endPositionY = 0.5f;
            childGO.transform.DOLocalMoveY(endPositionY, animDuration).
                SetEase(animEase).
                SetLoops(setLoop, LoopType.Yoyo).
                OnComplete(() =>
                {
                    OnCompleteAnimation(childGO);
                });
        

    }

    //This animation is use to swap images in sprite renderer component of gameobject
    // for eg. House in first scene. May be this animation is use by all gameobject
    // after camea shake
    public async void ImageSwapAnimation(GameObject gameObject, Sprite[] sprite, int setLoop)
    {
        if (gameObject != null && sprite.Length > 0 && setLoop > 0)
        while(setLoop > 0)
        {
            for (int i = 0; i < sprite.Length; i++)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite[i];
                await new WaitForSeconds(0.3f);
            }
            setLoop--;
        }   
    }

    // This animation is use to rotate the game object for eg. Tree in first scene
    // which use this animation
    public async void RotateOnZaxis (GameObject gameObject)
    {
        await new WaitForSeconds(0.3f);
        Quaternion temp = gameObject.GetComponent<Transform>().rotation;
        temp.z = 0;
        gameObject.GetComponent<Transform>().rotation = temp;
    }

    // This Method is to disable correct or wrong sign when the animation
    //DoMoveAnimation is completed.
    public void OnCompleteAnimation(GameObject childGO)
    {
        childGO.gameObject.SetActive(false);
    }
}
