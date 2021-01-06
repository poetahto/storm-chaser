using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class DamageListener : MonoBehaviour
{
    [SerializeField] private UnityEvent TakeDamage = null;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Damaging"))
        {
            TakeDamage.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Damaging"))
        {
            TakeDamage.Invoke();
        }
    }
}