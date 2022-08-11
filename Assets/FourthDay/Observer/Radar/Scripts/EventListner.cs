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

    // ���
    private void OnEnable()
    {
        globalEvent.Register(this);
    }

    // ����
    private void OnDisable()
    {
        globalEvent.UnRegister(this);
    }

    // �߻�
    public void OnEventOccurs(GameObject obj)
    {
        responseObject.Invoke(obj);
    }
}
