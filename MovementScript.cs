using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MovementScript: MonoBehaviour {


	public GameLogic gameLogic;

	NavMeshAgent agent;

	public bool targetReached = true;

	void Start () {

		agent = GetComponent<NavMeshAgent> ();

	}

	public void SetMoveTarget(GameObject target) {

		targetReached = false;

		agent.SetDestination (target.transform.position);

	}

	void Update () {

		if (agent.remainingDistance < 0.5f) {
			
			if (targetReached == false) {

				GameEvents.Instance.unitReachedTarget ();

				targetReached = true;

			}

		}

	}

}
