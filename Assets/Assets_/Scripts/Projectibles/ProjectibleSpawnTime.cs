using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

    [Serializable]
    public struct ProjectileSpawnTime : IComponentData
    {
        public float SpawnTime;
    }
