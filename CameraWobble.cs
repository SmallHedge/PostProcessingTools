//Created 05/08/2024
//Author: Small Hedge

using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class CameraWobble : MonoBehaviour
{
    [SerializeField] private float frequency;
    [SerializeField] private float rigidness;
    [SerializeField] private Vector2 maxValues;
    [SerializeField] private PostProcessVolume volume;
    private LensDistortion lens;
    private bool stop = false;

    void Start()
    {
        if (!volume.profile.TryGetSettings(out lens))
            lens = (LensDistortion)volume.profile.AddSettings(typeof(LensDistortion));
        lens.centerX.overrideState = true;
        lens.centerY.overrideState = true;
        if (lens.intensity.value == 0)
            lens.intensity.Override(-10);
        TriggerWobble();
    }

    public void StopWobble()
    {
        stop = true;
    }

    public void TriggerWobble()
    {
        stop = false;
        StartCoroutine(WobbleCycle());
        IEnumerator WobbleCycle()
        {
            while (!stop)
            {
                Vector2 target;
                target.x = Random.Range(-maxValues.x, maxValues.x);
                target.y = Random.Range(-maxValues.y, maxValues.y);

                float timer = Time.time + frequency;
                while(!stop && Time.time <= timer)
                {
                    if(rigidness > 0)
                    {
                        lens.centerX.value = Mathf.Lerp(lens.centerX, target.x, rigidness * Time.deltaTime);
                        lens.centerY.value = Mathf.Lerp(lens.centerY, target.y, rigidness * Time.deltaTime);
                    }
                    else
                    {
                        lens.centerX.value = target.x;
                        lens.centerY.value = target.y;
                    }

                    yield return null;
                }
            }

            lens.centerX.value = 0;
            lens.centerY.value = 0;
        }
    }
}
