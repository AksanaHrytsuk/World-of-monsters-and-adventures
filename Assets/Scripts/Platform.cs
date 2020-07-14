using UnityEngine;
using DG.Tweening;

public class Platform :  BaseClass
{
  
  [Header("Positions")]
  [SerializeField] bool forward;

  [SerializeField] bool back;

  [SerializeField] bool left;

  [SerializeField] bool right;
    
  [Header("Distance")]
  [SerializeField] int units;

  private float _targetRight;
  private float _targetLeft;
  private float _targetForward;
  private float _targetBack;

  private void Start()
  {
    // сохранить состояние
    _targetRight = transform.position.x + units;
    _targetLeft = transform.position.x - units;
    _targetForward = transform.position.y + units;
    _targetBack = transform.position.y - units;
  }

  public void Move()
  {
    if (forward)
    {
      transform.DOMoveY(_targetForward, duration: 1, false);
    }

    if (back)
    {
      transform.DOMoveY(_targetBack, 1, false);
    }

    if (left)
    {
      transform.DOMoveX(_targetLeft, 1, false);
    }

    if (right)
    {
      transform.DOMoveX (_targetRight, 1, false);
    }
  }
 

}
