using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
// using Newtonsoft.Json;

public class Web : MonoBehaviour
{
    public Button LoginButton;
    public Text loginfo;
    public Text reginfo;

    public Text userDisplay;
    public Text scoreText;
    public Image MenuImage;

    public InputField UsernameInput;
    public InputField PasswordInput;
    public InputField ConfirmPasswordInput;
    public Button RegisterButton;
 

    public string currentUsername;

    void Start()
    {

    }

    void Update()
    {
        currentUsername = "";
        if (PlayerPrefs.HasKey("UsernameInput"))
        {
            if (PlayerPrefs.GetString("UsernameInput") != "")
            {
                currentUsername = PlayerPrefs.GetString("UsernameInput");
                loginfo.text = "You're Logged In";
                userDisplay.text = currentUsername;
            }
            else
            {
                loginfo.text = "You're Logged Out";
                userDisplay.text = "GUEST";
            }
        }
        else
        {
            loginfo.text = "You're Logged Out";
            userDisplay.text = "GUEST";
        }

       
    }

    public void LogOut()
    {
        currentUsername = "";
        PlayerPrefs.SetString("UsernameInput", currentUsername);
        loginfo.text = "You're just Logged Out";
        LoginButton.interactable = true;
    }

    [Serializable]
    public class UserDetail{
        public string message;
        public Data data;
    }

    [Serializable]
    public class Data{
       public string username;
       public string password;
       public string id;
    }

    [Serializable]
    public class Score
    {
        public string id;
        public string score;
        public string user_id;
    }


    public IEnumerator Login(string username,string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/api/login", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
            }
            else
            {
                string response = www.downloadHandler.text;
                Debug.Log(response);
                UserDetail userDetail=JsonUtility.FromJson<UserDetail>(response);
                Data data = JsonUtility.FromJson<Data>(response);
                if(userDetail.message=="Login success")
                {
                    PlayerPrefs.SetString("UsernameInput", username);
                    PlayerPrefs.SetString("UserId", data.id);
                    SceneManager.LoadScene(1);
                    LoginButton.interactable=false;

                }
                else
                {
                    loginfo.text = "Login Failed";
                }
            }
        } 
    }

    public IEnumerator RegisterUser(string username, string password)
    {
        WWWForm form = new WWWForm();
        form.AddField("username", username);
        form.AddField("password", password);

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/api/register", form))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(www.error);
                
            }
            else
            {
                string response = www.downloadHandler.text;
                Debug.Log(response);
                UserDetail userDetail=JsonUtility.FromJson<UserDetail>(response);
                Data data = JsonUtility.FromJson<Data>(response);
                if(userDetail.message=="New user created")
                {
                    PlayerPrefs.SetString("UsernameInput", username);
                    PlayerPrefs.SetString("UserId", data.id);
                    SceneManager.LoadScene(1);
                    LoginButton.interactable=false;

                }
                else
                {
                    reginfo.text = "Username already exists";
                }
               
            }
        }
    }

    public IEnumerator InsertScore(string username,float score)
    {
       WWWForm form = new WWWForm();
       form.AddField("username", username);
       form.AddField("score", Convert.ToString(score));

       using (UnityWebRequest www = UnityWebRequest.Post("http://localhost:3000/api/score", form))
       {
           yield return www.SendWebRequest();

           if (www.result != UnityWebRequest.Result.Success)
           {
               Debug.Log(www.error);
           }
           else
           {
               Debug.Log(www.downloadHandler.text);
           }
       }
    }

    public IEnumerator BestScore()
    {
        string uri="http://localhost:3000/api/bestscore/" + PlayerPrefs.GetString("UserId");
        using (UnityWebRequest www = UnityWebRequest.Get(uri))
        {
            yield return www.SendWebRequest();
            if(www.result != UnityWebRequest.Result.Success)
                Debug.Log(www.error);
            else
            {
                string response=www.downloadHandler.text;
                Debug.Log(response);
                Score score=JsonUtility.FromJson<Score>(response);
                PlayerPrefs.SetString("bestscore",score.score);
                
            }
        }
    }

    public IEnumerator DeleteUser()
    {
        string uri="http://localhost:3000/api/user/" + PlayerPrefs.GetString("UserId");
        using (UnityWebRequest www = UnityWebRequest.Delete(uri))
        {
            yield return www.SendWebRequest();
            if(www.result != UnityWebRequest.Result.Success)
                Debug.Log(www.error);
            else
            {
                string response=www.downloadHandler.text;
                Debug.Log(response);
            }
        }
    }


}
