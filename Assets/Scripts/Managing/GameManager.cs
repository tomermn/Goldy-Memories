
using UnityEngine;
using UnityEngine.SceneManagement; // required for loading a scene

/// <summary>
/// Manages the game state, level progression, player lives, and checkpoints.
/// </summary>
public class GameManager : MonoBehaviour  
{
    public static GameManager Instance { get; private set; } // Gets the singleton instance of the GameManager.

    public int level { get; private set; }

    public int phase { get; private set; }

    public int lives { get; private set; }

    public int n_items_to_collect;

    private Vector2 respawnPoint;


    /// <summary>
    /// Initializes the GameManager singleton instance and sets the initial respawn point.
    /// </summary>
    private void Awake()
    {
        if (Instance != null) //is there already an instance available
        {
            DestroyImmediate(gameObject);

        }
        else
        {
            Instance = this;
            respawnPoint = new Vector2(-7, -2); // Player's starting position
            DontDestroyOnLoad(gameObject);
        }
    }

    /// <summary>
    /// Handles the destruction of the GameManager instance.
    /// </summary>
    private void OnDestroy() 
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

    /// <summary>
    /// Starts a new game by setting the initial number of lives and loading the first level.
    /// </summary>
    private void NewGame()
    {
        lives = 1;
        LoadLevel(1, 1);
    }

    /// <summary>
    /// Loads the specified level and phase.
    /// </summary>
    private void LoadLevel(int level, int phase)
    {
        this.level = level;
        this.phase = phase;
       
        SceneManager.LoadScene($"{level}-{phase}"); 
    }

    /// <summary>
    /// Initiates the transition to the next level after a specified delay.
    /// </summary>
    public void NextLevel(float delay)
    {
        Invoke(nameof(NextLevel), delay);
        SceneManager.LoadScene("MinigameOrderTest");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("MinigameOrderTest");
        // OPTIONAL: multilevel game
        //if (phase == 2)
        //{
        //    phase = 1;
        //    level++;
        //}
        //else
        //{
        //    phase++;
        //}
        //LoadLevel(level, phase);

    }

    /// <summary>
    /// Resets the current level after a specified delay (Optional for future use).
    /// </summary>
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

    /// <summary>
    /// Handles the game over scenario (Optional for future use).
    /// </summary>
    public void GameOver()
    {
        NewGame();
    }

    /// <summary>
    /// Sets the respawn point to the specified checkpoint position.
    /// </summary>
    public void SetCheckpoint(Transform checkpoint)
    {
        respawnPoint = (Vector2) checkpoint.position + new Vector2(0, 2); // Spawn a little bit higher than the ground
    }


    /// <summary>
    /// Gets the current respawn point.
    /// </summary>
    public Vector2 GetCheckpoint()
    {
        return respawnPoint;
    }

}
