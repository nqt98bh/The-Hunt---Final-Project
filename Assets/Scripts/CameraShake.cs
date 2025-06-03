using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public CinemachineVirtualCamera VirtualCamera;
    private CinemachineBasicMultiChannelPerlin noise;
    public float shakeInterval = 1f;

    public float amplitude;
    public float frequency;
    float timer = 0;
    bool isShaking = false;
    Coroutine c_waitShake;
    private void Start()
    {
        noise = VirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }
    void Shake()
    {

        noise.m_AmplitudeGain = amplitude;
        noise.m_FrequencyGain = frequency;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isShaking )
        {

            Shake();
            isShaking = true;
            if(c_waitShake != null)
            {
                StopCoroutine(c_waitShake);
                c_waitShake = null;
            }
            c_waitShake = StartCoroutine(WaitShake());

           //timer = shakeInterval;
        }
        //if (timer > 0)
        //{
        //    timer -= Time.deltaTime;
        //    if(timer < 0)
        //    {
        //        noise.m_AmplitudeGain = 0;
        //        noise.m_FrequencyGain = 0;
        //        timer = 0;
        //    }
        //}

    }
    IEnumerator WaitShake()
    {
        yield return new WaitForSeconds( shakeInterval );
        noise.m_AmplitudeGain = 0;
        noise.m_FrequencyGain = 0;
        isShaking = false;
    }

}
