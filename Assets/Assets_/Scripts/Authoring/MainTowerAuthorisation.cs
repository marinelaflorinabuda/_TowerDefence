using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

[RequiresEntityConversion]
public class MainTowerAuthorisation : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager entityManager, GameObjectConversionSystem conversionSystem)
    {
        //Debug.Log("MainTowerAuthorisation add health");

        var mainTowerHealth = new Health
        {
            Value = 5
        };

        entityManager.AddComponentData(entity, new MainTowerTag());
        entityManager.AddComponentData(entity, mainTowerHealth);
    }
}


