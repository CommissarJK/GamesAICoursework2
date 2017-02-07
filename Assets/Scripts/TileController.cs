using UnityEngine;
using System.Collections.Generic;

public class TileController : MonoBehaviour {

	public GameObject treeObj;
	public GameObject ironObj;
	protected GameObject tree;
	protected GameObject resourceModel;

	protected int owner = -1;
	protected int type;
	protected int resource = -1;
	bool hill = false;
	bool forest = false;
	bool improved = false;

	int food = 0;
	int production = 0;
	int gold = 0;

	public TileController(){}

	public void tSetup(int _type, int _resource, bool _hill, bool _forest){
		type = _type;
		resource = _resource;
		hill = _hill;
		forest = _forest;

		Color newcolour;
		Renderer colour = gameObject.GetComponentInChildren<Renderer> ();

		switch (type) {
		case 0: //grasslands
			food += 2;
			newcolour = new Color (0f, 0.8f, 0f);
			colour.material.color = newcolour;
			break;
		case 1: //plains
			food += 1;
			production += 1;

			newcolour = new Color (0.5f, 0.8f, 0f);
			colour.material.color = newcolour;
			break;
		case 2: //ice
			newcolour = new Color (0.9f, 0.9f, 0.9f);
			colour.material.color = newcolour;
			break;
		case 3: //desert 
			newcolour = new Color (0.8f, 0.8f, 0.1f);
			colour.material.color = newcolour;
			break;
		case 4: //sea
			food += 1;
			gold += 1;

			newcolour = new Color (0.2f, 0.2f, 1f);
			gameObject.transform.position+= new Vector3(0f,-0.1f,0f);
			colour.material.color = newcolour;
			break;
		case 5: //mountain
			newcolour = new Color (0.4f, 0.4f, 0.4f);
			gameObject.transform.position+= new Vector3(0f,0.45f,0f);
			colour.material.color = newcolour;
			break;

		default:
			newcolour = new Color (1f, 0f, 0f);
			break;
		}

		if (hill) {
			production += 1;
			gameObject.transform.position+= new Vector3(0f,0.2f,0f);
		}

		if (forest) {
			production += 1;
			Vector3 temp = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y+0.3f, gameObject.transform.position.z);
			tree = Instantiate (treeObj, temp, Quaternion.Euler (0, 0, 0)) as GameObject;
			tree.transform.SetParent (gameObject.transform);
		}

		switch (resource) {
		case 0: //iron
			production += 1;
			Vector3 temp = new Vector3 (gameObject.transform.position.x, gameObject.transform.position.y+0.25f, gameObject.transform.position.z);
			resourceModel = Instantiate (ironObj, temp, Quaternion.Euler (0, 0, 0)) as GameObject;
			resourceModel.transform.SetParent (gameObject.transform);
			break;

		}
	}

	void Update () {

	}
}
