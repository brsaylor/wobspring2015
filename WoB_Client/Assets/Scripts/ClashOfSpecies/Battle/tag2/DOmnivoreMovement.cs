using UnityEngine;
using System.Collections;

public class DOmnivoreMovement : MonoBehaviour {

	
	Animator anim;   
	NavMeshAgent nav;         				      // Reference to the nav mesh agent.
	
	
	
	GameObject[] DherbivoreList;                          // List of herbivores
	GameObject Dherbivore;                          // List of herbivores
	
	GameObject[] DomnivoreList;                          // List of Omnivores
	GameObject Domnivore;                         		 // List of herbivores
	
	GameObject[] DcarnivoreList;                          // List of carnivoreList
	GameObject Dcarnivore;                          // List of carnivoreList
	GameObject enemy;
	
	
	
	GameObject currentlyAttacking;
	Health currentEnemyHealth;                 		 // Reference to the player's health.	
	bool allEnemiesDead = false;
	
	
	void Awake ()
	{
		// Setup Enemy Lists
		DherbivoreList = GameObject.FindGameObjectsWithTag ("DHerbivore");
		DcarnivoreList = GameObject.FindGameObjectsWithTag ("DCarnivore");
		DomnivoreList = GameObject.FindGameObjectsWithTag ("DOmnivore");
		
		nav = GetComponent <NavMeshAgent> ();
		anim = GetComponent <Animator> ();
	}
	
	void findEnemy ()
	{
		
		allEnemiesDead = true;
		while (true) {
			foreach (GameObject Dherbivore in DherbivoreList) {
				currentEnemyHealth = Dherbivore.GetComponent <Health> ();
				if (currentEnemyHealth.currentHealth > 0 && Dherbivore != this.gameObject) {
					//Debug.Log ("Found Herbivore");
					enemy = Dherbivore;
					allEnemiesDead = false;
					break;
					
				}
				
			}
			if (!allEnemiesDead)
				break;
			foreach (GameObject Dcarnivore in DcarnivoreList) {
				currentEnemyHealth = Dcarnivore.GetComponent <Health> ();
				if (currentEnemyHealth.currentHealth > 0 && Dcarnivore != this.gameObject) {
					//Debug.Log ("Found Carnivore");D
					enemy = Dcarnivore;
					allEnemiesDead = false;
					break;
					
				}
				
			}
			if (!allEnemiesDead)
				break;
			foreach (GameObject Domnivore in DomnivoreList) {
				currentEnemyHealth = Domnivore.GetComponent <Health> ();
				if (currentEnemyHealth.currentHealth > 0 && Domnivore != this.gameObject) {
					//Debug.Log ("Found Omnivore");
					enemy = Domnivore;
					allEnemiesDead = false;
					break;
					
				}
				
			}
			break;
		}
		
		if (!allEnemiesDead && nav.enabled) {
			nav.SetDestination (enemy.transform.position);
			//anim.SetTrigger ("Walking");
			
		}
	}
	
	void Update ()
	{
		
		if (!allEnemiesDead) {
			findEnemy ();
		} else {
			// ... disable the nav mesh agent.
			nav.enabled = false;
			//anim.SetTrigger ("AllEnemiesDead");
		}
	}
}