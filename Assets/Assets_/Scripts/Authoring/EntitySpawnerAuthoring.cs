using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class EntitySpawnerAuthoring : MonoBehaviour
{
    [SerializeField] private GameObject towerLibraryReference;
    [SerializeField] private GameObject enemyLibraryReference;
    [SerializeField] private GameObject projectibleLibraryReference;
    private EntityManager entityManager;
    
    private void Awake()
    {
        entityManager = World.Active.EntityManager;
    }
    [BurstCompile]
    private Entity CreateEntityFromPrefab(GameObject _prefab, Vector3 _spawnPosition)
    {
        var entityFromPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(_prefab, World.Active);
        var instance = entityManager.Instantiate(entityFromPrefab);
        

        entityManager.SetComponentData(instance, new Translation{Value = _spawnPosition});

        return instance;
    }
    [BurstCompile]
    private NativeArray<Entity> CreateEntitiesFromPrefab(GameObject _prefab, Vector3[] _spawnPositions)
    {
        NativeArray<Entity> nativeArray = new NativeArray<Entity>(_spawnPositions.Length, Allocator.Temp);
        var entityFromPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(_prefab, World.Active);
        for (int i = 0; i < _spawnPositions.Length; i++)
        {
            var instance = entityManager.Instantiate(entityFromPrefab);
            entityManager.SetComponentData(instance, new Translation{Value = _spawnPositions[i]});
            entityManager.SetComponentData(instance, new Rotation{Value = Quaternion.identity});
            nativeArray[i] = instance;
        }
        return nativeArray;
    }

    public Entity CreateDefenderAt(Vector3 _spawnPosition)
    {
        Entity instance = CreateEntityFromPrefab(towerLibraryReference, _spawnPosition);
        return instance;
    }

    //public Entity CreateProjectibleAt(Vector3 _spawnPosition)
    //{
    //    Entity instance = CreateEntityFromPrefab(projectibleLibraryReference, _spawnPosition);
    //    return instance;
    //}

    public Entity CreateEnemyAt(Vector3 _spawnPosition)
    {
        Entity instance = CreateEntityFromPrefab(enemyLibraryReference, _spawnPosition);
        return instance;
    }

    public Entity CreateProjectibleAt(Vector3 _spawnPosition)
    {
        Entity instance = CreateEntityFromPrefab(projectibleLibraryReference, _spawnPosition);
        return instance;
    }

    public void CreateEnemyAt(Vector3[] _spawnPositions)
    {
        var tanks = CreateEntitiesFromPrefab(enemyLibraryReference, _spawnPositions);
        foreach (var tank in tanks)
        {
            entityManager.AddComponentData(tank,new RotateTowardTargetComponent
            {
                rotationSpeed = .5f
            });
            
            entityManager.AddComponentData(tank,new EnemyTag
            {
                
            });
            
            entityManager.AddComponentData(tank,new MoveToPositionComponent
            {
                movingSpeed = 5f
            });
            
            entityManager.AddComponentData(tank,new TargetComponent()
            {
                
            });
            entityManager.AddComponentData(tank, new Health
            {
                Value = 5f
            });
        }
    }
    
}
