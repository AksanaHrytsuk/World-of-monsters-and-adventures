using System;
using UnityEngine;

public class Exit : MonoBehaviour
{
    private CharacterScript _character;
    private Vector2 newPlayerPosition = new Vector3(x: -1, y: -1);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Character"))
        {
            SceneLoader.Instance.LoadNextScene();
            _character.transform.position = newPlayerPosition;
        }
    }

    private void Start()
    {
        _character = FindObjectOfType<CharacterScript>();
    }
}
