using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphNode : MonoBehaviour
{

    const int factor = 12;

    public Text date;
    public Text graphScore;

    public RectTransform rect;


    public void UpdateNode(int value, string data)
    {
        rect.sizeDelta = new Vector2(40, value * factor);
        date.text = data;
        graphScore.color = Color.black;
        string nodeVale = value.ToString();
        graphScore.text = nodeVale;
    }

    public void NodeClick()
    {
        graphScore.color = Color.black;
        StartCoroutine("FadeOff");
    }

    IEnumerator FadeOff()
    {
        yield return new WaitForSeconds(4);
        graphScore.color = Color.black;
    }
}
