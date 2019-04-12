using System;
using MinoFramework;
using UnityEngine;

public class GameMain : MonoBehaviour
{
    public static GameMain Instance;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
}