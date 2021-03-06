﻿using UnityEngine;
// Require a Rigidbody and LineRenderer object for easier assembly
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(LineRenderer))]

[System.Obsolete("This class is replaced by the Rope class")]
public class RopeBuilder : MonoBehaviour {

    public Transform target;
    public float resolution = 0.5F; 
    public float ropeDrag = 0.1F;                                
    public float ropeMass = 0.1F;                          
    public float ropeColRadius = 0.5F;               
                                                
    private Vector3[] segmentPos;          
    private GameObject[] joints;      
    private LineRenderer line;
    private int segments = 0; 
    private bool rope = false; 

    //Joint Settings
    public Vector3 swingAxis = new Vector3(1, 1, 1);
    public float lowTwistLimit = -100.0F; 
    public float highTwistLimit = 100.0F;
    public float swing1Limit = 20.0F;

    void Awake()
    {
        BuildRope();
    }

    void Update()
    {
        // Put rope control here!


        //Destroy Rope Test	(Example of how you can use the rope dynamically)
        if (rope && Input.GetKeyDown("d"))
        {
            DestroyRope();
        }
        if (!rope && Input.GetKeyDown("r"))
        {
            BuildRope();
        }
    }
    void LateUpdate()
    {
        // Does rope exist? If so, update its position
        if (rope)
        {
            for (int i = 0; i < segments; i++)
            {
                if (i == 0)
                {
                    line.SetPosition(i, transform.position);
                }
                else
                if (i == segments - 1)
                {
                    line.SetPosition(i, target.transform.position);
                }
                else
                {
                    line.SetPosition(i, joints[i].transform.position);
                }
            }
            line.enabled = true;
        }
        else
        {
            line.enabled = false;
        }
    }



    void BuildRope()
    {
        line = gameObject.GetComponent<LineRenderer>();

        // Find the amount of segments based on the distance and resolution
        // Example: [resolution of 1.0 = 1 joint per unit of distance]
        segments = (int)(Vector3.Distance(transform.position, target.position) * resolution);
        line.SetVertexCount(segments);
        segmentPos = new Vector3[segments];
        joints = new GameObject[segments];
        segmentPos[0] = transform.position;
        segmentPos[segments - 1] = target.position;

        // Find the distance between each segment
        var segs = segments - 1;
        var seperation = ((target.position - transform.position) / segs);

        for (int s = 1; s < segments; s++)
        {
            // Find the each segments position using the slope from above
            Vector3 vector = (seperation * s) + transform.position;
            segmentPos[s] = vector;

            //Add Physics to the segments
            AddJointPhysics(s);
        }

        // Attach the joints to the target object and parent it to this object	
        CharacterJoint end = target.gameObject.AddComponent<CharacterJoint>();
        end.connectedBody = joints[joints.Length - 1].transform.GetComponent<Rigidbody>();
        end.swingAxis = swingAxis;
        SoftJointLimit limit_setter = end.lowTwistLimit;
        limit_setter.limit = lowTwistLimit;
        end.lowTwistLimit = limit_setter;
        limit_setter = end.highTwistLimit;
        limit_setter.limit = highTwistLimit;
        end.highTwistLimit = limit_setter;
        limit_setter = end.swing1Limit;
        limit_setter.limit = swing1Limit;
        end.swing1Limit = limit_setter;
        target.parent = transform;

        // Rope = true, The rope now exists in the scene!
        rope = true;
    }

    void AddJointPhysics(int n)
    {
        joints[n] = new GameObject("Joint_" + n);
        joints[n].transform.parent = transform;
        Rigidbody rigid = joints[n].AddComponent<Rigidbody>();
        SphereCollider col = joints[n].AddComponent<SphereCollider>();
        CharacterJoint ph = joints[n].AddComponent<CharacterJoint>();
        ph.swingAxis = swingAxis;
        SoftJointLimit limit_setter = ph.lowTwistLimit;
        limit_setter.limit = lowTwistLimit;
        ph.lowTwistLimit = limit_setter;
        limit_setter = ph.highTwistLimit;
        limit_setter.limit = highTwistLimit;
        ph.highTwistLimit = limit_setter;
        limit_setter = ph.swing1Limit;
        limit_setter.limit = swing1Limit;
        ph.swing1Limit = limit_setter;
        //ph.breakForce = ropeBreakForce; <--------------- TODO

        joints[n].transform.position = segmentPos[n];

        rigid.drag = ropeDrag;
        rigid.mass = ropeMass;
        col.radius = ropeColRadius;

        if (n == 1)
        {
            ph.connectedBody = transform.GetComponent<Rigidbody>();
        }
        else
        {
            ph.connectedBody = joints[n - 1].GetComponent<Rigidbody>();
        }

    }

    void DestroyRope()
    {
        // Stop Rendering Rope then Destroy all of its components
        rope = false;
        for (int dj = 0; dj < joints.Length - 1; dj++)
        {
            Destroy(joints[dj]);
        }

        segmentPos = new Vector3[0];
        joints = new GameObject[0];
        segments = 0;
    }
}