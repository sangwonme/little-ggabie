using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealth : MonoBehaviour
{
    public Health health;
    public RectTransform healthBar;
    private float healthLength;

    private void updateHealthLength(){
        float length =  (health.hp / health.fullHp) * healthLength;
        Debug.Log(length);
        healthBar.sizeDelta = new Vector2(length, 28f);
    }
    // Start is called before the first frame update
    void Start()
    {
        healthLength = 330.0f;
    }

    // Update is called once per frame
    void Update()
    {
        updateHealthLength();
    }
}
