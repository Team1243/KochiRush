using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    public AudioSource _audioSource;

    public float _pitchRandomness = 0.2f;
    public float _basePitch;

    protected virtual void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {
        _basePitch = _audioSource.pitch;
    }

    public void PlayClipwithVariablePitch(AudioClip clip)
    {
        float randomPitcch = Random.Range(-_pitchRandomness, +_pitchRandomness);
        _audioSource.pitch = _basePitch + randomPitcch;
        PlayClip(clip);
    }

    public void PlayClip(AudioClip clip)
    {
        _audioSource.Stop();
        _audioSource.clip = clip;
        _audioSource.Play();
    }
}