using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenseEntityCreator : MonoBehaviour {

	public GameObject defenseEntity;
	public int totalDefenseEntities;
	public enum creationMode{Random,Quadrant,};
	public creationMode CreationMode;
	public List<GameObject> entities;

	public delegate void DefenseEntityEvent();
	public static DefenseEntityEvent OnDefenseEntityCreated;

	// Use this for initialization
	void Start () {
		CreateDefenseEntity ();
	}
	
	// Update is called once per frame
	void Update () {

	}
		
	void CreateDefenseEntity(){
		
		//Margin settings
		float distanceX = 5.6f;
        float distanceY = 2.933f;
        float marginX = 2.0f;
        float marginY = 1.0f;
        float coordX = -8.4f;
        float coordY = -4.4f;

		if (CreationMode == creationMode.Random) {
			//Creación random
			for (int i = 0; i < totalDefenseEntities; i++) {
				float posX = UnityEngine.Random.Range (-8.4f, 8.4f);
				float posY = UnityEngine.Random.Range (-4.4f, 4.4f);
				Vector3 defenseEntityPosition = new Vector3 (posX, posY, -1.0f);
				GameManager.Instance.defenseEntities.Add (GameObject.Instantiate (defenseEntity, defenseEntityPosition, Quaternion.identity));
			}
		} else if (CreationMode == creationMode.Quadrant) {
			//Creación por cuadrantes
			int i = 0;
			int j = 0;
			for (int k = 0; k < totalDefenseEntities; k++) {
                if (i == 1 && j == 1)
                {
                    k--;
                }
                else
                {
                    float minX = coordX + i * distanceX + marginX;
                    float maxX = coordX + i * distanceX + marginX + (distanceX - marginX * 2);
                    float minY = coordY + j * distanceY + marginY;
                    float maxY = coordY + j * distanceY + marginY + (distanceY - marginY * 2);

                    float posX = UnityEngine.Random.Range(minX, maxX);
                    float posY = UnityEngine.Random.Range(minY, maxY);

                    Vector3 defenseEntityPosition = new Vector3(posX, posY, -1.0f);
                    GameManager.Instance.defenseEntities.Add(GameObject.Instantiate(defenseEntity, defenseEntityPosition, Quaternion.identity));
                }
				if (i < 2) i++;
				else{
					i = 0;
					j = (j < 2) ? j + 1 : 0;

				}
			}

		}
		StartCoroutine (waitEnemiesExist());

	}

	IEnumerator waitEnemiesExist (){
		while (!EnemyCreator.enemiesCreated) yield return null;

        Debug.Log("EnemyCreator");
        if (OnDefenseEntityCreated != null)
		    OnDefenseEntityCreated ();
	}
}
