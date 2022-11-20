using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour {

	public GameObject enemy;
	public int totalEnemies;
	public static bool enemiesCreated;

	// Use this for initialization
	void Start () {
		enemiesCreated = false;
        ParticleSystem p1 = GameObject.FindGameObjectWithTag("AngularAttack1").GetComponent<ParticleSystem>();
        ParticleSystem p2 = GameObject.FindGameObjectWithTag("AngularAttack2").GetComponent<ParticleSystem>();
        ParticleSystem p3 = GameObject.FindGameObjectWithTag("AngularAttack3").GetComponent<ParticleSystem>();
        ParticleSystem p4 = GameObject.FindGameObjectWithTag("RadialAttack1").GetComponent<ParticleSystem>();
        ParticleSystem p5 = GameObject.FindGameObjectWithTag("RadialAttack2").GetComponent<ParticleSystem>();
        ParticleSystem p6 = GameObject.FindGameObjectWithTag("RadialAttack3").GetComponent<ParticleSystem>();
        for (int i = 0; i < totalEnemies; i++) {
			CreateEnemy ();
            p1.trigger.SetCollider(i, GameManager.Instance.enemies[i].GetComponent<Collider2D>());
            p2.trigger.SetCollider(i, GameManager.Instance.enemies[i].GetComponent<Collider2D>());
            p3.trigger.SetCollider(i, GameManager.Instance.enemies[i].GetComponent<Collider2D>());
            p4.trigger.SetCollider(i, GameManager.Instance.enemies[i].GetComponent<Collider2D>());
            p5.trigger.SetCollider(i, GameManager.Instance.enemies[i].GetComponent<Collider2D>());
            p6.trigger.SetCollider(i, GameManager.Instance.enemies[i].GetComponent<Collider2D>());
        }
		enemiesCreated = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CreateEnemy(){

		//Select Side
		enemySide side = (enemySide)UnityEngine.Random.Range(1,4);//Probar si se ve bien enemigos desde el norte Random.Range(1,3)

		//Create variable position
		float posX = UnityEngine.Random.Range(-8.4f,8.4f);
		float posY = UnityEngine.Random.Range(-4.4f,4.4f);
		Vector3 enemyPosition = new Vector3();

		//Create finally fixed position
		switch(side){
			case enemySide.north:
				enemyPosition = new Vector3 (posX,6.0f,-1.0f);
				break;
			case enemySide.south:
				enemyPosition = new Vector3 (posX,-6.0f,-1.0f);
				break;
			case enemySide.west:
				enemyPosition = new Vector3 (-9.3f,posY,-1.0f);
				break;
			case enemySide.east:
				enemyPosition = new Vector3 (9.3f,posY,-1.0f);
				break;
		}

		GameManager.Instance.enemies.Add(GameObject.Instantiate (enemy, enemyPosition, Quaternion.identity));
	}
}
