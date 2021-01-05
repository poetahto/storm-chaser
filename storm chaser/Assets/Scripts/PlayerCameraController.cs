using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Transform cameraTracker;
    
    private Player _player;
    
    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void Start()
    {
        var trackerTransform = cameraTracker.transform;
        trackerTransform.position = _player.transform.position;
        virtualCamera.m_Follow = trackerTransform;
    }

    private void Update()
    {
        var trackerPos = cameraTracker.transform.position;
        var playerPos = _player.transform.position;
        
        if (playerPos.x > trackerPos.x)
        {
            trackerPos.x = playerPos.x;
        }

        trackerPos.y = playerPos.y;

        cameraTracker.transform.position = trackerPos;
    }
}