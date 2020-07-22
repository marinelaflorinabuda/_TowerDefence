using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


public struct FixedRateSpawner : IComponentData
{
    public Entity Prefab;
}

[DisableAutoCreation]
public class FixedRateSpawnerSystem : ComponentSystem
{
    EntityQuery mainGroup;
    protected override void OnCreate()
    {
        mainGroup = GetEntityQuery(
            ComponentType.ReadOnly<FixedRateSpawner>(),
            ComponentType.ReadOnly<LocalToWorld>());
    }

    protected override void OnUpdate()
    {
        Entities.ForEach((Entity spawnerEntity, ref FixedRateSpawner spawnerData, ref LocalToWorld localToWord, ref Rotation rotation) =>
        {
            var spawnTime = UnityEngine.Time.time;//UnityEngine.Time.time
            var newEntity = PostUpdateCommands.Instantiate(spawnerData.Prefab);
            PostUpdateCommands.SetComponent(newEntity, new Translation {  Value = localToWord.Position });
            PostUpdateCommands.SetComponent(newEntity, new Rotation {  Value = rotation.Value });
            PostUpdateCommands.SetComponent(newEntity, new ProjectileSpawnTime { SpawnTime = spawnTime });

        });
    }
}

