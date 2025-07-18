using JetBrains.Annotations;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Colors colors;
    public GameObject redCam;
    public GameObject magentaCam;
    public GameObject blueCam;

    [Header("Colors")]
    public Light light;

    public Color red;
    public Color magenta;
    public Color blue;
    public enum Colors
    {
        Red,
        Magenta,
        Blue,
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        light.color = red;
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
            case Colors.Magenta:
                redCam.SetActive(false);
                magentaCam.SetActive(true);
                blueCam.SetActive(false);
                break;
            default:
                break;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            setRed();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            setMagenta();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            setBlue();
        }
    }
    public void setRed()
    {
        colors = Colors.Red;
        light.color = red;
    }

    public void setMagenta()
    {
        colors = Colors.Magenta;
        light.color = magenta;
    }

    public void setBlue()
    {
        colors = Colors.Blue;
        light.color = blue;
    }

}
