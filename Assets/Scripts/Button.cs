using UnityEngine;

public class Button : MonoBehaviour
{
  public Platform platform;
  private Platform _platform;


  public void OnTriggerEnter2D(Collider2D other)
  {
    if (other.CompareTag("Character"))
    { 
      _platform.Move();
    }
  }

  private void Start()
  {
    _platform = FindObjectOfType<Platform>();
  }
}
