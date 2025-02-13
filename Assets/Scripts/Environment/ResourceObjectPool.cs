using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class ResourceObjectPool : MonoBehaviour
{
    [SerializeField] private ResourceObject _prefab;
    [SerializeField] private float _spawnColdown = 4f;
    [SerializeField] private Transform _spawnedObjectsParent;
    [Space]
    [SerializeField] private Transform[] _spawnPoints;

    private ObjectPool<ResourceObject> _pool;

    private VFXPool _vfxPool;
    private AudioPlayer _audioPlayer;

    public void Initialize(AudioPlayer audio, VFXPool vfxPool)
    {
        _pool = new ObjectPool<ResourceObject>(() => Instantiate(_prefab), (obj) => obj.gameObject.SetActive(true), (obj) => obj.gameObject.SetActive(false), (obj) => Destroy(obj.gameObject), false);
        _vfxPool = vfxPool;
        _audioPlayer = audio;
    }

    public void SpawnAllObjects()
    {
        foreach(var spawnPoint in _spawnPoints) 
        {
            SpawnObject(spawnPoint.position);
        }
    }

    private void SpawnObject(Vector3 position)
    {
        var obj = _pool.Get();
        obj.Initialize(_audioPlayer, _vfxPool, position, OnObjectKilled);
        obj.transform.SetParent(_spawnedObjectsParent);
        obj.transform.localPosition = position;
        obj.transform.localRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
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