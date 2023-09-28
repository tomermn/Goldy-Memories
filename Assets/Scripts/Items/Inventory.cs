using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<string> collectedItems = new List<string>();
    public int n_collected;
    public int n_to_collect;
    public static Inventory Instance;
    public ProgressBar progressBar;
    


    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            
        }
        else
        {
            Destroy(this.gameObject);
        }
    }



    public void AddToInvetory(Item item)
    {
        collectedItems.Add(item.name);
        n_collected = collectedItems.Count;
        if (n_to_collect != 0)
        {
            float progressRatio = (float)n_collected / n_to_collect;
            Debug.Log(progressRatio);
            progressBar.IncProgress(progressRatio);

        }
    }
    

}
