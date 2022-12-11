using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionGenerator : MonoBehaviour
{
    public Vector2 coolTimeRange;
    public GameObject missionPrefab;
    private Transform[] missions;
    private int missionNum;
    private float coolTime;

    private float timeCount;


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

    private void giveNewMission(){
        Instantiate(missionPrefab, new Vector3(0,0,0), Quaternion.identity, transform);
        getAllMissions();
        missions[missions.Length-5].GetComponent<MissionController>().initMission();
        updateMissionPos();
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
        timeCount += Time.deltaTime;

        // update pos if mission is destroyed
        int tmp = missionNum;
        getAllMissions();
        if(tmp != missionNum){
            updateMissionPos();
        }

        // make new mission
        if(timeCount > coolTime){
            resetCoolTime();
            if(missionNum < 9){
                giveNewMission();
            }
        }
    }
}
