using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterPoint : MonoBehaviour
{
    const int countMax = 10;

    Queue<GeneralTable.Type> targetTypeQueue;

    public int currentPoint = 0;

    public MeshRenderer centerPointMeshRenderer;

    void Awake()
    {
        targetTypeQueue = new Queue<GeneralTable.Type>();
    }

    // Use this for initialization
    void Start ()
    {
		for(int i = 0; i < countMax; ++i)
        {
            targetTypeQueue.Enqueue(GeneralTable.GetRandomType());
        }

        centerPointMeshRenderer.material.color = GeneralTable.GetColor(targetTypeQueue.Peek());
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
            result = true;
        }
        return result;
    }
}
