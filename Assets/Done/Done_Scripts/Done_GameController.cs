using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Done_GameController : MonoBehaviour
{
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	
	public GUIText scoreText;
	public GUIText restartText;
	public GUIText gameOverText;

    public Image FirstBar;
    public Image SecondBar;
    public Image ThirdBar;
    public Image FourthBar;
    public Image FifthBar;

    private bool gameOver;
	private bool restart;
	private int score;
    private int weaponCharge;
	
	void Start ()
	{
		gameOver = false;
		restart = false;
		restartText.text = "";
		gameOverText.text = "";
		score = 0;
		UpdateScore();
        updateChargeBar();
		StartCoroutine (SpawnWaves ());
	}
	
	void Update ()
	{
		if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	}
	
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				GameObject hazard = hazards [Random.Range (0, hazards.Length)];
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);
			
			if (gameOver)
			{
				restartText.text = "Press 'R' for Restart";
				restart = true;
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
        }
    }
	
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}
	
	public void GameOver ()
	{
		gameOverText.text = "Game Over!";
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
}