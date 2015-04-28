using UnityEngine;
using System.Collections;

public class HerbivoreAttack : MonoBehaviour {

	
	public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
	public int attackDamage = 10;               // The amount of health taken away per attack.

		
	Animator anim;                              // Reference to the animator component.
	bool inRange;                        	 // Whether player is within the trigger collider and can be attacked.
	float timer;                                // Timer for counting up to the next attack.
	
	
	GameObject[] herbivoreList;                          // List of herbivores
	GameObject herbivore;                          // List of herbivores

	
	GameObject[] bushes;
	GameObject bush;               // Reference to the player's position.
	
	GameObject currentlyAttacking;
	Health currentEnemyHealth;                  // Reference to the player's health.
	Health myHealth;
	void Awake ()
	{
		// Setting up the references.
		herbivoreList = GameObject.FindGameObjectsWithTag ("Herbivore");
		bushes = GameObject.FindGameObjectsWithTag ("Plant");
		myHealth = this.gameObject.GetComponent<Health> ();
		
		
		anim = GetComponent <Animator> ();
	}
	
	
	void OnTriggerEnter (Collider other)
	{
		inRange = false;
		// If the entering collider is the player...
		foreach(GameObject herbivore in herbivoreList){
			if (inRange)	break;
			if(herbivore==other.gameObject && herbivore != this.gameObject)
			{
				// ... the player is in range.
				inRange = true;
				currentlyAttacking = other.gameObject;
				currentEnemyHealth = currentlyAttacking.GetComponent <Health> ();
				
			}
		}
		if (!inRange) {
			foreach (GameObject bush in bushes) {
				if (inRange)
					break;
				if (bush == other.gameObject && bush != this.gameObject) {
					// ... the player is in range.
					inRange = true;
					currentlyAttacking = other.gameObject;
					currentEnemyHealth = currentlyAttacking.GetComponent <Health> ();
					
				}
			}
		}
	}
	
	
	void OnTriggerExit (Collider other)
	{
		// If the exiting collider is the player...
		if(other.gameObject == currentlyAttacking)
		{
			// ... the player is no longer in range.
			inRange = false;
			//anim.SetBool("Attacking",false);
			
		}
	}
	
	
	void Update ()
	{
		// Add the time since Update was last called to the timer.
		timer += Time.deltaTime;
		
		// If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
		if (timer >= timeBetweenAttacks && inRange && currentEnemyHealth.currentHealth > 0 && myHealth.currentHealth>0) {
			// ... attack.
			//anim.SetBool ("Attacking", true);
			Attack ();
			
			
		}
		/*
		// If the player has zero or less health...
		if(currentEnemyHealth.currentHealth <= 0)
		{
			//anim.SetBool ("Attacking", false);
		}
		*/
	}
	
	
	void Attack ()
	{
		// Reset the timer.
		timer = 0f;
		
		// If the player has health to lose...
		if(currentEnemyHealth.currentHealth > 0)
		{
			// ... damage the player.
			currentEnemyHealth.TakeDamage (attackDamage);
		}
	}
}
