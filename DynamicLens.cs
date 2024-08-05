//Created 05/08/2024
//Author: Small Hedge

using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class DynamicLens : MonoBehaviour
{
    [SerializeField] private float rigidness;
    [SerializeField] private PostProcessVolume volume;
    public static DynamicLens instance;

    private float defaultIntensity;
    private const float CUTOFF = 0.01f;
    private Coroutine currentCoroutine = null;
    private LensDistortion lens;

    private void Awake()
    {
        instance = this;
        if (!volume.profile.TryGetSettings(out lens))
            lens = (LensDistortion)volume.profile.AddSettings(typeof(LensDistortion));
        lens.intensity.overrideState = true;
        defaultIntensity = lens.intensity;
    }

    public void ChangeIntensity(float intensity)
    {
        if (currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(Loop());
        IEnumerator Loop()
        {
            while (Mathf.Abs(lens.intensity.value - intensity) > CUTOFF)
            {
                lens.intensity.value = Mathf.Lerp(lens.intensity.value, intensity, Time.deltaTime * rigidness);
                yield return null;
            }

            lens.intensity.value = intensity;
            currentCoroutine = null;
        }
    }

    public void ReturnToDefault()
    {
        ChangeIntensity(defaultIntensity);
    }
}
