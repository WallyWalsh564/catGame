using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIController : MonoBehaviour
{
    private int enemiesRemaining = 25;
    [SerializeField] private TextMeshProUGUI scoreValue;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject YouWin;
    // Start is called before the first frame update


    private void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_DEAD, OnEnemyDead);
        Messenger.AddListener(GameEvent.PLAYER_HURT, OnPlayerHurt);
        Messenger.AddListener(GameEvent.PLAYER_DEAD, OnPlayerDead);

    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.PLAYER_DEAD, OnPlayerDead);
        Messenger.RemoveListener(GameEvent.ENEMY_DEAD, OnEnemyDead);
        Messenger.RemoveListener(GameEvent.PLAYER_HURT, OnPlayerHurt);
    }
    void Start()
    {
        updateScore(enemiesRemaining);
        healthBar.fillAmount = 1;
        healthBar.color = Color.green;
    }

    void OnPlayerHurt()
    {
        healthBar.fillAmount -= .20f;
        updateHealth(healthBar.fillAmount);

        if (healthBar.fillAmount <= 0.1)
        {
            Messenger.Broadcast(GameEvent.PLAYER_DEAD);
            gameOver.SetActive(true);
        }
    }

    void updateHealth(float healthPercentage)
    {
        healthBar.color = Color.Lerp(Color.red, Color.green, healthPercentage);
    }

    void OnEnemyDead()
    {
        enemiesRemaining = enemiesRemaining - 1;
        updateScore(enemiesRemaining);
    }
    public void updateScore(int enemiesRemaining)
    {
        scoreValue.text = enemiesRemaining.ToString();
        if (enemiesRemaining == 0)
        {
           // Messenger.Broadcast(GameEvent.PLAYER_WINS);
            YouWin.SetActive(true);
        }
        
    }

    public void OnPlayerDead()
    {
        gameOver.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
