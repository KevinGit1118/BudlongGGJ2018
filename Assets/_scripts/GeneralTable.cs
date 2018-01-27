using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralTable
{
    public enum Type
    {
        Red = 0,
        Blue = 1,
        Yellow = 2,
        Purple = 3,
        Green = 4,
        Orange = 5,
        TypeEnumMax = 6
    }

    public static Type Combine(Type type1, Type type2)
    {
        Type result = type1;
        if ((type1 == Type.Red && type2 == Type.Blue) || (type1 == Type.Blue && type2 == Type.Red))
        {
            result = Type.Purple;
        }
        else if ((type1 == Type.Blue && type2 == Type.Yellow) || (type1 == Type.Yellow && type2 == Type.Blue))
        {
            result = Type.Green;
        }
        else if ((type1 == Type.Yellow && type2 == Type.Red) || (type1 == Type.Red && type2 == Type.Yellow))
        {
            result = Type.Orange;
        }
        return result;
    }
    
    public static Type GetRandomType()
    {
        return (Type)Random.Range(0, (int)Type.TypeEnumMax);
    }

    public static Color GetColor(Type typeValue)
    {
        Color resultColor = Color.red;
        switch(typeValue)
        {
            case Type.Red:
                resultColor = Color.red;
                break;
            case Type.Blue:
                resultColor = Color.blue;
                break;
            case Type.Yellow:
                resultColor = Color.yellow;
                break;
            case Type.Purple:
                resultColor = new Color(255, 0, 255);
                break;
            case Type.Green:
                resultColor = Color.green;
                break;
            case Type.Orange:
                resultColor = new Color(255, 165, 0);
                break;
            default:
                break;
        }
        return resultColor;
    }
}
