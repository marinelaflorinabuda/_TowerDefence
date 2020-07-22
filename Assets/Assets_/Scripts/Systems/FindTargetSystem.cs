
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Unity.Burst;
//[BurstCompile]
public class FindTargetSystem : ComponentSystem
{
    [BurstCompile]
    protected override void OnUpdate()
    {
        Entities.WithAll(typeof(HasNoTargetTag)).WithNone<TowerInitializationComponent>().WithNone<WeaponCoolDown>().ForEach((
            Entity entity, ref LocalToWorld localToWorld,
            ref WeaponComponent weapon) =>
            {
                float3 defenderPosition = localToWorld.Position;
                float closestTargetDistance = weapon.maxDistanceDetected;

                Entity closestTarget = Entity.Null;

                Entities.WithAll(typeof(EnemyTag)).ForEach((Entity potentialTarget, ref Translation potentialTargetTranslation) =>
                {
                    float currentDistance = math.distance(defenderPosition, potentialTargetTranslation.Value);
                    if (currentDistance < closestTargetDistance)
                    {
                        closestTargetDistance = currentDistance;
                        closestTarget = potentialTarget;
                    }
                });

                if (EntityManager.Exists(closestTarget))
                {

                    PostUpdateCommands.RemoveComponent(entity, typeof(HasNoTargetTag));
                    PostUpdateCommands.SetComponent(entity, new RotateTowardTargetComponent
                    {
                        rotationSpeed = 10,
                        targetToRotateToward = closestTarget

                    });
                    weapon.target = closestTarget;
                }
        });
    }
}

