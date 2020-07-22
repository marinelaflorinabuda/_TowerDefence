using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public class AutoRemoveWeaponCoolDownSystem : JobComponentSystem
{
    EndSimulationEntityCommandBufferSystem bufferSystem;

    protected override void OnCreate()
    {
        bufferSystem = World.Active.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }
    //[BurstCompile]
    struct DiscardCoolDownJob : IJobForEachWithEntity<WeaponCoolDown>
    {
        public EntityCommandBuffer.Concurrent bufferCommandBuffer;
        public float mainThreadDeltaTime;

        public void Execute(Entity entity, int jobIndex, ref WeaponCoolDown weaponCoolDown)
        {
            weaponCoolDown.Value -= mainThreadDeltaTime;
            if (weaponCoolDown.Value <= 0f)
                bufferCommandBuffer.RemoveComponent<WeaponCoolDown>(jobIndex ,entity);

                //bufferCommandBuffer.DestroyEntity(jobIndex, entity);
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new DiscardCoolDownJob
        {
            bufferCommandBuffer = bufferSystem.CreateCommandBuffer().ToConcurrent(),
            mainThreadDeltaTime = Time.deltaTime
        };

        var handle = job.Schedule(this, inputDeps);
        bufferSystem.AddJobHandleForProducer(handle);

        return handle;
    }
}