using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chat_obj : MonoBehaviour
{

    public Text username;
    public Text message;


    public void Initialize(string _user, string _message)
    {
        username.text = _user;
        message.text = _message;
    }
}
