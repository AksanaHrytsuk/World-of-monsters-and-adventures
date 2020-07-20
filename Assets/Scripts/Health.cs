using System;
using UnityEngine;

public class Health : PickUp
{
    [SerializeField] private int addHeart;
    private HeartsManager _heartsManager;
    public override void Apply(CharacterScript player)
    {
        if (player.health + addHeart >= player.maxHealth) 
        {
            player.health = player.maxHealth;
        }
        else 
        {
            player.health += addHeart;
        } 
        _heartsManager.ChangeHearts();
    }

    private void Start()
    {
        _heartsManager = FindObjectOfType<HeartsManager>();
    }
}
