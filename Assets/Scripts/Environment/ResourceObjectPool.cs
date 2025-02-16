using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class ResourceObjectPool : MonoBehaviour
{
    [SerializeField] private ResourceSpawnData[] _resources;
    [SerializeField] private ResourceObject _prefab;
    [SerializeField] private float _defultSpawnColdown = 4f;
    [SerializeField] private Transform _spawnedObjectsParent;
    [Space]
    [SerializeField] private Transform[] _spawnPoints;

    private ObjectPool<ResourceObject> _pool;
    private float _spawnColdown;
    private VFXPool _vfxPool;
    private AudioPlayer _audioPlayer;
    private CollectablePool _collectablePool;
    private List<ResourceSpawnData> _resourcesList;

    public void Initialize(AudioPlayer audio, VFXPool vfxPool, CollectablePool pool)
    {
        _pool = new ObjectPool<ResourceObject>(() => Instantiate(_prefab), (obj) => obj.gameObject.SetActive(true), (obj) => obj.gameObject.SetActive(false), (obj) => Destroy(obj.gameObject), false);
        _spawnColdown = _defultSpawnColdown;
        _vfxPool = vfxPool;
        _audioPlayer = audio;
        _collectablePool = pool;
        _resourcesList = _resources.OrderBy((item) => item.ProbabilityToSpawn).ToList();
        foreach (var item in _resourcesList) 
        {
            print(item.ProbabilityToSpawn);
        }
    }

    public void SpawnAllObjects()
    {
        foreach(var spawnPoint in _spawnPoints) 
        {
            SpawnObject(spawnPoint.position);
        }
    }

    public void SetSpawnColdown(float value)
    {
        if (value <= 0)
        {
            value = 0.1f;
        }

        _spawnColdown = value;
    }

    public void ChangeSpawnProbabilty(ResourceObjectData data, float newProbability)
    {
        for (int i = 0; i < _resources.Length; i++)
        {
            if (_resources[i].Data == data)
            {
                _resources[i].SetProbability(newProbability);
                break;
            }
        }
    }

    private void SpawnObject(Vector3 position)
    {
        var obj = _pool.Get();
        var probability = UnityEngine.Random.Range(0f, 1f);

        ResourceSpawnData resourceSpawnData = _resourcesList.LastOrDefault();
        for (int i = 0; i < _resourcesList.Count; i++)
        {
            if (probability <= _resourcesList[i].ProbabilityToSpawn)
            {
                resourceSpawnData = _resourcesList[i];
                break;
            }
        }
        obj.Initialize(resourceSpawnData.Data, _audioPlayer, _vfxPool, position, _collectablePool, OnObjectKilled);
        obj.transform.SetParent(_spawnedObjectsParent);
        obj.transform.localRotation = Quaternion.Euler(0f, UnityEngine.Random.Range(0f, 360f), 0f);
    }

    private void OnObjectKilled(ResourceObject obj, Vector3 initialPos)
    {
        _pool.Release(obj);
        StartCoroutine(SpawnNewObjectCoroutine(initialPos));
    }

    private IEnumerator SpawnNewObjectCoroutine(Vector3 position)
    {
        yield return new WaitForSecondsRealtime(_spawnColdown);
        SpawnObject(position);
    }
}

[Serializable]
public struct ResourceSpawnData : IEnumerable<ResourceSpawnData>
{
    [field: SerializeField] public ResourceObjectData Data { get; private set; }
    [SerializeField][Range(0f, 1f)] private float _probabilityToSpawn;

    public float ProbabilityToSpawn => _probabilityToSpawn;

    public void SetProbability(float probability)
    {
        if (probability is < 0 or > 1) return;
        _probabilityToSpawn = probability;
    }

    public IEnumerable<ResourceSpawnData> OrderByProbability(IEnumerable<ResourceSpawnData> collection, bool descending = false)
    {
        return descending
            ? collection.OrderByDescending(item => item.ProbabilityToSpawn)
            : collection.OrderBy(item => item.ProbabilityToSpawn);
    }

    public IEnumerator<ResourceSpawnData> GetEnumerator()
    {
        yield return this;
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}