using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] private float pageSpeed = 0.5f;
    [SerializeField] private List<Transform> pages;
    [SerializeField] private GameObject forwardButton;
    [SerializeField] private GameObject prevButton;
    private int index = -1;
    private bool rotate = false;


    private void Start()
    {
        prevButton.SetActive(false);
    }


    public void RotateForward()
    {
        if (rotate) { return; }
        
        if (prevButton.activeInHierarchy == false)
        {
            prevButton.SetActive(true);
        }

        if (index == pages.Count - 1)
        {
            forwardButton.SetActive(false);
        }

        index++;
        float angle = 180;
        pages[index].SetAsLastSibling();
        StartCoroutine(Rotate(angle, true));
    }

    public void RotatePrev()
    {
        if (rotate)
        {
            return;
        }

        if (forwardButton.activeInHierarchy == false)
        {
            forwardButton.SetActive(true);
        }

        if (index - 1 == -1)
        {
            prevButton.SetActive(false);
        }


        float angle = 0;
        pages[index].SetAsLastSibling();
        StartCoroutine(Rotate(angle, false));

    }

    IEnumerator Rotate(float angle, bool forward)
    {
        float value = 0;
        while (true)
        {
            rotate = true;
            Quaternion targetRotation = Quaternion.Euler(0f, angle, 0);
            value += Time.deltaTime * pageSpeed;
            pages[index].rotation = Quaternion.Slerp(pages[index].rotation, targetRotation, value); // Smoothly turn the page
            float angle2 = Quaternion.Angle(pages[index].rotation, targetRotation);

            if (angle2 < 0.1f)
            {
                if (!forward)
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

