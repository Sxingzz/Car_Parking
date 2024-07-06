using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Route : MonoBehaviour
{
    [HideInInspector]
    public bool isActive = true;

    public Line line;
    public Park park;
    public Car car;

    [Space]
    [Header("Colors: ")]

    public Color carColor;

    [SerializeField]
    private Color lineColor;


    public void Disactivate()
    {
        isActive = false;
    }

    // Auto positionning and assign colors in the editor
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying && line != null && car != null && park != null)
        {
            line.lineRenderer.SetPosition(0,car.bottomTransform.position);
            line.lineRenderer.SetPosition(1, park.transform.position );

            car.SetColor(carColor);
            park.SetColor(carColor);
            line.SetColor(lineColor);
        }
    }

#endif

}
