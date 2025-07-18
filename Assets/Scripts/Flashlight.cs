using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Colors colors;
    public GameObject redCam;
    public GameObject magentaCam;
    public GameObject blueCam;
    public enum Colors
    {
        Red,
        Pink,
        Blue,
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (colors)
        {
            case Colors.Red:
                redCam.SetActive(true);
                magentaCam.SetActive(false);
                blueCam.SetActive(false);
                break;
            case Colors.Blue:
                redCam.SetActive(false);
                magentaCam.SetActive(false);
                blueCam.SetActive(true);
                break;
            case Colors.Pink:
                redCam.SetActive(false);
                magentaCam.SetActive(true);
                blueCam.SetActive(false);
                break;
            default:
                break;
        }
    }
}
