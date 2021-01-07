using UnityEngine;

public class LevelSelect : MonoBehaviour
{
    [SerializeField] private LevelButton[] levels = null;
    [SerializeField] private LevelButton level2 = null;
    
    public void UnlockLevel(int index)
    {
        if (levels.Length > PlayerPrefs.GetInt("UnlockedLevels") && levels[index].locked)
        {
            levels[index].Unlock();
            PlayerPrefs.SetInt("UnlockedLevels", PlayerPrefs.GetInt("UnlockedLevels") + 1);
        }
    }
    
    // private void OnGUI()
    // {
    //     GUILayout.Label($"Levels: {PlayerPrefs.GetInt("UnlockedLevels")}");
    //     
    //     if (GUILayout.Button("Unlock level 1"))
    //         UnlockLevel(0);
    //     
    //     if (GUILayout.Button("Unlock level 2"))
    //         UnlockLevel(1);
    //     
    //     if (GUILayout.Button("Reset Progress"))
    //         ResetProgress();
    //     
    //     if (GUILayout.Button("Test"))
    //         PlayerPrefs.SetInt("FinishedLevelOne", 1);
    // }

    private void ResetProgress()
    {
        PlayerPrefs.SetInt("UnlockedLevels", 0);
        PlayerPrefs.SetInt("FinishedLevelOne", 0);
        foreach (var level in levels)
        {
            level.Lock();
        }
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("FinishedLevelOne") == 1)
        {
            level2.Unlock();
        }
        //
        // var unlockedLevelCount = Mathf.Min(PlayerPrefs.GetInt("UnlockedLevels"), levels.Length);
        // for (int i = 0; i < unlockedLevelCount; i++)
        // {
        //     levels[i].Unlock();
        // }
    }
}
