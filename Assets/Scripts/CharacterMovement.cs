using UnityEngine;

public class CharacterMovement : Movement
{
    void Update()
    {
        Move();
        ApplyAnimation();
    }
    
    public override void ApplyAnimation()
    {
        base.ApplyAnimation();
        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 ||
            Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            Animator.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal")); 
            Animator.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));
        }
    }

    public override Vector2 Direction()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");
        return new Vector2(inputX, inputY);
    }
}
