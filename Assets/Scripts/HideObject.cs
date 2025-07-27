using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class HideObject : MonoBehaviour
{
    GameObject Player;
    PlayerController playerController;
    public Flashlight flashlight;
    bool hiding = false;

    public Animator anim;

    public Colors color;
    Text interactText;
    public Volume volume;

    public Transform hidingPosition;
    public Transform leavingPosition;
   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        volume = GameObject.Find("Vignette").GetComponent<Volume>();
        Player = GameObject.FindWithTag("Player");
        flashlight = GameObject.FindWithTag("Player").GetComponentInChildren<Flashlight>();
        interactText = GameObject.Find("InteractMessage").GetComponent<Text>();
        playerController = GameObject.FindWithTag("Player").GetComponentInChildren<PlayerController>();
        anim = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && color == flashlight.colors)
        {
            interactText.text = "press [E] to hide";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && color == flashlight.colors)
        {
            interactText.text = "";
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && color == flashlight.colors && playerController.movementEnabled)
        { 
            if (Input.GetKey(KeyCode.E))
            {
                StartCoroutine("Hide");
            }
        }
        

        if (other.CompareTag("Player") && color == flashlight.colors && hiding)
        {
            if (Input.GetKey(KeyCode.E))
            {
                if (anim.GetCurrentAnimatorStateInfo(0).IsName("New State"))
                {
                    StartCoroutine("LeaveHiding");
                }  
            }
        }
    }

    public IEnumerator Hide()
    {
        anim.SetTrigger("Hide");
        playerController.movementEnabled = false;
        interactText.text = "";
        yield return new WaitForSeconds(.4f);
        Player.transform.position = hidingPosition.transform.position;
        Player.transform.rotation = hidingPosition.transform.rotation;
        hiding = true;
        playerController.hiding = true;
        interactText.text = "press [E] to leave";
    }

    public IEnumerator LeaveHiding()
    {
        anim.SetTrigger("Hide");
        interactText.text = "";
        yield return new WaitForSeconds(.4f);
        Player.transform.position = leavingPosition.transform.position;
        yield return new WaitForSeconds(.01f);
        playerController.movementEnabled = true;
        hiding = false;
        playerController.hiding = false;
    }
}