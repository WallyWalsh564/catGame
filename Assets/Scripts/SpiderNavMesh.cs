using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class SpiderNavMesh : MonoBehaviour
{
    private enum EnemyState { IDLE, CHASE };
    private EnemyState state;

    [SerializeField] private CharacterController target;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private AnimationsController anims;
    [SerializeField] private ParticleSystem blood;
    private bool damageCoolDown = false;
    private bool isDead = false;
    private float distanceToTarget = float.MaxValue;
    private float chaseRange = 40f;
    // Start is called before the first frame update
    void Start()
    {
        SetState(EnemyState.IDLE);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            distanceToTarget = Vector3.Distance(transform.position, target.transform.position);
            //        agent.SetDestination(target.transform.position);
            switch (state)
            {
                case EnemyState.IDLE: Update_Idle(); break;
                case EnemyState.CHASE: Update_Chase(); break;
            }
        }
    }
    void SetState(EnemyState newState)
    {
        state = newState;
    }
    void Update_Idle()
    {
        agent.isStopped = true;
        if (distanceToTarget <= chaseRange)
        {
            SetState(EnemyState.CHASE);
        }
    }
    void Update_Chase()
    {
        agent.isStopped = false;
        agent.SetDestination(target.transform.position);
        if (distanceToTarget > chaseRange)
        {
            SetState(EnemyState.IDLE);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        PlayerManager player = other.GetComponent<PlayerManager>();
        if (player != null)
        {
            Debug.Log("ots");
            if (player.IsAttacking() == true)
            {
               
                isDead = true;
                SetState(EnemyState.IDLE);
                anims.SetDead();
                StartCoroutine(DeathExplostion());
            }
            else
            {
                StartCoroutine(hurtPlayer());
            }

        }
    }

    IEnumerator hurtPlayer()
    {
        if (!damageCoolDown)
        {
            Messenger.Broadcast(GameEvent.PLAYER_HURT);
            damageCoolDown = true;
            yield return new WaitForSeconds(1);

            damageCoolDown = false;
        }
    }
    IEnumerator DeathExplostion()
    {
        
        Instantiate(blood, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
        Messenger.Broadcast(GameEvent.ENEMY_DEAD);
    }
}
