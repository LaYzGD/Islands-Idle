using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Pool;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] private AudioObject _audioObjectPrefab;
    [SerializeField] private float _minPitch = 0.9f;
    [SerializeField] private float _maxPitch = 1.2f;
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private string _soundNode;
    [SerializeField] private string _musicNode;

    private ObjectPool<AudioObject> _audioPool;


    public void Initialize()
    {
        _audioPool = new ObjectPool<AudioObject>(OnCreate, OnGet, OnRelease, OnAudioDestroy, false);

        var soundVolume = PlayerPrefs.GetFloat("SOUNDS", 0);
        var musicVolume = PlayerPrefs.GetFloat("MUSIC", 0);

        _mixer.SetFloat(_soundNode, soundVolume);
        _mixer.SetFloat(_musicNode, musicVolume);
    }

    public void PlaySound(AudioClip sound, Transform spawnPoint, float volume)
    {
        var audioObj = _audioPool.Get();
        audioObj.transform.position = spawnPoint.position;
        audioObj.Init(sound, volume, UnityEngine.Random.Range(_minPitch, _maxPitch), KillAudioObject);
        audioObj.PlaySound();
    }

    public void SetSoundVolume(float volume)
    {
        _mixer.SetFloat(_soundNode, volume);
        PlayerPrefs.SetFloat("SOUNDS", volume);
    }

    public void SetMusicVolume(float volume) 
    {
        _mixer.SetFloat(_musicNode, volume);
        PlayerPrefs.SetFloat("MUSIC", volume);
    }

    private void KillAudioObject(AudioObject audio) 
    {
        _audioPool.Release(audio);
    }

    private AudioObject OnCreate()
    {
        return Instantiate(_audioObjectPrefab);
    }

    private void OnGet(AudioObject audio)
    {
        audio.gameObject.SetActive(true);
    }

    private void OnRelease(AudioObject audio)
    {
        audio.gameObject.SetActive(false);
    }

    private void OnAudioDestroy(AudioObject audio)
    {
        Destroy(audio.gameObject);
    }
}
