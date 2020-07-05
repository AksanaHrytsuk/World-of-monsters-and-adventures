using Lean.Pool;
using UnityEngine;
using DG.Tweening;

// [RequireComponent(typeof(Rigidbody2D))]
public class BaseClass : MonoBehaviour
{
    [Header("Config parameters")]
    [SerializeField] protected float health;
    [SerializeField] protected int damage;
    [SerializeField] private GameObject iceCube;
    [SerializeField] protected internal float attackRadius;
    [SerializeField] protected internal LayerMask selectObjectsToHit;

    public Rigidbody2D Rigidbody2D { get; set; }
    public Collider2D Collider2D { get; set; }
    public Animator Animator { get; set; }
    public Movement Movement { get; set; }
    

    /// <summary>
    /// gameObject получает урон 
    /// </summary>
    /// <param name="getDamage"></param>
    public virtual void GetDamage(float getDamage)
    {
        health -= getDamage;
        if (health <= 0)
        {
            Death();
        }
    }
    
    protected virtual void Death()
    {
        Animator.SetTrigger("Death");
        Destroy(Movement);
        Destroy(Rigidbody2D);
        Destroy(Collider2D);
    }
    
    protected internal void Freeze(int damage)
    {
        GameObject ice = LeanPool.Spawn(iceCube, transform);
        GetDamage(damage);
        Movement.StopMovement();
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(2f);
        sequence.AppendCallback(() => { LeanPool.Despawn(ice); });
        sequence.AppendCallback(() => { Movement.StartMovement(); });
    }
    

    public Vector2 GetAttackDirection()
    {
        float x = Animator.GetFloat("LastMoveX");
        float y = Animator.GetFloat("LastMoveY");
        if (x == 1 && y == 0 )
        {
            return Vector2.right;
        }
        if (x == -1 && y == 0)
        {
            return Vector2.left;
        }
        if (y == 1)
        {
            return Vector2.up;
        }
        if (y == -1)
        {
            return Vector2.down;
        }
        return Vector2.down;
    }
    private void Awake()
    {
        Animator = GetComponentInChildren<Animator>();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Collider2D = GetComponent<Collider2D>();
        Movement = GetComponent<Movement>();
    }

    private void Start()
    {
        StartAdditional();
    }

    protected virtual void StartAdditional()
    {
        
    }
}
