using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth; // Firebase Authentication
using Firebase.Database; // Firebase Realtime Database
using System.Threading.Tasks;
using TMPro;
public class ProfileManager : MonoBehaviour
{
    private FirebaseAuthManager firebaseAuthManager;
    private DatabaseReference databaseRef;

    [SerializeField]
    TMP_Text Tname;
    [SerializeField]
    TMP_Text Tage;
    [SerializeField]
    TMP_Text Tgender;
    [SerializeField]
    TMP_Text Temail;
    [SerializeField]
    TMP_Text Tauthid;

    // Start is called before the first frame update
    void Start()
    {
        // Get FirebaseAuthManager instance
        firebaseAuthManager = FirebaseAuthManager.Instance;

        // Initialize Firebase Database reference
        databaseRef = FirebaseDatabase.DefaultInstance.RootReference;

        // Initialize Firebase asynchronously
        InitializeFirebase();
    }

    private async void InitializeFirebase()
    {
        // Wait for Firebase initialization to complete
        await FirebaseAuthManager.WaitForInitialization();

        // Check if Firebase is initialized
        if (firebaseAuthManager.IsInitialized())
        {
            // Get the current user
            FirebaseUser currentUser = firebaseAuthManager.GetCurrentUser();

            if (currentUser != null)
            {
                Debug.Log($"Current User ID: {currentUser.UserId}");
                Debug.Log($"Current User Email: {currentUser.Email}");

                // Fetch and display patient details from Firebase Realtime Database
                await FetchPatientDetails(currentUser.UserId);
            }
            else
            {
                Debug.Log("No user is currently logged in.");
            }
        }
        else
        {
            Debug.LogError("Firebase is not initialized.");
        }
    }

    // Function to fetch patient details from Firebase Realtime Database
    private async Task FetchPatientDetails(string userId)
    {
        // Reference to the user's patient details node in the database
        DatabaseReference patientDetailsRef = databaseRef.Child(userId).Child("Patient_Details");

        // Fetch the data asynchronously
        var task = patientDetailsRef.GetValueAsync();

        // Wait until the task completes
        await task;

        // Check if the task was successful
        if (task.IsCompleted)
        {
            DataSnapshot snapshot = task.Result;

            if (snapshot.Exists)
            {
                // Extract details from the snapshot
                string name = snapshot.Child("Name").Value?.ToString();
                string age = snapshot.Child("Age").Value?.ToString();
                string gender = snapshot.Child("Gender").Value?.ToString();
                string email = snapshot.Child("Email").Value?.ToString();
                string authid= snapshot.Child("AuthUID").Value?.ToString();
                // Ensure the Debug.Log calls are executed on the main thread
                DisplayPatientDetails(name, age, gender,email,authid);
            }
            else
            {
                Debug.LogWarning("No patient details found for this user.");
            }
        }
        else
        {
            Debug.LogError("Failed to retrieve patient details: " + task.Exception?.Message);
        }
    }

    private void DisplayPatientDetails(string name, string age, string gender, string email,string authid)
    {
       Tname.text=name;
       Tage.text=age;
       Tgender.text=gender;
       Temail.text=email;
       Tauthid.text=authid;
    }
}
