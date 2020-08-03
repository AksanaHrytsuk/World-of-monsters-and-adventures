using System;
using Lean.Pool;
using UnityEngine;

public class CharacterScript : BaseClass
{
    [SerializeField] private int poorHealth = 1;
    [SerializeField] private Bullets prefabIceBall;
    [SerializeField] private ParticleSystem attackCharacterEffect;
    [SerializeField] private AudioClip attackCharacter;

    public Action changeEnemyBehavior = delegate { };
    public string attackType = "None";

    public Vector2 startPosition = new Vector2(x:-1,y: -1);
    private GameManager _gameManager;

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
        if (_gameManager != null)
        {
            if (_gameManager.loadGame)
            {
                Load();
            }

            _gameManager.loadGame = true;
        }
        transform.position = startPosition;
        
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
        attackType = data.attackType;
        Vector2 loadPosition = new Vector2(data.startPositionX,data.startPositionY);
        startPosition = loadPosition;
        onHealthChanged();
    }

    private void SwordAttack()
    {
        Animator.Play("Player_Attack");
        musicManager.PLaySound(attackCharacter);
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, GetAttackDirection(), attackRadius,
            selectObjectsToHit);
        if (hit2D.collider != null)
        {
            BaseClass damageOwner = hit2D.collider.GetComponent<BaseClass>();
            if (damageOwner != null)
            {
                AttackEffect();
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
        if (Rigidbody2D != null)
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
        base.Death();
        _gameManager.loadGame = false;
        startPosition = new Vector2(-1, -1);
        Save();
        SceneLoader.Instance.LoadNextSceneByName("MainScene");
        transform.position = startPosition;
        health = maxHealth;
    }
    private void AttackEffect()
    {
        if (attackCharacterEffect != null && EnemyScript != null)
        {
            //Vector2 enemyPosition = EnemyScript.transform.position;
            LeanPool.Spawn(attackCharacterEffect, transform.position, transform.rotation);
        }
    }
}