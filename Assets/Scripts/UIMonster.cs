using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMonster : MonoBehaviour
{

    public Image keyImg;
    public Sprite Z;
    public Sprite X;
    public Sprite C;
    private GameObject uiKey;
    private GameObject uiMission;
    private float missionLength;

    public void setKeyImg(string key){
        switch(key){
            case "z":
                keyImg.sprite = Z;
                break;
            case "x":
                keyImg.sprite = X;
                break;
            case "c":
                keyImg.sprite = C;
                break;
        }
    }

    public void setUIKey(bool turn){
        uiKey.SetActive(turn);
    }

    public void setUIMission(bool turn){
        uiMission.SetActive(turn);
    }

    public void setMissionLength(float holdingTime, float totalTime=3.0f){
        missionLength = 0.68f * (holdingTime/totalTime);
        uiMission.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(missionLength, 0.0517f);
    }

    // Start is called before the first frame update
    void Start()
    {
        uiKey = gameObject.transform.GetChild(1).GetChild(0).gameObject;
        uiMission = gameObject.transform.GetChild(1).GetChild(1).gameObject;
        missionLength = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
