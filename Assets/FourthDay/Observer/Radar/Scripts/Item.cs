using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public EventSO droppedEvent;
    public EventSO pickUpEvent;
    public Image icon;
    Transform player;

    void Start()
    {
        droppedEvent.Occurred(gameObject);

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            pickUpEvent.Occurred(gameObject);
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            Destroy(gameObject, 3f);
        }
    }
}