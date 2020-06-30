using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterBaseClass : MonoBehaviour

{
    [Header("Config parameters")]
    public float speed;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] protected float damage;
    
    
    [Tooltip("Ссылки на компоненты")]
    private Rigidbody2D _rigidbody2D;
    private Collider2D _collider2D;
    private EnemyScript _enemyScript;
    private Animator _animator;
    
    public Rigidbody2D GetRigidbody2D()
    {
        return _rigidbody2D;
    } 
    public Animator GetAnimator()
    {
        return _animator;
    }

    public void SetRigidbody(Rigidbody2D a)
    {
        _rigidbody2D = a;
    }

    public Collider2D GetCollider2D()
    {
        return _collider2D;
    }

    public EnemyScript GetEnemy()
    {
        return _enemyScript;
    }

    // public Animator GetAnimator()
    // {
    //     return _animator;
    // }
    
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
        
        _animator.SetTrigger("Death");
        Destroy(GetRigidbody2D());
        Destroy(GetCollider2D());
    }
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _collider2D = GetComponent<Collider2D>();
    }

    private void Start()
    {
        StartAdditional();
    }

    protected virtual void StartAdditional()
    {
        
    }

    private void Update()
    {
        UpdateAdditional();
    }

    protected virtual void UpdateAdditional()
    {
        
    }
}
