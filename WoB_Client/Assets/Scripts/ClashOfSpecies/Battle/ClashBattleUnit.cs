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
		if (currentHealth <= 0.0f) {
			Die();		
		}
        if (!target) {
						Idle ();
				} else if ((target.currentHealth >= 0) && (timer >= timeBetweenAttacks) && (currentHealth >= 0.0f)) {
						Attack ();
				} else if (target.currentHealth <= 0) {
			target=null;		
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

            if (agent.remainingDistance < 1.0f) {
                // TODO: Attack animation.
                //Added by Omar triggers Attacking animation
                if (anim != null) {
                anim.SetTrigger("Attacking");
                }
                // TODO: Deliver damage.
                target.TakeDamage(species.attack);
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
	}

    void TakeDamage(int damage, ClashBattleUnit source = null) {
        currentHealth = Mathf.Max(0, currentHealth - damage);
    }
}
