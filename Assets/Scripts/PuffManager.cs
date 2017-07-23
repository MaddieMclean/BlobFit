using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuffManager : MonoBehaviour {

    public List <PuffBall> puffBalls; 
    public GameObject puffBall;
    public int food;
    public Text foodText;

    private int x = 0, y = 0, z = 0;
    private int hasFood;
    private List<GameObject> puffsList;

    // Use this for initialization
    void Start ()
    {
        food = 12;

        puffBalls = new List<PuffBall>();
        puffsList = new List<GameObject>();
        SpawnPuffBalls(4);
        foodText.text = food.ToString() + " food";
    }

    private void SpawnPuffBalls(int numPuff)
    { 
        for(int i = 0; i < numPuff; i++)
        {
            puffsList.Add(Instantiate(puffBall, new Vector3(x, y, z), Quaternion.identity));
            x = Random.Range(- 10, 10);
            y = Random.Range(0, -3);
            z = y - 3;
        }
    }

    public void AddPuffToList(PuffBall script)
    {
        puffBalls.Add(script);
    }

    public void Grow()
    {
        for (int i = 0; i < puffBalls.Count; i++)
        {
            puffBalls[i].Mood(food);
        }
    }

    public void Moving(bool move)
    {
        for (int i = 0; i < puffBalls.Count; i++)
        {
            puffBalls[i].Moving(move);
        }
    }

    public void updateFood(int newFood)
    {
        food += newFood;
        foodText.text = food.ToString() + " food";
    }
}
