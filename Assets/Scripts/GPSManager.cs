using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum LocationState
{
    Disabled,
    TimedOut,
    Failed,
    Enabled
}

public class GPSManager : MonoBehaviour
{
    public Text statusText;
    public static int SCREEN_DENSITY;
    // Approximate radius of the earth (in kilometers)
    const float EARTH_RADIUS = 6371;

    private float lowerLimit = 0.005F;
    private LocationState state;
    // Position on earth (in degrees)
    private float latitude;
    private float longitude;
    // Distance walked (in meters)
    



    // Use this for initialization
    IEnumerator Start()
    {

        state = LocationState.Disabled;
        latitude = 0f;
        longitude = 0f;
        if (Input.location.isEnabledByUser)
        {
            Input.location.Start();
            int waitTime = 15;
            while (Input.location.status == LocationServiceStatus.Initializing && waitTime > 0)
            {
                yield return new WaitForSeconds(1);
                waitTime--;
            }
            if (waitTime == 0)
            {
                state = LocationState.TimedOut;
                statusText.text = "TimedOut";
            }
            else if (Input.location.status == LocationServiceStatus.Failed)
            {
                state = LocationState.Failed;
                statusText.text = "Failed";
            }
            else
            {
                state = LocationState.Enabled;
                latitude = Input.location.lastData.latitude;
                longitude = Input.location.lastData.longitude;
            }
        }
    }

    IEnumerator OnApplicationPause(bool pauseState)
    {
        if (pauseState)
        {
            Input.location.Stop();
            state = LocationState.Disabled;
            statusText.text = "Paused";
        }
        else
        {
            Input.location.Start();
            int waitTime = 30;
            while (Input.location.status == LocationServiceStatus.Initializing && waitTime > 0)
            {
                yield return new WaitForSeconds(1);
                waitTime--;
            }
            if (waitTime == 0)
            {
                state = LocationState.TimedOut;
                statusText.text = "TimedOut";
            }
            else if (Input.location.status == LocationServiceStatus.Failed)
            {
                state = LocationState.Failed;
                statusText.text = "Failed";
            }
            else
            {
                state = LocationState.Enabled;
                latitude = Input.location.lastData.latitude;
                longitude = Input.location.lastData.longitude;
            }
        }
    }



    // The Haversine formula
    // Veness, C. (2014). Calculate distance, bearing and more between
    //  Latitude/Longitude points. Movable Type Scripts. Retrieved from
    //  http://www.movable-type.co.uk/scripts/latlong.html
    float Haversine(ref float lastLatitude, ref float lastLongitude)
    {
        float newLatitude = Input.location.lastData.latitude;
        float newLongitude = Input.location.lastData.longitude;
        float deltaLatitude = (newLatitude - lastLatitude) * Mathf.Deg2Rad;
        float deltaLongitude = (newLongitude - lastLongitude) * Mathf.Deg2Rad;
        float a = Mathf.Pow(Mathf.Sin(deltaLatitude / 2), 2) +
            Mathf.Cos(lastLatitude * Mathf.Deg2Rad) * Mathf.Cos(newLatitude * Mathf.Deg2Rad) *
            Mathf.Pow(Mathf.Sin(deltaLongitude / 2), 2);
        lastLatitude = newLatitude;
        lastLongitude = newLongitude;
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        return EARTH_RADIUS * c;
    }

    public bool Moving()
    {
        if (Input.acceleration.magnitude > lowerLimit)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    //public bool isActive()
    //{
    //    if (state == LocationState.Enabled)
    //    {
    //        float deltaDistance = Haversine(ref latitude, ref longitude) * 1000f;
    //        if (deltaDistance > 0f && Moving())
    //        {
    //            return true;
    //        }
    //    }
    //    return false;
    //}

}