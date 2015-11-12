using UnityEngine;
using System.Collections;

public class Fireball : ISkill {
	private Player player;
	private CoolDownTimer skd;
	public CoolDownTimer skillCoolDown
	{ 
		get
		{
			return skd;
		} 
	}

	private ModifiersManager mm;
	public ModifiersManager modifierManager
	{ 
		get
		{
			return mm;
		}
	}

	public Fireball(Player _player)
	{
		player = _player;
		mm = new ModifiersManager ();
		skd = new CoolDownTimer (player.stats.AttackFrequency);
	}
	
	private void CreateAttack()
	{
		//Initialize bullet
		GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet")) as GameObject;
		FireballObj bulletScript = bullet.GetComponent<FireballObj>();

		//Set initilization values
		Vector2 mouseScreenDif = (Input.mousePosition - Camera.main.WorldToScreenPoint(player.gameObject.transform.position));
		mouseScreenDif.Normalize();
		if(mouseScreenDif.x == 0 && mouseScreenDif.y == 0){
			mouseScreenDif = Vector2.up;
		}
		Vector2 startLocation = player.gameObject.transform.position + Vector3.up*4 + (Vector3)mouseScreenDif * 13;
		bulletScript.Make(startLocation,mouseScreenDif,player.stats.AttackSpeed,player.stats.AttackRange,player.stats.AttackDamage);
	}


	public void UseSkill()
	{
		if (skd.CanUse) {
			CreateAttack();
			skd.StartCoolDown();
		}
	}
	
}
