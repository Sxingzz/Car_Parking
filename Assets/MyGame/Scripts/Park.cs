using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Park : MonoBehaviour
{
    public Route route;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private ParticleSystem fx;

    private ParticleSystem.MainModule fxMainModule;

    private void Start()
    {
        fxMainModule = fx.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Car car))
        {
            if (car.route == route)
            {
                Game.Instance.OnCarEntersPark?.Invoke(route);
                StartFX();
            }
        }
    }

    private void StartFX()
    {
        fxMainModule.startColor = route.carColor;
    }

    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }
}