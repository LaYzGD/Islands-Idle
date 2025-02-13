using UnityEngine;
using System;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class AudioObject : MonoBehaviour
{
    [SerializeField] private AudioSource _source;

    private AudioClip _clip;

    private Action<AudioObject> _onStopAction;

    public void Init(AudioClip clip, float volume, float pitch, Action<AudioObject> onStopAction)
    {
        _source.pitch = pitch;
        _source.volume = volume;
        _clip = clip;
        _onStopAction = onStopAction;
    }

    public void PlaySound()
    {
        _source.PlayOneShot(_clip);
        StartCoroutine(CheckPlaying());
    }

    private IEnumerator CheckPlaying()
    {
        while (_source.isPlaying)
        {
            yield return null;
        }

        _onStopAction(this);
    }
}
