//Created 05/08/2024
//Author: Small Hedge

using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class MegaBlur : MonoBehaviour
{
    [SerializeField] private float amount;
    [SerializeField] private PostProcessVolume volume;

    void Start()
    {
        if (!volume.profile.TryGetSettings(out MotionBlur blur))
            blur = (MotionBlur)volume.profile.AddSettings(typeof(MotionBlur));
        blur.shutterAngle.Override(amount);
    }
}
