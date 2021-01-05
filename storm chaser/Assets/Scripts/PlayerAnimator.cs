using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator playerAnimator = null;
    [SerializeField] private float movementThreshold = 0.1f;
    
    private Player _player;
    private PlayerMovement _movement;
    private PlayerGrappling _grappling;

    public void DisableAnimation()
    {
        playerAnimator.StopPlayback();
    }
    
    private void Awake()
    {
        _player = GetComponent<Player>();
        _movement = _player.movement;
        _grappling = _player.grappling;
    }

    private void Update()
    {
        if (_movement.PlayerVelocity.x > movementThreshold)
        {
            playerAnimator.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        else if (_movement.PlayerVelocity.x < -movementThreshold)
        {
            playerAnimator.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (_grappling.IsGrappling)
        {
            playerAnimator.Play("Grapple");
        }
        else if (_movement.Airborne)
        {
            // player is jumping
            playerAnimator.Play("Jump");
        }
        else if (_movement.PlayerVelocity.x > movementThreshold || _movement.PlayerVelocity.x < -movementThreshold)
        {
            // player is running
            playerAnimator.Play("Running");
        }
        else
        {
            // player is idle
            playerAnimator.Play("Idle");
        }
    }
}