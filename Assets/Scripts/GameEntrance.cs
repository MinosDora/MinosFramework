using System;
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
}