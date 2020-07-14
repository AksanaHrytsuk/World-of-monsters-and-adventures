using UnityEngine;

public class RoomMove : MonoBehaviour
{
    [SerializeField] private Vector3 characterChange;
    [SerializeField] private Vector2 cameraChange;

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
        }
    }
}
