using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionGenerator : MonoBehaviour
{
    public Vector2 coolTimeRange;
    public GameObject missionPrefab;
    private Transform[] missions;
    public Timer timer;
    private int missionNum;
    private float coolTime;

    private float timeCount;
    

    public void destroyChildren(){
         foreach (Transform child in transform) {
            Destroy(child.gameObject);
        }
    }

    public void clearMissionOfBaby(int babyIdx){
        for(int i = 0; i < missions.Length; i++){
            if(missions[i].tag == "Mission"){
                if(missions[i].GetComponent<MissionController>().getBabyIdx() == babyIdx){
                    missions[i].GetComponent<MissionController>().clearMission();
                }
            }
        }
    }

    // return priory mission of given baby, if doesn't exist return "none"
    public string getPrioryMission(int babyIdx){
        getAllMissions();
        for(int i = 0; i < missions.Length; i++){
            if(missions[i].tag == "Mission"){
                if(missions[i].GetComponent<MissionController>().getBabyIdx() == babyIdx){
                    return missions[i].GetComponent<MissionController>().getMissionType();
                }
            }
        }
        return "none";
    }

    private void resetCoolTime(){
        timeCount = 0.0f;
        coolTime = Random.Range(coolTimeRange.x, coolTimeRange.y);
    }

    // check which baby has no mission
    private void giveNewMission(){
        // tmp dir, tmp i = baby direction, start baby idx
        int tmpi = 3 + (int)Random.Range(1, 4);
        int tmpdir = Random.Range(0, 1.0f) < 0.5f ? -1 : 1;
        for(int i = 0; i < 3; i ++){
            int idx = tmpi % 3 + 1;
            if(getPrioryMission(idx) == "none"){
                // add mission
                Instantiate(missionPrefab, new Vector3(0,0,0), Quaternion.identity, transform);
                getAllMissions();
                missions[missions.Length-5].GetComponent<MissionController>().initMission(idx);
                updateMissionPos();
                return;
            }else{
                tmpi += tmpdir;
            }
        }


    }

    private void getAllMissions(){
        missions = transform.GetComponentsInChildren<Transform>();
        missionNum = 0;
        foreach(Transform mission in missions){
            missionNum += (mission.tag == "Mission") ? 1 : 0;
        }
    }

    private void updateMissionPos(){
        int cnt = 0;
        for(int i = 0; i < missions.Length; i++){
            if(missions[i].tag == "Mission"){
                missions[i].GetComponent<RectTransform>().position = new Vector3(100f+147f*cnt, 965f, 0f);
                cnt += 1;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        resetCoolTime();
        missionNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // time
        if(!timer.pause){
            timeCount += Time.deltaTime;
        }

        // update pos if mission is destroyed
        int tmp = missionNum;
        getAllMissions();
        if(tmp != missionNum){
            updateMissionPos();
        }

        // make new mission
        if(timeCount > coolTime){
            resetCoolTime();
            giveNewMission();
        }
    }
}
