using Coffee.UIEffects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEffectOnOff : MonoBehaviour
{
    [SerializeField] private UIEffectBase ui;

    public void OnUIOver()
    {
        ui.enabled = true;
    }

    public void OffUIOver()
    {
        ui.enabled = false;
    }
}
