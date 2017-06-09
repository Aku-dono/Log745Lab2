using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using UnityEngine.UI;
using Assets.Scripts;

public class Done_GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float startWait;
    public float waveWait;

    public GameObject gameUIPanel;
    public GameObject gameOverPanel;
    public GameObject movementZone;
    public Text scoreText;
    public Text finalScoreText;

    private bool gameOver;
    private bool restart;
    private int score;

    public Image FirstBar;
    public Image SecondBar;
    public Image ThirdBar;
    public Image FourthBar;
    public Image FifthBar;

    public GameObject SecondaryWeaponBtn;
    private int weaponCharge;

    void Start()
    {
        gameOver = false;
        restart = false;
        gameOverPanel.SetActive(false);
        score = 0;
        UpdateScore();
        updateChargeBar();
        StartCoroutine(SpawnWaves());
        if((int)ConfigGlobal.InputMode == 0)
        {
            movementZone.SetActive(false);
        }
        else if ((int)ConfigGlobal.InputMode == 1)
        {
            movementZone.SetActive(true);
        }
    }

    public void AddScore(int newScoreValue)
    {
        score += newScoreValue;
        UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
        finalScoreText.text = "Final Score: " + score;
    }


    public void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    void Update()
    {

    }

    IEnumerator SpawnWaves()
    {
        GameObject[] currentHazards = hazards;
        int currentHazardCount = hazardCount;
        float currentSpawnWait = spawnWait;

        //Depending on difficulty level, change which enemies spawn and at which rate. 
        switch (ConfigGlobal.DifficultyLevel)
        {
            case DifficultyLevel.Easy:
                //At easy, only spawn asteroids. 
                currentHazards = hazards.Where(h => h.name.IndexOf("asteroid", System.StringComparison.InvariantCultureIgnoreCase) != -1).ToArray();
                break;
            case DifficultyLevel.Normal:
                break;
            case DifficultyLevel.Hard:
                currentHazardCount = (int)(hazardCount * 1.5f);
                break;
            case DifficultyLevel.Lunatic:
                currentHazardCount = (int)(hazardCount * 1.5f);
                currentSpawnWait = spawnWait / 1.5f;
                break;
        }

        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = currentHazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);

            if (gameOver)
            {
                break;
            }
        }
    }

    void updateChargeBar()
    {
        if (weaponCharge < 20)
        {
            FirstBar.color = new Color(0, 0, 0, 0);
            SecondBar.color = new Color(0, 0, 0, 0);
            ThirdBar.color = new Color(0, 0, 0, 0);
            FourthBar.color = new Color(0, 0, 0, 0);
            FifthBar.color = new Color(0, 0, 0, 0);
            SecondaryWeaponBtn.SetActive(false);
        }
        else if (weaponCharge < 40)
        {
            FirstBar.color = Color.green;
        }
        else if (weaponCharge < 60)
        {
            SecondBar.color = Color.green;
        }
        else if (weaponCharge < 80)
        {
            ThirdBar.color = Color.green;
        }
        else if (weaponCharge < 100)
        {
            FourthBar.color = Color.green;
        }
        else
        {
            FirstBar.color = Color.red;
            SecondBar.color = Color.red;
            ThirdBar.color = Color.red;
            FourthBar.color = Color.red;
            FifthBar.color = Color.red;
            SecondaryWeaponBtn.SetActive(true);
        }
    }

    public void GameOver()
    {
        gameUIPanel.SetActive(false);
        gameOverPanel.SetActive(true);
        gameOver = true;
    }

    public void addCharge(int value)
    {
        if (weaponCharge < 100)
        {
            weaponCharge += value;
        }
        updateChargeBar();
    }

    public int getCharge()
    {
        return weaponCharge;
    }

    public void resetCharge()
    {
        weaponCharge = 0;
        updateChargeBar();
    }

    public bool isGameOver()
    {
        return gameOver;
    }
}