using UnityEngine;
using System.Collections;

public class OmnivoreMovement : MonoBehaviour {
	Animator anim;   
	NavMeshAgent nav;         				      // Reference to the nav mesh agent.
	
	
	
	GameObject[] herbivoreList;                          // List of herbivores
	GameObject herbivore;                          // List of herbivores
	
	GameObject[] omnivoreList;                          // List of Omnivores
	GameObject omnivore;                         		 // List of herbivores
	
	GameObject[] carnivoreList;                          // List of carnivoreList
	GameObject carnivore;                          // List of carnivoreList
	GameObject enemy;
	
	
	
	GameObject currentlyAttacking;
	Health currentEnemyHealth;                 		 // Reference to the player's health.	
	bool allEnemiesDead = false;

	
	void Awake ()
	{
		// Setup Enemy Lists
		herbivoreList = GameObject.FindGameObjectsWithTag ("Herbivore");
		carnivoreList = GameObject.FindGameObjectsWithTag ("Carnivore");
		omnivoreList = GameObject.FindGameObjectsWithTag ("Omnivore");
		
		nav = GetComponent <NavMeshAgent> ();
		anim = GetComponent <Animator> ();
	}
	
	void findEnemy ()
	{
		
		allEnemiesDead = true;
		while (true) {
			foreach (GameObject herbivore in herbivoreList) {
				currentEnemyHealth = herbivore.GetComponent <Health> ();
				if (currentEnemyHealth.currentHealth > 0 && herbivore != this.gameObject) {
					//Debug.Log ("Found Herbivore");
					enemy = herbivore;
					allEnemiesDead = false;
					break;
					
				}
				
			}
			if (!allEnemiesDead)
				break;
			foreach (GameObject carnivore in carnivoreList) {
				currentEnemyHealth = carnivore.GetComponent <Health> ();
				if (currentEnemyHealth.currentHealth > 0 && carnivore != this.gameObject) {
					//Debug.Log ("Found Carnivore");
					enemy = carnivore;
					allEnemiesDead = false;
					break;
					
				}
				
			}
			if (!allEnemiesDead)
				break;
			foreach (GameObject omnivore in omnivoreList) {
				currentEnemyHealth = omnivore.GetComponent <Health> ();
				if (currentEnemyHealth.currentHealth > 0 && omnivore != this.gameObject) {
					//Debug.Log ("Found Omnivore");
					enemy = omnivore;
					allEnemiesDead = false;
					break;
					
				}
				
			}
			break;
		}
		
		if (!allEnemiesDead) {
			nav.SetDestination (enemy.transform.position);
			anim.SetTrigger ("Walking");
			
		}
	}
	
	void Update ()
	{
		
		if (!allEnemiesDead) {
			findEnemy ();
		} else {
			// ... disable the nav mesh agent.
			nav.enabled = false;
			anim.SetTrigger ("AllEnemiesDead");
		}
	}
}