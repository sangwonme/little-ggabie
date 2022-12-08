using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIClock : MonoBehaviour
{
    private Timer timer;
    private float timeLeft;
    private float totalTime;
    private RectTransform clockNeedle;
    private string dayText;


    // Start is called before the first frame update
    void Start()
    {
        // timer
        timer = GameObject.Find("GameController").GetComponent<Timer>();
        totalTime = timer.totalTime;
        updateTimeLeft();
        // ui asset
        clockNeedle = GameObject.Find("ClockNeedle").GetComponent<RectTransform>();
        // text
        dayText = "Day 1";
    }

    // check timeleft
    void updateTimeLeft(){
        timeLeft = timer.timeLeft;
    }

    // set time needle
    void setTimeNeedle(){
        updateTimeLeft();
        float neeldeAng = (timeLeft/totalTime) * (360);
        // clockNeedle.Rotate(new Vector3(0, 0, neeldeAng));
        clockNeedle.rotation = Quaternion.Euler(new Vector3(0, 0, neeldeAng+1));
        
    }

    // show day count
    void setText(){

    }


    // Update is called once per frame
    void Update()
    {
        setTimeNeedle();
    }
}
