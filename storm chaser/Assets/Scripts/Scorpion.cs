using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
public class Scorpion : MonoBehaviour
{
    [SerializeField] private float walkDistance = 3;
    [SerializeField] private float walkTime = 5;
    [SerializeField] private float waitTime = 2;
    [SerializeField] private float randomOffset = 0.5f;

    private bool _currentlyWalking;
    private Rigidbody2D _rigidbody;
    private bool _movedRight;

    private void Start()
    {
        _currentlyWalking = false;
        _movedRight = false;
        _rigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(WanderBehaviour());
    }

    private IEnumerator WanderBehaviour()
    {
        while (true)
        {
            yield return new WaitForSeconds(waitTime + Random.Range(0, randomOffset));

            Vector2 destination = new Vector2(transform.position.x + walkDistance * (_movedRight ? -1 : 1) , transform.position.y);
            transform.rotation = _movedRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(0, 180, 0);
            _movedRight = !_movedRight;

            var elapsedTime = 0f;
            
            while (elapsedTime < walkTime)
            {
                elapsedTime += Time.deltaTime;
                _rigidbody.MovePosition(Vector2.Lerp(transform.position, destination, elapsedTime / walkTime));
                yield return null;
            }
        }
    }
}
