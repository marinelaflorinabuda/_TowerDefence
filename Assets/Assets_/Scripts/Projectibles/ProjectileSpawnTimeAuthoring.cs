using System;
using Unity.Entities;
using UnityEngine;

[RequiresEntityConversion]
public class ProjectileSpawnTimeAuthoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public float SpawnTime;

    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var data = new ProjectileSpawnTime { SpawnTime = SpawnTime };

        dstManager.AddComponentData(entity, data);
    }
}
