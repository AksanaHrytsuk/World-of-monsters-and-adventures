using System.Collections;
using Lean.Pool;
using UnityEngine;


public class EnemyScript : BaseClass
{
  [Header("AI Config")] [SerializeField] private float meleeAttackDistance;
  [SerializeField] private float rangeAttackDistance;
  [SerializeField] private float searchAngel;
  [SerializeField] private float iceBallSpeed;
  [SerializeField] private float rate;
  [SerializeField] private float delay;

  [SerializeField] private Transform targetPosition;
  [SerializeField] private Rigidbody2D prefabIceBall;

  private bool canShoot = true;

  [Tooltip("Components")]
  private CharacterMovement characterMovement;
  
  private void Update()
  {
    UpdateStates();
  }

  enum EnemyState
  {
    RangedAttack,
    MeleeAttack
  }

  private EnemyState activeState;

  protected override void StartAdditional()
  {
    characterMovement = FindObjectOfType<CharacterMovement>();
    Movement = GetComponent<Movement>();
    ChangeState(EnemyState.RangedAttack); // состояние зомби при старте игры RangedAttack(патрулирует и стреляет льдом)
  }

  private void UpdateStates()
  {
    if (characterMovement != null)
    {
      // Distance to player
      float distance = Vector3.Distance(transform.position, characterMovement.transform.position); 
      switch (activeState)
      {
        case EnemyState.RangedAttack:
          if (distance < meleeAttackDistance)
          {
            ChangeState(EnemyState.MeleeAttack);
          }
          break;
        case  EnemyState.MeleeAttack:
        {
          if (distance > meleeAttackDistance)
          {
            ChangeState(EnemyState.RangedAttack);
          }
          break;
          
        }
      }
    }
  }

  protected override void Death()
  {
    base.Death();
    Destroy(this);
  }

  void ChangeState(EnemyState newState)
  {
    activeState = newState;

    switch (activeState)
    {
      case EnemyState.RangedAttack:
        StartCoroutine(CreateIceBall(delay,rate));
        canShoot = true;
        break;
      case EnemyState.MeleeAttack:
        EnemyMeleeAttack();
        canShoot = false;
        StopCoroutine(CreateIceBall(delay,rate));
        break;
    }
  }
  Vector2 Direction()
  {
    return characterMovement.transform.position - transform.position;
  }

  IEnumerator CreateIceBall(float delay, float rate)
  {
    yield return new WaitForSeconds(delay);

    while (canShoot)
    {
      Rigidbody2D clone = LeanPool.Spawn(prefabIceBall, targetPosition.position, transform.rotation);
      clone.velocity = Direction().normalized * iceBallSpeed;
      // todo _animator.SetTrigger("Shoot");
      yield return new WaitForSeconds(rate);
    }
    // ReSharper disable once IteratorNeverReturns
  }

  private void EnemyMeleeAttack()
  {
    // float distance = Vector2.Distance(transform.position, characterMovement.transform.position);
    Vector3 direction = characterMovement.transform.position - transform.position;
    // угол между двумя векторами 
    float angel = Mathf.Abs(Vector3.Angle(direction, -transform.up));
  
    if (angel <= searchAngel)
    {
      RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, meleeAttackDistance, selectObjectsToHit);
      if (hit.collider != null)
      {
        hit.collider.GetComponent<BaseClass>().GetDamage(damage);
      }
    }
  }
  
  
  
  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.blue;
    var position = transform.position;
    Gizmos.DrawWireSphere(position, meleeAttackDistance); 
	
    Gizmos.color = Color.magenta;
    Vector3 lookDirection = -transform.up;
    Gizmos.DrawRay(transform.position, lookDirection * meleeAttackDistance);
    Gizmos.color = Color.green;
    //Quaternion rotation = Quaternion.AngleAxis(searchAngel, Vector3.forward);
    Vector3 v1 = Quaternion.AngleAxis(searchAngel, Vector3.forward) * lookDirection;
    Vector3 v2 = Quaternion.AngleAxis(-searchAngel, Vector3.forward) * lookDirection;
    Gizmos.DrawRay(transform.position, v1 * meleeAttackDistance);
    Gizmos.DrawRay(transform.position, v2 * meleeAttackDistance);
  }
}
