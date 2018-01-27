using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLine : MonoBehaviour
{
    public MeshRenderer colorLineRenderer;

    public void SetColor(Color color)
    {
        colorLineRenderer.material.color = color;
    }

    public void ResetColor()
    {
        colorLineRenderer.material.color = Color.white;
    }
}
