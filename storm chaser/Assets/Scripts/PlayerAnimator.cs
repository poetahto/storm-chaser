using FMOD.Studio;
using FMODUnity;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerAnimator : MonoBehaviour
{
    [SerializeField, EventRef] private string slideSound;
    [SerializeField, EventRef] private string footstepSound;
    [SerializeField, EventRef] private string groundJumpSound;
    [SerializeField, EventRef] private string airJumpSound;
    
    private EventInstance _silentInstance;
    private EventInstance _slideInstance;
    private EventInstance _footstepInstance;
    private EventInstance _groundJumpInstance;
    private EventInstance _airJumpInstance;
    
    [SerializeField] private Animator playerAnimator = null;
    [SerializeField] private float movementThreshold = 0.1f;
    
    private Player _player;
    private PlayerMovement _movement;
    private PlayerGrappling _grappling;

    public EventInstance _currentSound;

    public void DisableAnimation()
    {
        playerAnimator.StopPlayback();
    }
    
    private void Awake()
    {
        _player = GetComponent<Player>();
        _movement = _player.movement;
        _grappling = _player.grappling;

        _silentInstance = new EventInstance();
        _currentSound = _silentInstance;

        _slideInstance = RuntimeManager.CreateInstance(slideSound);
        _footstepInstance = RuntimeManager.CreateInstance(footstepSound);
        _groundJumpInstance = RuntimeManager.CreateInstance(groundJumpSound);
        _airJumpInstance = RuntimeManager.CreateInstance(airJumpSound);
    }

    private void OnDestroy()
    {
        _slideInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _footstepInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _groundJumpInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        _airJumpInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        
        _slideInstance.release();
        _footstepInstance.release();
        _groundJumpInstance.release();
        _airJumpInstance.release();
    }

    private void Update()
    {
        if (_movement.PlayerVelocity.x > movementThreshold && !_grappling.IsGrappling)
        {
            _player.parentObject.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
        else if (_movement.PlayerVelocity.x < -movementThreshold && !_grappling.IsGrappling)
        {
            _player.parentObject.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        if (_grappling.IsGrappling)
        {
            playerAnimator.Play("Grapple");
        }
        else if (_movement.Sliding)
        {
            // player is sliding
            playerAnimator.Play("Sliding");
            UpdateSound(_slideInstance);
        }
        else if (_movement.Airborne)
        {
            playerAnimator.Play(_movement.PlayerVelocity.y > 0 ? "Jump Up" : "Jump Down");
            
            if (Input.GetKeyDown(KeyCode.Space) && _movement.RemainingJumps > 0)
                UpdateSound(_airJumpInstance, true);
            else
            {
                if (!_currentSound.Equals(_airJumpInstance))
                    UpdateSound(_silentInstance);
                    
            }
        }
        else if (_movement.PlayerVelocity.x > movementThreshold || _movement.PlayerVelocity.x < -movementThreshold)
        {
            // player is running
            playerAnimator.Play("Running");
            UpdateSound(_footstepInstance);
        }
        else
        {
            // player is idle
            playerAnimator.Play("Idle");
            UpdateSound(_silentInstance);
        }
    }

    private void UpdateSound(EventInstance newInstance, bool retrigger = false)
    {
        if (_currentSound.Equals(newInstance) && !retrigger) return;
        
        _currentSound.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        newInstance.start();
        _currentSound = newInstance;
    }
}