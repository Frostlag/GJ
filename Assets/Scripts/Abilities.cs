using UnityEngine;
using System.Collections;
using System;

public abstract class Ability:MonoBehaviour
{
	public abstract void cast();

	public float cooldownAmount;
	
	protected bool onCD;
	protected GameObject caster;
	protected Animator animator;
	protected string animationname;


	protected IEnumerator offCooldown(){
		yield return new WaitForSeconds (cooldownAmount);

		onCD = false;
	}

	protected bool canCast(){
		return !onCD;
	}

	protected IEnumerator animate(){
		animator.SetBool(animationname,true);
		yield return new WaitForSeconds (0.1f);
		animator.SetBool(animationname,false);
	}

	protected void doneCasting(){
		onCD = true;
		StartCoroutine(offCooldown());	
		StartCoroutine(animate());	
	}

}

public class AirAbility:Ability
{

	public GameObject airEffect = Resources.Load("AirWave") as GameObject; 

	public float airDistance = 4;
	public float airCooldownAmount = 1;

	//Override
	public void Start(){ 
		cooldownAmount = airCooldownAmount;
		caster = gameObject;
		animator = caster.GetComponent<Animator>();
		animationname = "airing";
	}

	public override void cast()
	{
		if (!canCast()) return;
		
		Vector3 v = caster.transform.position;
		float side = caster.transform.localScale.x / Mathf.Abs(caster.transform.localScale.x);
		BoxCollider2D box = caster.GetComponent<BoxCollider2D> ();
		v.x += side * airDistance;
		v.y += box.size.y / 2;
		caster.transform.position = v;
		v.x -= side * airDistance / 2;
		GameObject temp = GameObject.Instantiate (airEffect) as GameObject;
		temp.transform.position = v;
		Vector3 s = temp.transform.localScale;
		s.x = s.x * side;
		temp.transform.localScale = s;
		
		doneCasting();
	}

}

public class WaterAbility:Ability{

	public GameObject wave = Resources.Load("Wave") as GameObject; 

	public float waterCooldownAmount = 1;

	//Override
	public void Start(){
		cooldownAmount = waterCooldownAmount;
		caster = gameObject;
		animator = caster.GetComponent<Animator>();
		animationname = "watering";
	}

	//Override
	public override void cast()
	{
		if (!canCast())return;

		caster.SendMessage("rootMove",0.6f);
		
		Vector3 v = caster.transform.position;
		float side = caster.transform.localScale.x / Mathf.Abs(caster.transform.localScale.x);
		GameObject newWave = Instantiate (wave) as GameObject;
		
		BoxCollider2D box = caster.GetComponent<BoxCollider2D> ();
		Vector3 newPos = v;
		newPos.x = v.x + box.size.x*side*5;

		newPos.z = 0;
		newWave.transform.position = newPos;
		
		Vector3 s = newWave.transform.localScale;
		s.x = s.x * side;
		
		newWave.transform.localScale = s;
		
		onCD = true;
		
		doneCasting();
	}

}

public class EarthAbility:Ability{
	public GameObject column = Resources.Load("EarthColumn") as GameObject; 

	public float earthCooldownAmount = 1.5f;

	//Override
	public void Start(){
		cooldownAmount = earthCooldownAmount;
		caster = gameObject;
		animator = caster.GetComponent<Animator>();
		animationname = "earthing";
	}

	public override void cast()
	{
		if (!canCast()) return;
		caster.SendMessage("rootMove",0.5f);
		
		Vector3 v = caster.transform.position;
		float side = caster.transform.localScale.x / Mathf.Abs(caster.transform.localScale.x);
		GameObject newColumn = Instantiate (column) as GameObject;
		
		BoxCollider2D box = GetComponent<BoxCollider2D> ();
		BoxCollider2D otherbox = newColumn.GetComponent<BoxCollider2D> ();	
		Vector3 newPos = v;
		newPos.x = v.x + box.size.x*side*7;
		newPos.y -= otherbox.size.y*5.5f;
		newPos.z = 0;
		newColumn.transform.position = newPos;
		
		Vector3 s = newColumn.transform.localScale;
		s.x = s.x * side;

		newColumn.transform.localScale = s;
		
		doneCasting();
	}
	
}

public class FireAbility:Ability{
	public GameObject Fireball = Resources.Load("Fireball") as GameObject; 

	public float fireCooldownAmount = 0.45f;

	//Override
	public void Start(){
		cooldownAmount = fireCooldownAmount;
		caster = gameObject;
		animator = caster.GetComponent<Animator>();
		animationname = "firing";
	}

	public override void cast()
	{
		if (!canCast()) return;

		Invoke("fire",0.28f);

		doneCasting();
	}

	//Override
	private void fire(){

		Vector3 v = caster.transform.position;
		float side = caster.transform.localScale.x / Mathf.Abs(caster.transform.localScale.x);
		GameObject fireball = Instantiate (Fireball) as GameObject;
		
		BoxCollider2D box = GetComponent<BoxCollider2D> ();
		
		Vector3 newPos = v;
		newPos.x = v.x + box.size.x*side*7;
		
		newPos.z = 0;
		fireball.transform.position = newPos;
		
		Vector3 s = fireball.transform.localScale;
		s.x = s.x * side;

		fireball.transform.localScale = s;

	}
}