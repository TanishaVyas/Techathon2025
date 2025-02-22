using System;
using Android.BLE;
using TMPro;
using UnityEngine;

public class MyScript : MonoBehaviour
{
    private bool IsConnected=false;

    private string deviceAddress = "34:B7:DA:52:97:65"; //E4:65:B8:83:A5:5A;

    private string serviceUUID = "4fafc201-1fb5-459e-8fcc-c5c9c331914b";

    private string characteristicUUID = "beb5483e-36e1-4688-b7f5-ea07361b26a8";

    [SerializeField]
    TMP_Text te;
    [SerializeField]
    GameObject blecanvas;

    [SerializeField]
    movesoham leg;
    [SerializeField]
    GameObject joint;
    private BleDevice bleDevice;

    private BleGattCharacteristic characteristic;
private const string PlayerPrefKey = "UserAddress";
    private void Start()
    {   
        deviceAddress=PlayerPrefs.GetString(PlayerPrefKey);
        ConnectToDevice();
    }

    private void ConnectToDevice()
    {
        BleManager.Instance.SearchForDevices(5000, OnDeviceFound);
    }

    private void OnDeviceFound(BleDevice device)
    {
        if (device.MacAddress == deviceAddress)
        {
            bleDevice = device;
            bleDevice.Connect (OnDeviceConnected, OnDeviceDisconnected);
        }
    }
public void disconnected()
{
     DisconnectDevice();
}
private void DisconnectDevice()
    {
        if (IsConnected && bleDevice != null)
        {
            Debug.Log("Disconnecting device...");

            if (characteristic != null)
            {
                characteristic.Unsubscribe();
                characteristic = null;
            }

            bleDevice.Disconnect();
            bleDevice = null;

            IsConnected = false;
            blecanvas.SetActive(true);
            joint.SetActive(false);

            Debug.Log("Device disconnected successfully.");
        }
        else
        {
            Debug.LogWarning("No device to disconnect or already disconnected.");
        }
    }

    private void OnDeviceConnected(BleDevice device)
    {
        characteristic =
            device.GetCharacteristic(serviceUUID, characteristicUUID);
        if (characteristic != null)
        {
            characteristic.Subscribe (OnCharacteristicValueChanged);
        }
    }

    private void OnDeviceDisconnected(BleDevice device)
    {
    
        DisconnectDevice();

       
    }

    
    private void OnCharacteristicValueChanged(byte[] data)
{
    if (!IsConnected)
    {
        blecanvas.SetActive(false);
        joint.SetActive(true);
        IsConnected = true;
    }

    // Check if the received data has the expected length (4 floats, 4 bytes each)
    if (data.Length == 16)
    {
        // Convert the byte array into four separate float values
        float quatW = BitConverter.ToSingle(data, 0);
        float quatX = BitConverter.ToSingle(data, 4);
        float quatY = BitConverter.ToSingle(data, 8);
        float quatZ = BitConverter.ToSingle(data, 12);

        // Store the quaternion values in the leg.data array
        leg.data = new string[] { quatW.ToString(), quatX.ToString(), quatY.ToString(), quatZ.ToString() };

        // Update the text field with the quaternion values
        //te.text = string.Join(", ", leg.data);
    }
    else
    {
        // Handle the case where the received data has an unexpected length
        Debug.LogWarning("Received data has an unexpected length: " + data.Length);
    }
}
}
