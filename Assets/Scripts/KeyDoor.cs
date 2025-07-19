using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    public GameObject key;
    public Inventory inventory;

    public Animator anim;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKey(KeyCode.E) && inventory.itemInHand == key)
            {
                anim.SetTrigger("DoorOpen");
            }
        }
    }
}
