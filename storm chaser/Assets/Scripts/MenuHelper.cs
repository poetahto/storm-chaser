using UnityEngine;

public class MenuHelper : MonoBehaviour
{
    public void QuitGame()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    public void LoadLevel(Level level)
    {
        level.Load();
    }

    public void UnloadLevel(Level level)
    {
        level.UnloadAsync();
    }
}
