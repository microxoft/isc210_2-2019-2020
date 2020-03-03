using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EssenceType
{
    Blue = 0,
    Green,
    Orange,
    Purple,
    Red,
    Yellow
}
public class EssenceInstantiator : MonoBehaviour
{
    public GameObject BlueEssencePrefab,
        GreenEssencePrefab,
        OrangeEssencePrefab,
        PurpleEssencePrefab,
        RedEssencePrefab,
        YellowEssencePrefab,
        EnemyPrefab;

    float _lastSpawnedTime, _spawnDeltaTime = 0.5f;
    float enemyProbability = 16.67f;
    Dictionary<EssenceType, GameObject> EssencePrefabs;
    PlayerControllerEssence _playerControllerEssence;

    private void Awake()
    {
        _playerControllerEssence = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerEssence>();
    }

    void Start()
    {
        _lastSpawnedTime = Time.time;
        EssencePrefabs = new Dictionary<EssenceType, GameObject> {
            { EssenceType.Blue, BlueEssencePrefab },
            { EssenceType.Green, GreenEssencePrefab },
            { EssenceType.Orange, OrangeEssencePrefab },
            { EssenceType.Purple, PurpleEssencePrefab },
            { EssenceType.Red, RedEssencePrefab },
            { EssenceType.Yellow, YellowEssencePrefab },
        };
    }

    void Update()
    {
        if(!_playerControllerEssence.IsGameOver && Time.time - _lastSpawnedTime > _spawnDeltaTime)
        {
            _lastSpawnedTime = Time.time;
            InstantiateEssence();
            InstantiateEnemy();
        }
    }

    void InstantiateEssence()
    {
        Instantiate(EssencePrefabs[(EssenceType)Random.Range(0, 6)], new Vector3(10, Random.Range(-4, 5)), Quaternion.identity);
    }

    void InstantiateEnemy()
    {
        if(Random.Range(0f, 100f) <= enemyProbability)
        {
            Instantiate(EnemyPrefab, new Vector3(10, Random.Range(-4, 5)), Quaternion.identity);
        }
    }
}
