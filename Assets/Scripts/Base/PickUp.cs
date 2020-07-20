using UnityEngine;

public class PickUp : MonoBehaviour
{
    CharacterScript _characterScript;
   public void OnTriggerEnter2D(Collider2D other)
   {
       if (other.CompareTag("Character"))
       {
           CharacterScript player = other.GetComponent<CharacterScript>(); 
           Apply(player); 
           Destroy(gameObject);
       }
   }

   public virtual void Apply(CharacterScript player) { }
}
