//Created 05/08/2024
//Author: Small Hedge

using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class AdaptiveFocus : MonoBehaviour
{
    [SerializeField, UnityEngine.Min(0)] private float rigidness;
    [SerializeField, UnityEngine.Min(0)] private float maxDistance = float.PositiveInfinity;
    [SerializeField] private LayerMask layer;

    [SerializeField] private PostProcessVolume volume;
    private DepthOfField depthOfField;

    private void Start()
    {
        if(!volume.profile.TryGetSettings(out depthOfField))
            depthOfField = (DepthOfField)volume.profile.AddSettings(typeof(DepthOfField));
        depthOfField.focusDistance.overrideState = true;
    }

    void Update()
    {
        float target = maxDistance;
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance, layer))
            target = Vector3.Distance(transform.position, hit.point);

        if (rigidness <= 0)
            depthOfField.focusDistance.value = target;
        else
            depthOfField.focusDistance.value = Mathf.Lerp(depthOfField.focusDistance.value, target, rigidness * Time.deltaTime);

    }
}
