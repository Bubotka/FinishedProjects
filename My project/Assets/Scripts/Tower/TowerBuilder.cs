using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class TowerBuilder : MonoBehaviour
{
    [SerializeField] private int _levelCount;
    [SerializeField] private GameObject _beam;
    [SerializeField] private StartPlatform _startPlatform;
    [SerializeField] private FinishPlatform _finishPlatform;
    [SerializeField] private Platform[] _platform;
    [SerializeField] private float _additionalSkaleY;

    private float _startAndFinishAdditionalSkale = 0.5f;

    public float BeamSkaleY => _levelCount / 2f + _startAndFinishAdditionalSkale+_additionalSkaleY/2;

    private void Start()
    { 
        Build();
    }

    private void Build()
    {
        GameObject beam = Instantiate(_beam,transform);
        beam.transform.localScale = new Vector3(1, BeamSkaleY, 1);

        Vector3 spawnPosition = beam.transform.position;
        spawnPosition.y += beam.transform.localScale.y - _additionalSkaleY; ;

        SpawnPlatforms(_startPlatform,ref spawnPosition,beam.transform);

        for (int i = 0; i < _levelCount; i++)
        {
            SpawnPlatforms(_platform[Random.Range(0, _platform.Length)], ref spawnPosition, beam.transform);
        }

        SpawnPlatforms(_finishPlatform, ref spawnPosition, beam.transform);
    }

    private void SpawnPlatforms(Platform platform, ref Vector3 spawnPosition, Transform parent)
    {
        Instantiate(platform, spawnPosition, Quaternion.Euler(0, Random.Range(0, 360), 0), parent);

        spawnPosition.y -= 1;
    }
}
