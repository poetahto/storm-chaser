using UnityEngine;

public class LevelGameplayController : MonoBehaviour
{
    [SerializeField] private Level levelUI = null;
    
    private void Awake()
    {
        levelUI.Load();
    }
}