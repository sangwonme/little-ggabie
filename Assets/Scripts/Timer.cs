using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public Light sunlight;
    public Light bulblight;
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
            if(sunlight.intensity < 0.8f){
                sunlight.intensity += 0.008f;
            }
        }else{
            if(sunlight.intensity > 0.0f){
                sunlight.intensity -= 0.008f;
            }
        }
        
        // bulb light set
        if(!isDay){
            if(bulblight.intensity < 5.0f){
                bulblight.intensity += 0.05f;
            }
        }else{
            if(bulblight.intensity > 0.0f){
                bulblight.intensity -= 0.05f;
            }
        }
    }
}
