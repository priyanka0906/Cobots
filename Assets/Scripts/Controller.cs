using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Controller : MonoBehaviour
{
    GameObject RobotBase;
    public float[] jointValues = new float[6];
    private GameObject[] jointList = new GameObject[6];
    public Slider[] sliders = new Slider[6];
    public GameObject[] TextFields = new GameObject[6];
    public TMP_Text[] angles = new TMP_Text[6];
    int noOfJoints;

    public void update()
    {
        
        for (int i = 0; i < noOfJoints; i++)
        {
            jointValues[i] = sliders[i].value;

            Vector3 currentRotation = jointList[i].transform.localEulerAngles;
            Debug.Log(currentRotation);
            currentRotation.z = jointValues[i];
            jointList[i].transform.localEulerAngles = currentRotation;

            angles[i].text = jointValues[i].ToString();
        }
        
    }

    public void setRobotBase(GameObject robotbase) 
    {
        RobotBase = robotbase;
        initializeJoints();

    }
    public void setValueOfn(int n)
    {
        noOfJoints = n;
    }
    public void SetNoOfSliders()
    {   
        for(int i=0;i<6;i++)
        {
            TextFields[i].SetActive(false);
            angles[i].text = "";
        }
       
        if(RobotBase.name =="UR5")
        {
            for (int i = 0; i < noOfJoints; i++)
            {
                TextFields[i].SetActive(true);
                angles[i].text = 0.ToString();
            }
        }
        else if(RobotBase.name=="TM5")
        {
            for (int i = 0; i < noOfJoints; i++)
            {
                TextFields[i].SetActive(true);
                angles[i].text = 0.ToString();
            }
        }
        else if(RobotBase.name == "KR_30")
        {
            for(int i=0;i< noOfJoints; i++)
            {
                TextFields[i].SetActive(true);
                angles[i].text = 0.ToString();
            }
        }

        else if(RobotBase.name == "KR8")
        {
            for (int i = 0; i < noOfJoints; i++)
            {
                TextFields[i].SetActive(true);
                angles[i].text = 0.ToString(); ;
            }
        }

    }

    public void setSlidersLimit()
    {
        if(RobotBase.name == "UR5")
        {
             float[] upperLimit = { 180f, 90f, 90f, 90f, 90f, 180f };
             float[] lowerLimit = { -180f, -90f, -90f, -90f, -90f, -180f };
            for (int i = 0; i < noOfJoints; i++)
            {
                sliders[i].maxValue = upperLimit[i];
                sliders[i].minValue = lowerLimit[i];
                sliders[i].value = 0;
            }

        }
        else if (RobotBase.name == "TM5")
        {
              float[] upperLimit = { 180f, 90f, 90f, 90f, 180f, 90f };
              float[] lowerLimit = { -180f, -90f, -90f, -90f, -180f, -90f };
            for (int i = 0; i < noOfJoints; i++)
            {
                sliders[i].maxValue = upperLimit[i];
                sliders[i].minValue = lowerLimit[i];
                sliders[i].value = 0;
            }
        }
        else if (RobotBase.name == "KR_30")
        {
            float[] upperLimit = {185f,60f,60f};
            float[] lowerLimit = { -185f, -60f, -60f };

            for(int i=0;i< noOfJoints; i++)
            {
                sliders[i].maxValue = upperLimit[i];
                sliders[i].minValue = lowerLimit[i];
                sliders[i].value = 0;
            }

        }
        else if(RobotBase.name == "KR8")
        {
            float[] upperLimit = {180f,65f,85f,165f,350f };
            float[] lowerLimit = { -180f, -65f, -85f, -165f, -350f};
            for (int i = 0; i < noOfJoints; i++)
            {
                sliders[i].maxValue = upperLimit[i];
                sliders[i].minValue = lowerLimit[i];
                sliders[i].value = 0;
            }
        }
    }

    // Create the list of GameObjects that represent each joint of the cobot
    void initializeJoints()
    {
        var Joints = RobotBase.GetComponentsInChildren<Transform>();
        for (int i = 0; i < Joints.Length; i++)
        {
            if (Joints[i].name == "control0")
            {
                jointList[0] = Joints[i].gameObject;
            }
            else if (Joints[i].name == "control1")
            {
                jointList[1] = Joints[i].gameObject;
            }
            else if (Joints[i].name == "control2")
            {
                jointList[2] = Joints[i].gameObject;
            }
            else if (Joints[i].name == "control3")
            {
                jointList[3] = Joints[i].gameObject;
            }
            else if (Joints[i].name == "control4")
            {
                jointList[4] = Joints[i].gameObject;
            }
            else if (Joints[i].name == "control5")
            {
                jointList[5] = Joints[i].gameObject;
            }
            
        }
    }
}
