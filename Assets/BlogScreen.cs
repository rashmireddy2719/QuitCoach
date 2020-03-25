using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SendBird;

public class BlogScreen : PanelBase
{

    public InputField mesg;

    public GameObject blogObject;
    public GameObject blogObjectParent;

    void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        ClearList();
        IntializeList();
    }

    void IntializeList()
    {
        for (int resultIndex = 0; resultIndex < Chatmanager.Instance.blogHistrory.Count; resultIndex++)
        {
            GameObject temp = Instantiate(blogObject.gameObject);
            temp.transform.SetParent(blogObjectParent.transform);
            temp.transform.localScale = Vector3.one;
            UserMessage m = ((UserMessage)Chatmanager.Instance.blogHistrory[resultIndex]);
            temp.GetComponent<Post_obj>().Initialize(m.Sender.Nickname, AppManager.Instance.city, m.Message);
        }
    }

    void ClearList()
    {
        for (int childIndex = blogObjectParent.transform.childCount - 1; childIndex >= 0; --childIndex)
        {
            Transform child = blogObjectParent.transform.GetChild(childIndex);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
    }

    public void AddBlog(BaseMessage msg)
    {
        Chatmanager.Instance.blogHistrory.Insert(0, msg);
        GameObject temp = Instantiate(blogObject.gameObject);
        temp.transform.SetParent(blogObjectParent.transform);
        temp.transform.SetSiblingIndex(0);
        temp.transform.localScale = Vector3.one;
        UserMessage m = ((UserMessage)msg);
        if (m.Sender.Nickname.Equals(AppManager.Instance.Name))
            temp.GetComponent<Post_obj>().Initialize(m.Sender.Nickname, AppManager.Instance.city, m.Message);
        else
            temp.GetComponent<Post_obj>().Initialize(m.Sender.Nickname, "", m.Message);
    }

    public void Send()
    {
        if (!string.IsNullOrEmpty(mesg.text))
        {
            Chatmanager.Instance.SendBlogPost(mesg.text);
            mesg.text = "";
        }
    }

    public void GoToHome()
    {
        ScreenManager.Instance.Activate<HomeScreen>();
    }
}
