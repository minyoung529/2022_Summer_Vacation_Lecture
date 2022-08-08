using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSB : MonoBehaviour
{
    void Start()
    {
        StringTest();
    }

    private void StringTest()
    {
        string s = "";
        StringBuilder sb = new StringBuilder();

        Debug.Log($"String StartTime: {DateTime.Now.ToLongTimeString()}");

        for(int i = 0; i < 50000; i++)
        {
            s += "HI ";
        }

        Debug.Log($"String EndTime: {DateTime.Now.ToLongTimeString()}");
        Debug.Log($"SB Start Time: {DateTime.Now.ToLongTimeString()}");

        for (int i = 0; i < 50000; i++)
        {
            sb.Append("HI ");
        }

        Debug.Log($"SB EndTime: {DateTime.Now.ToLongTimeString()}");
    }
}
