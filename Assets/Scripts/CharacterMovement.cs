using System;
using System.Linq;
using UnityEngine;

public class CharacterMovement : CharacterBaseClass
{
    [Header("Config Parameters")]
    [SerializeField] float attackRadius;
   
    [SerializeField] LayerMask selectObjectsToHit;

    [Tooltip("Ссылки на компоненты")]
    private Animator _animator;

    void Start()
    {
       StartAdditional();
    }

    void Update()
    {
        UpdateAdditional();
    }

    protected override void UpdateAdditional()
    {
        Move();
        DoDamage();
    }

    protected override void StartAdditional()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    void DoDamage()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _animator.Play("Player_Attack");
            
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, GetAttackDirection(), attackRadius, selectObjectsToHit);
            if (hit2D.collider != null)
            {
                Debug.Log("here");
                EnemyScript damageOwner = hit2D.collider.GetComponent<EnemyScript>();
                damageOwner.GetDamage(damage);
            }
        }
    }

    Vector2 GetAttackDirection()
    {
        float x = _animator.GetFloat("LastMoveX");
        float y = _animator.GetFloat("LastMoveY");
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

    void Move()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        if (GetRigidbody2D() != null)
        { 
            GetRigidbody2D().velocity = new Vector2(inputX, inputY) * (speed * Time.deltaTime);
            _animator.SetFloat("MoveX", GetRigidbody2D().velocity.x );
            _animator.SetFloat("MoveY", GetRigidbody2D().velocity.y);

            if(Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
            {
                _animator.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
                _animator.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));
            }
        }
    }
}
