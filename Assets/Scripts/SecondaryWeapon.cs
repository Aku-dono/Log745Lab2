using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SecondaryWeapon : MonoBehaviour {

    public Button button;
        
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
        if (gameController.getCharge() >= 100 && !gameController.isGameOver())
        {
            gameController.resetCharge();
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            Done_DestroyByContact script;
            foreach (var enemy in enemies)
            {
                if (enemy.name != "Done_Bolt-Enemy")
                {
                    script = enemy.GetComponent<Done_DestroyByContact>();
                    if (script != null)
                    {
                        if (script.explosion != null)
                        {
                            Instantiate(script.explosion, enemy.transform.position, enemy.transform.rotation);
                        }

                        if (script.playerExplosion != null)
                        {
                            Instantiate(script.playerExplosion, enemy.transform.position, enemy.transform.rotation);
                        }

                        gameController.AddScore(script.scoreValue);
                        Destroy(enemy);
                    }
                }
            }
        }
    }
}
