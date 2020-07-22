using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

//[UpdateAfter(typeof(MoveForwardSystem))]
//[UpdateBefore(typeof(TimedDestroySystem))]
public class CollisionSystem : JobComponentSystem
{
	EntityQuery enemyGroup;
	EntityQuery projectileGroup;
	EntityQuery mainTowerGroup;

	protected override void OnCreate()
	{
		mainTowerGroup = GetEntityQuery(typeof(Health), ComponentType.ReadOnly<Translation>(), ComponentType.ReadOnly<MainTowerTag>());
		enemyGroup = GetEntityQuery(typeof(Health), ComponentType.ReadOnly<Translation>(), ComponentType.ReadOnly<EnemyTag>());
		projectileGroup = GetEntityQuery(typeof(ProjectileTag), ComponentType.ReadOnly<Translation>());
	}

	//[BurstCompile]
	struct CollisionJob : IJobChunk
	{
		public float radius;

		public ArchetypeChunkComponentType<Health> healthType;
		[ReadOnly] public ArchetypeChunkComponentType<Translation> translationType;

		[DeallocateOnJobCompletion]
		[ReadOnly] public NativeArray<Translation> transToTestAgainst;


		public void Execute(ArchetypeChunk chunk, int chunkIndex, int firstEntityIndex)
		{
			var chunkHealths = chunk.GetNativeArray(healthType);
			var chunkTranslations = chunk.GetNativeArray(translationType);

			for (int i = 0; i < chunk.Count; i++)
			{
				float damage = 0f;
				Health health = chunkHealths[i];
				Translation pos = chunkTranslations[i];

				for (int j = 0; j < transToTestAgainst.Length; j++)
				{
					Translation pos2 = transToTestAgainst[j];

					if (CheckCollision(pos.Value, pos2.Value, radius))
					{
						damage += 1;
					}
				}

				if (damage > 0)
				{
					health.Value -= damage;
					chunkHealths[i] = health;
				}
			}
		}
	}

	protected override JobHandle OnUpdate(JobHandle inputDependencies)
	{
		var healthType = GetArchetypeChunkComponentType<Health>(false);
		var translationType = GetArchetypeChunkComponentType<Translation>(true);

		float enemyRadius = GameSettings.EnemyCollisionRadius;
		float towerRadius = GameSettings.TowerCollisionRadius;

		var jobEvB = new CollisionJob()
		{
			radius = enemyRadius * enemyRadius,
			healthType = healthType,
			translationType = translationType,
			transToTestAgainst = projectileGroup.ToComponentDataArray<Translation>(Allocator.TempJob)
		};

		//Debug.Log("jobEvB.radius : " + jobEvB.radius);
		//Debug.Log("jobEvB.healthType : " + jobEvB.healthType);
		//Debug.Log("jobEvB.translationType : " + jobEvB.translationType);
		//Debug.Log("jobEvB.transToTestAgainst : " + jobEvB.transToTestAgainst);

		JobHandle jobHandle = jobEvB.Schedule(enemyGroup, inputDependencies);

		if (GameSettings.IsTowerDown())
		{
			//Debug.Log("Tower is down");
			return jobHandle;
		}

		var jobPvE = new CollisionJob()
		{
			radius = towerRadius * towerRadius,
			healthType = healthType,
			translationType = translationType,
			transToTestAgainst = enemyGroup.ToComponentDataArray<Translation>(Allocator.TempJob)
		};
		
			//Debug.Log("jobPvE.radius : " + jobPvE.radius );
			//Debug.Log("jobPvE.healthType : "+ jobPvE.healthType);
			//Debug.Log("jobPvE.translationType : "+ jobPvE.translationType);
			//Debug.Log("jobPvE.transToTestAgainst : "+ jobPvE.transToTestAgainst);
		

		return jobPvE.Schedule(mainTowerGroup, jobHandle);
	}

	static bool CheckCollision(float3 posA, float3 posB, float radiusSqr)
	{
		float3 delta = posA - posB;

		float distanceSquare = delta.x * delta.x + delta.z * delta.z;

		return distanceSquare <= radiusSqr;
	}
}