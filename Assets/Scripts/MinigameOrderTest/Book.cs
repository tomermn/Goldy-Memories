using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UIDocument = UnityEngine.UIElements.UIDocument;
using UnityEngine.UI;


/// <summary>
/// Represents a book with rotating pages and buttons for displaying and interacting with content.
/// </summary>
public class Book : MonoBehaviour
{
    public static Book Instance;

    // Members for rotate actions
    [SerializeField] private float pageSpeed = 0.5f;
    [SerializeField] private List<Transform> pages;
    private int index = -1;
    private bool rotate = false;
    [SerializeField] private bool toShowArrows;

    // Buttons
    [SerializeField] private GameObject startGameButton;
    [SerializeField] private GameObject prevButton;
    [SerializeField] private GameObject nextButton;
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;

    // Text and Images
    [SerializeField] private UIDocument title;
    [SerializeField] private UIDocument instructions;
    [SerializeField] private Image image1;
    [SerializeField] private Image image2;


    /// <summary>
    /// Displays the book, turning off images and buttons based on configuration.
    /// </summary>
    public void DisplayBook()
    {
        Instance.gameObject.SetActive(true);
        TurnOffImagesAndButtons();
        if (!toShowArrows)
        {
            Instance.nextButton.gameObject.SetActive(false);
            Instance.prevButton.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Rotates the page forward.
    /// </summary>
    public void RotateForward()
    {
        if (rotate == true) { return; }
        index++;
        float angle = 180; //in order to rotate the page forward, you need to set the rotation by 180 degrees around the y axis
        ForwardButtonActions();
        pages[index].SetAsLastSibling();
        StartCoroutine(Rotate(angle, true));
    }

    /// <summary>
    /// Rotates the page backward.
    /// </summary>
    public void RotateBack()
    {
        if (rotate == true) { return; }
        float angle = 0; //in order to rotate the page back, you need to set the rotation to 0 degrees around the y axis
        pages[index].SetAsLastSibling();
        BackButtonActions();
        StartCoroutine(Rotate(angle, false));
    }

    /// <summary>
    /// Displays the next images in the book with a delay.
    /// </summary>
    public void DisplayNextItems() 
    {
        Instance.StartCoroutine(Instance.DisplayNextItemsDelay());
    }

    /// <summary>
    /// Closes the book.
    /// </summary>
    public void CloseBook()
    {
        Instance.gameObject.SetActive(false);
    }

    /// <summary>
    /// Handles the click event for button1.
    /// </summary>
    public void OnButton1Click()
    {
        float timePressed = Time.time;
        MinigameManager.Instance.RecordResult(0, timePressed);
        TurnOffImagesAndButtons();
    }

    /// <summary>
    /// Handles the click event for button2.
    /// </summary>
    public void OnButton2Click()
    {
        float timePressed = Time.time;
        MinigameManager.Instance.RecordResult(1, timePressed);
        TurnOffImagesAndButtons();
    }

    private IEnumerator DisplayNextItemsDelay()
    {
        Instance.RotateForward();

        Instance.startGameButton.SetActive(false);
        Instance.title.gameObject.SetActive(false);
        Instance.instructions.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        Sprite[] spritesToDisplay = MinigameManager.Instance.PrepareNextPair();
        if (spritesToDisplay != null)
        {
            Instance.DisplayNextItems(spritesToDisplay[0], spritesToDisplay[1]);
        }
    }

    private void DisplayNextItems(Sprite sprite1, Sprite sprite2)
    {
        if (sprite1 != null && sprite2 != null)
        {
            Instance.image1.sprite = sprite1;
            Instance.image2.sprite = sprite2;
            Instance.image1.gameObject.SetActive(true);
            Instance.image2.gameObject.SetActive(true);
            Instance.button1.gameObject.SetActive(true);
            Instance.button2.gameObject.SetActive(true);
            Instance.button1.interactable = true;
            Instance.button2.interactable = true;
        }
    }

    private void Awake()
    {
        // Ensure there's only one instance of the Book class
        if (Instance == null)
        {
            Instance = this;
            Instance.InitialState();
            TurnOffImagesAndButtons();
            Instance.gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }    
    }
    private void TurnOffImagesAndButtons()
    {
        Instance.button1.gameObject.SetActive(false);
        Instance.image1.gameObject.SetActive(false);
        Instance.button2.gameObject.SetActive(false);
        Instance.image2.gameObject.SetActive(false);
    }

    private void InitialState()
    {
        for (int i = 0; i < Instance.pages.Count; i++)
        {
            Instance.pages[i].transform.rotation = Quaternion.identity;
        }
        Instance.pages[0].SetAsLastSibling();
        Instance.prevButton.SetActive(false);
    }

    private void ForwardButtonActions()
    {
        if (Instance.prevButton.activeInHierarchy == false)
        {
            Instance.prevButton.SetActive(true); //every time we turn the page forward, the back button should be activated
        }
        if (index == Instance.pages.Count - 1)
        {
            Instance.nextButton.SetActive(false); //if the page is last then we turn off the forward button
        }
    }

    private void BackButtonActions()
    {
        if (Instance.nextButton.activeInHierarchy == false)
        {
            Instance.nextButton.SetActive(true); //every time we turn the page back, the forward button should be activated
        }
        if (index - 1 == -1)
        {
            Instance.prevButton.SetActive(false); //if the page is first then we turn off the back button
        }
    }

    private IEnumerator Rotate(float angle, bool forward)
    {
        float value = 0f;
        while (true)
        {
            rotate = true;
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
            value += Time.deltaTime * pageSpeed;
            Instance.pages[index].rotation = Quaternion.Slerp(pages[index].rotation, targetRotation, value); //smoothly turn the page
            float angle1 = Quaternion.Angle(Instance.pages[index].rotation, targetRotation); //calculate the angle between the given angle of rotation and the current angle of rotation
            if (angle1 < 0.1f)
            {
                if (forward == false)
                {
                    index--;
                }
                rotate = false;
                break;

            }
            yield return null;

        }
    }
}
