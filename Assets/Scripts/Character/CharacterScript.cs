using System;
using UnityEngine;

public class CharacterScript : BaseClass
{
    [SerializeField] private int poorHealth = 280;
    public Action changeEnemyBehavior = delegate {  };
    private void Update()
    {
        DoDamage();
        LoseHealth();
    }

    void DoDamage()
    {
        if ( Rigidbody2D != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Animator.Play("Player_Attack");
                RaycastHit2D hit2D = Physics2D.Raycast(transform.position, GetAttackDirection(), attackRadius,
                    selectObjectsToHit);
                if (hit2D.collider != null)
                {
                    BaseClass damageOwner = hit2D.collider.GetComponent<BaseClass>();
                    damageOwner.GetDamage(damage);
                }
            }
        }
    }

    void LoseHealth()
    {
        if (health <= poorHealth)
        {
            changeEnemyBehavior();
        }
    }
}
