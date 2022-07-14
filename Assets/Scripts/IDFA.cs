using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class IDFA : MonoBehaviour
{
#if UNITY_IOS
    [DllImport("__Internal")]
    private static extern void _requestIDFA();
#endif


    private void Start()
    {
#if UNITY_IOS
        _requestIDFA();
#endif
    }
}
