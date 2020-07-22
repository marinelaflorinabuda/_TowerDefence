using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[RequiresEntityConversion]
public class ProjectibleConverterAutoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
    {
        //entityManager.AddComponentData(entity, new RotateTowardTargetComponent
        //{
        //    rotationSpeed = .5f
        //});

        entityManager.AddComponentData(entity, new TimeToLive
        {
            Value = 5f
        });

        entityManager.AddComponentData(entity, new MoveToPositionComponent
        {
            movingSpeed = 5f
        });
        entityManager.AddComponentData(entity, new ProjectileTag());
        
    }
}


