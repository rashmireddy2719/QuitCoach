using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Post_obj : MonoBehaviour
{

    public Text username;
    public Text city;
    public Text message;


    public void Initialize(string _user, string _city, string _message)
    {
        username.text = _user;
        city.text = _city;
        message.text = _message;
    }
}
