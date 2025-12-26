using Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Audio;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private Character _characterPrefab;
    [SerializeField] private HealthBar _healthBar;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private CinemachineVirtualCamera _virtualCamera;
    [SerializeField] private DestinationMarker _destinationMarker;
    [SerializeField] private ControllerUpdater _controllerUpdater;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private MedkitSpawner _medkitSpawner;
    
    private void Awake()
    {
        Character character = Instantiate(_characterPrefab, _spawnPoint.position, _spawnPoint.rotation);
        InitializeComponents(character);
    }

    private void InitializeComponents(Character character)
    {
        AudioHandler audioHandler = new(_audioMixer, _audioSource);

        _healthBar.Initialize(character);
        _destinationMarker.Initialize(character);
        _virtualCamera.Follow = character.transform;
        _medkitSpawner.Initialize(character.transform);
        _controllerUpdater.Initialize(new PointClickMovementController(character), new NavMeshAgentJumpController(character, character.GetComponent<NavMeshAgent>()));
    }
}
