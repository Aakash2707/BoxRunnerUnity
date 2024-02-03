using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    
    public GameObject obstacle;
    public Transform spawnPoint;
    public int score = 0;
    
    public TextMeshProUGUI scoreText;
    public GameObject player;

    public static GameManager instance;
    public int coins;
    private int currentPlayerIndex2;
    
    private IEnumerator obstacleCoroutine;

    private void Awake()
    {
        instance= this;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameStart();
        PlayerData lodedData = SaveSystem.loadPlayer();
        coins = lodedData.score;
        currentPlayerIndex2= lodedData.playerIndex;

    }

    IEnumerator SpawnObstacle()
    {
        while(true)
        {
            float waitTime = Random.Range(0.5f,2.5f);
            yield return new WaitForSeconds(waitTime);
            Instantiate(obstacle,spawnPoint.position, Quaternion.identity);
        }
    }
    public void StartSpawningObstacles()
    {
        obstacleCoroutine = SpawnObstacle();
        StartCoroutine(obstacleCoroutine);
    }

// Stop the obstacle spawning coroutine
    public void StopSpawningObstacles()
    {
        StopCoroutine(obstacleCoroutine);
    }
    void ScoreUp(){
        score++;
        scoreText.text = score.ToString();
    }
    public void TotalScore(){
        coins = coins + score;
        PlayerData playerData = new PlayerData();
        playerData.score = coins;
        playerData.playerIndex = currentPlayerIndex2;
        SaveSystem.SavePlayer(playerData);
    }
    public void GameStart()
    {
        player.SetActive(true);
        StartSpawningObstacles();
        InvokeRepeating("ScoreUp",2f,1f);
    }
}
