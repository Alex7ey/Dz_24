using UnityEngine;
using UnityEngine.UI;

public class VolumeSwitcher : MonoBehaviour
{
    [SerializeField] private Button _soundButton;
    [SerializeField] private Button _musicButton;

    [SerializeField] private Color _playSound;
    [SerializeField] private Color _stopSound;

    private const string MusicKey = "Music";
    private const string SoundKey = "Sound";

    public void ToggleMusicVolume()
    {
        AudioHandler.ToggleVolume(MusicKey);
        IsVolumeOn(MusicKey, _musicButton);
    }

    public void ToggleSoundVolume()
    {
        AudioHandler.ToggleVolume(SoundKey);
        IsVolumeOn(SoundKey, _soundButton);
    }

    private void IsVolumeOn(string key, Button button)
    {
        if (AudioHandler.IsSoundOn(key))
        {
            SetButtonColor(_playSound, button);
            return;
        }
        SetButtonColor(_stopSound, button);
    }

    private void SetButtonColor(Color color, Button button) => button.image.color = color;
}
