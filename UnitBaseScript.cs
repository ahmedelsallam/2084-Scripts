using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBaseScript : MonoBehaviour {

	public UnitAction unitAction = new UnitAction();

	public virtual void Attack (GameObject target) {}

	public virtual void TakeDamage (int damageAmount) {}

	public void SetAction (string action, GameObject target) { unitAction.unitAction = action; unitAction.target = target;}

	public virtual void DoAction () {}

}
