using System;
using System.Collections;
using FMOD.Studio;
using FMODUnity;
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
    [SerializeField, EventRef] private string deathSound;

    private EventInstance deathInstance;

    private void Awake()
    {
        deathInstance = RuntimeManager.CreateInstance(deathSound);
    }

    private void OnDestroy()
    {
        deathInstance.release();
    }
    //
    // // Display debug information about player components
    // private void OnGUI()
    // {
    //     GUILayout.Label($"Direction: { input.TargetDirection.ToString() }");
    //     GUILayout.Label($"Velocity: { movement.PlayerVelocity.ToString() }");
    //     GUILayout.Label($"Airborne: { movement.Airborne }");
    //     GUILayout.Label($"Air Jumps: { movement.RemainingJumps }");
    //     GUILayout.Label($"Grapple Charged: { grappling.GrappleCharged }");
    //     GUILayout.Label($"Completion: { gameplayController.PercentComplete }");
    // }

    public void SetDifficulty(int difficulty)
    {
        switch (difficulty)
        {
            case 0:
                DifficultyHelper(3);
                break;
            case 1:
                DifficultyHelper(2);
                break;
            case 2:
                DifficultyHelper(1);
                break;
        }
    }

    private void DifficultyHelper(float num)
    {
        grappling.SetCooldown(num);
        movement.SetCooldown(num);
    }
    
    public void DamagePlayer()
    {
        if (input.IsAcceptingInput)
            StartCoroutine(DeathEffect());
    }

    private IEnumerator DeathEffect()
    {
        Destroy(parentObject.GetComponent<DistanceJoint2D>());
        Destroy(parentObject.GetComponent<Rigidbody2D>());
        deathInstance.start();
        input.SetAcceptingInput(false);
        animator.DisableAnimation();
        playerSprite.enabled = false;
        bloodParticles.Play();
        yield return new WaitForSeconds(deathTime);
        gameplayController.RestartLevel();
    }
}