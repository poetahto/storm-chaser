using UnityEngine;

// TODO: refactor

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Collider2D standingCollider = null;
    [SerializeField] private Collider2D slidingCollider = null;
    
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

    [Header("Slide Settings")] 
    [SerializeField] private float slideSpeed = 1;
    [SerializeField] private float slideCooldown = 3;
    
    private Player _player;
    private PlayerInput _input;
    private bool _grounded = false;
    private int _remainingJumps;
    private bool _sliding = false;
    private float _timeSinceSlide;
    
    public Vector2 PlayerVelocity => playerRigidbody.velocity;
    public bool Airborne => !_grounded;
    public int RemainingJumps => _remainingJumps;
    public bool Sliding => _sliding;
    
    private void Awake()
    {
        _timeSinceSlide = slideCooldown;
        _player = GetComponent<Player>();
        _input = _player.input;
        _remainingJumps = maxJumps;
        SetStanding(true);
    }

    private void SetStanding(bool standing)
    {
        _sliding = !standing;
        standingCollider.enabled = standing;
        slidingCollider.enabled = !standing;
    }

    private void FixedUpdate()
    {
        _timeSinceSlide += Time.fixedDeltaTime;
        
        if (!_input.IsAcceptingInput)
            return;
        
        _grounded = groundCheckCollider.IsTouchingLayers(1<<11);
        
        if (_grounded)
            _remainingJumps = maxJumps;

        if (_input.TargetDirection.y < 0 && _grounded && _timeSinceSlide > slideCooldown)
        {
            // slide
            SetStanding(false);
            playerRigidbody.velocity = new Vector2(slideSpeed * _input.TargetDirection.x, playerRigidbody.velocity.y);
            return;
        }

        if (_sliding)
        {
            _timeSinceSlide = 0;
            SetStanding(true);
        }
        
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