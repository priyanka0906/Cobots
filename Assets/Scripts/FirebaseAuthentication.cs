using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;


public class FirebaseAuthentication : MonoBehaviour
{
   public static FirebaseAuthentication instance;
   

    [Header("Firebase")]
    public FirebaseAuth auth;
    public FirebaseUser user;

    [Header("Login Refrences")]
    [SerializeField]
    private TMP_InputField LoginEmail;
    [SerializeField]
    private TMP_InputField LoginPassword;
    [SerializeField]
    private TMP_Text LoginOutput;

    [Header("SignUp Refrences")]
    [SerializeField]
    private TMP_InputField SignUpEmail;
    [SerializeField]
    private TMP_InputField SignUpPassword;
    [SerializeField]
    private TMP_InputField SignUpConfirmPassword;
    [SerializeField]
    private TMP_Text SignUpOutput;

    [Header("ForgotPassword Refrences")]
    [SerializeField]
    private TMP_InputField ForgotPasswordEmail;
    [SerializeField]
    private TMP_Text EmailText;

    

    [Header("Toogle Button")]
    [SerializeField]
    private Toggle toggle;

   


   private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if(instance == null)
        {
            instance = this;
        }
        else if(instance!=this)
        {
            Destroy(instance.gameObject);
            instance = this;
        }


    }

    private void Start()
    { 
       
        
        StartCoroutine(CheckAndFixDependancies());
    }

    private IEnumerator CheckAndFixDependancies()
    {
        var checkAndFixDependanciesTask = FirebaseApp.CheckAndFixDependenciesAsync();
        yield return new WaitUntil(predicate: () => checkAndFixDependanciesTask.IsCompleted);
        var dependencyResult = checkAndFixDependanciesTask.Result;

            if (dependencyResult == DependencyStatus.Available)
            {
                InitializeFirebase();
                
            }
            else
            {
                Debug.LogError("$Could not resolve all Firebase dependencies: {dependencyStatus}");
            }
        
    }

   
    private void InitializeFirebase()
    {
        auth = FirebaseAuth.DefaultInstance;

        if (PlayerPrefs.GetInt("isChecked") == 1)
            StartCoroutine(checkAutoLogin());
        else if (SceneController.scene1 == true || SceneController.scene2 == true||SceneController.scene3==true)
        {
            UIManager.instance.MenuPanel();
            SceneController.scene1 = false;
            SceneController.scene2 = false;
            SceneController.scene3 = false;

        }

        else
            UIManager.instance.LoginPanel();

        auth.StateChanged += AuthStateChanged;
        AuthStateChanged(this, null);
    }

    private void AuthStateChanged(object sender, System.EventArgs eventArgs)
    {
        if (auth.CurrentUser != user)
        {
            bool signedin = user != auth.CurrentUser && auth.CurrentUser != null;

            if (!signedin && user != null)
            {
                Debug.Log("Signed Out");
            }
            user = auth.CurrentUser;
            if (signedin)
            {
                Debug.Log($"Signed In: {user.UserId}");
            }
        }
    }

    private IEnumerator checkAutoLogin()
    {
        yield return new WaitForEndOfFrame();

       if(user!=null)
     {
            var reloadTask = user.ReloadAsync();

            yield return new WaitUntil(predicate: () => reloadTask.IsCompleted);

           AutoLogin();
        }
        else
       {
            UIManager.instance.LoginPanel();
       }
   }
   private void AutoLogin()
   {
       if(user!=null)
       {
            UIManager.instance.MenuPanel();
        }
       else
       {
            UIManager.instance.LoginPanel();
        }
   }
   
   

    public void outputClear()
    {
        LoginOutput.text = "";
        SignUpOutput.text = "";
    }

    public void LoginButton()
    {
        StartCoroutine(Login(LoginEmail.text.ToString().Trim(), LoginPassword.text.ToString().Trim()));
    }
    
    public void SignUpButton()
    {
        StartCoroutine(SignUp(SignUpEmail.text.ToString(), SignUpPassword.text.ToString().Trim(), SignUpConfirmPassword.text.ToString().Trim()));
    }

    public void ForgotPassword()
    {
        StartCoroutine(ForgotPasswordLogic(ForgotPasswordEmail.text.ToString().Trim()));
    }

   private IEnumerator Login(string _email,string _password)
    {
        Credential credential = EmailAuthProvider.GetCredential(_email, _password);

        var loginTask = auth.SignInWithCredentialAsync(credential);
        yield return new WaitUntil(predicate: () => loginTask.IsCompleted);
        string output = "Unknown Error, Please Try Again!";
        if(loginTask.Exception != null)
        {
            FirebaseException firebaseException = (FirebaseException)loginTask.Exception.GetBaseException();
            AuthError error = (AuthError)firebaseException.ErrorCode;

            switch(error)
            {
                case AuthError.MissingEmail:
                    output = "Please enter your Email";
                    break;
                case AuthError.MissingPassword:
                    output = "Please enter your Password";
                    break;
                case AuthError.InvalidEmail:
                    output = "Invalid Email";
                    break;
                case AuthError.WrongPassword:
                    output = "Incorrect Password";
                    break;
                case AuthError.UserNotFound:
                    output = "Account does not exist";
                    break;
            }

            LoginOutput.text = output; ;
        }
        else
        {
            if(user.IsEmailVerified)
            {
                yield return new WaitForSeconds(1f);
                if (toggle.isOn)
                    user = null;
                UIManager.instance.MenuPanel();
            }
            else
            {
                if (toggle.isOn)
                    user = null;
                UIManager.instance.MenuPanel();
            }
        }

    }

  private IEnumerator SignUp(string _email,string _password,string _confirmpassword)
    {
        if(_password!=_confirmpassword)
        {
            SignUpOutput.text = "Password do not match!";
        }
        else
        {
            var registerTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            yield return new WaitUntil(predicate: () => registerTask.IsCompleted);
            string output = "Unknown Error, Please Try Again!";
            if (registerTask.Exception != null)
            {
                FirebaseException firebaseException = (FirebaseException)registerTask.Exception.GetBaseException();
                AuthError error = (AuthError)firebaseException.ErrorCode;

                switch (error)
                {
                    case AuthError.InvalidEmail:
                        output = "Invalid Email";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        output = "Email already in use";
                        break;
                    case AuthError.WeakPassword:
                        output = "Weak PasswordS";
                        break;
                    case AuthError.MissingEmail:
                        output = "Please enter your email";
                        break;
                    case AuthError.MissingPassword:
                        output = "Please enter your password";
                        break;
                }

                SignUpOutput.text = output;
             }
            else
            {
                FirebaseUser newUser = registerTask.Result;
                Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                    newUser.DisplayName, newUser.UserId);
                UIManager.instance.LoginPanel();
            }

    
        }
    }

    private IEnumerator ForgotPasswordLogic(string _email)
    {
        var task = auth.SendPasswordResetEmailAsync(_email);
        yield return new WaitUntil(predicate: () => task.IsCompleted);
        string output = "Unknown Error, Please Try Again!";
        if (task.Exception != null)
        {
            FirebaseException firebaseException = (FirebaseException)task.Exception.GetBaseException();
            AuthError error = (AuthError)firebaseException.ErrorCode;

            switch (error)
            {
                case AuthError.MissingEmail:
                    output = "Please enter your Email";
                    break;
                case AuthError.InvalidEmail:
                    output = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    output = "Account does not exist";
                    break;
            }

            EmailText.text = output;
        }
        else
        {
            EmailText.text = "Password Reset mail has been sent to your mail";

        }
    }
    

    public void RememberMe()
    {
        PlayerPrefs.SetInt("isChecked", toggle.isOn ? 1:0);
    }

    


}
