using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantController : MonoBehaviour
{
    private UIMerchant ui;
    private Timer timer;
    private float opacity;
    public SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("GameController").GetComponent<Timer>();
        ui = GetComponent<UIMerchant>();
        ui.turnOffUI();
    }

    // Update is called once per frame
    void Update()
    {
        // appear
        if(timer.isDay){
            if(opacity < 1.0f){
                opacity += 0.01f;
            }
        }
        // vanish
        else{
            if(opacity > 0.0f){
                opacity -= 0.01f;
            }
            ui.setUIKey(false);
        }
        sprite.color = new Color(1.0f, 1.0f, 1.0f, opacity);
    }

    private void OnTriggerStay(Collider other) {
        if(other.tag == "Player" && timer.isDay){
            ui.setUIKey(true);
            if(Input.GetKey(KeyCode.X)){
                ui.setUIShop(true);
                timer.makePause();
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player"){
            ui.setUIKey(false);
        }
    }
}
