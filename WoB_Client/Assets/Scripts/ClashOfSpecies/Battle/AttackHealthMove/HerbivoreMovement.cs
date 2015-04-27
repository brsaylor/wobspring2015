using UnityEngine;
using System.Collections;

public class HerbivoreMovement : MonoBehaviour {

	Animator anim;  
	GameObject[] bushes;
	GameObject bush;               // Reference to the player's position.
	Health myHealth;      // Reference to the player's health.
	//EnemyHealth enemyHealth;        // Reference to this enemy's health.
	NavMeshAgent nav;               // Reference to the nav mesh agent.
	
	bool isbush = false;
	void Awake ()
	{
		// Set up the references.
		bushes = GameObject.FindGameObjectsWithTag ("Plant");
		if (bushes != null)
						isbush = true;
		myHealth = this.gameObject.GetComponent <Health> ();

		int randomIndex = Random.Range(0, 3);
		nav = GetComponent <NavMeshAgent> ();
		anim = GetComponent <Animator> ();
		bush = bushes [randomIndex];
	}
	
	
	void Update ()
	{
		if( myHealth.currentHealth > 0 && isbush && nav.enabled)
		{
			// ... set the destination of the nav mesh agent to the player.
			nav.SetDestination (bush.transform.position);
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