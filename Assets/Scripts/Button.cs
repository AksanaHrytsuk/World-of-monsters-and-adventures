﻿using UnityEngine;

public class Button : MonoBehaviour
{
  public Platform platform;

  public void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Character"))
    { 
      platform.Move();
    }
  }
}
