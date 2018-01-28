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

    public float fixedScale;

    public Animator centerPointAnimator;

    void Awake()
    {
        targetTypeQueue = new Queue<GeneralTable.Type>();

        fixedScale = this.transform.localScale.x;
    }

    // Use this for initialization
    void Start ()
    {
        Reset();
        GamePlayManager.OnGameStart += Reset;
        centerPointMeshRenderer.material.color = GeneralTable.GetColor(GeneralTable.Type.White);
    }

    public void Reset()
    {
        currentPoint = 0;

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

        this.transform.localScale = new Vector3(fixedScale, fixedScale, fixedScale);
    }

    void PlayExplosionSound()
    {
        AudioManager.PlayExplosion();
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
            }
        }
    }

    public void FinalGameOver()
    {
        GamePlayManager.OnGameOver();
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
            GamePlayManager.Instance.ResetTimer();
            if (failFlag)
            {
                PerformFail();
            }
            else
            {
                currentPoint++;
                GamePlayManager.Instance.NextStage();
                GeneralTable.Type currentType = targetTypeQueue.Peek();
                while(currentType == targetTypeQueue.Peek())
                {
                    targetTypeQueue.Dequeue();
                    targetTypeQueue.Enqueue(GeneralTable.GetRandomType());
                }
                centerPointMeshRenderer.material.color = GeneralTable.GetColor(targetTypeQueue.Peek());
            }
            
        }

        return result;
    }

    public void PerformFail()
    {
        GamePlayManager.Instance.StopTimer();
        blackFlag = true;
        centerPointAnimator.Play("Anim_Explode");
    }
}
