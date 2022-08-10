using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateCubeWorld : MonoBehaviour
{
    public int width;
    public int depth;
    public GameObject cubePrefab;

    private void Start()
    {
        for(int x = 0; x < width; x++)
        {
            for(int z = 0; z < depth; z++)
            {
                Vector3 pos = new Vector3(x, Mathf.PerlinNoise(x * 0.2f, z * 0.2f) * 3, z);
                /*GameObject obj =*/ Instantiate(cubePrefab, pos, Quaternion.identity);
            }
        }
    }
}
