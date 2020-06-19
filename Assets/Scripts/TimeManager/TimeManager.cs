using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    public float slowFactor = 0.05f;
    public float slowLength = 2f;

    public void SlowMotion()
    {
        Time.timeScale = slowFactor;
    }
}
