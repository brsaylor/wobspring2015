using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class ClashBattleUnit : MonoBehaviour {

    private NavMeshAgent agent;
    private ClashBattleController controller;

    public ClashBattleUnit target;
    public ClashSpecies species;
    public ClashStatusText status;
    public int currentHealth;



    //Added by Omar
    Animator anim;

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
        status = GetComponentInChildren<ClashStatusText>();
    }

	void Start() {
        // Set current health depending on the species data.
        currentHealth = species.hp;
        agent.speed = species.moveSpeed / 100.0f;

        // TODO: assign animator controller to individual prefabs (../Animation/foxController to the fox prefab

        //Added by Omar - finds the animator component This command assumes the Animator is already assigned
        anim = GetComponent<Animator>();
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
        anim.SetTrigger("Eating");
    }

    void Attack() {
        agent.destination = target.transform.position;

        if (agent.remainingDistance < 1.0f) {
            // TODO: Attack animation.
            //Added by Omar triggers Attacking animation
            anim.SetTrigger("Attacking");

            // TODO: Deliver damage.
            target.TakeDamage(species.attack);
        }
    }

    void TakeDamage(int damage, ClashBattleUnit source = null) {
        currentHealth = Mathf.Min(0, currentHealth - damage);
        Debug.Log(species.name + " took " + damage + "damage.", source);
    }
}
