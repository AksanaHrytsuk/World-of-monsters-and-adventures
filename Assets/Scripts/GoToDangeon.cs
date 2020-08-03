using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToDangeon : MonoBehaviour
{
    private CharacterScript _character;
    private Vector2 newPlayerPosition = new Vector3(x: 1.52f, y: -41.85f);

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Character"))
        {
            _character.startPosition = newPlayerPosition;
            _character.Save();
            
            SceneLoader.Instance.LoadNextSceneByName("SampleScene");
        }
    }

    private void Start()
    {
        _character = FindObjectOfType<CharacterScript>();
    }
}
