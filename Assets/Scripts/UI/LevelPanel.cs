using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelPanel : BasePanel
{
    Transform UIEnemyIcon;
    Transform UIEnemyCounter;
    Transform UIPlayerHealthIcon;
    Transform UIPlayerHealthCounter;
    Transform UIRestartOrQuitButton;
    Transform UIStopButton;
    Transform Content;

    override protected  void Awake()
    {
        base.Awake();
        InitUI();
    }

    private void Update() {
        RefreshUI();
    }

    public void RefreshUI()
    {
        //更新敌人数量
        TMP_Text enemyCountText = UIEnemyCounter.GetComponent<TMP_Text>();
        enemyCountText.text = GameManager.Instance.enemyCurrentCount.ToString() + " / " + GameManager.Instance.enemyTotalCount.ToString();
        //更新玩家血量
        TMP_Text playerHealthText = UIPlayerHealthCounter.GetComponent<TMP_Text>();
        playerHealthText.text = GameManager.Instance.PlayerHealth.ToString();
    }

    private void InitUI()
    {
        UIEnemyIcon = transform.Find("Up/Info/EnemyCount/Icon");
        UIEnemyCounter = transform.Find("Up/Info/EnemyCount/Counter");
        UIPlayerHealthIcon = transform.Find("Up/Info/PlayerHealth/Icon");
        UIPlayerHealthCounter = transform.Find("Up/Info/PlayerHealth/Counter");

        UIRestartOrQuitButton = transform.Find("UpLeft/RestartOrQuitButton");

        UIStopButton = transform.Find("UpRight/StopButton");

        Content = transform.Find("BottomRight/Content");

        UIRestartOrQuitButton.GetComponent<Button>().onClick.AddListener(OnRestartOrQuitButtonClick);
        UIStopButton.GetComponent<Button>().onClick.AddListener(OnStopButtonClick);
    }

    private void OnStopButtonClick()
    {
        throw new NotImplementedException();
    }

    private void OnRestartOrQuitButtonClick()
    {
        throw new NotImplementedException();
    }
}
