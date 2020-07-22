using Unity.Entities;

public struct WeaponComponent : IComponentData
{
    public float maxDistanceDetected;
    public Entity target;
}