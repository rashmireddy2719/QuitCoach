using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FeedBackScreen : MonoBehaviour
{

    public List<int> score;
    public int total = 0;

    public List<FeedBackBtn> btns;

    public Sprite normal;
    public Sprite clicked;

    public GameObject popup;
    public Text popupText;

    public void AddScore(int index)
    {
        score[index] = 1;
        btns[index].no.sprite = normal;
        btns[index].yes.sprite = clicked;
    }

    public void RemoveScore(int index)
    {
        score[index] = 0;
        btns[index].no.sprite = clicked;
        btns[index].yes.sprite = normal;
    }

    public void Sumbit()
    {
        total = 0;
        foreach (int i in score)
        {
            total += i;
        }
    }

    [Serializable]
    public class FeedBackBtn
    {
        public Image yes;
        public Image no;
    }

    public void ActivatePopup(string message)
    {
        popupText.text = message;
        popup.SetActive(true);
    }

    public void DeActivatePopup()
    {
        popup.SetActive(false);
    }
}
