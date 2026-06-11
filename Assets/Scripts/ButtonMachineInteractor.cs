using System;
using UnityEngine;

public class ButtonMachineInteractor : MonoBehaviour
{
    public CoffeeExercise coffeeExercise;

    private void OnTriggerEnter(Collider other)
    {
        if (coffeeExercise.currentStep == CoffeeExercise.ExerciseStep.PremirBotao)
        {
            coffeeExercise.PressButton();
        }
    }
}
