using UnityEngine;

interface IElement
{
    public void OnBeforeCameraShake();
    public void OnCameraShake();
    public void OnRightRepairFunc(GameObject go);
    public void OnWrongRepairFunc(GameObject go);
}
