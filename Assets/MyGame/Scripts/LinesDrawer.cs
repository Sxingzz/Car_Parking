using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinesDrawer : MonoBehaviour
{
    [SerializeField]
    private UserInput userInput;
    [SerializeField]
    private int interactableLayer;

    private Line currentLine;
    private Route currentRoute;

    private RaycastDetector raycastDetector = new();


    private void Start()
    {
        userInput.OnMouseDown += OnMouseDownHandler;
        userInput.OnMouseMove += OnMouseMoveHandler;
        userInput.OnMouseUp   += OnMouseUpHandler;
    }

    // Begin Draw
    private void OnMouseDownHandler()
    {
        ContactInfo contactInfo = raycastDetector.RayCast(interactableLayer);

        if (contactInfo.contacted)
        {
            bool isCar = contactInfo.collider.TryGetComponent(out Car _car);

            if (isCar && _car.route.isActive)
            {
                currentRoute = _car.route;
                currentLine = currentRoute.line;
                currentLine.Init();
            }
        }
    }

    // Draw
    private void OnMouseMoveHandler()
    {
        if (currentRoute != null)
        {
            ContactInfo contactInfo = raycastDetector.RayCast(interactableLayer);

            if (contactInfo.contacted)
            {
                Vector3 newPoint = contactInfo.point;

                currentLine.AddPoint(newPoint);

                bool isPark = contactInfo.collider.TryGetComponent(out Park _park);

                if (isPark)
                {
                    Route parkRoute = _park.route;
                    
                    if (parkRoute == currentRoute)
                    {
                        currentLine.AddPoint(contactInfo.transform.position);
                        OnMouseUpHandler();
                    }
                    else
                    {
                        // Delete the line
                        currentLine.Clear();
                    }
                    OnMouseUpHandler();
                }
            }
        }
    }

    // End Draw
    private void OnMouseUpHandler()
    {
        if (currentRoute != null)
        {
            ContactInfo contactInfo = raycastDetector.RayCast(interactableLayer);

            if (contactInfo.contacted)
            {
                bool isPark = contactInfo.collider.TryGetComponent(out Park _park);

                if (currentLine.pointsCount < 2 || !isPark)
                {
                    // Delette the line:
                    currentLine.Clear();
                }
                else
                {
                    currentRoute.Disactivate();
                }
            }
            else
            {
                // Dellete the line
                currentLine.Clear() ;
            }
        }
        ResetDrawer();
    }

    private void ResetDrawer()
    {
        currentRoute = null;
        currentLine = null;
    }
}
