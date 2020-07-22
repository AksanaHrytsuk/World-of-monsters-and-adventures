using System;
using UnityEngine;
public class CharacterScript : BaseClass
{
    [SerializeField] private int poorHealth = 280;
    private Vector2 newPlayerPosition = new Vector3(x: -1, y: -1);
    public Action changeEnemyBehavior = delegate {  };
    private void Update()
    {
        DoDamage();
        LoseHealth();
    }

    protected override void StartAdditional()
    {
        base.StartAdditional();
        DontDestroyOnLoad(gameObject);
    }

    public void Save()
    {
        GameSaveManager.SavePlayer(this);
    }

    public void Load()
    {
        int[] loadedStates = GameSaveManager.LoadPlayer();
        health = loadedStates[0];
        maxHealth = loadedStates[1];
        damage = loadedStates[2];
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

    protected override void Death()
    {
        Animator.SetTrigger("Death");
        SceneLoader.Instance.LoadNextSceneByName("MainScene");
        transform.position = newPlayerPosition;
        health = maxHealth;
    }

    void LoseHealth()
    {
        if (health <= poorHealth)
        {
            changeEnemyBehavior();
        }
    }
}
