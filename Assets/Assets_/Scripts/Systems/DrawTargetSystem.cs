using Unity.Entities;
using Unity.Transforms;
using UnityEngine;
using Unity.Burst;
using Unity.Mathematics;
//[BurstCompile]
public class DrawTargetSystem : ComponentSystem
{

    private const float bulletSpeed = 40f;

    EntityManager entityManager = World.Active.EntityManager;
    [BurstCompile]
    protected override void OnUpdate()
    {
        //Entities.WithNone<WeaponCoolDown>().ForEach((Entity entity, ref WeaponComponent weapon, ref LocalToWorld localToWorld) =>
        //{
        //    //Debug.Log("Foreach weapon in draw)");
        //    if (EntityManager.Exists(weapon.target) && !EntityManager.HasComponent<TimeToLive>(weapon.target) && EntityManager.HasComponent<Translation>(weapon.target))
        //    {

        //        Translation targetTranslation = EntityManager.GetComponentData<Translation>(weapon.target);//se sterge translation-ul in job?

        //        float currentDistance = math.distance(targetTranslation.Value, localToWorld.Position);

        //        if (EntityManager.HasComponent<Translation>(weapon.target))
        //        {

        //            float timeToBeDestroyed = currentDistance / bulletSpeed;

        //            Entity a = weapon.target;

        //            var timeToDestryTarget = new TimeToLive
        //            {
        //                Value = timeToBeDestroyed
        //            };
        //            weapon.target = Entity.Null;
        //            entityManager.AddComponentData(a, timeToDestryTarget);
        //            entityManager.RemoveComponent(a, typeof(EnemyTag));
        //            entityManager.AddComponentData(entity, new HasNoTargetTag());
        //            var WeaponCoolDown = new WeaponCoolDown
        //            {
        //                Value = 3
        //            };

        //            entityManager.AddComponentData(entity, WeaponCoolDown);

        //        }

        //    }
        //});
    }
}
