using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager instance;

    [Header(" UI Refrences")]
    public GameObject LoginUI;
    public GameObject SignUpUI;
    public GameObject ForgotPasswordUI;
    public GameObject MenuUI;
   

    [Header("Scene names")]
    public string UIScene;
    public string ARScene;
    public string AR2Scene;
    public string AR3Scene;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void ClearUI()
    {
        LoginUI.SetActive(false);
        SignUpUI.SetActive(false);
        MenuUI.SetActive(false);
        ForgotPasswordUI.SetActive(false);
    }

    public void LoginPanel()
    {
        ClearUI();
        LoginUI.SetActive(true);
    }

    public void SignUpPanel()
    {
        ClearUI();
        SignUpUI.SetActive(true);

    }

    public void MenuPanel()
    {
        ClearUI();
        MenuUI.SetActive(true);
    }
    
    public void quit()
    {

        Application.Quit();
    }
}
