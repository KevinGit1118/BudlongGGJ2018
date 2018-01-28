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

    public int estimateColorBallNum = 0;
    public int currentColorBallNum = 0;
    public bool failFlag = false;

    void Awake()
    {
        targetTypeQueue = new Queue<GeneralTable.Type>();
    }

    // Use this for initialization
    void Start ()
    {
        Reset();
        GamePlayManager.OnGameOver += Reset;
    }

    public void Reset()
    {
        blackFlag = false;
        cumulativeTime = 0;

        estimateColorBallNum = 0;
        currentColorBallNum = 0;
        failFlag = false;

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

    public void AddEstimateColorBallNum()
    {
        estimateColorBallNum++;
    }

    public void RemoveEstimateColorBallNum()
    {
        estimateColorBallNum--;
    }

    // if Color match queue color, add one point, and create another color into queue.
    public bool match(GeneralTable.Type typeValue)
    {
        currentColorBallNum++;

        bool result = false;
        if(!typeValue.Equals(targetTypeQueue.Peek()))
        {
            failFlag = true;
            result = true;
        }

        if (currentColorBallNum == estimateColorBallNum)
        {
            if (failFlag)
            {
                blackFlag = true;
            }
            else
            {
                currentPoint++;
                targetTypeQueue.Dequeue();
                targetTypeQueue.Enqueue(GeneralTable.GetRandomType());
                centerPointMeshRenderer.material.color = GeneralTable.GetColor(targetTypeQueue.Peek());
            }
            
        }

        return result;
    }
}
