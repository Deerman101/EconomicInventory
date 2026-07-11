using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSpeedController : MonoBehaviour
{
    public static GameSpeedController Instance { get; private set; }

    public event Action<float> OnSpeedChanged;

    public float CurrentSpeed { get; private set; } = 1f;

    private void Awake()
    {
        Instance = this;
        SetSpeed(1f);
    }

    public void SetX1() => SetSpeed(1f);
    public void SetX2() => SetSpeed(2f);
    public void SetX3() => SetSpeed(3f);

    public void SetSpeed(float speed)
    {
        CurrentSpeed = speed;
        Time.timeScale = speed;

        OnSpeedChanged?.Invoke(speed);
    }
}
