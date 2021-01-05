﻿using UnityEngine;

// TODO: refactor

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Collider2D groundCheckCollider = null;
    [SerializeField] private Rigidbody2D playerRigidbody = null;
    [SerializeField] private float maxSpeed = 1;
    [SerializeField] private float acceleration = 0.5f;
    [SerializeField] private float deacceleration = 0.5f;

    [Header("Jump Settings")]
    [SerializeField] private int maxJumps = 1;
    [SerializeField] private float jumpStrength = 1f;
    [SerializeField] private float lowJumpMultiplier = 1.5f;
    [SerializeField] private float fallMultiplier = 2f;
    [SerializeField] private float groundRaycastDistance = 0.1f;
    
    private Player _player;
    private PlayerInput _input;
    private bool _grounded = false;
    private int _remainingJumps;
    
    public Vector2 PlayerVelocity => playerRigidbody.velocity;
    public bool Airborne => !_grounded;
    public int RemainingJumps => _remainingJumps;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _input = _player.input;
        _remainingJumps = maxJumps;
    }

    public void AddForce(Vector2 force)
    {
        playerRigidbody.AddForce(force);
    }

    private void FixedUpdate()
    {
        if (!_input.IsAcceptingInput)
            return;
        
        _grounded = groundCheckCollider.IsTouchingLayers(~(1 << 8));
        
        if (_grounded)
            _remainingJumps = maxJumps;
        
        var targetVelocity = new Vector2(_input.TargetDirection.x, 0);
        
        targetVelocity.x *= acceleration;
        
        if (_input.WantsToJump)
        {
            if (_remainingJumps > 0)
            {
                var temp1 = playerRigidbody.velocity;
                temp1.y = 0;
                playerRigidbody.velocity = temp1;
                
                targetVelocity.y = jumpStrength;
                _remainingJumps--;
            }
            _input.ResetJump();
        }

        if (playerRigidbody.velocity.y < 0)
        {
            playerRigidbody.velocity += Vector2.up * Physics2D.gravity * (fallMultiplier - 1);
        }
        else if (playerRigidbody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            playerRigidbody.velocity += Vector2.up * Physics2D.gravity * (lowJumpMultiplier - 1);
        }
        
        playerRigidbody.AddForce(targetVelocity, ForceMode2D.Impulse);
        
        var temp = playerRigidbody.velocity;
        temp.x = Mathf.Clamp(temp.x, -maxSpeed, maxSpeed);
        if (_input.TargetDirection.x == 0)
        {
            temp.x = Mathf.MoveTowards(temp.x, 0, deacceleration);
        }

        playerRigidbody.velocity = temp;
    }
}