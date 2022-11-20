using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public float minDistanceEntities;
	public float speed;
	float step;
	public static bool[] aliveEntity;
    Animator enemyAnim;


    public delegate void KillEntity(EnemyController emitter, int entityDestroyed);
	public static KillEntity OnKillEntity;
    
    // Use before initialization
    void Awake() {
        Debug.Log("EnemyController");
		DefenseEntityCreator.OnDefenseEntityCreated += InitAttackEntities;
	}

	// Use this for initialization
	void Start () {
        enemyAnim = GetComponent<Animator>();

        OnKillEntity += KilledEnemy;
    }

    void OnParticleCollision(GameObject other)
    {
        /* Rigidbody body = other.GetComponent<Rigidbody>();
        if (body)
        {
            Vector3 direction = other.transform.position - transform.position;
            direction = direction.normalized;
            body.AddForce(direction * 5);
        } */
    }

    void OnDestroy(){
		DefenseEntityCreator.OnDefenseEntityCreated -= InitAttackEntities;
	}

	// Update is called once per frame
	void Update () {
		step = speed * Time.deltaTime;
	}

	void InitAttackEntities(){
        if (GameManager.Instance.defenseEntities.Count > 0)
        {
            if (aliveEntity == null)
            {
                //Debug.Log("GameManager.Instance.defenseEntities.Count = " + GameManager.Instance.defenseEntities.Count);
                aliveEntity = new bool[GameManager.Instance.defenseEntities.Count];
                for (int i = 0; i < aliveEntity.Length; i++) aliveEntity[i] = true;
            }
            //float distanceEntities = Vector3.Distance (transform.position,GameManager.Instance.defenseEntities[0].transform.position);
            float distanceEntities = float.MaxValue;

            Vector3 targetPosition;// = GameManager.Instance.defenseEntities[0].transform.position;
			int index = 0;
            int numDefenseAlive = 0;

			for (int i = 0; i < GameManager.Instance.defenseEntities.Count; i++)
            {
                //Debug.Log("aliveEntity is null:" + (aliveEntity == null ? 'Y' : 'N'));
                //Debug.Log("aliveEntity[" + i + "] is true:" + (aliveEntity[i] ? 'Y' : 'N'));
                //Debug.Log("GameManager.Instance.defenseEntities[" + i + "] is null:" + (GameManager.Instance.defenseEntities[i] == null? 'Y' : 'N'));

                if (aliveEntity[i] && distanceEntities > Vector3.Distance (transform.position, GameManager.Instance.defenseEntities [i].transform.position)) {
					distanceEntities = Vector3.Distance (transform.position, GameManager.Instance.defenseEntities [i].transform.position);
					targetPosition = GameManager.Instance.defenseEntities[i].transform.position;
					index = i;
                    numDefenseAlive++;

                }

			}
            if (numDefenseAlive > 0)
            {
			     StartCoroutine (SeekNearEntity (index));
			    //else InitAttackEntities ();
            }
		}

	}

	IEnumerator SeekNearEntity(int index){
        GameObject defenseGameObject = GameManager.Instance.defenseEntities[index];

        bool isAnimating = false;
        Vector3 targetPosition = defenseGameObject.transform.position;
        Vector2 direction = targetPosition - transform.position;

        //string animName = "idle";
        //if (direction.y > 0.0f)
        //    animName = "idleBottom";

        //Debug.Log("SetTrigger = " + animName);
        ////enemyAnim.SetTrigger(animName);

        aliveEntity[index] = aliveEntity[index] && defenseGameObject.activeInHierarchy;

        transform.localScale = new Vector3((direction.x > 0.0f ? -1.0f : 1.0f) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        float distance2Points;
		while ((distance2Points=Vector3.Distance (transform.position, targetPosition)) > 0.5f && aliveEntity[index]) {

            if (!isAnimating)
            {
                if (distance2Points < 1.5f) {
                    string animName = "swimBottom";
                    if (direction.y < 0.0f)
                    {
                        animName = "swimUpper";
                    }

					if(enemyAnim!=null)enemyAnim.SetTrigger(animName);
                    isAnimating = true;
                } else
                {
                    


                }
            }

            transform.position = Vector3.MoveTowards (transform.position, targetPosition, step);
            transform.localScale = new Vector3((direction.x > 0.0f ? -1.0f : 1.0f) * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            yield return null;

            aliveEntity[index] = aliveEntity[index] && defenseGameObject.activeInHierarchy;
        }
        //OnKillEntity(this, index);
        if (aliveEntity[index]) {
            aliveEntity[index] = false;
            defenseGameObject.SetActive(false);
            //GameManager.Instance.defenseEntities.Remove (defenseGameObject);
        }
        InitAttackEntities ();
	}

    private void KilledEnemy(EnemyController emitter, int entityDestroyed)
    {
        if (emitter == this)
            return;


    }
}
