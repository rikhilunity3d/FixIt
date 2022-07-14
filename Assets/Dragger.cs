using UnityEngine;

public class Dragger : MonoBehaviour
{
    [SerializeField]
    public ToolType toolType;
    [SerializeField]
    private AnimationType animationType;
    private Vector3 dragOffset;
    private Vector3 defaultLocation;
    private Camera cam;
    private Answer Correct = Answer.NONE;
    GameObject dropOnElement;
    
    
    private void Awake() => cam = Camera.main;
    
    private void OnMouseDown()
    {
        defaultLocation = transform.position;
        EventHandler.Instance.InvokeOnMouseDownHideToolList();
        dragOffset = transform.position - GetMousePosition();
        gameObject.SetActive(true);
        Correct = Answer.NONE;
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePosition() + dragOffset;
        gameObject.SetActive(true);
        Correct = Answer.NONE;
    }

    private void OnMouseUp()
    {
        print("OnMouseUp");
        switch (Correct)
        {    
            case Answer.TRUE:
                print("This " + gameObject.name + " collide with correct");
                switch (animationType)
                {
                    case AnimationType.InvokeToolMoveAnimation:
                        SetParent(dropOnElement, this.gameObject);
                        EventHandler.Instance.InvokeToolMoveAnimation(dropOnElement, this.gameObject);
                        break;
                }
                EventHandler.Instance.InvokeOnRightRepair(dropOnElement);
                
                EventHandler.Instance.InvokeDeleteToolOnCorrect(gameObject);
                //gameObject.SetActive(false);
                break;

            case Answer.FALSE:
                print("This " + gameObject.name + " collide with wrong");
                transform.position = defaultLocation;
                print("OnMouseUp " + transform.position + " & " + defaultLocation);
                EventHandler.Instance.InvokeOnWrongRepair(dropOnElement);
                gameObject.SetActive(true);
                break;

            case Answer.NONE:
                transform.position = defaultLocation;
                break;   
        }
        //EventHandler.Instance.InvokeOnMouseUpShowToolList();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<HouseElement>() !=null)
        {
            dropOnElement = collision.gameObject;
            if (collision.gameObject.GetComponent<HouseElement>().toolType == toolType)
            {
                Correct = Answer.TRUE;
                // then play animation of tool if applicable
                
            }
            else
            {
                Correct = Answer.FALSE;
                // Reduce player's life here if life is zero then show gameover
                // panel and also give Rewarded video ad to increase life.
            }
        }
    }
    
    Vector3 GetMousePosition()
    {
        var mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        return mousePosition;
    }

    public void SetParent(GameObject parentGO, GameObject correctSign)
    {
        //Makes the GameObject "newParent" the parent of the GameObject "player".
        correctSign.transform.parent = parentGO.transform;

        //Display the parent's name in the console.
        Debug.Log("Player's Parent: " + correctSign.transform.parent.name);

        // Check if the new parent has a parent GameObject.
        if (parentGO.transform.parent != null)
        {
            //Display the name of the grand parent of the player.
            Debug.Log(correctSign.gameObject.name + "'s" + correctSign.transform.parent.parent.name);
        }
    }
}
