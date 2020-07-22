using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abyss : BaseClass
{
    [SerializeField] private bool dangerousBlock;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Character") && dangerousBlock)
        {
            Animator.SetTrigger("SwitchOff/On");
            CharacterScript characterScript = FindObjectOfType<CharacterScript>();
            characterScript.GetDamage(damage);
        }
    }
}
