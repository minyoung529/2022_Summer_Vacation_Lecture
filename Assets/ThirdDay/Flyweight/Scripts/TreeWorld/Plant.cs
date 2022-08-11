using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Plant : MonoBehaviour
{
    [SerializeField]
    private PlantDataSO plantInfo;

    private SetPlantInfo setPlantInfo;

    private void Start()
    {
        setPlantInfo = FindObjectOfType<SetPlantInfo>();

        plantInfo.SetRandomName();
        plantInfo.SetRandomThreat();
    }

    private void OnMouseDown()
    {
        setPlantInfo.plantTexture.texture = plantInfo.Icon;
        setPlantInfo.planeName.text = plantInfo.Name;
        setPlantInfo.phreatLevel.text = plantInfo.Threat.ToString();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (plantInfo.Threat >= PlantDataSO.eThreat.Moderate)
            {
                PlayerController.dead = true;
            }
        }
    }
}