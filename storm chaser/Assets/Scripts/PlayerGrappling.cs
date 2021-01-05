using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerGrappling : MonoBehaviour
{
    [SerializeField] private float grappleCooldown = 3;
    
    [SerializeField] private float maxPullStrength = 1;
    [SerializeField] private float pullAcceleration = 1;
    [SerializeField] private float grappleMinDistance = 1;
    [SerializeField] private float grappleMaxDistance = 10;
    [SerializeField] private float grappleLaunchStrength = 1;

    [SerializeField] private DistanceJoint2D distanceJoint = null;
    [SerializeField] private Collider2D playerCollider = null;
    [SerializeField] private Rigidbody2D playerRigidbody = null;
    
    private Player _player;
    private PlayerInput _input;
    private Camera _camera;
    private float _timeSinceGrappled;
    private bool _isGrappling;
    
    public bool GrappleCharged => _timeSinceGrappled > grappleCooldown;
    public bool IsGrappling => _isGrappling;
    public Vector2 AimDirection => (_camera.ScreenToWorldPoint(Input.mousePosition) - _player.transform.position).normalized;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _camera = Camera.main;
        distanceJoint.enabled = false;
        _timeSinceGrappled = grappleCooldown;
    }

    private void Start()
    {
        _input = _player.input;
    }

    private void Update()
    {
        _timeSinceGrappled += Time.deltaTime;
    }

    public void FireGrapple()
    {
        if (!GrappleCharged)
        {
            // on cooldown
            return;
        }
        
        var result = Physics2D.Raycast(_player.transform.position, AimDirection, grappleMaxDistance, ~(1 << 8));

        if (result.point == Vector2.zero)
        {
            // we missed
        }
        else
        {
            StartCoroutine(Grapple(result.point));
        }
    }

    private IEnumerator Grapple(Vector2 point)
    {
        var pullAmount = maxPullStrength;
        _isGrappling = true;
        
        distanceJoint.enabled = true;
        distanceJoint.connectedAnchor = point;
        distanceJoint.distance = Vector2.Distance(transform.position, point);
        distanceJoint.anchor = new Vector2(0, 1);
        
        while (Input.GetKey(KeyCode.Mouse0))
        {
            distanceJoint.distance -= pullAmount;
            pullAmount = Mathf.Min(pullAmount + pullAcceleration, maxPullStrength);
            distanceJoint.distance = Mathf.Max(distanceJoint.distance, grappleMinDistance);
            Debug.DrawLine(transform.position, point, Color.red);
            yield return new WaitForFixedUpdate();
        }

        distanceJoint.enabled = false;
        _timeSinceGrappled = 0;
        _isGrappling = false;
    }
}