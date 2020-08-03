using System;
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
  [SerializeField] float units;
  
  [Header("CheckPLayer")]
  [SerializeField] private LayerMask platform;
  [SerializeField] private float platformInRadius = 1f;

  private float _targetRight;
  private float _targetLeft;
  private float _targetForward;
  private float _targetBack;


  // public void OnTriggerStay2D(Collider2D other)
  // {
  //   if (other.CompareTag("Character"))
  //   {
  //     RaycastHit2D hit2D = Physics2D.Raycast(_characterScript.transform.position, -Vector2.up, platformInRadius, platform);
  //     if (hit2D.collider != null && _onPlatform)
  //     {
  //       
  //     }
  //   }
  // }

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
