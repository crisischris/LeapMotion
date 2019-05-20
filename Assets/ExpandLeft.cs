using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandLeft : MonoBehaviour
{


    public bool isLeft;
    public bool isRight;

    public GameObject hand;

    private GameObject thumbTip;
    private GameObject indexTip;
    private GameObject middleTip;
    private GameObject ringTip;
    private GameObject pinkyTip;

    private GameObject palm;
    private GameObject wrist;


    public float thumbDist;
    public float indexDist;
    public float middleDist;
    public float ringDist;
    public float pinkyDist;

    private const double epsilon = .075;

    private GameObject sphere;

    private LineRenderer lr1;
    private LineRenderer lr2;
    private LineRenderer lr3;
    private LineRenderer lr4;
    private LineRenderer lr5;

    public GameObject[] thumbLineObjects;
    public GameObject[] indexLineObjects;
    public GameObject[] middleLineObjects;
    public GameObject[] ringLineObjects;
    public GameObject[] pinkyLineObjects;

    public Vector3[] thumbLines;
    public Vector3[] indexLines;
    public Vector3[] middleLines;
    public Vector3[] ringLines;
    public Vector3[] pinkyLines;

    float palmXpos;
    float palmYpos;
    float palmZpos;
    float palmXrotation;
    float palmYrotation;
    float palmZrotation;
    Vector3 palmPos;
    Quaternion palmRotation;

    private Color whiteColor = new Color(255, 255, 255);

    // Start is called before the first frame update
    void Start()
    {




        thumbLines = new Vector3[5];
        indexLines = new Vector3[5];
        middleLines = new Vector3[5];
        ringLines = new Vector3[5];
        pinkyLines = new Vector3[5];

        thumbLineObjects = new GameObject[5];
        indexLineObjects = new GameObject[5];
        middleLineObjects = new GameObject[5];
        ringLineObjects = new GameObject[5];
        pinkyLineObjects = new GameObject[5];

        if (isLeft)
        {
            palm = GameObject.Find("Palm");
            wrist = GameObject.Find("Wrist");

            thumbTip = GameObject.Find("ThumbTip");
            indexTip = GameObject.Find("IndexTip");
            middleTip = GameObject.Find("MiddleTip");
            ringTip = GameObject.Find("RingTip");
            pinkyTip = GameObject.Find("PinkyTip");

            //set up thumb array
            thumbLineObjects[0] = palm;
            thumbLineObjects[1] = wrist;
            thumbLineObjects[2] = GameObject.Find("ThumbProximalJoint");
            thumbLineObjects[3] = GameObject.Find("ThumbDistalJoint");
            thumbLineObjects[4] = thumbTip;

            //set up index array
            indexLineObjects[0] = palm;
            indexLineObjects[1] = GameObject.Find("IndexKnuckle");
            indexLineObjects[2] = GameObject.Find("IndexMiddleJoint");
            indexLineObjects[3] = GameObject.Find("IndexDistalJoint");
            indexLineObjects[4] = indexTip;

            //set up middle array
            middleLineObjects[0] = palm;
            middleLineObjects[1] = GameObject.Find("MiddleKnuckle");
            middleLineObjects[2] = GameObject.Find("MiddleMiddleJoint");
            middleLineObjects[3] = GameObject.Find("MiddleDistalJoint");
            middleLineObjects[4] = middleTip;

            //set up ring array
            ringLineObjects[0] = palm;
            ringLineObjects[1] = GameObject.Find("RingKnuckle");
            ringLineObjects[2] = GameObject.Find("RingMiddleJoint");
            ringLineObjects[3] = GameObject.Find("RingDistalJoint");
            ringLineObjects[4] = ringTip;

            //set up pinky array
            pinkyLineObjects[0] = palm;
            pinkyLineObjects[1] = GameObject.Find("PinkyKnuckle");
            pinkyLineObjects[2] = GameObject.Find("PinkyMiddleJoint");
            pinkyLineObjects[3] = GameObject.Find("PinkyDistalJoint");
            pinkyLineObjects[4] = pinkyTip;
        }
        else
        {
            if (GameObject.FindGameObjectWithTag("Right") == true)
                {
                wrist = GameObject.Find("Wrist");


                thumbTip = GameObject.Find("ThumbTip");
                indexTip = GameObject.Find("IndexTip");
                middleTip = GameObject.Find("MiddleTip");
                ringTip = GameObject.Find("RingTip");
                pinkyTip = GameObject.Find("PinkyTip");

                //set up thumb array
                thumbLineObjects[0] = palm;
                thumbLineObjects[1] = wrist;
                thumbLineObjects[2] = GameObject.Find("ThumbProximalJoint");
                thumbLineObjects[3] = GameObject.Find("ThumbDistalJoint");
                thumbLineObjects[4] = thumbTip;

                //set up index array
                indexLineObjects[0] = palm;
                indexLineObjects[1] = GameObject.Find("IndexKnuckle");
                indexLineObjects[2] = GameObject.Find("IndexMiddleJoint");
                indexLineObjects[3] = GameObject.Find("IndexDistalJoint");
                indexLineObjects[4] = indexTip;

                //set up middle array
                middleLineObjects[0] = palm;
                middleLineObjects[1] = GameObject.Find("MiddleKnuckle");
                middleLineObjects[2] = GameObject.Find("MiddleMiddleJoint");
                middleLineObjects[3] = GameObject.Find("MiddleDistalJoint");
                middleLineObjects[4] = middleTip;

                //set up ring array
                ringLineObjects[0] = palm;
                ringLineObjects[1] = GameObject.Find("RingKnuckle");
                ringLineObjects[2] = GameObject.Find("RingMiddleJoint");
                ringLineObjects[3] = GameObject.Find("RingDistalJoint");
                ringLineObjects[4] = ringTip;

                //set up pinky array
                pinkyLineObjects[0] = palm;
                pinkyLineObjects[1] = GameObject.Find("PinkyKnuckle");
                pinkyLineObjects[2] = GameObject.Find("PinkyMiddleJoint");
                pinkyLineObjects[3] = GameObject.Find("PinkyDistalJoint");
                pinkyLineObjects[4] = pinkyTip;
            }

        }
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.SetActive(false);
        sphere.transform.localScale = new Vector3(.1f, .1f, .1f);
        sphere.AddComponent<SphereCollider>();
        sphere.AddComponent<Rigidbody>();

        lr1 = thumbTip.AddComponent<LineRenderer>();
        lr1.positionCount = thumbLineObjects.Length;
        lr1.startWidth = .005f;
        lr1.endWidth = .005f;
        lr1.SetColors(whiteColor, whiteColor);

        lr2 = indexTip.AddComponent<LineRenderer>();
        lr2.positionCount = indexLineObjects.Length;
        lr2.startWidth = .005f;
        lr2.endWidth = .005f;
        lr2.SetColors(whiteColor, whiteColor);

        lr3 = middleTip.AddComponent<LineRenderer>();
        lr3.positionCount = middleLineObjects.Length;
        lr3.startWidth = .005f;
        lr3.endWidth = .005f;
        lr3.SetColors(whiteColor, whiteColor);

        lr4 = ringTip.AddComponent<LineRenderer>();
        lr4.positionCount = ringLineObjects.Length;
        lr4.startWidth = .005f;
        lr4.endWidth = .005f;
        lr4.SetColors(whiteColor, whiteColor);

        lr5 = pinkyTip.AddComponent<LineRenderer>();
        lr5.positionCount = pinkyLineObjects.Length;
        lr5.startWidth = .005f;
        lr5.endWidth = .005f;
        lr5.SetColors(whiteColor, whiteColor);



        Rigidbody RB = sphere.GetComponent<Rigidbody>();
        RB.drag = 3;

        //GameObject.Instantiate(sphere);
    }

    private void OnDestroy()
    {
        Destroy(sphere);
    }

    // Update is called once per frame
    void Update()
    {
        //tracking all of the distances between palm and finger tips
        trackFingerDistances();

        //track palm
        trackPalm();

        //track the fingers
        trackFingers();

        //decide if user is making fist
        if (isFist())
        {
            sphere.SetActive(true);
            sphere.transform.SetParent(palm.transform);
            sphere.transform.position = palmPos;
            sphere.transform.rotation = palmRotation;
        }
        else
        {
            //sphere.SetActive(false);
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
}
