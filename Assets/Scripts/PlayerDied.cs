using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerDied : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HUDText;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.PLAYER_DEAD, OnPlayerDead);
        Messenger.AddListener(GameEvent.PLAYER_WINS, OnPlayerWins);
    }
    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.PLAYER_DEAD, OnPlayerDead);
        Messenger.RemoveListener(GameEvent.PLAYER_WINS, OnPlayerWins);
    }

    public void OnPlayerDead()
    {
        HUDText.text = "You are Dead";
    }

    public void OnPlayerWins()
    {
        HUDText.text = "Congrats You Win";
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
