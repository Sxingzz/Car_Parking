using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private LinesDrawer linesDrawer;

    [Space]
    [SerializeField] private CanvasGroup availableLineCanvasGroup;
    [SerializeField] private GameObject availableLineHolder;
    [SerializeField] private Image availableLineFill;

    private bool isAvailableUILineActive = false;

    [Space]
    [SerializeField] private Image fadePanel;
    [SerializeField] private float fadeDuaration;

    private Route activeRoute;

    private void Start()
    {
        fadePanel.DOFade(0f, fadeDuaration).From(1f);

        availableLineCanvasGroup.alpha = 0f;

        linesDrawer.OnBeginDraw += OnBeginDrawHandler;
        linesDrawer.OnDraw      += OnDrawHandler;
        linesDrawer.OnEndDraw   += OnEndDrawHandler;
    }

    private void OnBeginDrawHandler(Route route)
    {
        activeRoute = route;

        availableLineFill.color = activeRoute.carColor;
        availableLineFill.fillAmount = 1f;
        availableLineCanvasGroup.DOFade(1f, 0.3f).From(0f);
        isAvailableUILineActive = true;
    }

    private void OnDrawHandler()
    {
        if (isAvailableUILineActive)
        {
            float maxLineLength = activeRoute.maxLineLength;
            float lineLength = activeRoute.line.length;

            availableLineFill.fillAmount = 1 - (lineLength / maxLineLength);
        }
    }

    private void OnEndDrawHandler()
    {
        if (isAvailableUILineActive)
        {
            isAvailableUILineActive = false;
            activeRoute = null;

            availableLineCanvasGroup.DOFade(0f, 0.3f).From(1f);
        }
    }

   

    
}
