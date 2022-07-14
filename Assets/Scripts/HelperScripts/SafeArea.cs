using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeArea : MonoBehaviour
{
    RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        SafeAreaUpdate();

    }

    private void SafeAreaUpdate()
    {
        Vector2 anchorMin = Screen.safeArea.position;
        Vector2 anchorMax = Screen.safeArea.position + Screen.safeArea.size;

        anchorMin.x /= Screen.width;
        anchorMin.y /= Screen.height;
        anchorMax.x /= Screen.width;
        anchorMax.y /= Screen.height;

        rectTransform.anchorMin = anchorMin;
        rectTransform.anchorMax = anchorMax;

    }
}
