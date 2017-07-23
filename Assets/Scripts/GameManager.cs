using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    public PuffManager puffManager;
    public GPSManager gpsManager;
    public Text timeText;
    public static GameManager instance = null;

    int timeActive;
    int foodTime = 120;
    int day;
    int percentTimeActive;
    bool move;
    DateTime time;

    // Use this for initialization
    void Start ()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DateTime time = DateTime.Now;
        Application.runInBackground = true;
        timeActive = 0;
        day = time.Day;
        move = false;

        puffManager = GetComponent<PuffManager>();
        gpsManager = GetComponent<GPSManager>();

        StartCoroutine(DayTimer());
        StartCoroutine(Move());
    }

    IEnumerator DayTimer()
    {
        while (true)
        {
            time = DateTime.Now;

            if (day != time.Day)
            {
                day = time.Day;
                foodTime = 1200;
            }
            puffManager.updateFood(-1);
            yield return new WaitForSeconds(60f);
        }
    }

    IEnumerator Move()
    {
        while (true)
        {
            move = gpsManager.Moving();
            if (move)
            {
                GPSupdate();
            }
            yield return new WaitForSeconds(10f);
        }
    }

    private void GPSupdate()
    {
        timeActive = timeActive + 10;

        percentTimeActive = (timeActive / foodTime) * 100;

        timeText.text = (percentTimeActive).ToString() + " progress towards next food";

        if (timeActive >= foodTime)
        {
            timeActive = 0;
            foodTime = foodTime * 2;
            puffManager.updateFood(12);
        }
    }



    // Update is called once per frame
    void Update ()
    {
        puffManager.Moving(move);
        puffManager.Grow();

    }
}
