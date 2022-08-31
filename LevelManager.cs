using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  // load next scene

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitToLoad = 4f;

    public string nextLevel;

    public bool isPaused;

    public int currentCoins;

    public Transform startPoint;

    private void Awake()
    {
        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {
        PlayerController.instance.transform.position = startPoint.position;
        PlayerController.instance.canMove = true;

        currentCoins = CharacterTracker.instance.currentCoins;

        Time.timeScale = 1f;

        UIController.instance.coinText.text = currentCoins.ToString(); // convert currenCoins to a string.
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))  // escape key pause
        {
            PauseUnpause();
        }
    }

    public IEnumerator LevelEnd()
    {
        AudioManager.instance.PlayLevelWin();  // Play win music when finish level

        PlayerController.instance.canMove = false;

        UIController.instance.StartFadeToBlack();

        yield return new WaitForSeconds(waitToLoad);  // this is how we wait in between levels

        CharacterTracker.instance.currentCoins = currentCoins;
        CharacterTracker.instance.currentHealth = PlayerHealthController.instance.currentHealth;
        CharacterTracker.instance.maxHealth = PlayerHealthController.instance.maxHealth;

        SceneManager.LoadScene(nextLevel);
    }

    public void PauseUnpause()
    {
        if(!isPaused)  // not if paused
        {
            UIController.instance.pauseMenu.SetActive(true);  // show pause screen

            isPaused = true;

            Time.timeScale = 0f;  // this stops the progression of time in the game! (frozen in place)
        } else
        {
            UIController.instance.pauseMenu.SetActive(false);

            isPaused = false;

            Time.timeScale = 1f;  // time will start when we unpause the game.
        }
    }
    public void GetCoins(int amount)
    {
        currentCoins += amount;

        UIController.instance.coinText.text = currentCoins.ToString();
    }

    public void SpendCoins(int amount)
    {
        currentCoins -= amount;

        if(currentCoins < 0)// make sure dont go below 0
        {
            currentCoins = 0;  
        }

        UIController.instance.coinText.text = currentCoins.ToString();
    }
}


