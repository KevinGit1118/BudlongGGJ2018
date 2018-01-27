using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterPoint : MonoBehaviour
{
    const int colorCountMax = 10;

    Queue<Color> targetColorQueue;

    public int currentPoint = 0;

    void Awake()
    {
        targetColorQueue = new Queue<Color>();
    }

    // Use this for initialization
    void Start ()
    {
		for(int i = 0; i < colorCountMax; ++i)
        {
            targetColorQueue.Enqueue(ColorTable.GetRandomColor());
        }

        for (int i = 0; i < colorCountMax; ++i)
        {
            Debug.Log("#### " + targetColorQueue.Dequeue().ToString());
        }
    }

    // if Color match queue color, add one point, and create another color into queue.
    public bool match(Color color)
    {
        bool result = false;
        if(color.Equals(targetColorQueue.Peek()))
        {
            currentPoint++;
            targetColorQueue.Dequeue();
            targetColorQueue.Enqueue(ColorTable.GetRandomColor());
            result = true;
        }
        return result;
    }
}
