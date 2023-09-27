
using UnityEngine;
using UnityEngine.SceneManagement; // required for loading a scene

public class GameManager : MonoBehaviour  // singelton, "global class" - single instance, we can access it from anywhere.
{
    public static GameManager Instance { get; private set; }

    public int level { get; private set; }

    public int phase { get; private set; }

    public int lives { get; private set; }

    public int n_items_to_collect = 3;

    private Transform respawnPoint;

    private GameObject player;



    private void Awake()
    {
        if (Instance != null) //is there already an instance available
        {
            DestroyImmediate(gameObject);

        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            player = GameObject.FindWithTag("Player");
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

    public void Respawn(float delay)
    {
        Invoke(nameof(Respawn), delay);
    }

    public void Respawn()
    {
        Debug.Log("respawn");
        player.transform.position = respawnPoint.position;
    }


    public void GameOver()
    {
        NewGame();
    }

    public void SetCheckpoint(Transform checkpoint)
    {
        respawnPoint = checkpoint;
    }

    public Transform GetCheckpoint()
    {
        return respawnPoint;
    }

}
