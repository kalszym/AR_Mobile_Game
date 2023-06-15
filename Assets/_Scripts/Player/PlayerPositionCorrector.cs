﻿using System;
using UnityEngine;

public class PlayerPositionCorrector : MonoBehaviour
{
    private void Start()
    {
        CorrectPosition();
    }

    public void CorrectPosition()
    {
        var startingPosition = transform.position;
        var xEndPosition = (float)(Math.Round(startingPosition.x, MidpointRounding.ToEven));
        var zEndPosition = (float)(Math.Round(startingPosition.z, MidpointRounding.ToEven));
        Vector3 endPosition = new Vector3(xEndPosition, startingPosition.y, zEndPosition);
        transform.position = endPosition;
    }
}