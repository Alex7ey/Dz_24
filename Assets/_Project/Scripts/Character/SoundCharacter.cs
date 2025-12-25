using UnityEngine;

public class SoundCharacter : MonoBehaviour
{
    [SerializeField] private AudioClip _jumpAudio;

    private Character _character;
    private bool _canPlayJumpSound;

    private void Awake()
    {
        _character = GetComponent<Character>();
    }

    private void Update()
    {
        if (_character.InJumpProcess && _canPlayJumpSound)
        {
            _canPlayJumpSound = false;
            AudioHandler.PlaySound(_jumpAudio);
        }

        if (_character.InJumpProcess == false)
            _canPlayJumpSound = true;

    }
}
