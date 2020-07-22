
using Unity.Entities;
using UnityEngine;
using Unity.Burst;

public class CheckIfHasTargetSystem : ComponentSystem
{
    [BurstCompile]
    protected override void OnUpdate()
    {
        Entities.WithNone(typeof(HasNoTargetTag)).ForEach((Entity entity, ref WeaponComponent weapon) =>
        {
           
            if (EntityManager.Exists(weapon.target) == false)
            {
                PostUpdateCommands.AddComponent(entity, new HasNoTargetTag());
                //Debug.Log("added HasNoTargetTag");
            }
        });

    }
}
