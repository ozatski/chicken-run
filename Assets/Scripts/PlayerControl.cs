using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody rb;
    public float jumpPower;
    public int itemCount;
    public DoorToggle doorToogle;
    public GameObject door;
    public float speed;
    public float rotationSpeed;
    bool isJump;
    private Animator animator;
    private CharacterController playerController;
    public Gamemanger manager;
    int finalLevel = 3;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        door = GameObject.Find("ExitDoor");
        if (door)
        {
            doorToogle = door.GetComponent<DoorToggle>();
        }

        isJump = false;
        itemCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        float magnitude = Mathf.Clamp01(movementDirection.magnitude) * speed;

        movementDirection.Normalize();

        playerController.SimpleMove(movementDirection * magnitude);

        if(movementDirection != Vector3.zero)
        {
            animator.SetBool("IsMoving", true);
            Debug.Log("Trying to rotate");
            Quaternion toRotation = Quaternion.LookRotation(movementDirection, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("IsMoving", false);
        }


        if (Input.GetButtonDown("Jump") && !isJump)
        {
            isJump = true;
            rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
        }


    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJump = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
            itemCount++;
            other.gameObject.SetActive(false); //inactivate item
            manager.GetItem(itemCount);

            if (door && manager.totalItemCount == itemCount)
            {
                doorToogle.getDoorController().ToggleDoor();
            }
        }
        else if (other.tag == "Final")
        {
            Debug.Log(manager.totalItemCount);
            Debug.Log(itemCount);
            if ((manager.totalItemCount == itemCount) && (manager.level < finalLevel))
            {
                //Game Clear!
                SceneManager.LoadScene("Level" + (manager.level + 1));
            }
            else
            {
                //Restart
                SceneManager.LoadScene("Level" + manager.level);
            }

        }

    }
}
