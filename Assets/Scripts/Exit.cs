using System;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private CharacterScript _character;
    [SerializeField] Vector2 newPlayerPosition = new Vector2(x: -30, y: -14);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Character"))
        {
            // Character's data
            _character.startPosition = newPlayerPosition;
            _character.Save();

            SceneLoader.Instance.LoadNextSceneByName("MainScene");
        }
    }

    private void Start()
    {
        _character = FindObjectOfType<CharacterScript>();
    }
}
