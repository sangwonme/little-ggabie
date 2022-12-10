using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MissionController : MonoBehaviour
{
    public RectTransform timeBar;
    public Image boardImg;
    public Sprite eatImg;
    public Sprite playImg;
    public Sprite sleepImg;
    public float missionTime;
    private string missionType;
    private float remainingTime;
    private int babyIdx;

    public void initMission(){
        steRandomBaby();
        setRandomMission();
    }
    public void steRandomBaby(){
        float tmp = Random.Range(0, 3.0f);
        if(tmp < 1.0f){
            babyIdx = 1;
        }else if(tmp < 2.0f){
            babyIdx = 2;
        }else{
            babyIdx = 3;
        }
        setMissionType();
    }

    public void setRandomMission(){
        float tmp = Random.Range(0, 3.0f);
        if(tmp < 1.0f){
            missionType = "eat";
        }else if(tmp < 2.0f){
            missionType = "play";
        }else{
            missionType = "sleep";
        }
        setMissionType();
    }

    public void setMissionType(){
        switch(missionType){
            case "eat":
                boardImg.sprite = eatImg;
                break;
            case "play":
                boardImg.sprite = playImg;
                break;
            case "sleep":
                boardImg.sprite = sleepImg;
                break;
        }
    }

    public string getMissionType(){
        return missionType;
    }

    public void setBabyIdx(int idx){
        babyIdx = idx;
    }

    public int getBabyIdx(){
        return babyIdx;
    }

    // Start is called before the first frame update
    void Start()
    {
        remainingTime = missionTime;
    }

    // Update is called once per frame
    void Update()
    {
        remainingTime -= Time.deltaTime;

        // update timebar
        timeBar.sizeDelta = new Vector2((remainingTime/missionTime)*142f, 48f);

        // destroy
        if(remainingTime <= 0.0f){
            Destroy(gameObject);
        }
    }
}
