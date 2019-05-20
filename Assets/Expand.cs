using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expand : MonoBehaviour
{
    public GameObject trail;
    public GameObject thumbTip;
    public GameObject indexTip;
    public GameObject middleTip;
    public GameObject ringTip;
    public GameObject pinkyTip;


    public GameObject palm;
    private GameObject wrist;

    public float thumbDist;
    public float indexDist;
    public float middleDist;
    public float ringDist;
    public float pinkyDist;

    private Vector3 normal;

    private const double epsilon = .075;

    private GameObject sphere;

    private LineRenderer lr1;
    private LineRenderer lr2;
    private LineRenderer lr3;
    private LineRenderer lr4;
    private LineRenderer lr5;

    public GameObject[] thumbLineObjects = new GameObject[5];
    public GameObject[] indexLineObjects = new GameObject[5];
    public GameObject[] middleLineObjects = new GameObject[5];
    public GameObject[] ringLineObjects = new GameObject[5];
    public GameObject[] pinkyLineObjects = new GameObject[5];

    public GameObject[] thumbLineSphereObjects = new GameObject[5];
    public GameObject[] indexLineSphereObjects = new GameObject[5];
    public GameObject[] middleLineSphereObjects = new GameObject[5];
    public GameObject[] ringLineSphereObjects = new GameObject[5];
    public GameObject[] pinkyLineSphereObjects = new GameObject[5];



    public Vector3[] thumbLines = new Vector3[5];
    public Vector3[] indexLines = new Vector3[5];
    public Vector3[] middleLines = new Vector3[5];
    public Vector3[] ringLines = new Vector3[5];
    public Vector3[] pinkyLines = new Vector3[5];

    float palmXpos;
    float palmYpos;
    float palmZpos;
    float palmXrotation;
    float palmYrotation;
    float palmZrotation;
    Vector3 palmPos;
    Quaternion palmRotation;

    Rigidbody rb;

    private Color whiteColor = new Color(255, 255, 255);

    Vector3 palmPosVelocity;

    public Material laser;
    public Material laser2;
    public Material laser3;


    // Start is called before the first frame update
    void Start()
    {

        makeJointSpheres();

        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.SetActive(false);
        sphere.transform.localScale = new Vector3(.1f, .1f, .1f);
        sphere.AddComponent<SphereCollider>();
        sphere.AddComponent<Rigidbody>();
        sphere.GetComponent<Renderer>().material = laser2;
        trail.transform.SetParent(sphere.transform);

        lr1 = thumbTip.AddComponent<LineRenderer>();
        lr1.positionCount = thumbLineObjects.Length;
        lr1.startWidth = .005f;
        lr1.endWidth = .005f;
        lr1.material = laser;

        lr2 = indexTip.AddComponent<LineRenderer>();
        lr2.positionCount = indexLineObjects.Length;
        lr2.startWidth = .005f;
        lr2.endWidth = .005f;
        lr2.material = laser;


        lr3 = middleTip.AddComponent<LineRenderer>();
        lr3.positionCount = middleLineObjects.Length;
        lr3.startWidth = .005f;
        lr3.endWidth = .005f;
        lr3.material = laser;

        lr4 = ringTip.AddComponent<LineRenderer>();
        lr4.positionCount = ringLineObjects.Length;
        lr4.startWidth = .005f;
        lr4.endWidth = .005f;
        lr4.material = laser;

        lr5 = pinkyTip.AddComponent<LineRenderer>();
        lr5.positionCount = pinkyLineObjects.Length;
        lr5.startWidth = .005f;
        lr5.endWidth = .005f;
        lr5.material = laser;



        rb = sphere.GetComponent<Rigidbody>();
        rb.drag = 1;

        //GameObject.Instantiate(sphere);
    }

    private void OnDestroy()
    {
        Destroy(sphere);
    }

    // Update is called once per frame
    void Update()
    {
        normal = palm.transform.TransformDirection(Vector3.forward);

        //tracking all of the distances between palm and finger tips
        trackFingerDistances();

        //track palm
        trackPalm();

        //track the fingers
        trackFingers();

        //track the joint spheres
        trackJointSpheres();

        //decide if user is making fist
        if (isFist())
        {
            sphere.SetActive(true);
            sphere.transform.SetParent(palm.transform);
            sphere.transform.position = palmPos;
            sphere.transform.rotation = palmRotation;
            trail.GetComponent<TrailRenderer>().time = 0;
        }
        else
        {
            Rigidbody palmRB = palm.GetComponent<Rigidbody>();
            var localVelocity = transform.InverseTransformDirection(palmRB.velocity);
            sphere.transform.SetParent(null);
            rb.useGravity = true;
            rb.velocity = normal;
            rb.AddForce(normal * 2);
            //rb.angularVelocity = palmRB.angularVelocity;
            trail.GetComponent<TrailRenderer>().time = 5;
        }
    }


    bool isFist()
    {
        if (thumbDist < epsilon && indexDist < epsilon && middleDist < epsilon && ringDist < epsilon && pinkyDist < epsilon)
        {
            Debug.Log("fist");
            return true;
        }
        else
        {
            Debug.Log("NO fist");
            return false;
        }
       
        
    }

    void trackFingers()
    {
        for (int i = 0; i < 5; i++)
        {
            thumbLines[i] = new Vector3(thumbLineObjects[i].transform.position.x, thumbLineObjects[i].transform.position.y, thumbLineObjects[i].transform.position.z);
            indexLines[i] = new Vector3(indexLineObjects[i].transform.position.x, indexLineObjects[i].transform.position.y, indexLineObjects[i].transform.position.z);
            middleLines[i] = new Vector3(middleLineObjects[i].transform.position.x, middleLineObjects[i].transform.position.y, middleLineObjects[i].transform.position.z);
            ringLines[i] = new Vector3(ringLineObjects[i].transform.position.x, ringLineObjects[i].transform.position.y, ringLineObjects[i].transform.position.z);
            pinkyLines[i] = new Vector3(pinkyLineObjects[i].transform.position.x, pinkyLineObjects[i].transform.position.y, pinkyLineObjects[i].transform.position.z);
        }

        //line hand render
        lr1.SetPositions(thumbLines);
        lr2.SetPositions(indexLines);
        lr3.SetPositions(middleLines);
        lr4.SetPositions(ringLines);
        lr5.SetPositions(pinkyLines);
    }

    void trackFingerDistances()
    {
        thumbDist = Vector3.Distance(thumbTip.transform.position, palm.transform.position);
        indexDist = Vector3.Distance(indexTip.transform.position, palm.transform.position);
        middleDist = Vector3.Distance(middleTip.transform.position, palm.transform.position);
        ringDist = Vector3.Distance(ringTip.transform.position, palm.transform.position);
        pinkyDist = Vector3.Distance(pinkyTip.transform.position, palm.transform.position);
    }

    void trackPalm()
    {
        palmXpos = palm.transform.localPosition.x;
        palmYpos = palm.transform.localPosition.y;
        palmZpos = palm.transform.localPosition.z;
        palmXrotation = palm.transform.localRotation.x;
        palmYrotation = palm.transform.localRotation.y;
        palmZrotation = palm.transform.localRotation.z;

        palmPos = new Vector3(palmXpos, palmYpos, palmZpos);
        palmRotation = Quaternion.Euler(palmXrotation, palmYrotation, palmZrotation);
    }

    void makeJointSpheres()
    {
        for (int i = 0; i < 5; i++)
        {
            //skip the 
            /*
            if(i == 3)
            {
                continue;
            }
            */

            thumbLineSphereObjects[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            thumbLineSphereObjects[i].transform.localScale = new Vector3(.01f, .01f, .01f);
            thumbLineSphereObjects[i].GetComponent<Renderer>().material = laser3;

            indexLineSphereObjects[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            indexLineSphereObjects[i].transform.localScale = new Vector3(.01f, .01f, .01f);
            indexLineSphereObjects[i].GetComponent<Renderer>().material = laser3;


            middleLineSphereObjects[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            middleLineSphereObjects[i].transform.localScale = new Vector3(.01f, .01f, .01f);
            middleLineSphereObjects[i].GetComponent<Renderer>().material = laser3;

            ringLineSphereObjects[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            ringLineSphereObjects[i].transform.localScale = new Vector3(.01f, .01f, .01f);
            ringLineSphereObjects[i].GetComponent<Renderer>().material = laser3;

            pinkyLineSphereObjects[i] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            pinkyLineSphereObjects[i].transform.localScale = new Vector3(.01f, .01f, .01f);
            pinkyLineSphereObjects[i].GetComponent<Renderer>().material = laser3;

        }
    }

    void trackJointSpheres()
    {
        for (int i = 0; i < 5; i++)
        {
            /*
            if(i == 3)
            {
                continue;
            }
            */

            thumbLineSphereObjects[i].transform.position = new Vector3(thumbLineObjects[i].transform.position.x, thumbLineObjects[i].transform.position.y, thumbLineObjects[i].transform.position.z);
            indexLineSphereObjects[i].transform.position = new Vector3(indexLineObjects[i].transform.position.x, indexLineObjects[i].transform.position.y, indexLineObjects[i].transform.position.z);
            middleLineSphereObjects[i].transform.position = new Vector3(middleLineObjects[i].transform.position.x, middleLineObjects[i].transform.position.y, middleLineObjects[i].transform.position.z);
            ringLineSphereObjects[i].transform.position = new Vector3(ringLineObjects[i].transform.position.x, ringLineObjects[i].transform.position.y, ringLineObjects[i].transform.position.z);
            pinkyLineSphereObjects[i].transform.position = new Vector3(pinkyLineObjects[i].transform.position.x, pinkyLineObjects[i].transform.position.y, pinkyLineObjects[i].transform.position.z);
        }
    }
}
