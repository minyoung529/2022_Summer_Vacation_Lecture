using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameEnviroment
{
    private static GameEnviroment instance;
    private static GameEnviroment Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new GameEnviroment();
                instance.checkPoints.AddRange(GameObject.FindGameObjectsWithTag("Checkpoint"));

                instance.checkPoints = instance.checkPoints.OrderBy(wayPoints => wayPoints.name).ToList();
            }
        }
    }

    public List<GameObject> checkPoints = new List<GameObject>();
}
