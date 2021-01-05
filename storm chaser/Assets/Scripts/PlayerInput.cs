using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    private Vector2 _targetDirection;
    private Player _player;
    private bool _wantsToJump;
    private bool _acceptingInput = true;
    
    public Vector2 TargetDirection => _targetDirection;
    public bool WantsToJump => _wantsToJump;
    public bool IsAcceptingInput => _acceptingInput;
    
    public void ResetJump()
    {
        _wantsToJump = false;
    }

    public void SetAcceptingInput(bool acceptInput)
    {
        _acceptingInput = acceptInput;
    }
    
    private void Awake()
    {
        _targetDirection = Vector2.zero;
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (!_acceptingInput)
            return;
        
        CheckForPause();
        CheckForFireGrapple();
        CheckForJump();
        UpdateTargetDirection();
    }

    private void CheckForPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            UIElementController.Instance.TogglePauseMenu();
    }

    private void CheckForFireGrapple()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
            _player.grappling.FireGrapple();
    }

    private void CheckForJump()
    {
        if (Input.GetButtonDown("Jump"))
            _wantsToJump = true;
    }
    
    private void UpdateTargetDirection()
    {
        _targetDirection.x = Input.GetAxisRaw("Horizontal");
        _targetDirection.y = Input.GetAxisRaw("Vertical");
    }
}