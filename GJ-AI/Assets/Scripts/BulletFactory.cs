using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletFactory : MonoBehaviour
{
    public float heart;//Máu công trình
    public Button btnBuy;//Nút nâng cấp

    // Start is called before the first frame update
    void Start()
    {
        btnBuy.gameObject.SetActive(false);//Ẩn nút nâng cấp
    }

    public void BtnBuy()
    {
        if (GameManager.instance.coin >= 10 && GameManager.instance.bulletCount < 100)//Nếu cấp hiện tại < cấp cao nhât và tiền >= tiền cần để nâng cấp
        {
            GameManager.instance.coin -= 10;//Trừ tiền
            if(GameManager.instance.bulletCount >= 80)
            {
                GameManager.instance.bulletCount = 100;
            }
            else
            {
                GameManager.instance.bulletCount += 20;
            }
            
        }
    }
    public void TakeDamage(float damage)
    {
        heart -= damage;
    }
}
