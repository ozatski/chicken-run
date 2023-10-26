using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    private Rigidbody rb;
    public float jumpPower;
    public int itemCount;
    public Gamemanger manager;

    bool isJump;
    AudioSource audio;
    int finalLevel = 3;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("The Game is ready!");
        rb = GetComponent<Rigidbody>();
        isJump = false;
        itemCount = 0;
    }

    // Update is called once per frame
    void Update()
    {

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
            audio.Play(); //playback sound 
            other.gameObject.SetActive(false); //inactivate item
            manager.GetItem(itemCount);
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

    void FixedUpdate()
    {

        float movementX = Input.GetAxisRaw("Horizontal"); //GetAxisRaw : 0, 1 ,-1
        float movementY = Input.GetAxisRaw("Vertical");

        rb.AddForce(new Vector3(movementX, 0.0f, movementY), ForceMode.Impulse);
    }

    //this function is first called when creating a game object
    void Awake()
    {
            //Debug.Log("The Game is ready!");
            audio = GetComponent<AudioSource>();
    }

}
