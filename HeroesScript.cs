using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroesScript: UnitBaseScript {

	public HeroesStats heroesStats;

	public GameObject boss;

	public Animator anim;

	public bool inRangeForAttack = false;

	public Vector3 direction;

	public Image healthBar;

	public void Update(){
		
	}

	public override void Attack (GameObject target) {

			target.GetComponent<BossAttackScript> ().TakeDamage (heroesStats.damage);
			anim.SetTrigger ("Attack");

	}

	public override void TakeDamage (int damageAmount){

		heroesStats.CurrentHP -= damageAmount;
		float healthDifference = heroesStats.CurrentHP / heroesStats.baseHP;
		healthBar.fillAmount = healthDifference;
		anim.SetTrigger ("Wounded");

		if (heroesStats.CurrentHP <= 0) {

			GameEvents.Instance.heroes.Remove (gameObject);
			anim.SetTrigger ("Dead");
			GameEvents.Instance.heroDead ();

		}

	}

	public override void DoAction () {


		if (unitAction.unitAction == "Attack") {

			Attack (boss);



		} else if (unitAction.unitAction == "Move") {
			
			GetComponent<MovementScript> ().SetMoveTarget(unitAction.target);
			anim.SetTrigger ("Walk");

		}

	}
}
