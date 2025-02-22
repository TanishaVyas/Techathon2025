using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Make sure to include this namespace for TextMeshPro

public class EnterableAddress : MonoBehaviour
{
    // Serialized fields for the input field and placeholder text
    [SerializeField] private TMP_InputField inputField; // Reference to the TMP Input Field
    [SerializeField] private string placeholderText; // Placeholder text for the input field

    private const string PlayerPrefKey = "UserAddress"; // Key for PlayerPrefs

    // Start is called before the first frame update
    void Start()
    {
        // Get the saved address from PlayerPrefs and set it as the placeholder
        if (PlayerPrefs.HasKey(PlayerPrefKey))
        {
            placeholderText = PlayerPrefs.GetString(PlayerPrefKey);
            inputField.placeholder.GetComponent<TextMeshProUGUI>().text = placeholderText;
        }
        else
        {
            // Optionally set a default placeholder if no PlayerPrefs data is found
            inputField.placeholder.GetComponent<TextMeshProUGUI>().text = "Enter your address here...";
        }
    }

    // Method to save the input field value to PlayerPrefs
    public void SaveAddress()
    {
        string address = inputField.text;
        PlayerPrefs.SetString(PlayerPrefKey, address);
        PlayerPrefs.Save(); // Ensure the PlayerPrefs data is saved
    }
}
