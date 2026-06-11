using System;
using UnityEngine;

public class MachineCupInteractor : MonoBehaviour
{
    public CoffeeExercise coffeeExercise;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CoffeCup"))
        {
            coffeeExercise.CupInserted();
        }
    }
}
