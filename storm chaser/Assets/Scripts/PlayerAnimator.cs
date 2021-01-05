using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator = null;
    
    private Player _player;
    private PlayerMovement _movement;
    
    private void Awake()
    {
        _player = GetComponent<Player>();
        _movement = _player.movement;
    }

    private void Update()
    {
        if (_movement.Airborne)
        {
            // player is jumping
            playerAnimator.Play("Jump");
        }
        else if (_movement.PlayerVelocity.x > 0)
        {
            // player is moving right
            playerAnimator.transform.rotation = Quaternion.Euler(Vector3.zero);
            playerAnimator.Play("Running");
        }
        else if (_movement.PlayerVelocity.x < 0)
        {
            // player is moving left
            playerAnimator.transform.rotation = Quaternion.Euler(0, 180, 0);
            playerAnimator.Play("Running");
        }
        else if (_movement.PlayerVelocity == Vector2.zero)
        {
            // player is idle
            playerAnimator.Play("Idle");
        }
    }
}