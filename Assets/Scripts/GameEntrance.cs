using System;
using System.Collections.Generic;
using UnityEngine;
using MinosFramework;

public class GameEntrance : MonoBehaviour
{
    public static GameEntrance Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        TimerManager.Instance.Tick();
    }
}