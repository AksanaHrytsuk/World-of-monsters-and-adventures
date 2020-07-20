using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HeartsManager : MonoBehaviour
{
    #region Singletone

    public static HeartsManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); 
        }
        else
        {
            Instance = this;
        }
    }

    #endregion
    [SerializeField] private List<Image> hearts = new List<Image>();
    [SerializeField] private BaseClass _player;
    [SerializeField] private Sprite _heartSprite;
    [SerializeField] private Sprite _transparentSprite;

    private Health _health;
    
    public void ChangeHearts()
    {
        for (int i = 0; i < _player.health; i++)
        {
            hearts[i].sprite = _heartSprite;
        }
    
        for (int i = _player.health; i < hearts.Count; i++)
        {
            hearts[i].sprite = _transparentSprite;
        }
    }
    
    void Start()
    {
        ChangeHearts();
        _player = FindObjectOfType<CharacterScript>();
        _player.onHealthChanged += ChangeHearts;
    }
}
