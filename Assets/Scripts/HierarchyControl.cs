// Some code borrowed from 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kinect = Windows.Kinect;


public class HierarchyControl : MonoBehaviour {

    public GameObject BodySourceManager;
    GameObject shoulder;
    GameObject elbow;
    GameObject wrist;
    GameObject handTip;

    public NodePrimitive upperArm;
    public NodePrimitive foreArm;
    public NodePrimitive hand;

    public Transform shoulderPivot;
    public Transform elbowPivot;
    public Transform wristPivot;
    public Transform handPivot;

    public Vector3 rootPosition;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private BodySourceManager _BodyManager;

    // Use this for initialization
    void Start () {
        // dummy game objects for holding tracked values from kinect
        shoulder = new GameObject();
        elbow = new GameObject();
        wrist = new GameObject();
        handTip = new GameObject();

        // anchor position of the arm Hierarchy
        rootPosition = new Vector3(-5, 10, -10);

    }
	
	// Update is called once per frame
	void Update () {
        if (BodySourceManager == null)
        {
            return;
        }

        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null)
        {
            return;
        }
        Kinect.Body[] data = _BodyManager.GetData();
        if (data == null)
        {
            return;
        }

        foreach (var body in data)
        {
            if (body == null)
            {
                continue;
            }

            if (body.IsTracked)
            {
                //print("Tracked!");
                
                // store values read from kinect
                shoulder.transform.localPosition = GetVector3FromJoint(body.Joints[Kinect.JointType.ShoulderRight]);
                elbow.transform.localPosition = GetVector3FromJoint( body.Joints[Kinect.JointType.ElbowRight]);
                wrist.transform.localPosition = GetVector3FromJoint(body.Joints[Kinect.JointType.WristRight]);
                handTip.transform.localPosition = GetVector3FromJoint(body.Joints[Kinect.JointType.HandTipRight]);

                // calculate hierarchy positions
                shoulderPivot.localPosition = shoulder.transform.localPosition;
                shoulderPivot.transform.up = calculateOffset(shoulder.transform.localPosition, elbow.transform.localPosition).normalized;
                elbowPivot.localPosition = shoulderPivot.transform.InverseTransformPoint(elbow.transform.position);  
                elbowPivot.transform.up = calculateOffset(elbow.transform.localPosition, wrist.transform.localPosition).normalized;
                wristPivot.localPosition = elbowPivot.transform.InverseTransformPoint(wrist.transform.position);
                wristPivot.transform.up = calculateOffset(wrist.transform.localPosition, handTip.transform.localPosition).normalized;
                handPivot.localPosition = wristPivot.transform.InverseTransformPoint(handTip.transform.position);


                // set arm segment sizes
                upperArm.transform.localScale = new Vector3(1,elbowPivot.transform.localPosition.magnitude/2,1);
                upperArm.GetComponent<NodePrimitive>().Pivot.y = elbowPivot.transform.localPosition.magnitude / 4;
                foreArm.transform.localScale = new Vector3(1,wristPivot.transform.localPosition.magnitude/2, 1);
                foreArm.GetComponent<NodePrimitive>().Pivot.y = wristPivot.transform.localPosition.magnitude / 4;
                hand.transform.localScale = new Vector3(1, handPivot.transform.localPosition.magnitude/2, 1);
                hand.GetComponent<NodePrimitive>().Pivot.y = handPivot.transform.localPosition.magnitude / 4;

                // move Hierarchy to final position
                shoulderPivot.localPosition = rootPosition;
                //shoulderPivot.localRotation = // camera's rotation?

            }
        }
    }


    Vector3 calculateOffset(Vector3 parent, Vector3 child)
    {
        Vector3 res = new Vector3();
        res.x = parent.x - child.x;
        res.y = parent.y - child.y;
        res.z = parent.z - child.z;
        return res;
    }

    public Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        //  negative z value to make movement not mirrored
        return new Vector3(joint.Position.X  , joint.Position.Y, -joint.Position.Z ) * 20;
    }

    void drawArmSegment(Vector3 start, Vector3 end)
    {
        float length = (end - start).magnitude;
    }
}
