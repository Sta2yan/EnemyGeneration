using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private List<SpawnPoint> _spawnPoints = new List<SpawnPoint>();

    private WaitForSeconds _sleep = new WaitForSeconds(2);
    private Coroutine _coroutine;

    public bool IsActive { get; private set; }

    private bool _isFreePoints
    {
        get
        {
            for (int i = 0; i < _spawnPoints.Count; i++)
            {
                if (_spawnPoints[i].IsFree)
                    return true;
            }

            return false;
        }
    }

    private void Awake()
    {
        StartGeneration();
    }

    public void AddNewPoint(SpawnPoint spawnPoint)
    {
        _spawnPoints.Add(spawnPoint);
    }

    public void StartGeneration()
    {
        IsActive = true;
        _coroutine = StartCoroutine(Generation());
    }

    public void StopGeneration()
    {
        IsActive = false;

        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator Generation()
    {
        while (IsActive)
        {
            if (_isFreePoints)
            {
                int index;

                do
                {
                    index = UnityEngine.Random.Range(0, _spawnPoints.Count);
                }
                while (_spawnPoints[index].IsFree == false);

                Debug.Log("Появился в " + index);
                Instantiate(_enemy, _spawnPoints[index].transform.position, Quaternion.identity);
            }

            yield return _sleep;
        }
    }
}
