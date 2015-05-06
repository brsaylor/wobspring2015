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
        if (!target) {
            Idle();
        } else {
            Attack();
        }
	}

    void Idle() {
        //Added by Omar triggers eating animation
        if (anim != null) {
            anim.SetTrigger("Eating");
        }
    }

    void Attack() {
        if (agent) {
            agent.destination = target.transform.position;

            if (agent.remainingDistance < 1.0f) {
                // TODO: Attack animation.
                //Added by Omar triggers Attacking animation
                anim.SetTrigger("Attacking");

                // TODO: Deliver damage.
                target.TakeDamage(species.attack);
            }
        }
    }

    void TakeDamage(int damage, ClashBattleUnit source = null) {
        currentHealth = Mathf.Min(0, currentHealth - damage);
    }
}
