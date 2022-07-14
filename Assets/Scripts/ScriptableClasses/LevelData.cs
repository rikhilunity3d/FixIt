using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "ScriptableObjects/LevelData", order = 1)]
public class LevelData : ScriptableObject
{
    public string levelName;
    public Sprite levelUnlockImage;
    public Sprite levelLockImage;
    public bool isLevelUnlock;
    
}
