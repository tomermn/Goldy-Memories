using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] float pageSpeed = 0.5f;
    [SerializeField] List<Transform> pages;
    int index = -1;
    bool rotate = false;
    [SerializeField] GameObject prevButton;
    [SerializeField] GameObject nextButton;

    private void Start()
    {
        InitialState();
    }

    public void InitialState()
    {
        for (int i = 0; i < pages.Count; i++)
        {
            pages[i].transform.rotation = Quaternion.identity;
        }
        pages[0].SetAsLastSibling();
        prevButton.SetActive(false);

    }

    public void RotateForward()
    {
        if (rotate == true) { return; }
        index++;
        float angle = 180; //in order to rotate the page forward, you need to set the rotation by 180 degrees around the y axis
        ForwardButtonActions();
        pages[index].SetAsLastSibling();
        StartCoroutine(Rotate(angle, true));

    }

    public void ForwardButtonActions()
    {
        if (prevButton.activeInHierarchy == false)
        {
            prevButton.SetActive(true); //every time we turn the page forward, the back button should be activated
        }
        if (index == pages.Count - 1)
        {
            nextButton.SetActive(false); //if the page is last then we turn off the forward button
        }
    }

    public void RotateBack()
    {
        if (rotate == true) { return; }
        float angle = 0; //in order to rotate the page back, you need to set the rotation to 0 degrees around the y axis
        pages[index].SetAsLastSibling();
        BackButtonActions();
        StartCoroutine(Rotate(angle, false));
    }

    public void BackButtonActions()
    {
        if (nextButton.activeInHierarchy == false)
        {
            nextButton.SetActive(true); //every time we turn the page back, the forward button should be activated
        }
        if (index - 1 == -1)
        {
            prevButton.SetActive(false); //if the page is first then we turn off the back button
        }
    }

    IEnumerator Rotate(float angle, bool forward)
    {
        float value = 0f;
        while (true)
        {
            rotate = true;
            Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
            value += Time.deltaTime * pageSpeed;
            pages[index].rotation = Quaternion.Slerp(pages[index].rotation, targetRotation, value); //smoothly turn the page
            float angle1 = Quaternion.Angle(pages[index].rotation, targetRotation); //calculate the angle between the given angle of rotation and the current angle of rotation
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
