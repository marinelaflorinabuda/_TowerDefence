using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public class RemoveDeadSystem : ComponentSystem
{
	protected override void OnUpdate()
	{
		Entities.ForEach((Entity entity, ref Health health, ref Translation pos) =>
		{
			
			if (health.Value <= 0)
			{
				if (EntityManager.HasComponent(entity, typeof(MainTowerTag)))
				{
					GameSettings.GameOver();
				}

				else if (EntityManager.HasComponent(entity, typeof(EnemyTag)))
				{
					PostUpdateCommands.DestroyEntity(entity);
					ProjectileImpactPool.PlayProjectileImpact(pos.Value);
				}
			}
		});
	}
}