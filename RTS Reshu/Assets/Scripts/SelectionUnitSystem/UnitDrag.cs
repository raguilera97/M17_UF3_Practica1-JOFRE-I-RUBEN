using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitDrag : MonoBehaviour
{
    Camera myCam;
    [SerializeField] RectTransform selectorVisual;

    Rect selectorBox;

    Vector2 startPosition;
    Vector2 endPosition;

    void Start()
    {
        myCam = Camera.main;
        startPosition = Vector2.zero;
        endPosition = Vector2.zero;
        DrawVisual();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPosition = Input.mousePosition;
            selectorBox = new Rect();
        }

        if (Input.GetMouseButton(0))
        {
            endPosition = Input.mousePosition;
            DrawVisual();
            DrawSelection();
        }

        if (Input.GetMouseButtonUp(0))
        {
            SelectUnits();
            startPosition = Vector2.zero;
            endPosition = Vector2.zero;
            DrawVisual();
            DrawSelection();
        }

    }

    void DrawVisual()
    {
        Vector2 selectorStart = startPosition;
        Vector2 selectorEnd = endPosition;

        Vector2 boxCenter = (selectorStart + selectorEnd) / 2;
        selectorVisual.position = boxCenter;

        Vector2 selectorSize = new Vector2(Mathf.Abs(selectorStart.x-selectorEnd.x), Mathf.Abs(selectorStart.y-selectorEnd.y));
        selectorVisual.sizeDelta = selectorSize;

        }

    void DrawSelection()
    {
        if(Input.mousePosition.x < startPosition.x)
        {
            selectorBox.xMin = Input.mousePosition.x;
            selectorBox.xMax = startPosition.x;
        }
        else
        {
            selectorBox.xMin = startPosition.x;
            selectorBox.xMax = Input.mousePosition.x;
        }

        if(Input.mousePosition.y < startPosition.y)
        {
            selectorBox.yMin = Input.mousePosition.y;
            selectorBox.yMax = startPosition.y;
        }
        else
        {
            selectorBox.yMin = startPosition.y;
            selectorBox.yMax = Input.mousePosition.y;
        }
    }

    void SelectUnits()
    {
        foreach (Unit unit in UnitSelection.Instance.unitList)
        {
            if (selectorBox.Contains(myCam.WorldToScreenPoint(unit.transform.position)))
            {
                UnitSelection.Instance.DragSelect(unit);
            }
        }
    }
}
