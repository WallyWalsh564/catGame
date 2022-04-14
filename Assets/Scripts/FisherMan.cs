using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FisherMan : MonoBehaviour
{
    private void Awake()
    {
        Messenger.AddListener(GameEvent.FISHER_DIED, OnFisherDied);
        
    }
    private void OnDestroy()
    {
        Messenger.Broadcast(GameEvent.FISHER_DIED);
        Messenger.RemoveListener(GameEvent.FISHER_DIED, OnFisherDied);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnFisherDied()
    {
        Debug.Log("FISHER DIED");
    }
}
