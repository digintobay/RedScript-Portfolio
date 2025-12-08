using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class UIvolumeSlider : MonoBehaviour
{
    private PostProcessVolume volume;
    private Vignette vignette;
    public Slider vignetteSlider;


    private void Awake()
    {
        // PostProcessVolume 가져오기
        volume = GetComponent<PostProcessVolume>();

        vignette = volume.profile.GetSetting<Vignette>();


    }

    private void Start()
    {
        float value = PlayerPrefs.GetFloat("Vignette", vignette.intensity.value);
        vignette.intensity.Override(value);
        vignetteSlider.value = value;

        vignetteSlider.onValueChanged.AddListener(SetVignetteIntensity);
    }

    public void SetVignetteIntensity(float value)
    {
        if (vignette != null)
        {
            vignette.intensity.Override(value);
            PlayerPrefs.SetFloat("Vignette", value);
        }
    }


  
}
