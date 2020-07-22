using Unity.Entities;
using UnityEngine;

public struct MoveToPositionComponent : IComponentData
{
    public float movingSpeed;
    public Vector3 destinationToMoveTo;
}