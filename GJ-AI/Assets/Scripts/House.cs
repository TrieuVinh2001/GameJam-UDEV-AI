using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class House : MonoBehaviour
{
    public float heart;//Máu công trình
    private float heartMax;
    public GameObject btnUpdate;//Nút nâng cấp

    public int[] level, coinUp, addCoin;//cấp độ nhà, tiền nâng cấp, tiền cộng thêm từ nhà sau mỗi cấp

    private int levelCurren = 1;//Cấp nhà hiện tại

    public Text levelText;
    public Text coinMinusText, coinAddText;

    public Image healthImage;

    private void Awake()
    {
        btnUpdate.gameObject.SetActive(false);//Ẩn nút nâng cấp
    }

    // Start is called before the first frame update
    void Start()
    {
        heartMax = heart;
        levelCurren = 1;
        levelText.text = "LV:" + levelCurren;
        coinAddText.text = "+" + addCoin[0] + "/s";
        coinMinusText.text = "-" + coinUp[0];
        GameManager.instance.AddCoinTime(addCoin[0]);//Thêm tiền cộng lúc cấp 1
        
    }

    // Update is called once per frame
    void Update()
    {
        healthImage.fillAmount = heart / heartMax;
        levelText.text = "LV:" + levelCurren;
        if (heart <= 0)
        {
            GameManager.instance.LossGame();
            Destroy(gameObject);
        }
    }

    public void BtnUpdate()
    {
        if (levelCurren < level.Length && GameManager.instance.coin>=coinUp[levelCurren-1])//Nếu cấp hiện tại < cấp cao nhât và tiền >= tiền cần để nâng cấp
        {
            GameManager.instance.coin -= coinUp[levelCurren-1];//Trừ tiền
            levelCurren += 1;//tăng cấp nhà
            
            for (int i = 1; i < level.Length; i++)
            {
                if (levelCurren == level[i])
                {
                    GameManager.instance.AddCoinTime(addCoin[i]);//Tăng tiền cộng thêm theo mỗi cấp nhà
                    if(levelCurren == level.Length)
                    {
                        coinMinusText.text = "max";
                        coinAddText.text = "max";
                    }
                    else
                    {
                        coinMinusText.text = "-" + coinUp[i];
                        coinAddText.text = "+" + addCoin[i+1] + "/s";
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
