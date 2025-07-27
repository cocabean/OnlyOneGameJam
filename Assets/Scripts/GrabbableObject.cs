using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GrabbableObject : MonoBehaviour
{
    public GameObject HoldingSpot;
    Text interactText;

    public bool holding = false;

    public Colors color;
    public Flashlight flashlight;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HoldingSpot = GameObject.Find("HoldingSpot");
        interactText = GameObject.Find("InteractMessage").GetComponent<Text>();
        flashlight = GameObject.FindWithTag("Player").GetComponentInChildren<Flashlight>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.E) && holding)
        {
            StartCoroutine("dropObject");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && !holding && flashlight.colors == color)
        {
            interactText.text = "hold [E] to carry";
            if (Input.GetKey(KeyCode.E) && flashlight.colors == color)
            {

                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.GetComponent<Rigidbody>().MovePosition(HoldingSpot.transform.position);
                GameObject.FindWithTag("Player").GetComponent<PlayerController>().holdingObject = true;
                interactText.text = "";
                //transform.SetParent(HoldingSpot.transform);

            }
            else
            {
                GameObject.FindWithTag("Player").GetComponent<PlayerController>().holdingObject = false;
                gameObject.GetComponent<Rigidbody>().isKinematic = false;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText.text = "";
        }
    }

    public IEnumerator dropObject()
    {
       // transform.SetParent(null);
        gameObject.GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(.1f);
        holding = false;
        
    }
}
