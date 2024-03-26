

using UnityEngine;
using UnityEngine.SceneManagement; // required for loading a scene

/// <summary>
/// Manages the game state, level progression, player lives, and checkpoints.
/// </summary>
public class GameManager : MonoBehaviour  
{
    public static GameManager Instance { get; private set; } 
    private static readonly Vector2 PlayerStartPosition = new Vector2(-7, -2);
    private static readonly Vector2 SpawnOffset = new Vector2(0, 2);
    
    [SerializeField] private int numOfItemsToCollect;
    
    private Vector2 respawnPoint;
    

    /// <summary>
    /// Starts a new game by setting the initial number of lives and loading the first level.
    /// </summary>
    private void NewGame()
    {
        LoadLevel(1, 1);
    }
    

    /// <summary>
    /// Initiates the transition to the next level after a specified delay.
    /// </summary>
    public void NextLevel(float delay)
    {
        Invoke(nameof(NextLevel), delay);
    }

    public Vector2 GetCheckpoint()
    {
        return respawnPoint;
    }

    public void StartMinigame1()
    {
        MinigameManager.Instance.StartMinigame();
    }
    

    public void ResumeGame()
    {   
        PlayerMovement.InMiniGame = false;
        GameObject invoker =  GameObject.FindWithTag(Constants.InvokerMinigame1);
        Destroy(invoker);
    }


    /// <summary>
    /// Sets the respawn point to the specified checkpoint position.
    /// </summary>
    public void SetCheckpoint(Transform checkpoint)
    {
        respawnPoint = (Vector2)checkpoint.position + SpawnOffset; // Spawn a little bit higher than the ground
    }

    private void Start()
    {
        NewGame();
    }
    
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
            respawnPoint = PlayerStartPosition; 
            DontDestroyOnLoad(gameObject);
        }
    }

    private void NextLevel()
    {
        SceneManager.LoadScene("MinigameOrderTest");
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

    /// <summary>
    /// Loads the specified level and phase.
    /// </summary>
    private void LoadLevel(int level, int phase)
    {
        SceneManager.LoadScene($"{level}-{phase}"); 
    }
    

    

}
