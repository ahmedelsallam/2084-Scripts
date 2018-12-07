using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//this handles endturn events where we create the endturn events
public class GameEvents : MonoBehaviour {

	public List<GameObject> heroes;

	private static GameEvents instance;
	public static GameEvents Instance {

		get {

			return instance ?? (instance = new GameObject ("GameEvents").AddComponent<GameEvents> ());

		}
	}

	public List<GameObject> currentRange = new List<GameObject> ();

	public void FlushRange () {

		currentRange = new List<GameObject> ();

	}

	public delegate void EndUnitTurn();
	public EndUnitTurn endUnitTurn;

	public delegate void UnitReachedTarget();
	public UnitReachedTarget unitReachedTarget;

	public delegate void HeroAttacked();
	public HeroAttacked heroAttacked;

	public delegate void BossDead();
	public BossDead bossDead;

	public delegate void HeroDead();
	public HeroDead heroDead;

}
