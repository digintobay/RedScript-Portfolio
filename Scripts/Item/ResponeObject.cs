using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ResponeObject : InteractiveObject
{
    public override void Deactivate()
    {
        base.Deactivate();
        StartCoroutine(RespawnProcess());
    }
}
