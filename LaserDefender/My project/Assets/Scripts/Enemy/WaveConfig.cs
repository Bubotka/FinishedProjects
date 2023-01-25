using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="EnemyWaveConfig")]

public class WaveConfig : ScriptableObject
{
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _pathPrefab;
    [SerializeField] private float _timeBetweenSpawn = 0.5f;
    [SerializeField] private float _spawnRandomFactor = 0.3f;
    [SerializeField] private int _numberOfEnemies = 5;
    [SerializeField] private float _moveSpeed = 2f;

    public GameObject GetEnemyPrefab() { return _enemyPrefab; }
    public List<Transform> GetWayPoints() 
    {
        List<Transform> wayPoints = new List<Transform>();

        foreach (Transform waypoint in _pathPrefab.transform)
        {
            wayPoints.Add(waypoint);
        }
        return wayPoints;
    }
    public float GetTimeBetweenSpawn() { return _timeBetweenSpawn; }
    public float GetSpawnRandomFactor() { return _spawnRandomFactor; }  
    public int GetNumberOfEnemies() { return _numberOfEnemies; }
    public float GetMoveSpeed() { return _moveSpeed; }
}
