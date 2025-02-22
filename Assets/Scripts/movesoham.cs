using UnityEngine;
using TMPro;

public class movesoham : MonoBehaviour
{
    public progressBar pb;
    public progressBar pb2;
    enum JointType
    {
        Knee,
        Elbow,
        Wrist,
        Ankle,
        Shoulder
    }

    [SerializeField] JointType jointType;
    [SerializeField] int kneeDeviation;
    [SerializeField] int elbowDeviation;
    [SerializeField] int wristDeviationX;
    [SerializeField] int wristDeviationY;

    public string[] data;
    public GameObject jointObject;
    public TMP_Text angleText;
    public TMP_Text angleyText;
    public Quaternion initialRotation;
    public Quaternion currentRotation;
    public int max;
    public int min =0;

    public int angle;
    void Start()
    {
    

        UpdateRotations();
        ResetInitialRotation();
    }

    void FixedUpdate()
    {
        if (data.Length < 4) return;


        UpdateRotations();
        ApplyRotation();
        UpdateAngleDisplay();
    }

    void UpdateRotations()
    {
        float w = float.Parse(data[0]);
        float x = float.Parse(data[1]);
        float y = float.Parse(data[2]);
        float z = float.Parse(data[3]);

        currentRotation = GetRotationForJoint(x, y, z, w);
    }

    Quaternion GetRotationForJoint(float x, float y, float z, float w)
    {
        switch (jointType)
        {
            case JointType.Knee:
                 return new Quaternion(x,-y,-z,w).normalized;//done
            case JointType.Elbow:
                return new Quaternion(z,x,y, w).normalized;//x y
            case JointType.Ankle:
                return new Quaternion(-y, -x, z, w).normalized;
            case JointType.Wrist:
                return new Quaternion(-z,-y,-x, w).normalized;//0,-y,-x
            default:
                return new Quaternion(0,0,x, w).normalized;
        }
    }

    void ApplyRotation()
    {
        Quaternion rotationDelta = currentRotation * initialRotation;
        jointObject.transform.localRotation = rotationDelta;
    }

    void UpdateAngleDisplay()
    {
        angle = CalculateAngle();
        max = Mathf.Max(max, Mathf.Abs(angle));

        if (jointType == JointType.Knee )
        {
            angleText.text = (-1*((-angle)+180)).ToString();
            pb.health = -1*(-angle+180);
        }
        else if (jointType == JointType.Elbow)
        {
            angleText.text = (-1*((-angle+180))).ToString();
            pb.health = -1*(-angle+180);
        }
        else if(jointType == JointType.Ankle || jointType == JointType.Wrist)
        {
            int angleY = CalculateAngleY();
            angleText.text = $"{Mathf.Abs(angle)-180}";
            angleyText.text = $"{Mathf.Abs(angleY)-180}";
            pb.health = Mathf.Abs(angle)-180;
            pb2.health = Mathf.Abs(angleY)-180;
        }
    }

    int CalculateAngle()
    {
        float rawAngle = -2 * (Mathf.Rad2Deg * Mathf.Acos(jointObject.transform.localRotation.x));
        return ((int)rawAngle - kneeDeviation) * -1;
    }

    int CalculateAngleY()
    {
        float rawAngle = -2 * (Mathf.Rad2Deg * Mathf.Acos(jointObject.transform.localRotation.z));
        return Mathf.Abs(((int)rawAngle * -1) - wristDeviationX);
    }

    public void ResetInitialRotation()
    {
        initialRotation = Quaternion.Inverse(currentRotation);
    }

    public void ResetMaxAngle()
    {
     max = 0;
    }
}