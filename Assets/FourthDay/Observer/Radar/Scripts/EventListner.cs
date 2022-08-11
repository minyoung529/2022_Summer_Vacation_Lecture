using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class UnityGameObjectEvent : UnityEvent<GameObject>
{

}

public class EventListner : MonoBehaviour
{
    public EventSO globalEvent;
    public UnityGameObjectEvent responseObject = new UnityGameObjectEvent();

    // 등록
    private void OnEnable()
    {
        globalEvent.Register(this);
    }

    // 해지
    private void OnDisable()
    {
        globalEvent.UnRegister(this);
    }

    // 발생
    public void OnEventOccurs(GameObject obj)
    {
        responseObject.Invoke(obj);
    }
}
