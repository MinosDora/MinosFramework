using System;
using MinosFramework;
using UnityEngine;

public class GameEntrance : MonoBehaviour
{
    public static GameEntrance Instance;
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
}