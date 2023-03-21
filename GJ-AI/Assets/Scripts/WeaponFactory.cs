﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponFactory : MonoBehaviour
{
    public float heart;//Máu công trình
    public Button btnUpdate;//Nút nâng cấp

    public int[] level, coinUp;//cấp độ nhà, tiền nâng cấp, tiền cộng thêm từ nhà sau mỗi cấp

    private int levelCurren = 1;//Cấp nhà hiện tại

    public Text levelText;
    public Text coinMinusText;

    // Start is called before the first frame update
    void Start()
    {
        btnUpdate.gameObject.SetActive(false);//Ẩn nút nâng cấp
        levelText.text = "LV:" + levelCurren;
        GameManager.instance.levelWeapon = levelCurren;
        coinMinusText.text = "-" + coinUp[0];
    }

    // Update is called once per frame
    void Update()
    {
        levelText.text = "LV:" + levelCurren;
    }

    public void BtnUpdate()
    {
        if (levelCurren < level.Length && GameManager.instance.coin >= coinUp[levelCurren-1])//Nếu cấp hiện tại < cấp cao nhât và tiền >= tiền cần để nâng cấp
        {
            GameManager.instance.coin -= coinUp[levelCurren - 1];//Trừ tiền
            levelCurren += 1;//tăng cấp nhà
            GameManager.instance.levelWeapon = levelCurren;
            for (int i = 1; i < level.Length; i++)
            {
                if (levelCurren == level[i])
                {
                    
                    if (levelCurren == level.Length)
                    {
                        coinMinusText.text = "max";
                    }
                    else
                    {
                        coinMinusText.text = "-" + coinUp[i];
                    }
                }
            }
        }
    }
    public void TakeDamage(float damage)
    {
        heart -= damage;
    }
}
