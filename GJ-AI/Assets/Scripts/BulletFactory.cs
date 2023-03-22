using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletFactory : MonoBehaviour
{
    public float heart;//Máu công trình
    
    public Button btnBuy;//Nút nâng cấp
    public Image healthImage;
    private float heartMax;
    // Start is called before the first frame update
    void Start()
    {
        heartMax = heart;
        btnBuy.gameObject.SetActive(false);//Ẩn nút nâng cấp
    }

    private void Update()
    {
        healthImage.fillAmount = heart / heartMax;
        if (heart <= 0)
        {
            Destroy(gameObject);
        }
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
