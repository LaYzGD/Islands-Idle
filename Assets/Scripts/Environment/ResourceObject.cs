using UnityEngine;

public class ResourceObject : MonoBehaviour, IHittable
{
    [SerializeField] private ResourceObjectData _data;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _hitPoint;
    [SerializeField] private float _soundVolume;
    [SerializeField] private string _hitTransitionName;
    [SerializeField] private string _touchTransitionName;

    private int _durability;
    private VFXPool _pool;
    private AudioPlayer _audioPlayer;
    private Vector3 _initialPosition;
    private System.Action<ResourceObject, Vector3> _killAction;

    public void Initialize(AudioPlayer audio, VFXPool pool, Vector3 initialPos, System.Action<ResourceObject, Vector3> killAction) 
    {
        _durability = _data.Durability;
        _audioPlayer = audio;
        _pool = pool;
        _initialPosition = initialPos;
        _killAction = killAction;
    }

    public void Hit(int damage)
    {
        _animator.SetTrigger(_hitTransitionName);
        _pool.SpawnVFX(_data.HitVFX, _hitPoint.position, Quaternion.identity);
        _audioPlayer.PlaySound(_data.HitAudio, _hitPoint, _soundVolume);

        if (damage <= 0)
        {
            return;
        }

        _durability -= damage;
        
        if (_durability <= 0)
        {
            _killAction(this, _initialPosition);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterCore core))
        {
            _animator.SetTrigger(_touchTransitionName);
        }
    }
}
