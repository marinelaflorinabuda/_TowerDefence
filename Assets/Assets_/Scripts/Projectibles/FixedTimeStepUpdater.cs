using Unity.Entities;
using UnityEngine;
using UnityEngine.UI;

public class FixedTimeStepUpdater : MonoBehaviour
{
    FixedRateSpawnerSystem spawnerSystem;
    public Vector2 fixedTimeToSpawnRange;

    private float _fixedTimeToSpawn=3;

    void FixedUpdate()
    {
        if (spawnerSystem == null)
        {
            spawnerSystem = World.Active.GetOrCreateSystem<FixedRateSpawnerSystem>();//GetOrCreateSystem<FixedRateSpawnerSystem>();

        }
        if (!GameSettings.IsTowerDown())
        {
            _fixedTimeToSpawn = Random.Range(fixedTimeToSpawnRange.x, fixedTimeToSpawnRange.y);
            Time.fixedDeltaTime = _fixedTimeToSpawn;
            spawnerSystem.Update();
        }
    }
}
