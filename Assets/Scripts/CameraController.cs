using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
 [SerializeField] private Transform target;
 [SerializeField] private float smoothing = 0.5f;
 [SerializeField] public Vector2 maxPosition;
 [SerializeField] public Vector2 minPosition;
 
 
 private void LateUpdate()
 {
  if (transform.position != target.position)
  {
   Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
   // ограничивает передвижение по оси х и оси у для камеры
   targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
   targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);
   transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
  }
 }
}
