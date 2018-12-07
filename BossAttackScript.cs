using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossAttackScript : UnitBaseScript {

	public BossStats bossStats;
	public GameLogic gameLogic;

	public GameObject currentTarget;

	public GridSystem gridSystem;

	public float distance;

	NavMeshAgent agent;

	public bool travelling;
	public bool attacking;

	public Animator anim;

	public Image healthBar;

	void Start () {

		agent = GetComponent<NavMeshAgent> ();
	}

	void Update(){

		if (agent.remainingDistance < 0.5f && travelling) {

			travelling = false;
			GameEvents.Instance.unitReachedTarget ();

		}

		if (attacking == true) {
			
			GameEvents.Instance.unitReachedTarget ();
			attacking = false;

		}
	}

	//this handles boss attack
	public override void Attack (GameObject target) {

		target.GetComponent<HeroesScript> ().TakeDamage (bossStats.damage);

	}

	//this handles boss taking damage
	public override void TakeDamage (int damageAmount){

		bossStats.CurrentHP -= damageAmount;
		float healthDifference = bossStats.CurrentHP / bossStats.baseHP;
		healthBar.fillAmount = healthDifference;
		anim.SetTrigger ("Wounded");
		EndTurn ();

		if (bossStats.CurrentHP <= 0 && gameLogic.bossDeathCounter >= 0) {
			bossStats.CurrentHP = bossStats.baseHP;
			anim.SetTrigger ("Dead");
			GameEvents.Instance.bossDead();

		}

	}

	public void FindAttackTarget () {

		GameObject attackTarget = SelectNearestTarget ();

		if (CheckHeroInRange (attackTarget)) {

			attackTarget.GetComponent<HeroesScript> ().TakeDamage (bossStats.damage);
			anim.SetTrigger ("Poke");
			attacking = true;

		} else {

			GameObject targetNode = FindClosestNodeToHero (attackTarget);
			agent.SetDestination (targetNode.transform.position);
			travelling = true;

		}

	}

	//this will end the turn after doing the action
	void EndTurn () {

		GameEvents.Instance.endUnitTurn ();

	}

	GameObject SelectNearestTarget () {

		GameObject closestHero = gameLogic.heroes [0];

		foreach (GameObject hero in gameLogic.heroes) {

			distance = Vector3.Distance (transform.position, hero.transform.position);

			if (distance < Vector3.Distance (closestHero.transform.position, transform.position)) {

				closestHero = hero;

			}

		}

		return closestHero;

	}

	bool CheckHeroInRange (GameObject hero) {

		bool inRange = false;

		if (Vector3.Distance (hero.transform.position, transform.position) <= bossStats.range) {

			inRange = true;

		}

		return inRange;

	}


	GameObject FindClosestNodeToHero (GameObject hero) {

		GameObject node = GetComponent<SelectableUnit> ().GetCurrentGridPoint ();

		GameEvents.Instance.FlushRange ();

		node.GetComponent<NodeScript> ().CheckNeighbourRange (bossStats.range, node);

		List<GameObject> rangeNodes = GameEvents.Instance.currentRange;

		GameObject returnNode = rangeNodes [0];

		foreach (GameObject rangeNode in rangeNodes) {

			if (Vector3.Distance (hero.transform.position, rangeNode.transform.position) < Vector3.Distance (hero.transform.position, returnNode.transform.position)) {

				returnNode = rangeNode;

			}

		}

		returnNode.GetComponent<MeshRenderer> ().material.color = Color.blue;

		return returnNode;

	}

}
