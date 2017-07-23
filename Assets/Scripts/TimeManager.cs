using System.Collections;
using System;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    private
        int Day;

	IEnumerator Timer () {
        DateTime time = DateTime.Now;
        yield return new WaitForSecondsRealtime(60f);
    }
}
