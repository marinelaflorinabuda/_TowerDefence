using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Unity.Burst;
//[BurstCompile]
public class RotateTowardTargetSystem : ComponentSystem
{
    [BurstCompile]
    protected override void OnUpdate()
    {
        ComponentDataFromEntity<Translation> translations = GetComponentDataFromEntity<Translation>();

        ComponentDataFromEntity<LocalToWorld> localToWorlds = GetComponentDataFromEntity<LocalToWorld>();

        Entities.ForEach((Entity entity, ref RotateTowardTargetComponent rotationTowardTarget, ref Translation
            translation, ref TargetComponent targetComponent, ref Rotation rotation) =>
        {
            float3 targetPosition = float3.zero;

            if (EntityManager.Exists(targetComponent.target))
            {
                targetPosition = translations[targetComponent.target].Value;
            }

            float3 direction = targetPosition - translation.Value;
            
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            
            rotation.Value = Quaternion.Lerp(rotation.Value, targetRotation, rotationTowardTarget
            .rotationSpeed*Time.deltaTime);
        });


        Entities.ForEach((Entity entity, ref RotateTowardTargetComponent rotationTowardTarget, ref Translation translation, ref Rotation rotation, ref LocalToWorld localToWorld, ref Parent parent)
            =>
        {
            if(EntityManager.Exists(rotationTowardTarget.targetToRotateToward)&&
            EntityManager.HasComponent<Translation>(rotationTowardTarget.targetToRotateToward))
            {
                float3 targetDirectionInWorldSpace = translations[rotationTowardTarget.targetToRotateToward].Value -
                localToWorld.Position;

                Quaternion targetRotationInWorldSpace = Quaternion.LookRotation(targetDirectionInWorldSpace);

                LocalToWorld parentLocalToWord = localToWorlds[parent.Value];
                quaternion parentWorldRotation = Quaternion.LookRotation(parentLocalToWord.Forward, parentLocalToWord.Up);

                quaternion relativeRotationOfTowels = math.mul(targetRotationInWorldSpace, math.inverse(parentWorldRotation));

                rotation.Value = Quaternion.Lerp(rotation.Value, relativeRotationOfTowels, rotationTowardTarget.rotationSpeed * Time.deltaTime);
            }
        });

    }
}
