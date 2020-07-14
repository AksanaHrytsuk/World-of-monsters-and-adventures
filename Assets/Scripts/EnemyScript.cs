using UnityEngine;

public class EnemyScript : BaseClass
{
  [SerializeField] public Transform targetPosition;

  public bool canShoot = true;

  private float distance;

  private float meleeAttackDistance;

  [Tooltip("Components")]
  private CharacterScript _character;

  void CheckFreeze()
  {
    if (_character.Frozen)
    {
      canShoot = false;
    }
    else
    {
      canShoot = true;
    }
  }
  
  private void Update()
  {
    UpdateDistance();
    CheckFreeze();
  }
  
  protected override void StartAdditional()
  {
    _character = FindObjectOfType<CharacterScript>();
    Movement = GetComponent<Movement>();
  }

  private void UpdateDistance()
  {
    if (_character != null)
    {
      // Distance to player
      distance = Vector3.Distance(transform.position, _character.transform.position);
      Animator.SetFloat("distance", distance);
    }
  }

  protected override void Death()
  {
    base.Death();
    Destroy(this);
  }

  public Vector2 Direction()
  {
      return _character.transform.position - transform.position;
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
  }
}
