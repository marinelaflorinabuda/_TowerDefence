using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
//[BurstCompile]
public class TowerInitializationSystem : ComponentSystem
{
    [BurstCompile]
    protected override void OnUpdate()
    {
        Entities.WithAll<TowerInitializationComponent>().ForEach(entity =>
        {
            PostUpdateCommands.RemoveComponent(entity, typeof(TowerInitializationComponent));
        });

    }
}
