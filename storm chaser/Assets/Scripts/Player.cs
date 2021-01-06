using System.Collections;
using UnityEngine;

//TODO : Add death and respawn

public class Player : MonoBehaviour
{
    [SerializeField] private LevelGameplayController gameplayController;

    [Header("Player Components")] 
    public Transform parentObject = null;
    public PlayerInput input = null;
    public PlayerMovement movement = null;
    public PlayerAnimator animator = null;
    public PlayerGrappling grappling = null;

    [SerializeField] private SpriteRenderer playerSprite = null;
    [SerializeField] private ParticleSystem bloodParticles = null;
    [SerializeField] private float deathTime = 2;
    
    // Display debug information about player components
    private void OnGUI()
    {
        GUILayout.Label($"Direction: { input.TargetDirection.ToString() }");
        GUILayout.Label($"Velocity: { movement.PlayerVelocity.ToString() }");
        GUILayout.Label($"Airborne: { movement.Airborne }");
        GUILayout.Label($"Air Jumps: { movement.RemainingJumps }");
        GUILayout.Label($"Grapple Charged: { grappling.GrappleCharged }");
        GUILayout.Label($"Completion: { gameplayController.PercentComplete }");
    }

    public void DamagePlayer()
    {
        if (input.IsAcceptingInput)
            StartCoroutine(DeathEffect());
    }

    private IEnumerator DeathEffect()
    {
        input.SetAcceptingInput(false);
        animator.DisableAnimation();
        playerSprite.enabled = false;
        bloodParticles.Play();
        yield return new WaitForSeconds(deathTime);
        gameplayController.RestartLevel();
    }
}