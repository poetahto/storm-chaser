using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class UnityEventTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent OnTriggerEnter = null;
    [SerializeField] private int LayerToTrigger;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerToTrigger)
            OnTriggerEnter.Invoke();
    }
}
