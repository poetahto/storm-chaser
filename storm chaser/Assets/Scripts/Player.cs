using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Level debugEscapeLevel = default;

    [Header("Player Components")]
    public PlayerInput input = null;
    public PlayerMovement movement = null;
    
    // Display debug information about player components
    private void OnGUI()
    {
        GUILayout.Label($"Direction: { input.TargetDirection.ToString() }");
        GUILayout.Label($"Velocity: { movement.PlayerVelocity.ToString() }");
        GUILayout.Label($"Airborne: { movement.Airborne }");
    }

    public void TogglePauseUI()
    {
        if (debugEscapeLevel.Loaded)
        {
            debugEscapeLevel.UnloadAsync();
        }
        else
        {
            debugEscapeLevel.LoadAsync();
        }
    }
}