using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading.Tasks;
using Firebase.Auth;
using Google;
using TMPro;
using UnityEngine.SceneManagement;
public class manager : MonoBehaviour
{


[SerializeField]
TMP_InputField name;
[SerializeField]
TMP_InputField age;
[SerializeField]
TMP_InputField email;
[SerializeField]
TMP_InputField password;
[SerializeField]
    TMP_Dropdown gender;
   public async Task LoginAndInitialize()
    {
        try
        {
            FirebaseUser user = await FirebaseAuthManager.Instance.LoginWithEmailPassword(email.text, password.text);
            
            if (user != null)
            {
                Debug.Log("User logged in successfully.");
                SceneManager.LoadScene("homepage");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Login failed: {e.Message}");
            // Handle the error (e.g., show a message to the user)
        }
    }

    public async Task SignUpAndInitialize()
    {
        try
        {
            FirebaseUser user = await FirebaseAuthManager.Instance.SignUpWithEmailPassword(email.text, password.text);
            
            if (user != null)
            {
                await firebase.Instance.CreateNewUser(name.text, age.text, gender.options[gender.value].text);
                Debug.Log("New user profile created.");
                SceneManager.LoadScene("homepage");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Sign-up failed: {e.Message}");
            // Handle the error (e.g., show a message to the user)
        }
    }

    // Call this method to start the login process
    public void StartLogin()
    {
        LoginAndInitialize().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError($"Login failed: {task.Exception}");
                // Handle the error (e.g., show a message to the user)
            }
            else
            {
                Debug.Log("Login completed successfully");
            }
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    // Call this method to start the sign-up process
    public void StartSignUp()
    {
        SignUpAndInitialize().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError($"Sign-up failed: {task.Exception}");
                // Handle the error (e.g., show a message to the user)
            }
            else
            {
                Debug.Log("Sign-up completed successfully");
            }
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    public void gotologin(){
        SceneManager.LoadScene("Login");
    }
    public void gotoRegister(){
        SceneManager.LoadScene("Register");
    }
}