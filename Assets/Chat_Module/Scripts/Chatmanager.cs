using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using SendBird;

public class Chatmanager : MonoBehaviour
{
    public static Chatmanager Instance;

    private UserListQuery mUserListQuery;
    private GroupChannelListQuery mGroupChannelListQuery;

    public BlogScreen blogScreen;
    public CommunityScreen chatScreen;
    public FriendsListScreen friendsScreen;

    public OpenChannel blogChannel;
    public OpenChannel chatChannel;
    public User selectedUser;

    public List<User> nearByPeople;
    public List<GroupChannel> friendsGroupChannel;
    public List<User> friendsUserList;
    public List<BaseMessage> chatHistrory;
    public List<BaseMessage> blogHistrory;



    public string userName = "vinay";

    // Use this for initialization
    void Start()
    {
        ConnectUser();

        SendBirdClient.ChannelHandler channelHandler = new SendBirdClient.ChannelHandler();
        channelHandler.OnMessageReceived = (BaseChannel channel, BaseMessage message) =>
        {
            if (blogChannel.Url == channel.Url)
            {
                if (message is UserMessage)
                {
                    blogScreen.AddBlog(message);
                    //chatScreen.AddChatMessage(message);
                    Debug.Log("Blog " + "   Message  :" + ((UserMessage)message).Message +
                        "   Sender  :" + ((UserMessage)message).Sender.Nickname);
                }
            }
            if (chatChannel.Url == channel.Url)
            {
                if (message is UserMessage)
                {
                    chatScreen.AddBlog(message);
                    Debug.Log("Chat " + "   Message  :" + ((UserMessage)message).Message +
                        "   Sender  :" + ((UserMessage)message).Sender.Nickname);
                }
            }
        };

        SendBirdClient.AddChannelHandler("default", channelHandler);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Awake()
    {
        Instance = this;

        SendBirdClient.SetupUnityDispatcher(gameObject); // Set SendBird gameobject to DontDestroyOnLoad.
        StartCoroutine(SendBirdClient.StartUnityDispatcher); // Start a Unity dispatcher.

        SendBirdClient.Init("074A037F-CDEF-4853-A91D-27DB781024EB"); // SendBird Sample Application ID
        SendBirdClient.Log += (message) =>
        {
            Debug.Log(message);
        };
    }

    public void ConnectUser()
    {
        SendBirdClient.Connect(userName, (user, e) =>
        {
            if (e != null)
            {
                Debug.Log(e.Code + ": " + e.Message);
                return;
            }

            SendBirdClient.UpdateCurrentUserInfo(userName, null, (e1) =>
            {
                if (e1 != null)
                {
                    Debug.Log(e.Code + ": " + e.Message);
                    return;
                }

                GetListOpenGroup();
            });
        });
    }



    public void PeopleNearYou()
    {
        //nearByPeople = new List<User>();
        //mUserListQuery = SendBirdClient.CreateUserListQuery();
        //mUserListQuery.Limit = 50;

        //mUserListQuery.Next((list, e) =>
        //{
        //    if (e != null)
        //    {
        //        Debug.Log(e.Code + ": " + e.Message);
        //        return;
        //    }

        //    foreach (User user in list)
        //    {
        //        if (!user.Nickname.Equals(userName))
        //            nearByPeople.Add(user);
        //    }
        //    Debug.Log("Near by people : " + nearByPeople.Count);
        //});

    }

    public void GetListOfFriends()
    {
        //friendsGroupChannel = new List<GroupChannel>();
        //friendsUserList = new List<User>();
        //mGroupChannelListQuery = GroupChannel.CreateMyGroupChannelListQuery();
        //mGroupChannelListQuery.Limit = 50;
        //mGroupChannelListQuery.Next((list, e) =>
        //{
        //    if (e != null)
        //    {
        //        Debug.Log(e.Code + ": " + e.Message);
        //        return;
        //    }
        //    foreach (GroupChannel groupChannel in list)
        //    {
        //        foreach (User user in groupChannel.Members)
        //        {
        //            if (!user.Nickname.Equals(userName))
        //            {
        //                friendsGroupChannel.Add(groupChannel);
        //                friendsUserList.Add(user);
        //                var index = nearByPeople.FindIndex(i => i.Nickname == user.Nickname);
        //                if (index >= 0)
        //                {   // ensure item found
        //                    nearByPeople.RemoveAt(index);
        //                }
        //                break;
        //            }

        //        }
        //    }
        //    Debug.Log("Number of friends " + friendsUserList.Count);
        //    Debug.Log("Near by people : " + nearByPeople.Count);
        //});
    }


    public void CreatePrivateChat()
    {
        //List<User> mUserList = new List<User>();
        //mUserList.Add(selectedUser);

        //GroupChannel.CreateChannel(mUserList, false, (channel, e) =>
        //{
        //    if (e != null)
        //    {
        //        Debug.Log(e.Code + ": " + e.Message);
        //        return;
        //    }
        //    currentChannel = channel;
        //    currentChannel.SendUserMessage("Hello I am using BeSober.", (message, e1) =>
        //    {
        //        if (e1 != null)
        //        {
        //            Debug.Log(e.Code + ": " + e1.Message);
        //            return;
        //        }

        //        GetListOfFriends();
        //        LoadPreviousChatHistory();
        //    });

        //});


    }


    public void GetListOpenGroup()
    {
        string s = "";
        OpenChannelListQuery mChannelListQuery = OpenChannel.CreateOpenChannelListQuery();
        mChannelListQuery.Next((List<OpenChannel> channels, SendBirdException e) =>
        {
            if (e != null)
            {
                // Error.
                return;
            }
            Debug.Log(channels[0].Url);
            EnterBlogChannel(channels[1].Url);
            EnterChatChannel(channels[0].Url);
        });

    }

    public void EnterChatChannel(string CHANNEL_URL)
    {
        OpenChannel.GetChannel(CHANNEL_URL, (OpenChannel openChannel, SendBirdException e) =>
        {
            if (e != null)
            {
                // Error.
                return;
            }
            chatChannel = openChannel;
            chatChannel.Enter((SendBirdException e3) =>
            {
                if (e3 != null)
                {
                    // Error.
                    return;
                }

                GetPreviousChatChannelData();
            });
        });
    }

    public void EnterBlogChannel(string CHANNEL_URL)
    {
        OpenChannel.GetChannel(CHANNEL_URL, (OpenChannel openChannel, SendBirdException e) =>
        {
            if (e != null)
            {
                // Error.
                return;
            }
            blogChannel = openChannel;
            blogChannel.Enter((SendBirdException e3) =>
            {
                if (e3 != null)
                {
                    // Error.
                    return;
                }

                GetPreviousBlogChannelData();
            });
        });
    }

    public void GetPreviousBlogChannelData()
    {
        blogHistrory = new List<BaseMessage>();
        PreviousMessageListQuery mPrevMessageListQuery = blogChannel.CreatePreviousMessageListQuery();
        mPrevMessageListQuery.Load(30, true, (List<BaseMessage> messages, SendBirdException e) =>
        {
            if (e != null)
            {
                // Error.
                return;
            }
            blogHistrory = messages;
            foreach (BaseMessage msg in messages)
            {
                Debug.Log("Blog  " + ((UserMessage)msg).Message);
            }
        });
    }

    public void GetPreviousChatChannelData()
    {
        chatHistrory = new List<BaseMessage>();
        PreviousMessageListQuery mPrevMessageListQuery = chatChannel.CreatePreviousMessageListQuery();
        mPrevMessageListQuery.Load(30, true, (List<BaseMessage> messages, SendBirdException e) =>
        {
            if (e != null)
            {
                // Error.
                return;
            }
            chatHistrory = messages;

            foreach (BaseMessage msg in messages)
            {
                Debug.Log("Chat  " + ((UserMessage)msg).Message);
            }
        });
    }

    public void LoadPreviousChatHistory()
    {
        //chatHistrory = new List<BaseMessage>();
        //foreach (GroupChannel groupChannel in friendsGroupChannel)
        //{
        //    foreach (User user in groupChannel.Members)
        //    {
        //        if (!user.Nickname.Equals(userName) && user.Nickname.Equals(selectedUser.Nickname))
        //        {
        //            currentChannel = groupChannel;
        //            break;
        //        }
        //    }
        //}
        //PreviousMessageListQuery mPrevMessageListQuery = currentChannel.CreatePreviousMessageListQuery();
        //mPrevMessageListQuery.Load(15, true, (List<BaseMessage> messages, SendBirdException e) =>
        //{
        //    if (e != null)
        //    {
        //        // Error.
        //        return;
        //    }

        //    chatHistrory = messages;
        //    screenManager.ShowChat();
        //});
    }

    public void SendPrivateMessage(string data)
    {

        //if (currentChannel != null && currentChannel.IsGroupChannel())
        //{
        //    currentChannel.SendUserMessage(data, (UserMessage message, SendBirdException e) =>
        //    {
        //        if (e != null)
        //        {
        //            Debug.Log(e.Code + ": " + e.Message);
        //            return;
        //        }

        //        chatScreen.AddChatMessage(message);

        //    });
        //}
    }

    public void SendBlogPost(string data)
    {

        if (blogChannel != null && blogChannel.IsOpenChannel())
        {
            blogChannel.SendUserMessage(data, (UserMessage message, SendBirdException e) =>
            {
                if (e != null)
                {
                    Debug.Log(e.Code + ": " + e.Message);
                    return;
                }

                blogScreen.AddBlog(message);

            });
        }
    }

    public void SendChat(string data)
    {

        if (chatChannel != null && chatChannel.IsOpenChannel())
        {
            chatChannel.SendUserMessage(data, (UserMessage message, SendBirdException e) =>
            {
                if (e != null)
                {
                    Debug.Log(e.Code + ": " + e.Message);
                    return;
                }
                chatScreen.AddBlog(message);
                //blogScreen.AddBlogPost(message);

            });
        }
    }

    public void UnFriend()
    {
        //GroupChannel group_Channel = null;
        //foreach (GroupChannel groupChannel in friendsGroupChannel)
        //{
        //    foreach (User user in groupChannel.Members)
        //    {
        //        if (!user.Nickname.Equals(userName) && user.Nickname.Equals(selectedUser.Nickname))
        //        {
        //            group_Channel = groupChannel;
        //            break;
        //        }
        //    }
        //}

        //var index = friendsUserList.FindIndex(i => i.Nickname == selectedUser.Nickname);
        //if (index >= 0)
        //    friendsUserList.RemoveAt(index);
        //friendsGroupChannel.Remove(group_Channel);

        //group_Channel.Leave((SendBirdException e) =>
        //{
        //    if (e != null)
        //    {
        //        // Error.
        //        return;
        //    }

        //    Debug.Log("UserLeft");
        //    friendsScreen.Refresh();
        //    PeopleNearYou();
        //    GetListOfFriends();
        //});
    }

}
