using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPush : MonoBehaviour
{
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask objects;

    private GameObject box;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       RaycastHit2D hit =  Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, maxDistance, objects);
       if (hit.collider != null)
       {
           box = hit.collider.gameObject;
       }
    }
}
