using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MissionController : MonoBehaviour
{
    public RectTransform timeBar;
    public Image boardImg;
    public Image timeBarColor;
    public Image idxColor;
    public Sprite eatImg;
    public Sprite playImg;
    public Sprite sleepImg;
    public float missionTime;
    private string missionType;
    private float remainingTime;
    private int babyIdx;
    private Color color;

    public void clearMission(){
        Destroy(gameObject);
    }

    public void initMission(int idx){
        setBabyIdx(idx);
        setRandomMission();
    }

    public void setRandomMission(){
        float tmp = Random.Range(0.0f, 1.0f);
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
        Debug.Log(idx);
        babyIdx = idx;
        switch(idx){
            case 1:
                ColorUtility.TryParseHtmlString("#4BCAE0", out color);
                break;
            case 2:
                ColorUtility.TryParseHtmlString("#E0D54B", out color);
                break;
            case 3:
                ColorUtility.TryParseHtmlString("#4BE060", out color);
                break;
        }
        timeBarColor.color = color;
        idxColor.color = color;
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
