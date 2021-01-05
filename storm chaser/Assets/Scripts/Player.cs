using UnityEngine;

//TODO : Add grapple hook mechanic, death and respawn

public class Player : MonoBehaviour
{
    [SerializeField] private Level debugEscapeLevel = default;

    [Header("Player Components")]
    public PlayerInput input = null;
    public PlayerMovement movement = null;
    public PlayerAnimator animator = null;
    
    // Display debug information about player components
    private void OnGUI()
    {
        GUILayout.Label($"Direction: { input.TargetDirection.ToString() }");
        GUILayout.Label($"Velocity: { movement.PlayerVelocity.ToString() }");
        GUILayout.Label($"Airborne: { movement.Airborne }");
        GUILayout.Label($"Air Jumps: { movement.RemainingJumps }");
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