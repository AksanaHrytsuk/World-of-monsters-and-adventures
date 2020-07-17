using System;
using UnityEngine;

public class RoomMove : MonoBehaviour
{
    [SerializeField] private Vector3 characterChange;
    [SerializeField] private Vector2 cameraChange;
    [SerializeField] private Vector3 spownMonsterposition;
    [SerializeField] private GameObject monsterPrefab;

    private CameraController _cameraController;
    private Monster _monster;

    private void Awake()
    {
        _cameraController =  Camera.main.GetComponent<CameraController>();
    }

    private void Start()
    {
        _monster = FindObjectOfType<Monster>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Character"))
        {
            _cameraController.minPosition += cameraChange;
            _cameraController.maxPosition += cameraChange;
            other.transform.position += characterChange;
            if (_monster == null )
            {
                Instantiate(monsterPrefab, spownMonsterposition, transform.rotation);
            }
        }
    }
}
