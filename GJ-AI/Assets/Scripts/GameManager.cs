using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int coinTime;
    public int coin;
    public int bulletCount;
    public Text coinText;
    public Text coinTimeText;
    public Text bulletCountText;
    public int levelWeapon;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AddCoin());
    }

    // Update is called once per frame
    void Update()
    {
        coinTimeText.text = coinTime + "/s";
        coinText.text = "Coin: " + coin;
        bulletCountText.text = "x" + bulletCount +"/100";
    }

    public void AddCoinTime(int coinNumber)
    {
        coinTime += coinNumber;//Tăng số tiền cộng thêm mỗi giây
    }

    IEnumerator AddCoin()//cộng thêm tiền mỗi giây
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            coin += coinTime;
        }
    }

}
