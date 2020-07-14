using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Animator Animator { get; private set; }
    public Rigidbody2D Rigidbody2D { get; private set; }
    
    public bool CanMove { get; set; }
 
    public float speed;

    void Start()
    {
        StartAdditional();
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();
        CanMove = true;
    }

    protected virtual void StartAdditional()
    {
        
    }

     void Update()
    {
        Move(); 
        ApplyAnimation();
    }

     private void FixedUpdate()
     {
         FUpdate();
     }

     protected virtual void FUpdate()
     {
         
     }
    
    public virtual void ApplyAnimation()
    {
        if (Rigidbody2D != null)
        {
            Animator.SetFloat("MoveX", Rigidbody2D.velocity.x); 
            Animator.SetFloat("MoveY", Rigidbody2D.velocity.y);
        }
    }

    public void Move()
    {
        if (CanMove && Rigidbody2D != null)
        {
            Rigidbody2D.velocity = Direction().normalized * (speed * Time.deltaTime);
        }
    }

    public virtual Vector3 Direction()
    {
        return Vector3.zero;
    }
    
    public void StopMovement()
    {
        if (Rigidbody2D != null)
        {
            Rigidbody2D.velocity = Vector3.zero;
            // Rigidbody2D.Sleep();
            CanMove = false;
        }
    }
    
    public void StartMovement()
    {
        if (Rigidbody2D != null)
        {
            CanMove = true;
        }
    }
}
