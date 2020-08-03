using System;
using UnityEngine;
using DG.Tweening;
using Lean.Pool;
public class PortalBetweenPoints : MonoBehaviour
{
    [SerializeField] Transform destinationPoint;
    [SerializeField] private ParticleSystem portalEffect;
    [SerializeField] private float waitTime = 0.5f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Apply(collision);
    }

    public virtual void Apply(Collider2D collision)
    {
        TeleportWithEffect(collision);
    }

    public void TeleportWithEffect(Collider2D collision)
    {
        
        Sequence sequence = DOTween.Sequence();
        Vector2 position = collision.transform.position;
        sequence.AppendCallback(() => Teleport(collision));
        sequence.AppendCallback(() => PortalEffect(position));
    }
    void Teleport(Collider2D collision)
    {
        if (collision.CompareTag("Character"))
        {
            collision.transform.position = destinationPoint.position;
        }
    }

    private void PortalEffect(Vector2 position)
    {
        if (portalEffect != null)
        {
            Vector3 portalEffectPosition = position;
            LeanPool.Spawn(portalEffect, portalEffectPosition, transform.rotation);
        }
    }
}
