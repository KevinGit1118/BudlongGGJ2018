using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBall : MonoBehaviour {

    public MeshRenderer ballMeshrenderer = null;
    private Color _ballcolor = Color.white; 
    public Color BallColor
    {
        get { return _ballcolor; }
        set { _ballcolor = value;
            SetColor(value);
        }
    }

    private void SetColor(Color color)
    {
        ballMeshrenderer.material.color = color;
    }

}
