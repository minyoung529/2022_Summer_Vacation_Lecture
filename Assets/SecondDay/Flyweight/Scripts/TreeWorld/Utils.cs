using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

using Random = UnityEngine.Random;

public static class Utils
{
    private static System.Random random = new System.Random();

    public static string GetRandomName(int length = 6)
    {
        string name = "";

        for (int i = 0; i < length; i++)
        {
            if (Random.Range(0, 2) == 0)
                name += (char)Random.Range('0', '9' + 1);

            else
                name += (char)Random.Range('A', 'Z' + 1);
        }

        return name;
    }

    public static string GetRandomNameTeacher(int length = 6)
    {
        const string chars = "ABCDEFGHIJKLMNOPQSTUVWSYZ123456789";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static T RandomEnum<T>()
    {
        Array values = Enum.GetValues(typeof(T));
        return (T)values.GetValue(random.Next(values.Length));
    }
}
