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

    void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }

	void Start() {
        // Set current health depending on the species data.
        currentHealth = species.hp;
        agent.speed = species.moveSpeed / 100.0f;
	}
	
	void Update () {
        if (!target) {
            Idle();
        } else {
            Attack();
        }
	}

    void Idle() { }

    void Attack() {
        agent.destination = target.transform.position;

        if (agent.remainingDistance < 1.0f) {
            // TODO: Attack animation.
            // TODO: Deliver damage.
            target.TakeDamage(species.attack);
        }
    }

    void TakeDamage(int damage, ClashBattleUnit source = null) {
        currentHealth = Mathf.Min(0, currentHealth - damage);
        Debug.Log(species.name + " took " + damage + "damage.", source);
    }
}
