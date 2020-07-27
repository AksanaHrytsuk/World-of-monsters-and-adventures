using System;
using UnityEngine;
public class CharacterScript : BaseClass
{
    [SerializeField] private int poorHealth = 1;
    [SerializeField] Vector2 newPlayerPosition = new Vector3(x: -1, y: -1);
    [SerializeField] private Bullets prefabIceBall;

    public Action changeEnemyBehavior = delegate {  };
    private GameManager _gameManager;
    private EnemyScript _enemyScript;
    public string attackType = "None";
    private void Update()
    {
        DoDamage();
        LoseHealth();
    }
    
    void IceAttack()
    {
        Bullets bullet = Instantiate(prefabIceBall, transform.position, transform.rotation);
        // bullet.TargetPosition = new Vector2(transform.position.x + GetAttackDirection().x * 100, transform.position.y + GetAttackDirection().y * 100);
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(screenPosition);
        bullet.TargetPosition = worldPos;

    }

    protected override void StartAdditional()
    {
        base.StartAdditional();
        _gameManager = FindObjectOfType<GameManager>();
        _enemyScript = FindObjectOfType<EnemyScript>();
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

    private void SwordAttack()
    {
        Animator.Play("Player_Attack");
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, GetAttackDirection(), attackRadius, 
                                                selectObjectsToHit);
        if (hit2D.collider != null)
        {
            BaseClass damageOwner = hit2D.collider.GetComponent<BaseClass>();
            if (damageOwner != null)
            {
                damageOwner.GetDamage(damage);
            }
        }
    }

    private void Attack()
    {
        switch (attackType)
        {
            case ("Ice"):
                IceAttack();
                break;
        }
    }

    void DoDamage()
    {
        if ( Rigidbody2D != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                SwordAttack();
            }

            if (Input.GetMouseButtonDown(1))
            {
                Attack();
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

    protected override void Death()
    {
        Animator.SetTrigger("Death");
        SceneLoader.Instance.LoadNextSceneByName("MainScene");
        transform.position = newPlayerPosition;
        health = maxHealth;
    }
}
