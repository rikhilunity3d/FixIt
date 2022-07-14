using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

// This animation is for air pump
public class ToolPumpAnimation : MonoBehaviour
{
    
    [SerializeField]
    GameObject movingObject;
    [SerializeField]
    int setLoop;
    [SerializeField]
    Vector3 startValue,endValue;

    private void OnEnable()
    {
        UpToDownAnim();
    }

    async void UpToDownAnim()
    {
        while (setLoop > 0)
        {
            movingObject.transform.localPosition = endValue;
            await new WaitForSeconds(0.5f);

            movingObject.transform.localPosition = startValue;
            await new WaitForSeconds(0.5f);

            setLoop--;
        }
    }
}
