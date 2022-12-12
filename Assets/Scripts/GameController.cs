using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameController : MonoBehaviour {

	public GameObject title;
	public GameObject gameover;
	public GameObject tutorial;
	public Health health;
	public Timer timer;
	public PlayerController player;
	public MonsterGeneration monsterGenrator;
	public MissionGenerator missions;
	public GameObject lights;
	public GameObject ingameUI;
	public BabyController[] babys;
	public Inventory inven;
	public CamerController cam;
	
	public TMP_Text dayText;
	
	private string state;

	void powerOnObject(bool turn){
		health.enabled = turn;
		timer.enabled = turn;
		player.enabled = turn;
		monsterGenrator.enabled = turn;
		monsterGenrator.transform.gameObject.SetActive(turn);
		missions.enabled = turn;
		missions.transform.gameObject.SetActive(turn);
		lights.SetActive(turn);
		ingameUI.SetActive(turn);
		foreach(BabyController baby in babys){
			baby.enabled = turn;
		}
		cam.enabled = turn;
		inven.enabled = turn;
	}

	void initGame(){
		// init timer
		timer.init();
		// init player pos
		player.init();
		// init baby pos
		foreach(BabyController baby in babys){
			baby.init();
		}
		// init inven
		inven.init();
		// init health
		health.init();
		// camera follow
		cam.init();
		// destory all monsters
		monsterGenrator.destroyChildren();
		// destory all missions
		missions.destroyChildren();
	}

    void Start ()
	{
		powerOnObject(false);
		title.SetActive(true);
		gameover.SetActive(false);
		tutorial.SetActive(false);
		state = "title";
	}

    void Update()
	{

		switch(state){
			case "title":
				powerOnObject(false);
				title.SetActive(true);
				tutorial.SetActive(false);
				gameover.SetActive(false);
				break;
			case "play":
				title.SetActive(false);
				gameover.SetActive(false);
				tutorial.SetActive(false);
				ingameUI.SetActive(true);
				timer.makeResume();
				// go to gameover
				if(health.isDead()){
					dayText.text = timer.dayCount.ToString();
					state = "gameover";
				}
				// menu
				if(Input.GetKeyDown(KeyCode.Escape)){
					state = (state == "pause") ? "play" : "pause";
				}
				break;
			case "pause":
				timer.makePause();
				tutorial.SetActive(true);
				ingameUI.SetActive(false);
				break;
			case "gameover":
				powerOnObject(false);
				gameover.SetActive(true);
				tutorial.SetActive(false);
				break;
		}

    }

    public void OnButton(string btn)
	{
		switch(btn){
			case "start":
				powerOnObject(true);
				state = "play";
				break;
			case "home":
				state = "title";
				initGame();
				break;
			case "resume":
				state = "play";
				break;
		}
	}

}
