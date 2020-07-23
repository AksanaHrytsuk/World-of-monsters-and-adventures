using System;
using UnityEngine;
public class CharacterScript : BaseClass
{
    [SerializeField] private int poorHealth = 1;
    [SerializeField] Vector2 newPlayerPosition = new Vector3(x: -1, y: -1);
    public Action changeEnemyBehavior = delegate {  };
    private GameManager _gameManager;
    private void Update()
    {
        DoDamage();
        LoseHealth();
    }

    protected override void StartAdditional()
    {
        base.StartAdditional();
        _gameManager = FindObjectOfType<GameManager>();
        
        if (!_gameManager.startGame)
        { 
            Load();
            _gameManager.startGame = false;
        }
    }
    
    // Save data
    public void Save()
    {
        GameSaveManager.SavePlayer(this);
    }
    
    // Load data
    public void Load()
    {
        PlayerData data = GameSaveManager.LoadPlayer();
        health = data.health;
        maxHealth = data.maxHealth;
        damage = data.damage;
        attackRadius = data.attackRadius;
        onHealthChanged();
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
