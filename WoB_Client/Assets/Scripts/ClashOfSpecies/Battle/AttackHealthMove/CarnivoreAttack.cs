using UnityEngine;
using System.Collections;

public class CarnivoreAttack : MonoBehaviour {

	public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
	public int attackDamage = 10;               // The amount of health taken away per attack.

	
	Animator anim;                              // Reference to the animator component.
	bool inRange;                        	 // Whether player is within the trigger collider and can be attacked.
	float timer;                                // Timer for counting up to the next attack.


	GameObject[] herbivoreList;                          // List of herbivores
	GameObject herbivore;                          // List of herbivores

	GameObject[] omnivoreList;                          // List of Omnivores
	GameObject omnivore;                          // List of herbivores

	GameObject currentlyAttacking;
	Health currentEnemyHealth;                  // Reference to the player's health.


	//EnemyHealth enemyHealth;                    // Reference to this enemy's health.





	void Awake ()
	{
		// Setting up the references.
		herbivoreList = GameObject.FindGameObjectsWithTag ("Herbivore");
		omnivoreList = GameObject.FindGameObjectsWithTag ("Omnivore");



		//HEALTH
		//playerHealth = player.GetComponent <HerbivoreHealth> ();
		//enemyHealth = GetComponent<EnemyHealth>();


		anim = GetComponent <Animator> ();
	}
	
	
	void OnTriggerEnter (Collider other)
	{
		// If the entering collider is the player...
		foreach(GameObject herbivore in herbivoreList){
			if(herbivore==other.gameObject)
			{
				// ... the player is in range.
				inRange = true;
				currentlyAttacking = other.gameObject;
				currentEnemyHealth = currentlyAttacking.GetComponent <Health> ();

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
			anim.SetBool("Attacking",false);

		}
	}
	
	
	void Update ()
	{
		// Add the time since Update was last called to the timer.
		timer += Time.deltaTime;
		
		// If the timer exceeds the time between attacks, the player is in range and this enemy is alive...
		if (timer >= timeBetweenAttacks && inRange && currentEnemyHealth.currentHealth > 0) {
						// ... attack.
						Attack ();
						anim.SetBool ("Attacking", true);

				} 
		
		// If the player has zero or less health...
		if(currentEnemyHealth.currentHealth <= 0)
		{
			// ... tell the animator the player is dead.
			//anim.SetTrigger ("AllEnemiesDead");
			anim.SetBool ("Attacking", false);
		}
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
