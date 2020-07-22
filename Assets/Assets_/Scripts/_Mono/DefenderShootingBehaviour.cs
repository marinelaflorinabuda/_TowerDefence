using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public class DefenderShootingBehaviour : MonoBehaviour
{
    public bool useECS = false;

    [Header("General")]
    public ParticleSystem shotVFX;
    public AudioSource shotAudio;
    public float fireRate = .1f;

    [Header("Bullets")]
    public GameObject bulletPrefab;

    float timer;

    EntityManager manager;
    Entity bulletEntityPrefab;

    void Start()
    {
        manager = World.Active.EntityManager;
        bulletEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(bulletPrefab, World.Active);
    }

    void Update()
    {
        Debug.Log("UpdateInDefender");
        timer += Time.deltaTime;

        if (timer >= fireRate)
        {
            Vector3 rotation = transform.rotation.eulerAngles;//towerRotation
            rotation.x = 0f;

            SpawnProjectibletECS(rotation);

            timer = 0f;

            if (shotVFX)
                shotVFX.Play();

            if (shotAudio)
                shotAudio.Play();
        }

    }

        void SpawnProjectibletECS(Vector3 rotation)
    {
        Entity bullet = manager.Instantiate(bulletEntityPrefab);

        manager.SetComponentData(bullet, new Translation { Value = transform.position });
        manager.SetComponentData(bullet, new Rotation { Value = Quaternion.Euler(rotation) });

        manager.AddComponentData(bullet, new RotateTowardTargetComponent
        {
            rotationSpeed = .5f
        });

        manager.AddComponentData(bullet, new MoveToPositionComponent
        {
            movingSpeed = 5f
        });
        manager.AddComponentData(bullet, new TimeToLive
        {
            Value = 10f
        });
    }

}