
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement; // required for loading a scene

public class GameManager : MonoBehaviour  // singelton, "global class" - single instance, we can access it from anywhere.
{
    public static GameManager Instance { get; private set; }

    public int level { get; private set; }

    public int phase { get; private set; }

    public int lives { get; private set; }

    public int n_items_to_collect = 3;

    private Vector2 respawnPoint;
    public Hashtable itemHashtable2;

    public Hashtable itemHashtable = new Hashtable()
    {
        {"Yellow Bottle",null},
        {"Sandwitch",null},
        {"Apple", null},
        {"Watch", null},
        {"Scissors", null}

    };



    private void Awake()
    {
        if (Instance != null) //is there already an instance available
        {
            DestroyImmediate(gameObject);

        }
        else
        {
            Instance = this;
            respawnPoint = new Vector2(-7, -2); //Player's starting position
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy() //if the game Manager is destroy
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        lives = 1;
        LoadLevel(1, 1);

    }

    private void LoadLevel(int level, int phase)
    {
        this.level = level;
        this.phase = phase;
        

        SceneManager.LoadScene($"{level}-{phase}"); //string interpolation

    }


    public void NextLevel(float delay)
    {
        Invoke(nameof(NextLevel), delay);
    }

    public void NextLevel()
    {
        if (phase == 2)
        {
            phase = 1;
            level++;
        }
        else
        {
            phase++;
        }
        LoadLevel(level, phase);

    }


    public void ResetLevel(float delay)
    {
        Invoke(nameof(ResetLevel), delay);
    }

    /*
     * this method is called when the player is dead. in the current state of the game, the player can't be dead, so its an optional method for future use.
     */
    public void ResetLevel()
    {
        lives--;
        if (lives > 0)
        {
            LoadLevel(level, phase);
        }
        else
        {
            GameOver();
        }
    }

    /*
     * this method is called when the player has no live. in the current state of the game, the player can't be dead, so its an optional method for future use.
     */
    public void GameOver()
    {
        NewGame();
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        respawnPoint = (Vector2) checkpoint.position + new Vector2(0, 2); // spawn a little bit higher
    }



    public Vector2 GetCheckpoint()
    {
        return respawnPoint;
    }

}
