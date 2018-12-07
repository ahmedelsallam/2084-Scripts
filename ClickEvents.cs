using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this handles click events where we create the click event
public class ClickEvents : MonoBehaviour {

	private static ClickEvents instance;
	public static ClickEvents Instance {

		get {

			return instance ?? (instance = new GameObject ("ClickEvents").AddComponent<ClickEvents> ());

		}

	}

	public delegate void UnitClicked (GameObject unit);
	public UnitClicked unitClicked;

	public delegate void UnitUnClicked ();
	public UnitUnClicked unitUnClicked;

	public delegate void GridNodeClicked (GameObject node);
	public GridNodeClicked gridNodeClicked;

	public delegate void BossClicked(GameObject BigBoss);
	public BossClicked bossClicked;

	public bool moving;





}
