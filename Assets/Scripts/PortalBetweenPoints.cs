using UnityEngine;
using DG.Tweening;
using Lean.Pool;
public class PortalBetweenPoints : MonoBehaviour
{
    [SerializeField] Transform destinationPoint;
    [SerializeField] private ParticleSystem portalEffect;
    [SerializeField] private float waitTime = 0.5f;
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendCallback(() => Teleport(collision));
        sequence.AppendCallback(PortalEffect);
    }
    void Teleport(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {collision.transform.position = destinationPoint.position;}
    }

    private void PortalEffect()
    {
        if (portalEffect != null)
        {
            Vector3 portalEffectPosition = transform.position;
            LeanPool.Spawn(portalEffect, portalEffectPosition, transform.rotation);
        }
    }
}
