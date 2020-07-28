using UnityEngine;
using DG.Tweening;
using Lean.Pool;

public class Monster : BaseClass
{
    [SerializeField] private int probability;
    [SerializeField] private GameObject [] pickUps;
    [SerializeField] private ParticleSystem deathMonsterEffect;
    public bool isMonster = true;
    
    [Tooltip("Components")]
   // private Movement _movement;
    private Sequence _sequence;

   public bool GetMonster()
   {
       return isMonster;
   }

   public void SetMonster(bool name)
   {
       isMonster = name;
   }
   
    
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
      //  _movement = FindObjectOfType<Movement>();
      isMonster = true;
    }
    
    protected override void Death()
    {
       // base.Death();
       _sequence.AppendInterval(0.5f);
       _sequence.AppendCallback(DeathEffect);
       _sequence.AppendInterval(1f);
        Destroy(gameObject);
        CreatePickUp();
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

    private void DeathEffect()
    {
        if (deathMonsterEffect != null)
        {
            Vector3 effectPosition = transform.position;
            Instantiate(deathMonsterEffect, effectPosition, transform.rotation);
        }
    }
}
