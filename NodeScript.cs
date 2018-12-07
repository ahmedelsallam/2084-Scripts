using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour {

	public List<GameObject> neighbours = new List<GameObject> ();

	public bool inRange;

	public void CheckNeighbourRange (int range, GameObject currentGridPoint) {
		

		currentGridPoint.GetComponent<NodeScript> ().inRange = true;
		currentGridPoint.GetComponent<MeshRenderer> ().material.color = Color.white;

		GameEvents.Instance.currentRange.Add (currentGridPoint);

		if (range > 0) {

			range--;

			foreach (GameObject gridPoint in currentGridPoint.GetComponent<NodeScript>().neighbours) {

				gridPoint.GetComponent<MeshRenderer> ().material.color = Color.white;

				gridPoint.GetComponent<NodeScript> ().CheckNeighbourRange (range, gridPoint);
			}
		}
	}
}
