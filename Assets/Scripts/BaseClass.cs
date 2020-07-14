﻿using Lean.Pool;
using UnityEngine;
using DG.Tweening;

public class BaseClass : MonoBehaviour
{
    [Header("Config parameters")]
    [SerializeField] protected float health;
    [SerializeField] public int damage;
    [SerializeField] protected internal float attackRadius;
    [SerializeField] protected internal LayerMask selectObjectsToHit;
    [SerializeField] protected GameObject iceCube;

    public Rigidbody2D Rigidbody2D { get; set; }
    public Collider2D Collider2D { get; set; }
    public Animator Animator { get; set; }
    public Movement Movement { get; set; }
    public CharacterScript CharacterScript { get; private set; }
    public bool Frozen { get; set; }

    /// <summary>
    /// gameObject получает урон 
    /// </summary>
    /// <param name="getDamage"></param>
    public void GetDamage(float getDamage)
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