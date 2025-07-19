using UnityEngine;



public class Key : MonoBehaviour
{
    public Inventory inventory;
    public Flashlight flashlight;

    public Colors color;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        flashlight = GameObject.FindWithTag("Player").GetComponentInChildren<Flashlight>();
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E) && color == flashlight.colors)
            {
                inventory.itemInHand = gameObject;
                gameObject.SetActive(false);
            }
        }
    }
}
