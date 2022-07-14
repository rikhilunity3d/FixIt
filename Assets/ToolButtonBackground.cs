
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;// Required when using Event data.


public class ToolButtonBackground : MonoBehaviour,IPointerDownHandler
{
    public Button ToolButton;
    public Image ToolImage;
    public int index = 0;
    public GameObject ToolToBeInstantiate;
    [HideInInspector]
    public GameObject ToolSpawnPoint;

    private void Start()
    {
        index++;
     //   ToolButton.onClick.AddListener(()=>HandleToolButtonClick(index));
       
    }

    void HandleToolButtonClick(int index)
    {
        Debug.Log("Button Clicked on "+index);
        // Instantiate Tool on Button Click
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(this.gameObject.name + " Was Clicked.");
        //var go = Instantiate(ToolToBeInstantiate, ToolSpawnPoint.transform);
        Transform parentGoTransform = ToolToBeInstantiate.transform.parent;
        if(ToolSpawnPoint.gameObject.transform.childCount > 0)
        {
            ToolSpawnPoint.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            ToolSpawnPoint.gameObject.transform.GetChild(0).gameObject.transform.parent = parentGoTransform;

        }
            var go = ToolToBeInstantiate;
            go.SetActive(true);
            ToolToBeInstantiate.transform.parent = ToolSpawnPoint.transform;
            go.transform.localPosition = new Vector3(0f, 0f, 0f);
            //go.transform.localScale = new Vector3(100f, 100f, 100f);
            go.SetActive(true);
            HandleToolButtonClick(index);
    }

   
}
