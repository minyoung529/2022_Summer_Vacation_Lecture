using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject eggPrefab;
    public GameObject medicPrefab;

    public Terrain terrain;
    private TerrainData terrainData;

    public enum eItemsPrefab
    {
        Egg,
        Medic
    }

    private Dictionary<eItemsPrefab, GameObject> itemPrefabDic = new Dictionary<eItemsPrefab, GameObject>();

    private void Awake()
    {
        itemPrefabDic.Add(eItemsPrefab.Egg, eggPrefab);
        itemPrefabDic.Add(eItemsPrefab.Medic, medicPrefab);
    }

    private void Start()
    {
        terrainData = terrain.terrainData;

        InvokeRepeating("CreateEgg", 1, 1);
    }

    private void CreateEgg()
    {
        SelectItemPrefab(eItemsPrefab.Egg);
    }

    private void SelectItemPrefab(eItemsPrefab prefab)
    {
        int x = (int)Random.Range(0f, terrainData.size.x);
        int z = (int)Random.Range(0f, terrainData.size.z);

        Vector3 pos = new Vector3(x, 0f, z);
        pos.y = terrain.SampleHeight(pos) + 10f;

        GameObject itemObj = Instantiate(itemPrefabDic[prefab], pos, Quaternion.identity);
        itemObj.transform.SetParent(transform); 
    }
}
