using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameSession : MonoBehaviour
{

   [Range(0.1f,10f)] [SerializeField] private float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed;
    [SerializeField] TextMeshProUGUI pointsText;
    [SerializeField] bool isAutoPlayEnabled = false;


    //state variable
    [SerializeField] int currentScore=0;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }    
    }
    private void Start()
    {
        pointsText.text = currentScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
    }

    public void AddToScore()
    {
        pointsPerBlockDestroyed = Random.Range(10,1001);
        currentScore += pointsPerBlockDestroyed;
        pointsText.text = currentScore.ToString();
    }

    public void ResetScore()
    {
        currentScore = 0;
        pointsText.text = currentScore.ToString();
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }
}

