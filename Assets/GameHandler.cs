using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    private GameObject[] capsules;

    // Start is called before the first frame update
    void Start()
    {
        capsules = GameObject.FindGameObjectsWithTag("Capsule");
        foreach (GameObject capsule in capsules)
        {
            capsule.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
