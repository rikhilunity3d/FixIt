using UnityEngine;

public class HouseElement : MonoBehaviour,IElement
{
    [SerializeField]
    int elementId;

    [SerializeField]
    ElementType elementType;

    public ToolType toolType;

    [SerializeField]
    AnimationType animationType;

    [SerializeField]
    GameObject correctSign, wrongSign;

    [SerializeField]
    bool isShowDefault = false;
    
    [SerializeField]
    Sprite[] animationImages;

    [SerializeField]
    int setLoop;

    [SerializeField]
    int orderInLayer;

    ParticleSystem whileCameraShakePS;  

    public void Start()
    {
        

        
    }
    private void OnEnable()
    {
        EventHandler.OnBeforeCameraShake += OnBeforeCameraShake;
        EventHandler.OnCameraShakeAnimation += OnCameraShake;
        EventHandler.OnCameraShakeComplete += OnCameraShakeComplete;
        EventHandler.OnRightRepair += OnRightRepairFunc;
        EventHandler.OnWrongRepair += OnWrongRepairFunc;
        //gameObject.AddComponent<SpriteRenderer>().sprite = defaultSprite;
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = orderInLayer;
        //gameObject.AddComponent<BoxCollider2D>();
    }
    private void OnDisable()
    {
        EventHandler.OnBeforeCameraShake -= OnBeforeCameraShake;
        EventHandler.OnCameraShakeAnimation -= OnCameraShake;
        EventHandler.OnCameraShakeComplete -= OnCameraShakeComplete;
        EventHandler.OnRightRepair -= OnRightRepairFunc;
        EventHandler.OnWrongRepair -= OnWrongRepairFunc;
    }

    public void OnBeforeCameraShake()
    {
        if (!isShowDefault)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = isShowDefault;
            gameObject.GetComponent<PolygonCollider2D>().enabled = isShowDefault;
        }
        DisableEnableChildGameObject(gameObject, false);
        print("OnBeforeCameraShake called " + gameObject.name + " show value is = " + isShowDefault.ToString());
    }

    public void OnCameraShakeComplete()
    {
        if(!isShowDefault)
        {
            isShowDefault = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = isShowDefault;
            //gameObject.GetComponent<BoxCollider2D>().enabled = isShowDefault;
        }
        print("OnCameraShakeComplete called " + gameObject.name + " show value is = " + isShowDefault.ToString());
        gameObject.GetComponent<SpriteRenderer>().sprite = animationImages[animationImages.Length-1]; 
    }

    public void OnCameraShake()
    {
        DisableEnableChildGameObject(this.gameObject, true);

        whileCameraShakePS = gameObject.GetComponentInChildren<ParticleSystem>();
        if(whileCameraShakePS!= null)
        {
            whileCameraShakePS.Play();
        }
        switch (animationType)
        {
            case AnimationType.InvokeOnImageSwapAnimation:
                EventHandler.Instance.InvokeOnImageSwapAnimation(this.gameObject, animationImages, setLoop);
                break;

            case AnimationType.InvokeOnRotateOnZaxis:
                EventHandler.Instance.InvokeOnRotateOnZaxis(this.gameObject);
                break;

            default:
                print("No Animation");
                break;
        }
    }

    public void DisableEnableChildGameObject(GameObject parentGO, bool flag)
    {
        int childCount = parentGO.transform.childCount;
        if(childCount == 0)
        {
            parentGO.GetComponent<PolygonCollider2D>().enabled = true;
        }
        for (int i = 0; i < childCount; i++)
        {
            if (parentGO.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>() != null)
            {
                parentGO.transform.GetChild(i).gameObject.GetComponent<SpriteRenderer>().enabled = flag;

                if(parentGO.transform.GetChild(i).gameObject.GetComponent<PolygonCollider2D>() == null)
                {
                    parentGO.GetComponent<PolygonCollider2D>().enabled = true;

                    parentGO.transform.GetChild(i).gameObject.AddComponent<PolygonCollider2D>().enabled = flag;
                    
                }
                else
                {
                    parentGO.GetComponent<PolygonCollider2D>().enabled = true;
                    parentGO.transform.GetChild(i).gameObject.GetComponent<PolygonCollider2D>().enabled = flag;
                    
                }
            }
        }
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


    public void OnRightRepairFunc(GameObject go)
    {
        // Remove Collider from this GameObject
        Destroy(go.GetComponent<PolygonCollider2D>());
        // Play awesome, fabulos sound 
        // Show Correct Sign Here
        SetParent(go,correctSign);
        EventHandler.Instance.InvokeOnAnimationDoMove(go, correctSign);
        // Add Score
        // Add Coins
        // Remove Tool from ToolsList
    }

    public void OnWrongRepairFunc(GameObject go)
    {
        // wrong sign animation
        SetParent(go,wrongSign);
        EventHandler.Instance.InvokeOnAnimationDoMove(gameObject, wrongSign);

        //await new WaitForSeconds(1.5f);
        EventHandler.Instance.InvokeOnDeviceVibrate();
    }
}
