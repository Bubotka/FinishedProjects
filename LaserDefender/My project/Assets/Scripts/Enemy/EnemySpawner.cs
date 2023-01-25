using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<WaveConfig> _waveConfigs;
    [SerializeField] private int startingWave = 0;
    [SerializeField] private bool _looping=false;

    IEnumerator Start()
    {
        while (_looping)
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
    }

    private IEnumerator SpawnAllWaves()
    {
        for (int waveIndex=startingWave; waveIndex<_waveConfigs.Count;waveIndex++)
        {
            var currentWave = _waveConfigs[waveIndex];
            yield return StartCoroutine(SpawnAllEnemiesInWave(currentWave));
        }
    }
    private IEnumerator SpawnAllEnemiesInWave(WaveConfig waveConfig)
    {
        for(int i=0;i<waveConfig.GetNumberOfEnemies();i++)
        {
            var newEnemy =Instantiate
           (waveConfig.GetEnemyPrefab()
           , waveConfig.GetWayPoints()[0].transform.position,
           Quaternion.identity);
            newEnemy.GetComponent<EnemyPath>().SetWaveConfig(waveConfig);
            yield return new WaitForSeconds(Random.Range(waveConfig.GetTimeBetweenSpawn(),
                waveConfig.GetSpawnRandomFactor()));
        }
    }
}
