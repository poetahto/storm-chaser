using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

[RequireComponent(typeof(LevelGameplayController))]
public class LevelMusic : MonoBehaviour
{
    [SerializeField, EventRef] private string musicPath = null;
    [SerializeField] private LevelObjective objective = null;
    
    private static EventInstance _musicInstance;
    private LevelGameplayController _gameplayController;
    
    private void Start()
    {
        objective.EndLevel += StopMusic;
        
        _gameplayController = GetComponent<LevelGameplayController>();

        if (!_musicInstance.hasHandle())
        {
            _musicInstance = RuntimeManager.CreateInstance(musicPath);
            _musicInstance.start();    
        }

        _musicInstance.setParameterByName("Intensity", _gameplayController.PercentComplete);
    }

    private void OnDisable()
    {
        objective.EndLevel -= StopMusic;
    }

    private void Update()
    {
        _musicInstance.getParameterByName("Intensity", out var val);
        _musicInstance.setParameterByName("Intensity", _gameplayController.PercentComplete);
    }

    private void StopMusic(object sender, EventArgs args)
    {
        // _musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }
}
