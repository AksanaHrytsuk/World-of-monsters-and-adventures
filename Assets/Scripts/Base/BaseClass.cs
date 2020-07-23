using System;
using Lean.Pool;
using UnityEngine;
using DG.Tweening;

public class BaseClass : MonoBehaviour
{
    [Header("Config parameters")]
    [SerializeField] public int health;
    [SerializeField] public int maxHealth;
    [SerializeField] public int damage;
    [SerializeField] protected internal float attackRadius;
    [SerializeField] protected internal LayerMask selectObjectsToHit;
    [SerializeField] protected GameObject iceCube;
   
    
    public Action onHealthChanged = delegate {  };

    public Rigidbody2D Rigidbody2D { get; set; }
    public Collider2D Collider2D { get; set; }
    public Animator Animator { get; set; }
    public Movement Movement { get; set; }
    public CharacterScript CharacterScript { get; private set; }
    public EnemyScript EnemyScript { get; set; }
    public AnimationHelper AnimationHelper { get; set; }
    public bool Frozen { get; set; }

    /// <summary>
    /// gameObject получает урон 
    /// </summary>
    /// <param name="getDamage"></param>
    public void GetDamage(int getDamage)
    {
        health -= getDamage;
        onHealthChanged();

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
        DeathAdditional();
       }

    protected virtual void DeathAdditional()
    {
        
    }
    
    public virtual void Freeze()
    {
        if (iceCube != null && !Frozen)
        {
            LeanPool.Spawn(iceCube, transform);
            Frozen = true;
        }
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
        CharacterScript = GetComponent<CharacterScript>();
        EnemyScript = GetComponent<EnemyScript>();
    }

    private void Start()
    {
        StartAdditional();
        Frozen = false;
    }

    protected virtual void StartAdditional()
    {
        
    }
}
