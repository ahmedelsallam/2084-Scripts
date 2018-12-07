using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameLogic : MonoBehaviour {

	public List<GameObject> heroes = new List<GameObject> ();
	public List<GameObject> villains = new List<GameObject> ();

	public List<List<GameObject>> teams = new List<List<GameObject>>();

	public GameObject selectedUnit;
	public GameObject boss;

	public GameObject youWin;
	public GameObject youLoss;

	public GameObject level1;
	public GameObject level2;
	public GameObject level3;


	public int currentTeam = 0;
	public int currentUnit = 0;

	public string currentUnitAction;

	public HeroesStats heroesStats;

	public int bossDeathCounter = 0;

	void Start () {

		GameEvents.Instance.heroes = heroes;
		level1.SetActive (true);
		level2.SetActive (false);
		level3.SetActive (false);

		//adds the characters to each of their respective teams
		teams.Add (heroes);
		teams.Add (villains);

		GameEvents.Instance.endUnitTurn += HandleEndUnitTurn;
		GameEvents.Instance.unitReachedTarget += HandleUnitReachedTarget;
		GameEvents.Instance.heroAttacked += HandleHeroAttacked;
		GameEvents.Instance.bossDead += HandleBossDead;
		GameEvents.Instance.heroDead += HandleHeroDead;


		ClickEvents.Instance.gridNodeClicked += HandleGridNodeClicked;
		ClickEvents.Instance.bossClicked += HandleBossClicked;
		ClickEvents.Instance.unitClicked (teams [0] [0]);


	}

	void Update(){
		
		heroesStats = heroes[currentUnit].GetComponent<HeroesScript>().heroesStats;


	}

	void FixedUpdate(){
		
		heroes [currentUnit].transform.rotation = Quaternion.Lerp (heroes [currentUnit].transform.rotation, heroes [currentUnit].transform.rotation, 0.1f);

	}

	void HandleHeroDead ()
	{if (heroes.Count > 0) {
			gameObject.SetActive (false);
		} else {
			gameObject.SetActive (false);
			youLoss.SetActive (true);
		}
	}

	void HandleBossDead ()
	{
		if (bossDeathCounter <= 0) {
			
			level1.SetActive (false);
			level2.SetActive (true);
			bossDeathCounter++;

		} else if (bossDeathCounter == 1) {
			level2.SetActive (false);
			level3.SetActive (true);
		}
		else {
			boss.SetActive (false);
			youWin.SetActive (true);
		}
	}

	//handles ending the turn after the action is done
	void HandleEndUnitTurn ()
	{

		currentUnitAction = "";

	}

	//handles if the unit have reached its target that he is trying to reach
	void HandleUnitReachedTarget ()
	{

		currentUnitAction = "";

		ClickEvents.Instance.unitUnClicked ();

		ProgressTurn ();

	}

	void HandleHeroAttacked ()
	{
		
		currentUnitAction = "";

		ClickEvents.Instance.unitUnClicked ();

		ProgressTurn ();

	}

	void HandleBossClicked (GameObject BigBoss)
	{
		float distance = (int) (Vector3.Distance (heroes [currentUnit].transform.position, boss.transform.position) * 1);

		int heroRange;

		heroRange = heroesStats.range;

		Debug.Log (heroRange);

		Debug.Log (distance);

		if (distance <= heroRange){

			if (currentUnitAction == "Attack") {

				teams [currentTeam] [currentUnit].GetComponent<UnitBaseScript> ().SetAction ("Attack", BigBoss);

				teams [currentTeam] [currentUnit].GetComponent<UnitBaseScript> ().DoAction ();

				heroes [currentUnit].GetComponent<HeroesScript> ().direction = boss.transform.position - heroes [currentUnit].transform.position;
				heroes [currentUnit].transform.rotation = Quaternion.FromToRotation (heroes [currentUnit].transform.forward, heroes [currentUnit].GetComponent<HeroesScript> ().direction);

				currentUnitAction = "";

				ClickEvents.Instance.unitUnClicked ();

				Invoke("ProgressTurn",2f);
			}
		}
	}
		
	void HandleGridNodeClicked (GameObject node)
	{

		//this checks if the player chooses the move action and excute the action
		if (currentUnitAction == "Move") {

			if (node.GetComponent<NodeScript> ().inRange) {


				teams [currentTeam] [currentUnit].GetComponent<UnitBaseScript> ().SetAction ("Move", node);

				teams [currentTeam] [currentUnit].GetComponent<UnitBaseScript> ().DoAction ();

			}

		}

	}	

	void ProgressTurn(){

		//this checks if you have done an action if so it will progress the turn to another character
		if (currentUnit < teams [currentTeam].Count - 1) {
			
			currentUnit++;

			ClickEvents.Instance.unitClicked (teams [currentTeam] [currentUnit]);

		} 

		//checks if all the units made their move and if so game will move to the next team
		else {


				currentUnit = 0;

				if (currentTeam < teams.Count - 1) {
				
					currentTeam++;

					ClickEvents.Instance.unitClicked (teams [currentTeam] [currentUnit]);
				} else {
					currentTeam = 0;
					currentUnit = 0;

					ClickEvents.Instance.unitClicked (teams [currentTeam] [currentUnit]);
				}
			

		}

	}

	public void SetUnitAction (string action) {

		currentUnitAction = action;

	}

}
