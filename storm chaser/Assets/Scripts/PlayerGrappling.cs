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

    [SerializeField] private DistanceJoint2D distanceJoint = null;
    [SerializeField] private LineRenderer grappleLine = null;
    
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
        grappleLine.enabled = false;
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
        
        var result = Physics2D.Raycast(_player.transform.position, AimDirection, grappleMaxDistance, 1<<11);

        if (result.collider == null)
        {
            // we missed
        }
        else
        {
            GameObject hook = new GameObject("Grapple Hook");
            hook.transform.position = result.point;
            hook.transform.SetParent(result.transform);
            
            StartCoroutine(Grapple(hook.transform));
        }
    }

    private IEnumerator Grapple(Transform point)
    {
        var pullAmount = maxPullStrength;
        _isGrappling = true;

        distanceJoint.enabled = true;
        distanceJoint.connectedAnchor = point.position;
        distanceJoint.distance = Vector2.Distance(transform.position, point.position);

        distanceJoint.anchor = _player.parentObject.rotation.eulerAngles.y != 0
            ? new Vector2(-1.15f, 2.13f)
            : new Vector2(1.15f, 2.13f);
        
        grappleLine.enabled = true;
        grappleLine.SetPosition(0, (Vector2) _player.parentObject.transform.position + distanceJoint.anchor);
        grappleLine.SetPosition(1, point.position);
        
        while (Input.GetKey(KeyCode.Mouse0))
        {
            distanceJoint.connectedAnchor = point.position;
            distanceJoint.distance -= pullAmount;
            pullAmount = Mathf.Min(pullAmount + pullAcceleration, maxPullStrength);
            distanceJoint.distance = Mathf.Max(distanceJoint.distance, grappleMinDistance);
            grappleLine.SetPosition(0,(Vector2) _player.parentObject.transform.position + distanceJoint.anchor);
            yield return new WaitForFixedUpdate();
        }

        grappleLine.enabled = false;
        
        distanceJoint.enabled = false;
        _timeSinceGrappled = 0;
        _isGrappling = false;
    }
}