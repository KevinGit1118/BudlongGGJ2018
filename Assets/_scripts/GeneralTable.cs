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
        TypeEnumMax = 6,
        White = 7
    }

    public static Type Combine(Type type1, Type type2)
    {
        Type result = type1;
        if(result == Type.White)
        {
            result = type2;
        }

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

    public static Type Remove(Type type1, Type type2)
    {
        Type result = Type.White;
        if(type1 != type2)
        {
            if(type1 == Type.Purple || type2 == Type.Purple)
            {
                if(type1 == Type.Red || type2 == Type.Red)
                {
                    result = Type.Blue;
                }
                else if(type1 == Type.Blue || type2 == Type.Blue)
                {
                    result = Type.Red;
                }
            }
            else if (type1 == Type.Green || type2 == Type.Green)
            {
                if (type1 == Type.Blue || type2 == Type.Blue)
                {
                    result = Type.Yellow;
                }
                else if (type1 == Type.Yellow || type2 == Type.Yellow)
                {
                    result = Type.Blue;
                }
            }
            else if (type1 == Type.Orange || type2 == Type.Orange)
            {
                if (type1 == Type.Yellow || type2 == Type.Yellow)
                {
                    result = Type.Red;
                }
                else if (type1 == Type.Red || type2 == Type.Red)
                {
                    result = Type.Yellow;
                }
            }
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
                resultColor = new Color(1, 0, 1);
                break;
            case Type.Green:
                resultColor = Color.green;
                break;
            case Type.Orange:
                resultColor = new Color(1, 0.6f, 0);
                break;
            case Type.White:
                resultColor = Color.white;
                break;
            default:
                break;
        }
        return resultColor;
    }
}
