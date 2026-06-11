using UnityEngine;

public class TeaWaterButtonTrigger : MonoBehaviour
{
    public TeaExerciseSequence teaExercise;

    private void OnTriggerEnter(Collider other)
    {
        if (teaExercise.currentStep == TeaExerciseSequence.ExerciseStep.AdicionarAgua)
        {
            teaExercise.AddWater();
        }
    }
}