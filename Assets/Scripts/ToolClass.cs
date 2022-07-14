using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolClass : MonoBehaviour
{
    //Data
    Sprite correctSprite;
    virtual public void CorrectAnimation()
    {
        /* CorrectAnimation() function will call when user puts the correct tool on the
           correct repair gameobject. i.e. if fire game object is there then user
           needs to put fire extinguisher. After that correct sign will come with
           animation. for the correct sign we are taking sprite.*/
        

    }

    virtual public void WrongAnimation()
    {

    }
    public void PlayParticleEffect()
    {

    }
    public void PlayNarrativeSound()
    {

    }
    public void PlaySolutionSound()
    {

    }
}
