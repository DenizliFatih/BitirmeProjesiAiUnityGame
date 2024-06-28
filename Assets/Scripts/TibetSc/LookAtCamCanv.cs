using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCreator.Runtime.Cameras;

public class LookAtCamCanv : MonoBehaviour
{
    public ShotCamera shotCam;
    // Update is called once per frame
    private void Awake()
    {
        shotCam = Camera.main.transform.parent.GetChild(0).GetComponent<ShotCamera>();
    }
    void Update()
    {
        transform.LookAt((shotCam.transform.position));
    }
}
