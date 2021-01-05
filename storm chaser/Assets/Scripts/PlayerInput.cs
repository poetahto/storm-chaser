using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerInput : MonoBehaviour
{
    public Vector2 TargetDirection => _targetDirection;
    public bool WantsToJump => _wantsToJump; 
    
    private Vector2 _targetDirection;
    private Player _player;
    private bool _wantsToJump;

    public void ResetJump()
    {
        _wantsToJump = false;
    }
    
    private void Awake()
    {
        _targetDirection = Vector2.zero;
        _player = GetComponent<Player>();
    }

    private void Update()
    {
        CheckForPause();
        CheckForJump();
        UpdateTargetDirection();
    }

    private void CheckForPause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            _player.TogglePauseUI();
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