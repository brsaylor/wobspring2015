using UnityEngine;
using System.Collections;

public class HerbivoreMovement : MonoBehaviour {

	Animator anim;    
	GameObject player;               // Reference to the player's position.
	Health playerHealth;      // Reference to the player's health.
	//EnemyHealth enemyHealth;        // Reference to this enemy's health.
	NavMeshAgent nav;               // Reference to the nav mesh agent.
	
	
	void Awake ()
	{
		// Set up the references.
		player = GameObject.FindGameObjectWithTag ("Plant");
		playerHealth = player.GetComponent <Health> ();
		//enemyHealth = GetComponent <EnemyHealth> ();
		nav = GetComponent <NavMeshAgent> ();
		anim = GetComponent <Animator> ();
	}
	
	
	void Update ()
	{
		// If the enemy and the player have health left...
		if(/*enemyHealth.currentHealth > 0 &&*/ playerHealth.currentHealth > 0)
		{
			// ... set the destination of the nav mesh agent to the player.
			nav.SetDestination (player.transform.position);
			anim.SetTrigger("Walking");
		}
		// Otherwise...
		else
		{
			// ... disable the nav mesh agent.
			nav.enabled = false;
		}
	}
}