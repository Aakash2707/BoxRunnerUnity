using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI totalScoreText; 
    public GameObject playButton;
    
    
    // Start is called before the first frame update
    void Start()
    {
        PlayerData lodedData = SaveSystem.loadPlayer();
        int points= lodedData.score;
        
        totalScoreText.text = points.ToString();
        Debug.Log(points);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ShopOpen(){
        SceneManager.LoadScene(2);
    }
    public void GameStart(){
        SceneManager.LoadScene(1);
    }    
    public void InvitePeople(){
        SceneManager.LoadScene(3);
    }
    public void QuitGame(){
        Application.Quit();
    }
}
