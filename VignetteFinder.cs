//Created 05/08/2024
//Author: Small Hedge

using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignetteFinder : MonoBehaviour
{
    [SerializeField] private float maxIntensity;
    [SerializeField] private float minIntensity;
    [SerializeField] private float rigidness;
    [SerializeField] private float sensitivity;
    [SerializeField] private Camera targetCamera;
    [SerializeField] private PostProcessVolume volume;
    private Vignette vignette;

    private Vector2 prevPos = new(0.5f, 0.5f);

    void Start()
    {
        if (!volume.profile.TryGetSettings(out vignette))
            vignette = (Vignette)volume.profile.AddSettings(typeof(Vignette));
        vignette.intensity.overrideState = true;
        vignette.center.overrideState = true;
    }

    private void Update()
    {
        vignette.center.value = Input.mousePosition / targetCamera.pixelRect.size;

        float movement = Vector2.Distance(prevPos, vignette.center.value) * Time.deltaTime;
        float targetIntensity = (minIntensity - maxIntensity) / (1 / sensitivity) * movement + maxIntensity;
        vignette.intensity.value = Mathf.Lerp(vignette.intensity.value, targetIntensity, rigidness * Time.deltaTime);
        vignette.intensity.value = Mathf.Clamp(vignette.intensity.value, minIntensity, maxIntensity);
        prevPos = vignette.center.value;
    }
}
