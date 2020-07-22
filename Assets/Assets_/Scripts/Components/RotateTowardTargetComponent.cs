using Unity.Entities;

public struct RotateTowardTargetComponent : IComponentData
{
    public float rotationSpeed;
    public Entity targetToRotateToward;
}
