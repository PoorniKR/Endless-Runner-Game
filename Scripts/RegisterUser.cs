using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterUser : MonoBehaviour
{
    public InputField UsernameInput;
    public InputField PasswordInput;
    public InputField ConfirmPasswordInput;
    public Button RegisterButton;
    public Text reginfo;

    // Start is called before the first frame update
    
    public void Register()
    {
        if((UsernameInput.text.Length !=0 || UsernameInput.text.Length <=8) && (PasswordInput.text.Length !=0 || PasswordInput.text.Length <=8))
            {
                if(string.Compare(PasswordInput.text, ConfirmPasswordInput.text)==0)
                {
                    StartCoroutine(Main.Instance.Web.RegisterUser(UsernameInput.text, PasswordInput.text));  
                }
                else
                {
                    reginfo.text="Password must be same";
                }
            }
            else
            {
                reginfo.text="Input all fields and characters must be more than 8";
            }
    }

    void Start()
    {
        // RegisterButton.onClick.AddListener(() =>
        // {
        //     StartCoroutine(Main.Instance.Web.RegisterUser(UsernameInput.text, PasswordInput.text));     
        // });
    }   
    
}
