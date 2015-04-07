using UnityEngine;
using System.Collections;

public class UnitAttributes : MonoBehaviour {
	public int unitHP;
	public int unitAttack;
	public string animalType; // carnivore, herbivore, omnivore, plant
	public float moveSpeed;
	public float attackRange; //distance apart from target to attack
	public float attackSpeed; //attacking interval 1 attack per attackSpeed seconds
	

	// Use this for initialization
	void Start () {
		this.unitHP = 100;
		//this.SetMoveSpeed (0.00); //move speed for plants are 0
	}
	
	// Update is called once per frame
	void Update () {
	}

	void TakeDamage(int damage) {
		unitHP -= damage;
	}

	public int GetUnitHP() {
		return this.unitHP;
	}

	public void SetUnitHP(int hp) {
		this.unitHP = hp;
	}

	public int GetUnitAttack() {
		return this.unitAttack;
	}

	public void SetUnitAttack(int att) {
		this.unitAttack = att;
	}

	public float GetMoveSpeed() {
		return GetComponent<NavMeshAgent> ().speed;
	}

	public void SetMoveSpeed(float s) {
		GetComponent<NavMeshAgent> ().speed = moveSpeed;
	}

	public float GetAttackRange() {
		return this.attackRange;
	}
	
	public void SetAttackRange(float attRange) {
		this.attackRange = attRange;
	}

	public float GetAttackSpeed() {
		return this.attackSpeed;
	}
	
	public void SetAttackSpeed(float attSpeed) {
		this.attackSpeed = attSpeed;
	}

	public string GetAnimalType() {
		return this.animalType;
	}

	public void SetAnimalType(string s) {
		this.animalType = s;
	}
}
