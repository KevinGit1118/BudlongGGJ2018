﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorLine : MonoBehaviour
{
    public MeshRenderer colorLineRenderer;
    public GeneralTable.Type colorType = GeneralTable.Type.White;

    public void Start()
    {
        colorLineRenderer.material.color = GeneralTable.GetColor(colorType);        
    }

    public void AddType(GeneralTable.Type type)
    {
        colorType = GeneralTable.Combine(type, colorType);
        colorLineRenderer.material.color = GeneralTable.GetColor(colorType);
    }

    public void RemoveType(GeneralTable.Type type)
    {
        colorType = GeneralTable.Remove(type, colorType);
        colorLineRenderer.material.color = GeneralTable.GetColor(colorType);
    }
}
