using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDefenderController : MonoBehaviour {

	Animator animGuardia;
    List<GameObject> angularAttacks;
    List<GameObject> radialAttacks;
    List<GameObject> radialStoneAttacks;
    int angularAttacksLastUsed = 0;
    int radialAttacksLastUsed = 0;

    int health;
    float step;
    float angle;
    Vector3 angleDirection;
    public float speedStone;

    // Use this for initialization
    void Start () {
		health = 3;
		InputManager.OnAngularAttack += DoAngularAttack;
		InputManager.OnRadialAttack += DoRadialAttack;

		animGuardia = transform.gameObject.GetComponent<Animator> ();
        angularAttacks = new List<GameObject>();
        angularAttacks.Add(GameObject.FindGameObjectWithTag("AngularAttack1"));
        angularAttacks.Add(GameObject.FindGameObjectWithTag("AngularAttack2"));
        angularAttacks.Add(GameObject.FindGameObjectWithTag("AngularAttack3"));
        radialAttacks = new List<GameObject>();
        radialAttacks.Add(GameObject.FindGameObjectWithTag("RadialAttack1"));
        radialAttacks.Add(GameObject.FindGameObjectWithTag("RadialAttack2"));
        radialAttacks.Add(GameObject.FindGameObjectWithTag("RadialAttack3"));
        radialStoneAttacks = new List<GameObject>();
        radialStoneAttacks.Add(GameObject.Find("StoneAttack1"));
        radialStoneAttacks.Add(GameObject.Find("StoneAttack2"));
        radialStoneAttacks.Add(GameObject.Find("StoneAttack3"));
        radialStoneAttacks[0].SetActive(false);
        radialStoneAttacks[1].SetActive(false);
        radialStoneAttacks[2].SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        step = speedStone * Time.deltaTime;
    }

	void DoAngularAttack(Vector3 targetPosition)
    {
        if (angularAttacks[angularAttacksLastUsed].GetComponent<ParticleSystem>().IsAlive())
            return;
        var v3 = targetPosition;
        v3.z = 9;
        Vector3 v3World = Camera.main.ScreenToWorldPoint(v3);
        Vector3 diference = angleDirection = v3World - transform.position;
        angleDirection.Normalize();
        float sign = (v3World.x < transform.position.x) ? -1.0f : 1.0f;
        angle = Vector2.Angle(Vector2.up, new Vector2(diference.x, diference.y)) * sign;

        angularAttacks[angularAttacksLastUsed].GetComponent<ParticleSystem>().startRotation = (angle) * 0.0174533f;

        Debug.Log("DoAngularAttack Angle: " + angle);

        float normalizeTarget = targetPosition.x / (float)Screen.width;
        transform.localScale= new Vector3((normalizeTarget > 0.5f? 1.0f : -1.0f)* Mathf.Abs( transform.localScale.x), transform.localScale.y, transform.localScale.z);
		animGuardia.SetTrigger ("radialAttack");
        StartCoroutine(OnCompleteAngularAttack());

    }

	void DoRadialAttack(Vector3 targetPosition)
    {
        Debug.Log("Radial Index: " + radialAttacksLastUsed);
        if (radialStoneAttacks[radialAttacksLastUsed].activeInHierarchy || radialAttacks[radialAttacksLastUsed].GetComponent<ParticleSystem>().IsAlive())
            return;
        var v3 = targetPosition;
        v3.z = 9;
        Vector3 v3World = Camera.main.ScreenToWorldPoint(v3);
        /*Vector3 diference = angleDirection = v3World - transform.position;
        angleDirection.Normalize();
        float sign = (v3World.x < transform.position.x) ? -1.0f : 1.0f;
        angle = Vector2.Angle(Vector2.up, new Vector2(diference.x, diference.y)) * sign;*/

        //Debug.Log("Is Alive " + angularAttacksLastUsed + ": " + (angularAttacks[angularAttacksLastUsed].GetComponent<ParticleSystem>().IsAlive(true) ? "T" : "N"));
        //Debug.Log("Is Alive " + angularAttacksLastUsed + ": " + (angularAttacks[angularAttacksLastUsed].GetComponent<ParticleSystem>().IsAlive(false) ? "T" : "N"));
        //Debug.Log("Is Alive " + angularAttacksLastUsed + ": " + (angularAttacks[angularAttacksLastUsed].GetComponent<ParticleSystem>().IsAlive() ? "T" : "N"));

        radialAttacks[radialAttacksLastUsed].GetComponent<ParticleSystem>().transform.position = v3World;
        radialStoneAttacks[radialAttacksLastUsed].transform.position = new Vector3(-0.2f, -1.0f, -1.0f);

        float normalizeTarget = targetPosition.x / (float)Screen.width;
        transform.localScale= new Vector3((normalizeTarget > 0.5f? 1.0f : -1.0f)* Mathf.Abs( transform.localScale.x), transform.localScale.y, transform.localScale.z);
		animGuardia.SetTrigger ("angularAttack");
        StartCoroutine(OnCompleteRadialAttack(v3World));
    }
    IEnumerator OnCompleteRadialAttack(Vector3 targetPosition)
    {
        while (animGuardia.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            yield return null;
        }
        GameObject stone = radialStoneAttacks[radialAttacksLastUsed];
        stone.SetActive(true);
        while (Vector3.Distance(stone.transform.position, targetPosition) > 0.5f)
        {
            stone.transform.position = Vector3.MoveTowards(stone.transform.position, targetPosition, step);
            yield return null;
        }
        /* while (!animGuardia.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            yield return null;
        } */
        Debug.Log("OnCompleteRadialAttack ending...");
        Debug.Log("Particle position: " + angularAttacks[radialAttacksLastUsed].GetComponent<ParticleSystem>().transform.position.ToString());
        radialAttacks[radialAttacksLastUsed].transform.rotation = Quaternion.Euler(new Vector3(angle - 90, 90, 0));
        stone.SetActive(false);
        radialAttacks[radialAttacksLastUsed].GetComponent<ParticleSystem>().Emit(5);
        yield return new WaitForSeconds(0.05f);
        radialAttacks[radialAttacksLastUsed].GetComponent<ParticleSystem>().Emit(5);
        yield return new WaitForSeconds(0.1f);
        radialAttacks[radialAttacksLastUsed].GetComponent<ParticleSystem>().Emit(10);
        radialAttacksLastUsed = (radialAttacksLastUsed + 1) % 3;
    }
    IEnumerator OnCompleteAngularAttack()
    {
        while (animGuardia.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            //Debug.Log("Wait idle end...");
            yield return null;
        }
        /* while (!animGuardia.GetCurrentAnimatorStateInfo(0).IsName("idle"))
        {
            //Debug.Log("Wait idle begin...");
            yield return null;
        } */
        //Debug.Log("OnCompleteAngularAttack ending...");

        Debug.Log("Particle startRotation: " + angularAttacks[angularAttacksLastUsed].GetComponent<ParticleSystem>().startRotation);

        angularAttacks[angularAttacksLastUsed].transform.rotation = Quaternion.Euler(new Vector3(angle - 90, 90, 0));
        angularAttacks[angularAttacksLastUsed].GetComponent<ParticleSystem>().Emit(20);
        angularAttacksLastUsed = (angularAttacksLastUsed + 1) % 3;
    }
}
