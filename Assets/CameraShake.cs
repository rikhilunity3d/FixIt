using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Set this duration & animationCurve variable from LevelDataSO. So below
    // variables will come under LevelDataSO
    [SerializeField]
    float duration = 1f;
    [SerializeField]
    AnimationCurve animationCurve;
    float elapsedTime = 0f;
    

    #region Step -1 Camera Shake Animation
    // This function is use to shake camera. Every level of the game use the
    // shake camera animation. Step-1.
    private void OnEnable()
    {
        EventHandler.OnCameraShakeAnimation += ListenerOnCameraShakeAnimation;
    }
    private void OnDisable()
    {
        EventHandler.OnCameraShakeAnimation -= ListenerOnCameraShakeAnimation;
    }
    public async void ListenerOnCameraShakeAnimation()
    {
        
        Vector3 startPosition = transform.position;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = animationCurve.Evaluate(elapsedTime / duration);
            transform.position = startPosition + Random.insideUnitSphere * strength;
            await new WaitForSeconds(0.0f);
            print("Camera Shake Complete");
        }
        transform.position = startPosition;

        #region Step - 2 Tell All Elements that Camera Shake is complete
        EventHandler.Instance.InvokeCameraShakeComplete();
        #endregion
    }
    #endregion
}
