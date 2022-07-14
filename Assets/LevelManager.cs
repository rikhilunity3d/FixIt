using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LevelManager : GenericSingleton<LevelManager>
{
    // Play time will be store in LevelDataSO. because diffrernt level can have
    // different time.
    

    [SerializeField]
    int playTime = 90;
    [SerializeField]
    float waitTimeBeforeCameraShake = 2f;

    [SerializeField]
    SpriteRenderer backgroundImage;

    [SerializeField]
    List<GameObject> ToolList;

    
     
    [SerializeField] GameObject toolButtonBackGround, toolButtonHG;
    [SerializeField] GameObject ToolSpawnPoint;

    [SerializeField]
    GameObject buttonPrefab, buttonParent;
    override protected void Awake()
    {
        base.Awake();
        SetCameraOrthographicSize();
    }


    private void Start()
    {
        // Update Play Time in UIManager
        GenerateToolsButtons();
        WaitBeforeCameraShake(waitTimeBeforeCameraShake);

    }

    private async void WaitBeforeCameraShake(float waitTimeBeforeCameraShake)
    {
        
        EventHandler.Instance.InvokeOnBeforeCameraShake();

        await new WaitForSeconds(waitTimeBeforeCameraShake);

        EventHandler.Instance.InvokeOnCameraShakeAnimation();
        EventHandler.Instance.InvokeOnDeviceVibrate();        
    }

    private void OnEnable()
    {

        EventHandler.OnDeviceVibrate += ListenerOnDeviceVibrate;
        EventHandler.OnMouseUpShowToolList += ListenerMouseUpShowToolList;
        EventHandler.DeleteToolOnCorrect += ListenerDeleteToolOnCorrect;
        EventHandler.OnMouseDownHideToolList += ListenerOnMouseDownHideToolList;

        //GenerateLevelButtons();

        
    }

    private void OnDisable()
    {
        EventHandler.OnDeviceVibrate -= ListenerOnDeviceVibrate;
        EventHandler.OnMouseUpShowToolList -= ListenerMouseUpShowToolList;
        EventHandler.DeleteToolOnCorrect -= ListenerDeleteToolOnCorrect;
        EventHandler.OnMouseDownHideToolList -= ListenerOnMouseDownHideToolList;

    }

    // Common Functions


    void GenerateToolsButtons()
    {
        for (int i = 0; i < ToolList.Count; i++)
        {
            GameObject toolButton = Instantiate(toolButtonBackGround, toolButtonHG.transform);
            toolButton.GetComponent<ToolButtonBackground>().ToolImage.sprite = ToolList[i].gameObject.GetComponent<SpriteRenderer>().sprite;
            toolButton.GetComponent<ToolButtonBackground>().index = i;
            toolButton.GetComponent<ToolButtonBackground>().ToolToBeInstantiate = ToolList[i].gameObject;
            toolButton.GetComponent<ToolButtonBackground>().ToolSpawnPoint = ToolSpawnPoint;
        }
    }

    

    void ListenerDeleteToolOnCorrect(GameObject go)
    {
        for(int i =0; i < ToolList.Count;i++)
        {
            if (go.name.Equals(ToolList[i].name))
            {
                ToolList.RemoveAt(i);
            }
        }
    }

    void ListenerMouseUpShowToolList()
    {
        print("ListenerMouseUpShowToolList is in LevelManager");
        foreach (GameObject i in ToolList)
        {
            i.SetActive(true);
        }    
    }

    void ListenerOnMouseDownHideToolList()
    {
        print("ListenerOnMouseDownHideToolList is in LevelManager");
        foreach (GameObject i in ToolList)
        {
            i.SetActive(false);
        }
    }

    #region Vibrate Device
    void ListenerOnDeviceVibrate()
    {
        Debug.Log("Vibrate Device Done");
        Handheld.Vibrate();
    }

    private void SetCameraOrthographicSize()
    {
        // Setting camera orthographic size accroding to the background image of
        //Game.If you want to calculate width and height use the below commented
        //code and comment below line
        Camera.main.orthographicSize = backgroundImage.bounds.size.y / 2;

        // this code will calculate camera orthograhpic size using width and height ratio
        // uncomment this block of code

        /*float screenRatio = (float)Screen.width / (float) Screen.height;
        float targetRatio = backgroundImage.bounds.size.x / backgroundImage.bounds.size.y;
        if(screenRatio >= targetRatio)
        {
            Camera.main.orthographicSize = backgroundImage.bounds.size.y / 2;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            Camera.main.orthographicSize = backgroundImage.bounds.size.y / 2*differenceInSize;
        }*/
    }
    #endregion
}
