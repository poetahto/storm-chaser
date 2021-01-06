using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LevelObjective : MonoBehaviour
{
    [SerializeField] private Level levelToLoad = null;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
            levelToLoad.Load();
    }
}