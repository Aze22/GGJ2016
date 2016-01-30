using UnityEngine;
using System;

public class Clock_Anim : MonoBehaviour
{
    public Transform cog;

    private const float
       RotationSpeed = 360f / 60f;

    public bool analog;


    void Update()
    {
        if (analog)
        {
            TimeSpan timespan = DateTime.Now.TimeOfDay;
            cog.localRotation = Quaternion.Euler(0f, 0f, (float)timespan.TotalSeconds * -RotationSpeed);
        }
        else
        {
            DateTime time = DateTime.Now;
            cog.localRotation = Quaternion.Euler(0f, 0f, time.Hour * -RotationSpeed);
 

        }

    }
}

