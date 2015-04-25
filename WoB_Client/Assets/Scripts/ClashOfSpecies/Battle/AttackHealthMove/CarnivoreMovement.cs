using UnityEngine;
using System.Collections;

public class CarnivoreMovement : MonoBehaviour {
	Animator anim;    

	NavMeshAgent nav;               // Reference to the nav mesh agent.
	
	GameObject[] herbivoreList;                          // List of herbivores
	GameObject herbivore;                          // List of herbivores
	
	GameObject[] omnivoreList;                          // List of Omnivores
	GameObject omnivore;                          // List of herbivores
	
	GameObject currentlyAttacking;
	Health currentEnemyHealth;                  // Reference to the player's health.	
	bool allEnemiesDead = false;
	
	void Awake ()
	{
		// Set up the references.
		//Debug.Log ("in awake");


		herbivoreList = GameObject.FindGameObjectsWithTag ("Herbivore");
		//omnivoreList = GameObject.FindGameObjectsWithTag ("Omnivore");
		/*
		player = GameObject.FindGameObjectWithTag ("Herbivore").transform;
		playerHealth = player.GetComponent <HerbivoreHealth> ();
		//enemyHealth = GetComponent <EnemyHealth> ();
		*/
		nav = GetComponent <NavMeshAgent> ();
		anim = GetComponent <Animator> ();
	}
	void findEnemy(){
		//Debug.Log ("in Find enemy");
		GameObject enemy = new GameObject("temp");
		allEnemiesDead = true;
		foreach (GameObject herbivore in herbivoreList) {
			currentEnemyHealth = herbivore.GetComponent <Health> ();
			if(currentEnemyHealth.currentHealth > 0)
				{
				Debug.Log ("Found enemy");
				enemy = herbivore;
				allEnemiesDead = false;
				break;

				}

				}


		if (!allEnemiesDead) {
						nav.SetDestination (enemy.transform.position);
						anim.SetTrigger ("Walking");
				}
		}
	
	void Update ()
	{

			if(!allEnemiesDead)
		{
			//Debug.Log ("in Update");
			// ... set the destination of the nav mesh agent to the player.
			
				findEnemy();
		}
		// Otherwise...
		else
		{
			// ... disable the nav mesh agent.
			nav.enabled = false;
			anim.SetTrigger ("AllEnemiesDead");
		}
}
}