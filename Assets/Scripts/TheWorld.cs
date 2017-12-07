using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class TheWorld : MonoBehaviour {

    public SceneNode RootNode;
    public Camera NodeCam;
    //public LineSegment LineOfSight;
    //private float kSightLength = 20f;
    //private float kNodeCamPos = 3f;

    	// Use this for initialization
	void Start () {
        Debug.Assert(RootNode != null);
        //Debug.Assert(NodeCam != null);
        //Debug.Assert(LineOfSight != null);
        //LineOfSight.SetWidth(0.05f);
    }

    void Update()
    {
        Vector3 pos, dir;
        Matrix4x4 m = Matrix4x4.identity;
        RootNode.CompositeXform(ref m, out pos, out dir);

        //Vector3 p1 = pos;
        //Vector3 p2 = pos + kSightLength * dir;
        //LineOfSight.SetEndPoints(p1, p2);

        // Now update NodeCam
        //NodeCam.transform.localPosition = pos + kNodeCamPos * dir;
        //NodeCam.transform.LookAt(p2, Vector3.up);
    }

    public SceneNode GetRootNode() { return RootNode; }
    
}
