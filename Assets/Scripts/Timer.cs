using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Light sunlight;
    public Light moonLight;
    public Light redLight;
    public Light bulbLight;
    public float nightTime;
    public float totalTime;
    public bool isDay = true;
    public float timeLeft;
    public int dayCount;

    // Start is called before the first frame update
    void Start()
    {
        timeLeft = totalTime;
        dayCount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // countdown
        timeLeft -= Time.deltaTime;
        if(timeLeft <= nightTime){
            isDay = false;
        }
        if(timeLeft <= 0){
            isDay = true;
            timeLeft = totalTime;
            dayCount += 1;
        }

        // sun light set
        if(isDay){
            if(sunlight.intensity < 10.0f){
                sunlight.intensity += 0.1f;
            }
        }else{
            if(sunlight.intensity > 0.0f){
                sunlight.intensity -= 0.1f;
            }
        }
        
        // moon light set
        if(!isDay){
            if(moonLight.intensity < 5.0f){
                moonLight.intensity += 0.05f;
            }
            if(redLight.intensity < 2.0f){
                redLight.intensity += 0.02f;
            }
        }else{
            if(moonLight.intensity > 0.0f){
                moonLight.intensity -= 0.05f;
            }
            if(redLight.intensity < 0.0f){
                redLight.intensity -= 0.02f;
            }
        }

        // bulb light set
        if(isDay && sunlight.intensity > 5.0f){
            if(bulbLight.intensity > 0.0f){
                bulbLight.intensity -= 0.07f;
            }
        }
        else if(!isDay && moonLight.intensity > 2.5f){
            if(bulbLight.intensity < 7.0f){
                bulbLight.intensity += 0.07f;
            }
        }
    }
}
