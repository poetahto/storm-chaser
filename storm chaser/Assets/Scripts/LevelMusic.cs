using FMOD.Studio;
using FMODUnity;
using UnityEngine;

[RequireComponent(typeof(LevelGameplayController))]
public class LevelMusic : MonoBehaviour
{
    [SerializeField, EventRef] private string musicPath = null;

    private static EventInstance _musicInstance;
    private LevelGameplayController _gameplayController;
    
    private void Start()
    {
        _gameplayController = GetComponent<LevelGameplayController>();

        if (!_musicInstance.hasHandle())
        {
            _musicInstance = RuntimeManager.CreateInstance(musicPath);
            _musicInstance.start();    
        }

        _musicInstance.setParameterByName("Intensity", _gameplayController.PercentComplete);
    }

    private void Update()
    {
        _musicInstance.getParameterByName("Intensity", out var val);
        _musicInstance.setParameterByName("Intensity", _gameplayController.PercentComplete);
    }
}
