using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
// This animation is for wood cutter tool
public class MoveBackNForthAnim : MonoBehaviour
{    
        [SerializeField]
        Vector3 addValueForMove;
        [SerializeField]
        float durationForMove;
        [SerializeField]
        int setLoop;

        private void OnEnable()
        {
            Vector3 originalPosition = transform.position;

            addValueForMove.x = originalPosition.x + addValueForMove.x;
            addValueForMove.y = originalPosition.y + addValueForMove.y;
            addValueForMove.z = originalPosition.z + addValueForMove.z;

            this.transform.DOMove(addValueForMove, durationForMove).SetEase(Ease.OutQuad).SetLoops(4);                
        }
    }

