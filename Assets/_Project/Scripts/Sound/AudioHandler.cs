using UnityEngine;
using UnityEngine.Audio;

public class AudioHandler
{
    private static AudioMixer _audioMixer;
    private static AudioSource _audioSource;

    private const float OffVolumeValue = -80f;
    private const float OnVolumeValue = 0f;

    public AudioHandler(AudioMixer audioMixer, AudioSource audioSource)
    {
        _audioMixer = audioMixer;
        _audioSource = audioSource;
    }

    public static void ToggleVolume(string key)
    {
        if (IsSoundOn(key))
        {
            OffAudio(key);
            return;
        }

        OnAudio(key);
    }

    public static bool IsSoundOn(string key)
    {
        _audioMixer.GetFloat(key, out float volume);

        if (Mathf.Abs(volume - OffVolumeValue) <= 0.1f)
            return false;

        return true;
    }

    public static void PlaySound(AudioClip audio) => _audioSource.PlayOneShot(audio);

    public static void OnAudio(string key) => _audioMixer.SetFloat(key, OnVolumeValue);

    public static void OffAudio(string key) => _audioMixer.SetFloat(key, OffVolumeValue);
}
