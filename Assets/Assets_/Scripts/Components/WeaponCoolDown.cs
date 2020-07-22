using System;
using Unity.Entities;

[Serializable]
public struct WeaponCoolDown : IComponentData
{
    public float Value;
}