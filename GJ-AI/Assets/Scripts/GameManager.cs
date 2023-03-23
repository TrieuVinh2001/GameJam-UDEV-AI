using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int coinTime;
    public int coin;
    public int bulletCount;
    public Text coinText;
    public Text coinTimeText;
    public Text bulletCountText;
    public int levelWeapon;
    public GameObject note;

    public GameObject panelPause;
    public GameObject panelLoss;
    public GameObject panelWin;

    public Text timeGameText;
    public float timeGame;

    private void Awake()
    {
        panelPause.SetActive(false);
        panelLoss.SetActive(false);
        panelWin.SetActive(false);
        note.SetActive(false);
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        StartCoroutine(AddCoin());
    }

    // Update is called once per frame
    void Update()
    {
        coinTimeText.text = coinTime + "/s";
        coinText.text = "Coin: " + coin;
        bulletCountText.text = "x" + bulletCount +"/100";

        if (bulletCount <=0)
        {
            note.SetActive(true);
        }
        else
        {
            note.SetActive(false);
        }

        timeGame -= Time.deltaTime;
        timeGameText.text = Mathf.RoundToInt(timeGame).ToString();

        if (timeGame <= 0)
        {
            WinGame();
        }
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

    public void PauseGame()
    {
        Time.timeScale = 0f;
        panelPause.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        panelPause.SetActive(false);
    }

    public void LossGame()
    {
        Time.timeScale = 0f;
        panelLoss.SetActive(true);
    }

    public void WinGame()
    {
        Time.timeScale = 0f;
        panelWin.SetActive(true);
    }

    public void RePlayGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenuGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

}
