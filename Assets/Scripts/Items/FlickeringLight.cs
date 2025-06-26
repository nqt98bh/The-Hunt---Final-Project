using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlickeringLight : MonoBehaviour
{
    private Light2D lightToFlick;

    //Intensity of Light component
    [SerializeField, Range(0f, 20f)] private float minIntensity = 0.5f; 
    [SerializeField, Range(0f, 20f)] private float maxIntensity = 0.5f;
    [SerializeField, Min(0f)] private float timeBetweenIntensity = 0.1f;

    private float currentTime;
    private void Awake()
    {
        if (lightToFlick == null) 
        { 
            lightToFlick = GetComponent<Light2D>();
        }
        ValidateIntensityBounds();
    }
    private void FixedUpdate()
    {
        currentTime += Time.fixedDeltaTime;
        if(currentTime >= timeBetweenIntensity)
        {
            lightToFlick.intensity = Random.Range(minIntensity, maxIntensity);
            currentTime = 0f;
        }
    }

    private void ValidateIntensityBounds() //check min is not greater than max intensity
    {
        if (!(minIntensity > maxIntensity)) return;
        Debug.LogError("Min Intensity is greater than Max Intensity");
        (minIntensity,maxIntensity) = (maxIntensity,minIntensity);
    }

}
