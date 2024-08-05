//Created 05/08/2024
//Author: Small Hedge

using System.Collections;
using UnityEngine;

public class DynamicFOV : MonoBehaviour
{
    [SerializeField] private float rigidness;
    public static DynamicFOV instance;

    private float defaultFOV;
    private Camera c;
    private const float CUTOFF = 0.1f;
    private Coroutine currentCoroutine = null;

    private void Awake()
    {
        instance = this;
        c = GetComponent<Camera>();
        defaultFOV = c.fieldOfView;
    }

    public void ChangeFOV(float FOV)
    {
        if(currentCoroutine != null)
            StopCoroutine(currentCoroutine);
        currentCoroutine = StartCoroutine(Loop());
        IEnumerator Loop()
        {
            while(Mathf.Abs(FOV - c.fieldOfView) > CUTOFF)
            {
                c.fieldOfView = Mathf.Lerp(c.fieldOfView, FOV, Time.deltaTime * rigidness);
                yield return null;
            }

            c.fieldOfView = FOV;
            currentCoroutine = null;
        }
    }

    public void ReturnToDefault()
    {
        ChangeFOV(defaultFOV);
    }
}
