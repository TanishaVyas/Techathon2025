using System;
using System.Threading.Tasks;
using Firebase.Auth;
using UnityEngine;
using Firebase;
public class FirebaseAuthManager : MonoBehaviour
{
    public static FirebaseAuthManager Instance { get; private set; }
    private bool firebaseInitialized;
    private FirebaseAuth auth;
    private static TaskCompletionSource<bool> initializationComplete = new TaskCompletionSource<bool>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializeFirebaseAsync();
    }

    private async void InitializeFirebaseAsync()
    {
        await FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                auth = FirebaseAuth.DefaultInstance;
                initializationComplete.SetResult(true);
                firebaseInitialized=true;
                Debug.Log("Firebase initialized successfully");
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
                initializationComplete.SetResult(false);
            }
        });
    }

    public static async Task WaitForInitialization()
    {
        await initializationComplete.Task;
    }


     public async Task<FirebaseUser> LoginWithEmailPassword(string email, string password)
    {
        await WaitForInitialization();

        try
        {
            var signInResult = await auth.SignInWithEmailAndPasswordAsync(email, password);
            Debug.Log("User logged in successfully");
            return signInResult.User;
        }
        catch (FirebaseException e)
        {
            Debug.LogError($"Failed to log in: {e.Message}");
            throw;
        }
    }

    public async Task<FirebaseUser> SignUpWithEmailPassword(string email, string password)
    {
        await WaitForInitialization();

        try
        {
            var createResult = await auth.CreateUserWithEmailAndPasswordAsync(email, password);
            Debug.Log("New user created and signed in successfully");
            return createResult.User;
        }
        catch (FirebaseException e)
        {
            Debug.LogError($"Failed to create user: {e.Message}");
            throw;
        }
    }
    public FirebaseUser GetCurrentUser()
    {
        return auth.CurrentUser;
    }

    public bool IsInitialized()
{
    return firebaseInitialized;
}
}