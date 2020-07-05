using UnityEngine;

public class Movement : MonoBehaviour
{
    public Animator Animator { get; set; }
    public Rigidbody2D Rigidbody2D { get; set; }
    public bool CanMove { get; set; }

    public float speed;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponentInChildren<Animator>();
        CanMove = true;
    }

    void Update()
    {
        Move();
        ApplyAnimation();
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

    public virtual Vector2 Direction()
    {
        return Vector2.zero;
    }
    
    public void StopMovement()
    {
        if (Rigidbody2D != null)
        {
            Rigidbody2D.velocity = Vector2.zero;
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
