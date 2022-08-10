using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCar : MonoBehaviour
{
    public GameObject carObj;
    public GameObject mainCam;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 pos = Vector3.one * 10f;
            GameObject obj = Instantiate(carObj, pos, Quaternion.identity);
            mainCam.GetComponent<SmoothFollow>().target = obj.transform;
        }
    }
}
