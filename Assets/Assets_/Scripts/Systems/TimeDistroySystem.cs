using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
public class TimedDestroySystem : JobComponentSystem
{
    EndSimulationEntityCommandBufferSystem bufferSystem;

    protected override void OnCreate()
    {
        bufferSystem = World.Active.GetOrCreateSystem<EndSimulationEntityCommandBufferSystem>();
    }
    [BurstCompile]
    struct DiscardJob : IJobForEachWithEntity<TimeToLive>
    {
        public EntityCommandBuffer.Concurrent bufferCommandBuffer;
        public float mainThreadDeltaTime;

        public void Execute(Entity entity, int jobIndex, ref TimeToLive timeToLive)
        {
            timeToLive.Value -= mainThreadDeltaTime;
            if (timeToLive.Value <= 0f)
                bufferCommandBuffer.DestroyEntity(jobIndex, entity);
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new DiscardJob
        {
            bufferCommandBuffer = bufferSystem.CreateCommandBuffer().ToConcurrent(),
            mainThreadDeltaTime = Time.deltaTime
        };

        var handle = job.Schedule(this, inputDeps);
        bufferSystem.AddJobHandleForProducer(handle);

        return handle;
    }
}