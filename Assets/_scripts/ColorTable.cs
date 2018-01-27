using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorTable
{
    //TODO: real color table, MQ

    // May not need, can directly use Equal.
    public static bool Compare(Color color1, Color color2)
    {
        return color1.Equals(color2);
    }

    // Combine 2 Colors and return combined Color.
    public static Color Combine(Color color1, Color color2)
    {
        Color result = color1;
        if (color1.Equals(Color.red) && color2.Equals(Color.blue))
        {
            result = new Color(255, 0, 255);
        }
        else if (color1.Equals(Color.blue) && color2.Equals(Color.yellow))
        {
            result = Color.green;
        }
        else if (color1.Equals(Color.yellow) && color2.Equals(Color.red))
        {
            result = new Color(255, 128, 0);
        }
        return result;
    }

    // Get Random Color from 6 colors.
    public static Color GetRandomColor()
    {
        int colorRelatedNumber = Random.Range(0, 6);
        Color result = Color.red;
        switch(colorRelatedNumber)
        {
            case 0:
                // red
                break;
            case 1:
                result = Color.blue;
                break;
            case 2:
                result = Color.yellow;
                break;
            case 3:
                result = Color.green;
                break;
            case 4:
                result = new Color(255, 0, 255);
                break;
            case 5:
                result = new Color(255, 128, 0);
                break;
            default:
                break;
        }
        return result;
    }
}
