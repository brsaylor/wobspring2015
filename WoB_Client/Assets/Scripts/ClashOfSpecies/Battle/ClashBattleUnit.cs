using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class ClashBattleUnit : MonoBehaviour {

    private NavMeshAgent agent;
    private ClashBattleController controller;
    private Animator anim;

    public ClashBattleUnit target;
    public ClashSpecies species;
    public ClashStatusText status;
    public int currentHealth;
	public float timeBetweenAttacks = 0.5f;     // The time in seconds between each attack.
	float timer;                                // Timer for counting up to the next attack.

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

	void Start() {
        // Set current health depending on the species data.
        currentHealth = species.hp;
        if (agent != null) {
            agent.speed = species.moveSpeed / 100.0f;
        }
	}
	
	void Update () {
		timer += Time.deltaTime;
        if (!target) {
			Idle();
		} else if ((target.currentHealth > 0) && (timer >= timeBetweenAttacks) && (currentHealth >= 0.0f)) {
			Attack();
		} else if (target.currentHealth <= 0) {
			target = null;		
		}
	}

    void Idle() {
        //Added by Omar triggers eating animation
        if (anim != null) {
            anim.SetTrigger("Eating");
        }
    }

    void Attack() {
		timer = 0f;
        if (agent) {
            agent.destination = target.transform.position;
			
//			Debug.Log (tag + " " + species.name +
//			           " distance to " +
//			           target.tag + " " + target.species.name +
//			           " is " + agent.remainingDistance);
            if (agent.remainingDistance < 5.0f) {
                // TODO: Attack animation.
                //Added by Omar triggers Attacking animation
//				Debug.Log(species.name + " attacking " + target.species.name);
                if (anim != null) {
                	anim.SetTrigger("Attacking");
                }
                // TODO: Deliver damage.
                target.TakeDamage(species.attack, this);
            }else{
				if (anim != null) {
					anim.SetTrigger("Walking");
				}
			}
        }
    }
	void Die(){
		//Disable all functions here
		if (anim != null) {
			anim.SetTrigger("Dead");
		}
		target=null;
		agent.enabled = false;
	}

    void TakeDamage(int damage, ClashBattleUnit source = null) {
//		Debug.Log (tag + " " + species.name + " taking " + damage + " damage from " + source.tag + " " + source.species.name);
        currentHealth = Mathf.Max(0, currentHealth - damage);
		if (currentHealth == 0) {
			Die();
		}
    }
}
