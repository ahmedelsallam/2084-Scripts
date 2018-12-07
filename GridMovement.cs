using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridMovement : MonoBehaviour {

	void Update () {

		if (Input.GetMouseButtonDown (0)) {

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			if (Physics.Raycast (ray, out hit)) {

				if (hit.collider.gameObject.GetComponent<SelectableUnit> ()) {



				} else if (hit.collider.gameObject.GetComponent<NodeScript> ()) {

					ClickEvents.Instance.gridNodeClicked (hit.collider.gameObject);

				} 
				if (hit.collider.gameObject.name == "BigBoss") {
					
					ClickEvents.Instance.bossClicked (hit.collider.gameObject);

				}

			} else {



			}

		}
		
	}

}
