using System;
using MinoFramework;
using UnityEngine;

public class Main : MonoBehaviour
{
    private void OnGUI()
    {
        if (GUI.Button(new Rect(100, 100, 100, 100), "Add"))
        {
            EventMgr.Instance.AddListener(MinoFramework.EventType.MyType, (Action)MyFunc);
        }
        else if (GUI.Button(new Rect(100, 200, 100, 100), "Delete"))
        {
            EventMgr.Instance.RemoveListener(MinoFramework.EventType.MyType, (Action)MyFunc);
        }
        else if (GUI.Button(new Rect(100, 300, 100, 100), "Trigger"))
        {
            EventMgr.Instance.TriggerEvent(MinoFramework.EventType.MyType);
        }
    }

    public void MyFunc()
    {
        Debug.LogError("哈哈哈哈哈哈哈哈哈哈哈");
    }
}