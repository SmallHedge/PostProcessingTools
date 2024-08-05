//Created 05/08/2024
//Author: Small Hedge

using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class RotationalShift : MonoBehaviour
{
    [SerializeField] private Vector2 speed = Vector2.zero;
    [SerializeField] private Vector2 multiplier = Vector2.one;
    [SerializeField] private Transform target;
    [SerializeField] private PostProcessVolume volume;
    private LensDistortion lens;
    private Vector2 offset = Vector2.zero;

    void Start()
    {
        if (!volume.profile.TryGetSettings(out lens))
            lens = (LensDistortion)volume.profile.AddSettings(typeof(LensDistortion));
        lens.centerX.overrideState = true;
        lens.centerY.overrideState = true;
        if (lens.intensity.value == 0)
            lens.intensity.Override(-30);
    }

    private void Update()
    {
        offset += speed * Time.deltaTime;
        lens.centerX.value = multiplier.x * GetValue(target.eulerAngles.y + offset.x);
        lens.centerY.value = multiplier.y * GetValue(target.eulerAngles.x + offset.y);

        static float GetValue(float value)
        {
            return Mathf.Abs(value % 360 - 180) / 90 - 1;
        }
    }
}
