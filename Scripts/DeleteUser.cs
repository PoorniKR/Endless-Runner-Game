using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeleteUser : MonoBehaviour
{
    public Button DeleteButton;

    // Start is called before the first frame update
    void Start()
    {
        DeleteButton.onClick.AddListener(() =>
        {
            StartCoroutine(Main.Instance.Web.DeleteUser());
        });
    }
}
