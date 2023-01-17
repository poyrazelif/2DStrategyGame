using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollContent : MonoBehaviour
{
    public float Height { get { return height; } }
    public float ChildHeight { get { return childHeight; } }

    private RectTransform rectTransform;
    private RectTransform[] rtChildren;
    private float width, height;
    private float childWidth, childHeight;
    public float horizontalMargin, verticalMargin;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rtChildren = new RectTransform[rectTransform.childCount];

        for (int i = 0; i < rectTransform.childCount; i++)
        {
            rtChildren[i] = rectTransform.GetChild(i) as RectTransform;
        }
        
        width = rectTransform.rect.width - (2 * horizontalMargin);
        height = rectTransform.rect.height - (2 * verticalMargin);

        childWidth = rtChildren[0].rect.width;
        childHeight = rtChildren[0].rect.height;
        
        InitializeContentVertical();
    }

    private void InitializeContentVertical()
    {
        float originY = 0 - (height * 0.5f);
        float posOffset = childHeight * 0.5f;
        
        for (int i = 0; i < rtChildren.Length; i++)
        {
            Vector2 childPos = rtChildren[i].localPosition;
            childPos.y = originY + posOffset + i * (childHeight*1.5f);
            rtChildren[i].localPosition = childPos;
        }
    }
}
