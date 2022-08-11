using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventSO", menuName = "GameEvent", order = 0)]
public class EventSO : ScriptableObject         
{
    private List<EventListner> eventListners = new List<EventListner>();

    // 등록
    public void Register(EventListner listner)
    {
        eventListners.Add(listner);
    }

    // 해지
    public void UnRegister(EventListner listner)
    {
        eventListners.Remove(listner);
    }
    
    // 발생
    public void Occurred(GameObject obj)
    {
        for(int i = 0; i < eventListners.Count; i++)
        {
            eventListners[i].OnEventOccurs(obj);
        }
    }
}
