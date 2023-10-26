using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Gamemanger : MonoBehaviour
{
    public int totalItemCount;
    public int level;
    public Text stageCount;
    public Text playerCount;

    void Awake()
    {
        stageCount.text = "/ " + totalItemCount; 
    }

    public void GetItem(int count)
    {
        playerCount.text = count.ToString();


    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Restart: Level" + level);
            SceneManager.LoadScene("Level"+level);
        }
    }
}
