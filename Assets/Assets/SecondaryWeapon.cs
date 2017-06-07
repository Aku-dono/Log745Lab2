using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondaryWeapon : MonoBehaviour {

    public Button button;
    public GameObject enemyExplosion;
    public int scoreValue;
        
    private Done_GameController gameController;

    // Use this for initialization
    void Start () {

        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<Done_GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        var btn = button.GetComponent<Button>();
        btn.onClick.AddListener(WipeEnemies);
	}

    void WipeEnemies()
    {
        if (gameController.getCharge() >= 100)
        {
            gameController.resetCharge();
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            foreach (var enemy in enemies)
            {
                if (enemy.name != "Done_Bolt-Enemy")
                {
                    if (enemyExplosion != null)
                    {
                        Instantiate(enemyExplosion, enemy.transform.position, enemy.transform.rotation);
                    }

                    gameController.AddScore(scoreValue);
                    Destroy(enemy);
                }
            }
        }
    }
}
