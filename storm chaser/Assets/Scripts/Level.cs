using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu]
public class Level : ScriptableObject
{
    [Header("Level Settings")]
    [SerializeField] private int levelBuildIndex = 0;
    [SerializeField] private string levelName = "Default Level Name";
    [SerializeField] private bool additiveLoad = false;

    private LoadSceneMode LoadSceneMode => additiveLoad ? LoadSceneMode.Additive : LoadSceneMode.Single;
    public bool Loaded => _scene.isLoaded;

    private Scene _scene;
    private static Level _currentlyLoaded;
    
    private void Awake()
    {
        _scene = SceneManager.GetSceneByBuildIndex(levelBuildIndex);
    }

    public void Load()
    {
        SceneManager.LoadScene(levelBuildIndex, LoadSceneMode);
        _currentlyLoaded = this;
    }

    public AsyncOperation LoadAsync()
    {
        return SceneManager.LoadSceneAsync(levelBuildIndex, LoadSceneMode);
    }

    public AsyncOperation UnloadAsync()
    {
        return SceneManager.UnloadSceneAsync(levelBuildIndex);
    }

    public override string ToString()
    {
        return levelName;
    }
}