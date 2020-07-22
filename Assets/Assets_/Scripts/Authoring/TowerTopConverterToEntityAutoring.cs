using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[RequiresEntityConversion]
public class TowerTopConverterToEntityAutoring : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
    {
        var weapon = new WeaponComponent
        {
            maxDistanceDetected = 100,
            target = Entity.Null
        };
        var rotationToward = new RotateTowardTargetComponent
        {
            rotationSpeed = 10
        };

        var towerInitialization = new TowerInitializationComponent();

        entityManager.AddComponentData(entity, weapon);
        entityManager.AddComponentData(entity, rotationToward);
        entityManager.AddComponentData(entity, towerInitialization);
    }
}


