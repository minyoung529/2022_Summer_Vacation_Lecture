using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;

public class SpawnProxy : MonoBehaviour, IDeclareReferencedPrefabs, IConvertGameObjectToEntity
{
    public GameObject cubePrefab;
    public int rows;
    public int columns;

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        referencedPrefabs.Add(cubePrefab);
    }

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var spawnerData = new Spawner
        {
            prefab = conversionSystem.GetPrimaryEntity(cubePrefab),
            rows = this.rows,
            columns = this.columns
        };

        dstManager.AddComponentData(entity, spawnerData);
    }
}