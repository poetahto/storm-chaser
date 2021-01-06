using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LevelObjective : MonoBehaviour
{
    public event EventHandler EndLevel;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == 8)
            EndLevel?.Invoke(this, EventArgs.Empty);
    }
}