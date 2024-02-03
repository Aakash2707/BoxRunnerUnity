using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float jumpForce;
    bool canJump;

    public GameManager gm;
    private int currentPlayerIndex1;
    public GameObject[] players;

    public Button mainMenuButton;
    public Button adButton;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        mainMenuButton.gameObject.SetActive(false);
        adButton.gameObject.SetActive(false);

        PlayerData lodedData = SaveSystem.loadPlayer();
        currentPlayerIndex1=lodedData.playerIndex; 
        foreach(GameObject player in players)
        {
            player.SetActive(false);
        }
        players[currentPlayerIndex1].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && canJump){
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            FindObjectOfType<AudioManager>().PlaySound("Jump");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.tag == "Ground"){
        canJump = true;
       } 
    }
    private void OnCollisionExit(Collision collision)
    {
       if(collision.gameObject.tag == "Ground"){
        canJump = false;
       }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Obstacle")
        {
            gm.TotalScore();
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
            FindObjectOfType<AudioManager>().PauseSound("MainTheme");
            mainMenuButton.gameObject.SetActive(true);
            adButton.gameObject.SetActive(true);
            Time.timeScale = 0;
            
        }
    }
    public void Back(){
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
        
    }
    public void WatchAd(){
        Time.timeScale = 1;
        mainMenuButton.gameObject.SetActive(false);
        adButton.gameObject.SetActive(false);
    }

}
