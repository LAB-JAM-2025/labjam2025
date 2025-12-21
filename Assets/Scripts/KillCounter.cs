using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;
public class KillCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI killCountText;
    [SerializeField] private Image ProgressBar;
    public int killCount = 0;
    public static event Action<int> OnSwitchWave;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        killCountText.text = "Kills: " + killCount.ToString();
        if (ProgressBar != null)
            ProgressBar.fillAmount = (float)killCount / 60;
        if (killCount == 15)
        {
            SwitchWave(2);
        }
        else if (killCount == 35)
        {
            SwitchWave(3);
        }
        else if (killCount == 60)
        {
            //EndGame();
        }
    }


    void OnEnable()
    {
        Enemy.OnEnemyKilled += AddKill;
    }

    void OnDisable()
    {
        Enemy.OnEnemyKilled -= AddKill;
    }

    public void AddKill()
    {

        killCount += 1;
        // here goes the  logic to switch waves or end the game

    }

    void SwitchWave(int waveNumber)
    {
        switch (waveNumber)
        {
            case 2:
                OnSwitchWave?.Invoke(2);
                break;
            case 3:
                OnSwitchWave?.Invoke(3);
                break;
            default:
                Debug.Log("Unknown wave number!");
                break;
        }
    }


}