using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class Level : ScriptableObject
{
    [Header("Level Settings")]
    [SerializeField] private int LevelBuildIndex = 0;
    [SerializeField] private string LevelName = "Default Level Name";
    [SerializeField] private bool AdditiveLoad = false;

    private LoadSceneMode LoadSceneMode => AdditiveLoad ? LoadSceneMode.Additive : LoadSceneMode.Single;
    
    public void Load()
    {
        SceneManager.LoadScene(LevelBuildIndex, LoadSceneMode);
    }

    public AsyncOperation LoadAsync()
    {
        return SceneManager.LoadSceneAsync(LevelBuildIndex, LoadSceneMode);
    }

    public void UnloadAsync()
    {
        SceneManager.UnloadSceneAsync(LevelBuildIndex);
    }

    public override string ToString()
    {
        return LevelName;
    }
}