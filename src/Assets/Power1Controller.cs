using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power1Controller : MonoBehaviour {

	public static int killedFrogs;

	// Use this for initialization
	void Start () {
		killedFrogs = 0;
    }
   
	void OnParticleCollision(GameObject enemy)
    {
        //Debug.Log("Particle Collision 2");
        //Destroy(other.gameObject);

		killedFrogs++;

        //Select Side
        enemySide side = (enemySide)UnityEngine.Random.Range(1, 4);//Probar si se ve bien enemigos desde el norte Random.Range(1,3)

        //Create variable position
        float posX = UnityEngine.Random.Range(-8.4f, 8.4f);
        float posY = UnityEngine.Random.Range(-4.4f, 4.4f);
        Vector3 enemyPosition = new Vector3();

        //Create finally fixed position
        switch (side)
        {
            case enemySide.north:
                enemyPosition = new Vector3(posX, 6.0f, -1.0f);
                break;
            case enemySide.south:
                enemyPosition = new Vector3(posX, -6.0f, -1.0f);
                break;
            case enemySide.west:
                enemyPosition = new Vector3(-9.3f, posY, -1.0f);
                break;
            case enemySide.east:
                enemyPosition = new Vector3(9.3f, posY, -1.0f);
                break;
        }
        Debug.Log("Reaparece en: " + enemyPosition + " - Side: " + side);
        enemy.transform.position = enemyPosition;
        enemy.transform.localScale = new Vector3(Mathf.Abs(enemy.transform.localScale.x), Mathf.Abs(enemy.transform.localScale.y), Mathf.Abs(enemy.transform.localScale.z));
        //Destroy(this.gameObject);
        /* Rigidbody body = other.GetComponent<Rigidbody>();
        if (body)
        {
            Vector3 direction = other.transform.position - transform.position;
            direction = direction.normalized;
            body.AddForce(direction * 5);
        } */
    }

    // Update is called once per frame
    void Update () {
		
	}
}
