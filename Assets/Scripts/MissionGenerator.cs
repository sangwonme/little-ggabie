using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionGenerator : MonoBehaviour
{
    public Vector2 coolTimeRange;
    public GameObject missionPrefab;
    private Transform[] missions;
    private float coolTime;

    private float timeCount;


    private void resetCoolTime(){
        timeCount = 0.0f;
        coolTime = Random.Range(coolTimeRange.x, coolTimeRange.y);
    }

    private void giveNewMission(){
        Instantiate(missionPrefab, new Vector3(0,0,0), Quaternion.identity, transform);
        getAllMissions();
        missions[missions.Length-4].GetComponent<MissionController>().initMission();
        updateMissionPos();
    }

    private void getAllMissions(){
        missions = transform.GetComponentsInChildren<Transform>();
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
    }

    // Update is called once per frame
    void Update()
    {
        // time
        timeCount += Time.deltaTime;

        if(timeCount > coolTime){
            resetCoolTime();
            giveNewMission();
        }
    }
}
