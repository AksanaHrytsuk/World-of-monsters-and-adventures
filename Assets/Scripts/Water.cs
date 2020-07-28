using UnityEngine;

public class Water : MonoBehaviour
{
    public Collider2D _collider2D;

    public void OnCollisionEnter2D(Collision2D other1)
    {
        if (other1.collider.CompareTag("Freezing"))
        {
            _collider2D.enabled = false;
        }
    }
  }
