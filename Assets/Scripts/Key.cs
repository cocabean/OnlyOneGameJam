using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    public Inventory inventory;
    public Flashlight flashlight;

    public Colors color;
    public Text interactText;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        flashlight = GameObject.FindWithTag("Player").GetComponentInChildren<Flashlight>();
        interactText = GameObject.Find("InteractMessage").GetComponent<Text>();
        interactText.text = "";
        Debug.Log(GameObject.Find("InteractMessage").name);
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            interactText.text = "press [E] to pick up";
            if (Input.GetKey(KeyCode.E) && color == flashlight.colors)
            {
                interactText.text = "";
                inventory.itemInHand = gameObject;
                gameObject.SetActive(false);
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
