using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ShopManager : MonoBehaviour
{
    
    //Text That shows Coins Value 
    public TextMeshProUGUI totalScoreText;

    public int currentPlayerIndex = 0;
    public GameObject[] playerModels;
    
    public float rotationSpeed = 50;
    
    public PlayerBlueprint[] players;
    public Button buyButton;
    
    PlayerData lodedData;
    int points;
    
    void Awake(){
        lodedData = SaveSystem.loadPlayer(); 
        points = lodedData.score;
    }
     
   
    // Start is called before the first frame update
    void Start()
    {
        //GetPoints Value and update text
        totalScoreText.text = points.ToString();
        
        foreach(PlayerBlueprint player in players){
            if(player.price == 0){
                player.isUnlocked = true;
            }
            else{
                player.isUnlocked = PlayerPrefs.GetInt(player.name,0) == 0 ? false:true;
            }
        }
        currentPlayerIndex=lodedData.playerIndex;
        foreach(GameObject player in playerModels)
        {
            player.SetActive(false);
        }
        playerModels[currentPlayerIndex].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,rotationSpeed*Time.deltaTime,0);
        UpdateUI();
    }
    public void ChangeNext(){
        playerModels[currentPlayerIndex].SetActive(false); 
        currentPlayerIndex++;
        if(currentPlayerIndex == playerModels.Length)
        {
            currentPlayerIndex = 0;
        }
        playerModels[currentPlayerIndex].SetActive(true);
        PlayerBlueprint p =players[currentPlayerIndex];
        if(!p.isUnlocked)
            return;
        PlayerData playerData = new PlayerData();
        playerData.score=points;
        playerData.playerIndex = currentPlayerIndex;
        SaveSystem.SavePlayer(playerData);  
    }

    public void ChangePrevious(){
        playerModels[currentPlayerIndex].SetActive(false); 
        currentPlayerIndex--;
        if(currentPlayerIndex == -1)
        {
            currentPlayerIndex = playerModels.Length -1;
        }
        playerModels[currentPlayerIndex].SetActive(true);
        PlayerBlueprint p =players[currentPlayerIndex];
        if(!p.isUnlocked)
            return;
        PlayerData playerData = new PlayerData();
        playerData.score=points;
        playerData.playerIndex = currentPlayerIndex;
        SaveSystem.SavePlayer(playerData);  
    }

    public void BackButton(){
        SceneManager.LoadScene(0);
    }
    public void UnlockePlayer(){
        PlayerBlueprint p =players[currentPlayerIndex];
        PlayerPrefs.SetInt(p.name,1);
        p.isUnlocked = true;
        
        PlayerData playerData = new PlayerData();
        playerData.score = points - p.price;
        playerData.playerIndex = currentPlayerIndex;
        SaveSystem.SavePlayer(playerData);
        
        lodedData = SaveSystem.loadPlayer(); 
        points = lodedData.score;
        totalScoreText.text = points.ToString();
    }
    private void UpdateUI(){
        
        PlayerBlueprint p =players[currentPlayerIndex];
        
        if(p.isUnlocked){
            buyButton.gameObject.SetActive(false);
        }
        
        else {
            buyButton.gameObject.SetActive(true);
            buyButton.GetComponentInChildren<TextMeshProUGUI>().text = "BUY -"+ p.price;

            if(p.price< points){
                buyButton.interactable = true;
            }
            else{
                buyButton.interactable = false;
            }
        }
    }

}
