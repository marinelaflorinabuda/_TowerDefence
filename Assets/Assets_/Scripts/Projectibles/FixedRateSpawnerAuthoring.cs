using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;


public class FixedRateSpawnerAuthoring : MonoBehaviour, IConvertGameObjectToEntity, IDeclareReferencedPrefabs
{
    public GameObject projectilePrefab;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var spawnerData = new FixedRateSpawner
        {
            Prefab = conversionSystem.GetPrimaryEntity(projectilePrefab),
        };
        dstManager.AddComponentData(entity, spawnerData);
    }

    public void DeclareReferencedPrefabs(List<GameObject> referencedPrefabs)
    {
        Debug.Log("DeclareReferencedPrefabs");
        referencedPrefabs.Add(projectilePrefab);
    }
}
