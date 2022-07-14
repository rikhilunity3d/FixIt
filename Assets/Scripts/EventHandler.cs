using UnityEngine;
using System;

public class EventHandler : GenericSingleton<EventHandler>
{
    public static event Action OnNext;
    public static event Action OnBack;

    public static event Action OnRateApp;
    public static event Action OnRestoreApp;
    public static event Action OnMoreApps;
    public static event Action OnShareApp;

    public static event Action<Sounds> OnButtonClickSound;
    public static event Action<Sounds> OnPlayBackGroundSound;
    public static event Action<bool> OnMuteSound;

    public static event Action<int> OnLoadLevelAction;

    public static event Action <GameObject> OnRightRepair, OnWrongRepair,DeleteToolOnCorrect;

    #region Dotween Animation
    public static event Action <GameObject,GameObject> OnAnimationDoMove,ToolMoveAnimation;
    #endregion

    #region Step -1 Camera Shake Animation
    public static event Action OnCameraShakeAnimation;
    #endregion

    public static event Action OnCameraShakeComplete;

    public static event Action OnBeforeCameraShake;

    public static event Action OnMouseDownHideToolList;
    public static event Action OnMouseUpShowToolList;


    #region Vibrate Device
    public static event Action OnDeviceVibrate;
    #endregion

    #region Animation
    public static event Action <GameObject, Sprite[],int> OnImageSwapAnimation;
    public static event Action <GameObject> OnRotateOnZaxis;
    #endregion

    //ChangeableScriptableObjects changeableSO;
    //public static event Action<ChangeableScriptableObjects> onChangeableAction;

    #region Admob Interstitial Ad
    public static event Action OnShowInterstitialAd;
    #endregion

    bool isMute = false;

    public void InvokeOnNext() => OnNext?.Invoke();
    public void InvokeOnBack() => OnBack?.Invoke();
    public void InvokeOnButtonClickSound() => OnButtonClickSound?.Invoke(Sounds.ButtonClick);
    public void InvokeOnPlayBackGroundSound() => OnPlayBackGroundSound?.Invoke(Sounds.BgMusic);
    //public void InvokeOnShowInterstitialAd() => OnShowInterstitialAd?.Invoke();

    public void InvokeOnMuteSound()
    {
        isMute = !isMute;
        OnMuteSound?.Invoke(isMute);
    }

    public void InvokeOnLoadLevel(int id) => OnLoadLevelAction?.Invoke(id);

    // public void InvokeOnChangeableAction(ChangeableScriptableObjects changeableScriptableObjects)=>onChangeableAction?.Invoke(changeableScriptableObjects);

    #region MainScreen Menu-Buttons Event
    public void InvokeOnRateApp()=> OnRateApp?.Invoke();
    public void InvokeOnRestoreApp() => OnRestoreApp?.Invoke();
    public void InvokeOnMoreApps() => OnMoreApps?.Invoke();
    public void InvokeOnShareApp() => OnShareApp?.Invoke();
    #endregion

    #region Step -1 Camera Shake Animation

    public void InvokeOnBeforeCameraShake() => OnBeforeCameraShake?.Invoke();
    public void InvokeOnCameraShakeAnimation() => OnCameraShakeAnimation?.Invoke();
    #endregion

    public void InvokeCameraShakeComplete() => OnCameraShakeComplete?.Invoke();


    public void InvokeDeleteToolOnCorrect(GameObject go)
    {
        DeleteToolOnCorrect?.Invoke(go);
    }
    public void InvokeOnMouseDownHideToolList()
    {
        print("InvokeOnMouseDownHideToolList");
        OnMouseDownHideToolList?.Invoke();   
    }


    public void InvokeOnMouseUpShowToolList()
    {
        print("InvokeOnMouseUpShowToolList");
        OnMouseUpShowToolList?.Invoke();
    }

    public void InvokeOnRightRepair(GameObject go) => OnRightRepair.Invoke(go);

    public void InvokeOnWrongRepair(GameObject go) => OnWrongRepair.Invoke(go);

    #region Vibrate Device
    public void InvokeOnDeviceVibrate() => OnDeviceVibrate?.Invoke();
    #endregion

    public void InvokeOnImageSwapAnimation(GameObject gameObject,Sprite[] sprites,int setLoop)
        =>OnImageSwapAnimation?.Invoke(gameObject,sprites, setLoop);

    public void InvokeOnRotateOnZaxis(GameObject gameObject)=> OnRotateOnZaxis?.Invoke(gameObject);

    #region Dotween Animation
    #region DOMoveAnimation
    public void InvokeOnAnimationDoMove(GameObject parentGO,GameObject childGO)
        => OnAnimationDoMove?.Invoke(parentGO,childGO);

    public void InvokeToolMoveAnimation(GameObject parentGO, GameObject childGO)
        => ToolMoveAnimation?.Invoke(parentGO, childGO);
    #endregion
    #endregion
}
