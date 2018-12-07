using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSystem : MonoBehaviour {



	public GameObject nodePrefab;

	public List<List<GameObject>> grid = new List<List<GameObject>>();

	public float offSetX;
	public float offSetZ;

	public NodeScript nodeScript;


	void Start () {

		BuildGrid ();
		FindingNeighbours ();

		ClickEvents.Instance.unitClicked += HandleUnitClicked;
		ClickEvents.Instance.unitUnClicked += HandleUnitUnClicked;

	}


	void HandleUnitClicked (GameObject unit)
	{
			ResetGridColours ();

			Debug.Log (unit.GetComponent<SelectableUnit> ().GetCurrentGridPoint ());

			GameObject currentGridPoint = unit.GetComponent<SelectableUnit> ().GetCurrentGridPoint ();

			int range = 0;

			if (unit.GetComponent<HeroesScript> ()) {

				range = unit.GetComponent<HeroesScript> ().heroesStats.range;

			} else if (unit.GetComponent<BossAttackScript> ()) {

				range = unit.GetComponent<BossAttackScript> ().bossStats.range;

			}
			
			currentGridPoint.GetComponent<NodeScript> ().CheckNeighbourRange (range, currentGridPoint);
	}

	void HandleUnitUnClicked ()
	{
		ResetGridColours ();
	}

	void BuildGrid(){

		for (int x = 0; x < 10; x++) {

			List<GameObject> gridRow = new List<GameObject> ();

			for (int z = 0; z < 10; z++) {

				GameObject node = GameObject.Instantiate (nodePrefab, new Vector3 (x + offSetX, 0, z + offSetZ), transform.rotation);

				node.name = x + " , " + z;

				gridRow.Add (node);

			}

			grid.Add (gridRow);

		}

			
	}

	void FindingNeighbours (){

		for (int i = 0; i < 10; i++) {

			for (int j = 0; j < 10; j++) {

				//for top row
				try {

					try {

						grid[i][j].GetComponent<NodeScript>().neighbours.Add(grid[i-1][j+1]);

					} catch {
					}
					try {

						grid[i][j].GetComponent<NodeScript>().neighbours.Add(grid[i][j+1]);

					} catch {
					}
					try {

						grid[i][j].GetComponent<NodeScript>().neighbours.Add(grid[i+1][j+1]);

					} catch {
					}
				} catch {
				}

				//look for right

				try {

					grid[i][j].GetComponent<NodeScript>().neighbours.Add(grid[i+1][j]);

				} catch{
				}

				//for bottom row

				try {
					try {

						grid[i][j].GetComponent<NodeScript>().neighbours.Add(grid[i+1][j-1]);

					} catch {
					}
					try {

						grid[i][j].GetComponent<NodeScript>().neighbours.Add(grid[i][j-1]);

					} catch {
					}
					try {

						grid[i][j].GetComponent<NodeScript>().neighbours.Add(grid[i-1][j-1]);

					} catch {
					}
				} catch {
				}

				//for the left one

				try {

					grid[i][j].GetComponent<NodeScript>().neighbours.Add(grid[i-1][j]);

				} catch {
				}

			}
		}
	}

	public void ResetGridColours () {

		for (int i = 0; i < grid.Count; i++) {

			for (int j = 0; j < grid [i].Count; j++) {

				grid [i] [j].GetComponent<MeshRenderer> ().material.color = Color.black;
				grid [i] [j].GetComponent<NodeScript> ().inRange = false;

			}

		}

	}

}