using System.Collections;
using Cinemachine;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public Player playerPrefab;
    private Player player;

    public Transform spawnPoint;

    public GameObject loseLevelPanel;
    public GameObject finishLevelPanel;

    public TextMeshProUGUI playerHpText;
    
    private float time;
    public TextMeshProUGUI finishTime;

    public bool isFinished = true;

    public GameObject cameraSettings;
    public Transform finishCameraPos;


    private void Start()
    {
        StartSetup();
    }

    private void StartSetup()
    {
        //spawn
        player = Instantiate(playerPrefab);

        player.hpText = playerHpText;
        Spawn();

        //set some fields
        player.SetCameraSettings(cameraSettings);
    }

    public void StartGame()
    {
        isFinished = false;
        StartCoroutine(Timer());
    }

    private void OnDeath()
    {
        LoseLevel();
        player.DeathEvent -= OnDeath;
    }

    private void Spawn()
    {
        player.gameObject.SetActive(true);

        player.transform.position = spawnPoint.position;

        player.DeathEvent += OnDeath;

        player.FullRestore();
    }

    private void LoseLevel()
    {
        isFinished = true;
        OpenOrClosePanel(loseLevelPanel, true);
    }

    public void FinishLevel()
    {
        isFinished = true;
        playerHpText.enabled = false;
        
        FinishCamera();
        player.gameObject.SetActive(false);
        OpenOrClosePanel(finishLevelPanel, true);

        TimeFormat();
    }

    private void TimeFormat()
    {
        int minutes = (int)time / 60;
        float seconds = time % 60;

        string formattedTime = string.Format("Время забега: <color=red>{0}м.:{1:00.##}</color>c.", minutes, seconds);
        finishTime.text = formattedTime;
    }

    public void ResetLvl()
    {
        OpenOrClosePanel(loseLevelPanel, false);
        OpenOrClosePanel(finishLevelPanel, false);

        time = 0f;
        
        playerHpText.enabled = true;
        
        Spawn();
        StartCamera();
    }

    private IEnumerator Timer()
    {
        do
        {
            time += Time.deltaTime;
            if (isFinished == true)
            {
                break;
            }

            yield return new WaitForSeconds(Time.deltaTime);
        } while (true);
    }

    private void FinishCamera()
    {
        var settings = cameraSettings.GetComponent<CinemachineVirtualCamera>();
        settings.Follow = null;
        settings.LookAt = null;
        cameraSettings.transform.position = finishCameraPos.position;
        cameraSettings.transform.rotation = finishCameraPos.rotation;
    }

    private void StartCamera()
    {
        player.SetCameraSettings(cameraSettings);
    }

    private void OpenOrClosePanel(GameObject panel, bool isOpen)
    {
        panel.SetActive(isOpen);
    }
}