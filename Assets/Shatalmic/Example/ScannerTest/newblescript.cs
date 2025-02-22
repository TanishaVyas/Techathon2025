using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class newblescript : MonoBehaviour
{
    private string DeviceName = "ESP32";

    private string ServiceUUID = "4fafc201-1fb5-459e-8fcc-c5c9c331914b";

    private string Characteristic = "beb5483e-36e1-4688-b7f5-ea07361b26a8";

    [SerializeField]
    private TMP_Text stateText;

    [SerializeField]
    movesoham leg;

    [SerializeField]
    GameObject joint;

    [SerializeField]
    GameObject blecanvas;

    private bool IsConnected = false;

    enum States
    {
        None,
        Scan,
        Connect,
        RequestMTU,
        Subscribe,
        Unsubscribe,
        Disconnect,
        Communication
    }

    private bool _workingFoundDevice = true;

    private bool _connected = false;

    private float _timeout = 0f;

    private States _state = States.None;

    private bool _foundID = false;

    private string _deviceAddress;

    void Reset()
    {
        _workingFoundDevice = false; // used to guard against trying to connect to a second device while still connecting to the first
        _connected = false;
        _timeout = 0f;
        _state = States.None;
        _foundID = false;
        _deviceAddress = null;
    }

    void SetState(States newState, float timeout)
    {
        _state = newState;
        _timeout = timeout;
    }

    void setStateText(string test)
    {
        stateText.text = test;
    }

    void StartProcess()
    {
        setStateText("Initializing...");

        Reset();
        BluetoothLEHardwareInterface
            .Initialize(true,
            false,
            () =>
            {
                SetState(States.Scan, 0.1f);
                setStateText("Initialized");
            },
            (error) =>
            {
                setStateText("Error: " + error);
            });
    }

    // Use this for initialization
    void Start()
    {
        setStateText("");

        StartProcess();
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeout > 0f)
        {
            _timeout -= Time.deltaTime;
            if (_timeout <= 0f)
            {
                _timeout = 0f;

                switch (_state)
                {
                    case States.None:
                        break;
                    case States.Scan:
                        setStateText("Scanning for devices...");

                        BluetoothLEHardwareInterface
                            .ScanForPeripheralsWithServices(null,
                            (address, name) =>
                            {
                                // we only want to look at devices that have the name we are looking for
                                // this is the best way to filter out devices
                                if (name.Contains(DeviceName))
                                {
                                    _workingFoundDevice = true;

                                    // it is always a good idea to stop scanning while you connect to a device
                                    // and get things set up
                                    BluetoothLEHardwareInterface.StopScan();
                                    setStateText("");

                                    // add it to the list and set to connect to it
                                    _deviceAddress = address;

                                    setStateText("Found Device" +
                                    _deviceAddress);

                                    SetState(States.Connect, 0.5f);

                                    _workingFoundDevice = false;
                                }
                            },
                            null,
                            false,
                            false);
                        break;
                    case States.Connect:
                        // set these flags
                        _foundID = false;

                        setStateText("Connecting to Device");

                        // note that the first parameter is the address, not the name. I have not fixed this because
                        // of backwards compatiblity.
                        // also note that I am note using the first 2 callbacks. If you are not looking for specific characteristics you can use one of
                        // the first 2, but keep in mind that the device will enumerate everything and so you will want to have a timeout
                        // large enough that it will be finished enumerating before you try to subscribe or do any other operations.
                        BluetoothLEHardwareInterface
                            .ConnectToPeripheral(_deviceAddress,
                            null,
                            null,
                            (address, serviceUUID, characteristicUUID) =>
                            {
                                if (IsEqual(serviceUUID, ServiceUUID))
                                {
                                    // if we have found the characteristic that we are waiting for
                                    // set the state. make sure there is enough timeout that if the
                                    // device is still enumerating other characteristics it finishes
                                    // before we try to subscribe
                                    if (
                                        IsEqual(characteristicUUID,
                                        Characteristic)
                                    )
                                    {
                                        //blecanvas.SetActive(false);
                                        //joint.SetActive(true);
                                        _connected = true;
                                        SetState(States.RequestMTU, 2f);

                                        setStateText("Connected to device");
                                    }
                                }
                            },
                            (disconnectedAddress) =>
                            {
                                blecanvas.SetActive(true);
                                joint.SetActive(false);
								IsConnected = false;
                                setStateText("Device disconnected: " +
                                disconnectedAddress);
                                setStateText("Disconnected");
                                StartProcess();
                            });
                        break;
                    case States.RequestMTU:
                        setStateText("Requesting MTU");

                        BluetoothLEHardwareInterface
                            .RequestMtu(_deviceAddress,
                            185,
                            (address, newMTU) =>
                            {
                                setStateText("MTU set to " + newMTU.ToString());

                                SetState(States.Subscribe, 0.1f);
                            });
                        break;
                    case States.Subscribe:
                        setStateText("Subscribing to device");

                        BluetoothLEHardwareInterface
                            .SubscribeCharacteristicWithDeviceAddress(_deviceAddress,
                            ServiceUUID,
                            Characteristic,
                            null,
                            (address, characteristicUUID, bytes) =>
                            {
                                
                                if (bytes.Length >=16)
                                {	
                                    // Convert the byte array into four separate float values
                                    float quatW =
                                        BitConverter.ToSingle(bytes, 0);
                                    float quatX =
                                        BitConverter.ToSingle(bytes, 4);
                                    float quatY =
                                        BitConverter.ToSingle(bytes, 8);
                                    float quatZ =
                                        BitConverter.ToSingle(bytes, 12);
									if (!IsConnected)
                                {
                                    blecanvas.SetActive(false);
                                    joint.SetActive(true);
                                    IsConnected = true;
                                }
                                    leg.data =
                                        new string[] {
                                            quatW.ToString(),
                                            quatX.ToString(),
                                            quatY.ToString(),
                                            quatZ.ToString()
                                        };

                                    setStateText(quatW.ToString() +
                                    " " +
                                    quatX.ToString() +
                                    " " +
                                    quatY.ToString() +
                                    " " +
                                    quatZ.ToString());
                                    // Update the text field with the quaternion values
                                    //te.text = string.Join(", ", leg.data);
                                }
                            });

                        // set to the none state and the user can start sending and receiving data
                        _state = States.None;
                        setStateText("Waiting...");
                        break;
                    case States.Unsubscribe:
                        BluetoothLEHardwareInterface
                            .UnSubscribeCharacteristic(_deviceAddress,
                            ServiceUUID,
                            Characteristic,
                            null);
                        SetState(States.Disconnect, 4f);
                        break;
                    case States.Disconnect:
                        if (_connected)
                        {
                            BluetoothLEHardwareInterface
                                .DisconnectPeripheral(_deviceAddress,
                                (address) =>
                                {
                                    BluetoothLEHardwareInterface
                                        .DeInitialize(() =>
                                        {
                                            _connected = false;
                                            _state = States.None;
                                        });
                                });
                        }
                        else
                        {
                            BluetoothLEHardwareInterface
                                .DeInitialize(() =>
                                {
                                    _state = States.None;
                                });
                        }
                        StartProcess();
                        break;
                }
            }
        }
    }

	 public void DisconnectDevice()
    {
        if (_connected)
        {
            setStateText("Unsubscribing and disconnecting...");

            // Unsubscribe from the characteristic
            BluetoothLEHardwareInterface.UnSubscribeCharacteristic(
                _deviceAddress,
                ServiceUUID,
                Characteristic,
                null
            );

            // Disconnect the peripheral
            BluetoothLEHardwareInterface.DisconnectPeripheral(_deviceAddress, (address) =>
            {
                setStateText("Device disconnected.");
                BluetoothLEHardwareInterface.DeInitialize(() =>
                {
                    Reset();
                    setStateText("Disconnected and deinitialized.");
                });
            });
        }
        else
        {
            setStateText("No device connected to disconnect.");
        }
    }

    string FullUUID(string uuid)
    {
        return "0000" + uuid + "-0000-1000-8000-00805F9B34FB";
    }

    bool IsEqual(string uuid1, string uuid2)
    {
        return (uuid1.ToUpper().Equals(uuid2.ToUpper()));
    }
}
