using UnityEngine;
using DG.Tweening;
using Lean.Pool;

public class Monster : BaseClass
{
    [SerializeField] private int probability;
    [SerializeField] private GameObject [] pickUps;
    [SerializeField] private ParticleSystem deathMonsterEffect;
    
    [Tooltip("Components")]
    private Movement _movement;
    private Sequence _sequence;
    
    
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Character"))
        {
            CharacterScript characterScript = FindObjectOfType<CharacterScript>();
            characterScript.GetDamage(damage);
        }
    }

    protected override void StartAdditional()
    {
        base.StartAdditional();
        _movement = FindObjectOfType<Movement>();
    }
    
    protected override void Death()
    {
        base.Death();
        Effect();;
        Destroy(gameObject);
        CreatePickUp();
    }

    void Effect()
    {
        _sequence.AppendCallback(DeathEffect);
        _sequence.AppendInterval(0.5f);
    }

    private void DeathEffect()
    {
        if (deathMonsterEffect != null)
        {
            Vector3 effectPosition = transform.position;
            Instantiate(deathMonsterEffect, effectPosition, transform.rotation);
        }
    }
    public void CreatePickUp()
    {
        if (Chance())
        {
            {
                LeanPool.Spawn(pickUps[0], transform.position, Quaternion.identity);
            }
        }
    }

    bool Chance()
    {
        int chance = Random.Range(1, 100);
        if (chance < probability)
        {
            return true;
        }

        return false;
    }

}
