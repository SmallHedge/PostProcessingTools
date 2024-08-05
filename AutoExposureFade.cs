//Created 05/08/2024
//Author: Small Hedge

using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AutoExposureFade : MonoBehaviour
{
    [SerializeField] private float defaultValue = 1;
    [SerializeField] private float whiteValue = 30;
    [SerializeField] private PostProcessVolume volume;
    private AutoExposure autoExposure;

    private void Start()
    {
        if (!volume.profile.TryGetSettings(out autoExposure))
            autoExposure = (AutoExposure)volume.profile.AddSettings(typeof(AutoExposure));
        autoExposure.keyValue.overrideState = true;
        autoExposure.speedUp.overrideState = true;
        autoExposure.speedDown.overrideState = true;
    }

    public void FadeToBlack(float speed)
    {
        SetValue(0, speed);
    }

    public void FadeToWhite(float speed)
    {
        SetValue(whiteValue, speed);
    }

    public void ResetExposure(float speed)
    {
        SetValue(defaultValue, speed);
    }

    private void SetValue(float target, float speed)
    {
        autoExposure.speedUp.value = speed;
        autoExposure.speedDown.value = speed;
        autoExposure.keyValue.value = target;
    }
}
