using System.Collections.Generic;
using Lean.Pool;
using UnityEngine;

public class RoomMove : MonoBehaviour
{
    [SerializeField] private Vector3 characterChange; 
    [SerializeField] private Vector2 cameraChange;
    public List<GameObject>  objectsToDestroy = new List<GameObject>();
    public GameObject[] objectsToCreate;
    public Vector3[] positionsToCreate;
    [SerializeField] private RoomMove anotherSideOfTheDoor;
    
    private List<GameObject> instantiatedObjects = new List<GameObject>();

    private CameraController _cameraController;

    private void Awake()
    {
        _cameraController =  Camera.main.GetComponent<CameraController>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Character"))
        {
            _cameraController.minPosition += cameraChange;
            _cameraController.maxPosition += cameraChange;
            other.transform.position += characterChange;
            DestroyObjects(anotherSideOfTheDoor.instantiatedObjects);
            DestroyObjects(objectsToDestroy);
            CreateObjects();
        }
    }

    private void DestroyObjects(List<GameObject> list)
    {
        foreach (var obj in list)
        {
            if (obj == null)
            {
                continue;
            }
            LeanPool.Despawn(obj);
        }
    }

    private void CreateObjects()
    {
        for (int i = 0; i < objectsToCreate.Length; i++)
        {
            GameObject obj = LeanPool.Spawn(objectsToCreate[i], positionsToCreate[i], transform.rotation);
            instantiatedObjects.Add(obj);
        }
    }
}
