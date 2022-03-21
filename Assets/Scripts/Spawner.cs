using System;
using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private SpawnPoint[] _spawnPoints = new SpawnPoint[0];

    private WaitForSeconds _sleep = new WaitForSeconds(2);
    private Coroutine _coroutine;

    private bool _isFreePoints
    {
        get
        {
            for (int i = 0; i < _spawnPoints.Length; i++)
            {
                if (_spawnPoints[i].IsFree)
                    return true;
            }

            return false;
        }
    }

    private void Awake()
    {
        _coroutine = StartCoroutine(Generation());
    }

    public void Init(SpawnPoint spawnPoint)
    {
        Array.Resize(ref _spawnPoints, _spawnPoints.Length + 1);
        _spawnPoints[_spawnPoints.Length - 1] = spawnPoint;
    }

    public void StartGeneration()
    {
        _coroutine = StartCoroutine(Generation());
    }

    public void StopGeneration()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator Generation()
    {
        if (_isFreePoints)
        {
            int index;

            do
            {
                index = UnityEngine.Random.Range(0, _spawnPoints.Length);
            }
            while (_spawnPoints[index].IsFree == false);

            Debug.Log("Появился в " + index);
            Instantiate(_enemy, _spawnPoints[index].transform.position, Quaternion.identity);
        }

        yield return _sleep;
        _coroutine = StartCoroutine(Generation());
    }
}
