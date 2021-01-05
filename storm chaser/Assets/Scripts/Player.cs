using UnityEngine;

//TODO : Add grapple hook mechanic, death and respawn

public class Player : MonoBehaviour
{
    [Header("Player Components")]
    public PlayerInput input = null;
    public PlayerMovement movement = null;
    public PlayerAnimator animator = null;
    public PlayerGrappling grappling = null;

    // Display debug information about player components
    private void OnGUI()
    {
        GUILayout.Label($"Direction: { input.TargetDirection.ToString() }");
        GUILayout.Label($"Velocity: { movement.PlayerVelocity.ToString() }");
        GUILayout.Label($"Airborne: { movement.Airborne }");
        GUILayout.Label($"Air Jumps: { movement.RemainingJumps }");
        GUILayout.Label($"Grapple Charged: { grappling.GrappleCharged }");
    }
}