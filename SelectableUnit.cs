using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableUnit : MonoBehaviour {

	public bool selected;

	// Use this for initialization
	void Start () {

		ClickEvents.Instance.unitClicked += HandleUnitClicked;
		ClickEvents.Instance.unitUnClicked += HandleUnitUnClicked;

	}

	void HandleUnitClicked (GameObject unit)
	{

		GetComponent<MeshRenderer> ().material.color = Color.white;

		selected = false;

		if (unit == gameObject) {

			selected = true;
			GetComponent<MeshRenderer> ().material.color = Color.red;

			if (gameObject.name == "BigBoss") {

				GetComponent<BossAttackScript> ().FindAttackTarget ();

			}

		}

	}

	void HandleUnitUnClicked ()
	{
		GetComponent<MeshRenderer> ().material.color = Color.white;
		selected = false;
	}

	public GameObject GetCurrentGridPoint () {

		RaycastHit hit;
		Ray ray = new Ray (transform.position, transform.up * -1);

		if (Physics.Raycast (ray, out hit)) {

			if (hit.collider.gameObject.tag == "GridPoint") {

				return hit.collider.gameObject;

			} else {

				return null;

			}

		} else {

			return null;

		}

	}

}
