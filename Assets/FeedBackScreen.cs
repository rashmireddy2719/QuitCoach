using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class FeedBackScreen : PanelBase
{

    public List<int> score;
    public int total = 0;

    public List<FeedBackBtn> btns;

    public Sprite normal;
    public Sprite clicked;

    public GameObject popup;
    public Text popupText;

    public void GoToHome()
    {
        ScreenManager.Instance.Activate<HomeScreen>();
    }

    public void AddScore(int index)
    {
        if (index == 2 || index == 3 || index == 4)
        {
            score[index] = 0;
            btns[index].no.sprite = normal;
            btns[index].yes.sprite = clicked;
        }
        else
        {
            score[index] = 1;
            btns[index].no.sprite = normal;
            btns[index].yes.sprite = clicked;
        }

    }

    public void RemoveScore(int index)
    {
        if (index == 2 || index == 3 || index == 4)
        {
            score[index] = 1;
            btns[index].no.sprite = clicked;
            btns[index].yes.sprite = normal;
        }
        else
        {
            score[index] = 0;
            btns[index].no.sprite = clicked;
            btns[index].yes.sprite = normal;
        }

    }

    public void Sumbit()
    {
        total = 0;
        foreach (int i in score)
        {
            total += i;
        }
        total = total + 1;
        if (total == 7 || total == 6)
        {
            ActivatePopup("Low!Keep going!  you are on the right track");
        }
        else if (total == 5 || total == 4)
        {
            ActivatePopup("Moderate!More attention is required to avoid your cravings");
        }
        else if (total == 3 || total == 2 || total == 1)
        {
            ActivatePopup("High!Connect with addiction experts or community to avoid your craving");
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
