using UnityEngine;

public class IceBall : MonoBehaviour
{
    [Header("Config parameters")]
    [SerializeField] private bool explosive;
    [SerializeField] private float explodeRadius;
    [SerializeField] private float distanceToObject;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask _layerMask;
    
    [Tooltip("Components")]
    private CharacterMovement _characterMovement;

    private Vector2 _playerPosition;

    private void Update()
    {
        CheckHit();
    }

    private void Start()
    {
        _characterMovement = FindObjectOfType<CharacterMovement>();
        _playerPosition = _characterMovement.transform.position;
    }
    
    private void Explosion()
    {
        if (explosive)
        {
            // Find objects in radius
            Collider2D[] objectsInRadius = Physics2D.OverlapCircleAll(transform.position, explodeRadius, _layerMask);
            foreach (Collider2D objectI in objectsInRadius)
            {
                if (objectI.gameObject == gameObject)
                {
                    continue; //the same gameObject ==> next iteration
                }
                
                BaseClass damageOwner = objectI.GetComponent<BaseClass>();
                if (damageOwner == null)
                {
                    Destroy(damageOwner);
                }
                else
                {
                    damageOwner.Freeze(damage);
                }
            }
        }
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Explosion();
    }
    
    void CheckHit()
    {
        float distance = Vector2.Distance(transform.position, _playerPosition);
        if (distance < distanceToObject)
        {
            Explosion();
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explodeRadius);
    }
}
