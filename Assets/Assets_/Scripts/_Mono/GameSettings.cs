using Unity.Entities;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    static GameSettings instance;

    [Header("Game Object References")]
    public Transform tower;
    public ScenesManager sceneManager;

    [Header("Collision Info")]

    public float enemyCollisionRadius = 10f;
    public static float EnemyCollisionRadius
    {
        get
        {
            return instance.enemyCollisionRadius;
        }
    }
    public float towerCollisionRadius = 10f;
    public static float TowerCollisionRadius
    {
        get
        {
            return instance.towerCollisionRadius;
        }
    }

    public ParticleSystem particleSystemEntity;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            Debug.Log("Destroy gameobject");
        }
        else
            instance = this;
    }
    public static Vector3 TowerPosition
    {
        get
        {
            return instance.tower.position;
        }
    }

    public static void GameOver()
    {
        if (instance.tower == null)
            return;

        instance.tower = null;

        instance.sceneManager.GameIsOver();

    }

    public static bool IsTowerDown()
    {
        return instance.tower == null;
    }


}