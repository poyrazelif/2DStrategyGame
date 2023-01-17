using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : Product
{
    public Transform startCellPos;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public void ChangeColor(Color color)
    {
        _spriteRenderer.color = color;
    }
}
