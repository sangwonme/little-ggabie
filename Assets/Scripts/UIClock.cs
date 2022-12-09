using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIClock : MonoBehaviour
{
    private Timer timer;
    private float timeLeft;
    private float totalTime;
    private RectTransform clockNeedle;
    public TMP_Text dayText;


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
        dayText.text = "Day 1";
    }

    // check timeleft
    void updateTimeLeft(){
        timeLeft = timer.timeLeft;
    }

    // set time needle
    void setTimeNeedle(){
        updateTimeLeft();
        float neeldeAng = (timeLeft/totalTime) * (360);
        clockNeedle.rotation = Quaternion.Euler(new Vector3(0, 0, neeldeAng+1));
        
    }

    void updateDate(){
        dayText.text = "D-" + timer.dayCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        setTimeNeedle();
        updateDate();
    }
}
