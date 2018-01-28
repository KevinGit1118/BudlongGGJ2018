using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterPoint : MonoBehaviour
{
    const int countMax = 10;

    Queue<GeneralTable.Type> targetTypeQueue;

    public int currentPoint = 0;

    public MeshRenderer centerPointMeshRenderer;

    public bool blackFlag = false;

    public float colorLerpPeriod = 1f;

    private float cumulativeTime = 0f;

    void Awake()
    {
        targetTypeQueue = new Queue<GeneralTable.Type>();
    }

    // Use this for initialization
    void Start ()
    {
        Reset();
    }

    public void Reset()
    {
        blackFlag = false;
        cumulativeTime = 0;

        targetTypeQueue.Clear();
        for (int i = 0; i < countMax; ++i)
        {
            targetTypeQueue.Enqueue(GeneralTable.GetRandomType());
        }
        centerPointMeshRenderer.material.color = GeneralTable.GetColor(targetTypeQueue.Peek());
    }

    private void Update()
    {
        if(blackFlag)
        {
            cumulativeTime += Time.deltaTime;
            centerPointMeshRenderer.material.color = Color.Lerp(centerPointMeshRenderer.material.color, GeneralTable.GetColor(GeneralTable.Type.White), cumulativeTime / colorLerpPeriod);
            if(cumulativeTime / colorLerpPeriod >= 1)
            {
                blackFlag = false;
                GamePlayManager.OnGameOver();
            }
        }
        
    }

    // if Color match queue color, add one point, and create another color into queue.
    public bool match(GeneralTable.Type typeValue)
    {
        bool result = false;
        if(typeValue.Equals(targetTypeQueue.Peek()))
        {
            currentPoint++;
            targetTypeQueue.Dequeue();
            targetTypeQueue.Enqueue(GeneralTable.GetRandomType());
            centerPointMeshRenderer.material.color = GeneralTable.GetColor(targetTypeQueue.Peek());
            result = true;
        }
        else
        {
            blackFlag = true;
        }
        return result;
    }
}
