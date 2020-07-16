using UnityEngine;

public class Health : PickUp
{
    [SerializeField] private int addHeart;
            protected override void Apply(CharacterScript player)
            {
                if (player.health + addHeart >= player.maxHealth) 
                {
                    player.health = player.maxHealth;
                }
                else 
                {
                    player.health += addHeart;
                }
            }
}
