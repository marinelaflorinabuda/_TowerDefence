using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

//[BurstCompile]
public class MoveForwardSystem : ComponentSystem
{
    [BurstCompile]
    protected override void OnUpdate()
    {
        Entities.ForEach((Entity entity, ref MoveToPositionComponent moveToPosition, ref Translation translation,
            ref Rotation rotation) =>
        {
            float3 directionOfMove = math.mul(rotation.Value, Vector3.forward);
            translation.Value += directionOfMove * moveToPosition.movingSpeed * Time.deltaTime;
        });
    }
}
