using UnityEngine;
using System.Collections.Generic;

public class GridCreator : MonoBehaviour {
	public GameObject tile;
	protected List<GameObject> grid = new List<GameObject>();
    public bool finished = false;
	public int width = 40;
	public int height = 20;
	void Start () {
		makeGrid ();
	}

	void makeGrid(){
		for (int y = 0; y < height; y++) {
			for (int x = 0; x < width; x++) {
				Vector3 temp = new Vector3 ();
				if (y % 2 == 0) {
					temp.Set(x, 0, y);
				} else {
					
					temp.Set (x + 0.5f, 0f, y);
				}
				grid.Add(Instantiate (tile, temp, Quaternion.Euler (0, 0, 0)) as GameObject);
				grid[grid.Count-1].transform.SetParent (gameObject.transform);
			}
		}

		int rSeed = Random.Range (0, 99999);
		Random.seed = rSeed;
		for (int i = 0; i < grid.Count; i++) {
			int rng = Random.Range (0, 10);
			int type = 0;
			if (getY(i, width) <= 2 || getY(i, width) >= 17) {
				if (rng == 0 || rng == 1) {
					type = 4;
				} else if (rng == 2) {
					type = 0;
				} else if (rng > 2) {
					type = 2;
				}
			} else if (getY(i, width) <= 5 || getY(i, width) >= 14) {
				if (rng == 0) {
					type = 4;
				} else if (rng == 1) {
					type = 5;
				} else if (rng > 1) {
					type = 0;
				}
			} else if (getY(i, width) <= 7 || getY(i, width) >= 12) {
				if (rng == 0) {
					type = 4;
				} else if (rng == 1) {
					type = 0;
				} else if (rng == 2 || rng == 3) {
					type = 5;
				} else if (rng > 3) {
					type = 1;
				}
			} else {
				if (rng == 0) {
					type = 4;
				} else if (rng == 1) {
					type = 5;
				} else if (rng == 2 || rng == 3) {
					type = 1;
				} else if (rng > 3) {
					type = 3;
				}
			}

			rng = Random.Range (0, 10);
			if (getX(i, width) <= 2 || getX(i, width) >= 37) {
				type = 4;
			} else if (getX(i, width) <= 5 || getX(i, width) >= 34) {
				if (rng > 4) {
					type = 4;
				} 
			}

			bool hill = false;
			rng = Random.Range (0, 5);
			if (type != 4 && type != 5 && rng == 0) {
				hill = true;
			} 

			bool forest = false;
			rng = Random.Range (0, 5);
			if (type == 0 || type == 1) {
				if (rng < 2) {
					forest = true;
				}
			}

			int resource = -1;

			if (type < 2) {
				rng = Random.Range (0, 100);
				if (hill && rng > 70) {
					resource = 0;
				} else if (!hill && rng > 95) {
					resource = 0;
				}
			}

			TileController setup = grid [i].GetComponentsInChildren<TileController> () [0];
			setup.tSetup (type, resource, hill, forest);

            

		}
        Debug.Log("Grid Made");
        finished = true;
	}

	protected Vector2 gettwoDPoint(int point, int gridWidth){
		Vector2 temp = new Vector2 ();
		temp.y = point / gridWidth;
		temp.x = point - (gridWidth * temp.y);
		return temp;
	}

	protected int getX(int point, int gridWidth){
		return point - (gridWidth * getY (point, gridWidth));
	}

	protected int getY(int point, int gridWidth){
		return point / gridWidth;
	}

	protected int getNo(int _x, int _y, int _width){
		return (_y*_width)+_x;
	}

	public List<GameObject> getGrid(){
		return grid;
	}

	public int getWidth(){
		return width;
	}
}
