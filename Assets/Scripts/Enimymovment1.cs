using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enimymovment1 : MonoBehaviour
{

    public LayerMask EnemyLayers;

    public float AttackRange = 0.5f;
    public int AttackDamage = 10;

    public static Enimymovment1 instance;
    public float lockRadius=10f;
    public float currentspeed;
    public float Maxspeed=5f;
  
    public Transform target;
    NavMeshAgent agent;

    CharacterController Enemy;
    float gravity = -12f;
  
    float velocityY;
    
    //float attackTime = 0.5f;
 
    public float attackrate=2f;
    

    // Start is called before the first frame update
    void Start()
    {
    
        agent = GetComponent<NavMeshAgent>();
        Enemy = GetComponent<CharacterController>();
      
        
    }

    // Update is called once per frame
    void Update()
    {
        float Distance = Vector3.Distance(transform.position, target.position);
        if(Distance<=lockRadius)
        {
           
            agent.SetDestination(target.position);
            
             //  animator.SetFloat("Move", currentspeed/Maxspeed);

                currentspeed += Time.deltaTime;
                currentspeed = Mathf.Clamp(currentspeed, 0, 5);
            
          
        }
        Gravity();
    }
   
    void Gravity()
    {
        velocityY += Time.deltaTime * gravity;
        Enemy.Move(Vector3.up * velocityY * Time.deltaTime);
        if (Enemy.isGrounded)
        {
            velocityY = 0;
        }
    }
    
    void Facetarger()
    {
        Vector3 Direction = (target.position - transform.position).normalized;
        Quaternion LockRotation = Quaternion.LookRotation(new Vector3(Direction.x, 0, Direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation,LockRotation,Time.deltaTime*5f);
    }
   
}
