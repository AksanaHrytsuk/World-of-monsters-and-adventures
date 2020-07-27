using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class EnemyScript : BaseClass
{
  [SerializeField] public Transform targetPosition;

  [SerializeField] private Text text;
  public bool canShoot = true;

  private float distance;
  private float meleeAttackDistance;

  [Tooltip("Components")]
  private CharacterScript _character;

  private Portal _portal;
  

  public void CreatPortal()
  {
    Sequence sequence = DOTween.Sequence();
    sequence.AppendInterval(1f);
    //sequence.AppendCallback(() => AudioManager.Instance.PLaySound(portalMusic));
    sequence.AppendCallback(_portal.NextLevelEffect);
    sequence.AppendInterval(1f);
    sequence.AppendCallback(_portal.OnEnablePortal);
  }

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

  void UpdateHpText()
  {
    text.text = "HP: " + health + "/" + maxHealth;
  }
  
  protected override void StartAdditional()
  {
    _portal = FindObjectOfType<Portal>();
    _character = FindObjectOfType<CharacterScript>();
    Movement = GetComponent<Movement>();
    UpdateHpText();
    onDeath += CreatPortal;
    onHealthChanged += UpdateHpText;
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
    _character.attackType = "Ice";
  }

  // public Vector2 Direction()
  // {
  //     return _character.transform.position - transform.position;
  // }

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
