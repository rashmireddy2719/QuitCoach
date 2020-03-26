using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SendBird;

public class CommunityScreen : PanelBase
{

    public GameObject profileScreen;

    public GameObject blogObject;
    public GameObject blogObjectParent;

    public InputField mesg;


    public void GoToBlog()
    {
        ScreenManager.Instance.Activate<BlogScreen>();
    }

    public void ShowProfile()
    {
        profileScreen.SetActive(true);
    }

    public void ShowChat()
    {
        profileScreen.SetActive(false);
    }

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
        for (int resultIndex = 0; resultIndex < Chatmanager.Instance.chatHistrory.Count; resultIndex++)
        {
            GameObject temp = Instantiate(blogObject.gameObject);
            temp.transform.SetParent(blogObjectParent.transform);
            temp.transform.localScale = Vector3.one;
            UserMessage m = ((UserMessage)Chatmanager.Instance.chatHistrory[resultIndex]);
            temp.GetComponent<Chat_obj>().Initialize(m.Sender.Nickname, m.Message);
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
        temp.GetComponent<Chat_obj>().Initialize(m.Sender.Nickname, m.Message);
    }

    public void Send()
    {
        if (!string.IsNullOrEmpty(mesg.text))
        {
            Chatmanager.Instance.SendChat(mesg.text);
            mesg.text = "";
        }
    }


}
