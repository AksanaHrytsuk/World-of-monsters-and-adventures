using System;
using UnityEngine;

public class Bullets : MonoBehaviour
{
    [Header("Config parameters")] 
    [SerializeField] private bool explosive;
    [SerializeField] private float explodeRadius;
    [SerializeField] private float distanceToObject;
    [SerializeField] private float speed;
    [SerializeField] private LayerMask _layerMask;
    public Vector3 TargetPosition { get; set; }
    public Transform target;

    public int damage;

    private Rigidbody2D Rigidbody2D { get; set; }

    private void Update()
    {
        CheckHit();
    }

    private void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Rigidbody2D.velocity = Direction().normalized * speed;
//      TargetPosition = target.position;
    }

    public void Move()
    {
    }
    // направление до таргета
    public virtual Vector2 Direction()
    {
        return TargetPosition - transform.position;
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
                    continue;
                }

                BaseClass damageOwner = objectI.GetComponent<BaseClass>();
                if (damageOwner == null)
                {
                    Destroy(damageOwner);
                }
                else
                {
                    ApplyEffect(damageOwner);
                }
            }
        }

        Destroy(gameObject);
    }

    public virtual void ApplyEffect(BaseClass obj)
    {
        Strike(obj);
    }
    // если дистанция от снаяда до таргета меньше distanceToObject, то происходит взрыв
    private void CheckHit()
    {
        float distance = Vector2.Distance(transform.position, TargetPosition);
        if (distance < distanceToObject)
        {
            Explosion();
        }
    }
    // получить урон
    public void Strike(BaseClass obj)
    {
        obj.GetDamage(damage);
    }

    // при коллизии с другим объетом происходит взрыв
    private void OnCollisionEnter2D(Collision2D other)
    {
        Explosion();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, explodeRadius);
    }
}