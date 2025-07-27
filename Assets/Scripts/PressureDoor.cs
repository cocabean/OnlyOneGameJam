using UnityEngine;
using UnityEngine.UI;


public class PressureDoor : MonoBehaviour
{

    public Animator anim;
    public PressurePlate Plate;
    bool interactable = true;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(interactable && Plate.pressed)
        {
            anim.SetBool("ButtonOn", true);
        }
        else
        {
            anim.SetBool("ButtonOn", false);
        }
    }

}
