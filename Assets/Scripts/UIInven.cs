using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIInven : MonoBehaviour
{
    private Inventory inven;
    public TMP_Text meatNum;
    public TMP_Text flowerNum;
    
    // Start is called before the first frame update
    void Start()
    {
        inven = GameObject.Find("GameController").GetComponent<Inventory>();
        meatNum.text = "";
        flowerNum.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        // update inven nums
        meatNum.text = inven.getMeatNum().ToString();
        flowerNum.text = inven.getFlowerNum().ToString();
    }
}
