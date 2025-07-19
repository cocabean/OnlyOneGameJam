using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyDoor : MonoBehaviour
{
    public GameObject key;
    public Inventory inventory;

    public Animator anim;
    public Text interactText;

    bool interactable = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        interactText = GameObject.Find("InteractMessage").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && interactable)
        {
            interactText.text = "press [E] to open";
            if (Input.GetKey(KeyCode.E) && inventory.itemInHand == key)
            {
                interactable = false;
                anim.SetTrigger("DoorOpen");
                interactText.text = "";
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
}
