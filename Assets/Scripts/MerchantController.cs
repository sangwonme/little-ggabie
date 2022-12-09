using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MerchantController : MonoBehaviour
{
    private UIMerchant ui;

    // Start is called before the first frame update
    void Start()
    {
        ui = GetComponent<UIMerchant>();
        ui.turnOffUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other) {
        if(other.tag == "Player"){
            ui.setUIKey(true);
            if(Input.GetKeyDown(KeyCode.C)){
                ui.setUIShop(true);
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.tag == "Player"){
            ui.setUIKey(false);
        }
    }
}
