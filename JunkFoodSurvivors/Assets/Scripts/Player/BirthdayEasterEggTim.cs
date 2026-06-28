using UnityEngine;
using System;

public class BirthdayEasterEggTim : MonoBehaviour
{
    [Header("Target Date (Only ON on this exact day)")]
    public int month = 6;
    public int day = 27;
    [Header("The object that should turn on")]
    public GameObject objectToActivate;
    void Start()
    {
        DateTime today = DateTime.Today;
        if (today.Month == month && today.Day == day)
        {
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(true);
                Debug.Log("correct day hat goes on!");
            }
        }
        else
        {
            if (objectToActivate != null)
            {
                objectToActivate.SetActive(false);
                Debug.Log("wrong day hat goes out.");
            }
        }
    }
}