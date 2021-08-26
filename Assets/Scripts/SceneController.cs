using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneController : MonoBehaviour
{
   public static bool scene1=false, scene2 = false, scene3=false;
    public  static SceneController instance;
    public TMP_Text msgTxt;

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            BackButtonPressed();
    }


    public void changeSceneToScene1()
    {
        scene1 = true;
        SceneManager.LoadScene(UIManager.instance.ARScene);
    }

    public void changeSceneToScene2()
    {
        scene2 = true;
        SceneManager.LoadScene(UIManager.instance.AR2Scene);
    }

    public void changeSceneToScene3()
    {
        scene3 = true;
        SceneManager.LoadScene(UIManager.instance.AR3Scene);
    }

    

    public void BackScene()
    {
        if(SceneManager.GetActiveScene().name == UIManager.instance.ARScene)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

        else if(SceneManager.GetActiveScene().name == UIManager.instance.AR2Scene)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);

        UIManager.instance.MenuPanel();
    }

    public void BackButtonPressed()
    {
        
            if (SceneManager.GetActiveScene().name == UIManager.instance.ARScene || SceneManager.GetActiveScene().name == UIManager.instance.AR2Scene|| SceneManager.GetActiveScene().name == UIManager.instance.AR3Scene)
            {

                BackScene();

            }
            else
            {

                if (UIManager.instance.LoginUI.activeSelf)
                {
                    UIManager.instance.quit();

                }
                else if (UIManager.instance.SignUpUI.activeSelf)
                {
                    UIManager.instance.LoginPanel();

                }
                else if (UIManager.instance.ForgotPasswordUI.activeSelf)
                {
                    UIManager.instance.LoginPanel();

                }
                else if (UIManager.instance.MenuUI.activeSelf)
                {
                    if (PlayerPrefs.GetInt("isChecked") == 1)
                        UIManager.instance.quit();
                    else
                        UIManager.instance.LoginPanel();

                }

            }


        

    }

    public void clearMsg()
    {
        msgTxt.text = "";

    }
    public void setMsg()
    {
        msgTxt.text = "Scan the QR code ";
    }

}
