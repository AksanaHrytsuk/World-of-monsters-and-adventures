using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Sprite redFlag;
    public Sprite greenFlag;
    private SpriteRenderer checkpointSpriteRenderer;
    public bool checkpointReached;
    // Use this for initialization
    void Start () {
        checkpointSpriteRenderer = GetComponent<SpriteRenderer> ();
    }
  
    // Update is called once per frame
    void Update () {
  
    }
    void OnTriggerEnter2D(Collider2D other){
        if (other.CompareTag($"Character")) {
            checkpointSpriteRenderer.sprite = greenFlag;
            checkpointReached = true;
        }
    }
}
