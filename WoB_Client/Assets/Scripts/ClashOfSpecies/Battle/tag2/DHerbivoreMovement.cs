using UnityEngine;
using System.Collections;

public class DHerbivoreMovement : MonoBehaviour {

	Animator anim;  
	GameObject[] Dbushes;
	GameObject Dbush;               // Reference to the player's position.
	Health myHealth;      // Reference to the player's health.
	//EnemyHealth enemyHealth;        // Reference to this enemy's health.
	NavMeshAgent nav;               // Reference to the nav mesh agent.
	
	bool isDbush = false;
	void Awake ()
	{
		// Set up the references.
		Dbushes = GameObject.FindGameObjectsWithTag ("DPlant");
		if (Dbushes != null)
			isDbush = true;
		myHealth = this.gameObject.GetComponent <Health> ();
		
		int randomIndex = Random.Range(0, 3);
		nav = GetComponent <NavMeshAgent> ();
		anim = GetComponent <Animator> ();
		Dbush = Dbushes [randomIndex];
	}
	
	
	void Update ()
	{
		if( myHealth.currentHealth > 0 && isDbush && nav.enabled)
		{
			// ... set the destination of the nav mesh agent to the player.
			nav.SetDestination (Dbush.transform.position);
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