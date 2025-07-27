using UnityEngine;
using UnityEngine.UI;

public class PressurePlate : MonoBehaviour
{
    public bool pressed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("InteractObject"))
        {
            pressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player") || other.CompareTag("InteractObject"))
        {
            pressed = false;
        }
    }
}
