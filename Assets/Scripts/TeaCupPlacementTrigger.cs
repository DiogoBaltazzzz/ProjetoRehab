using UnityEngine;

public class TeaCupPlacementTrigger : MonoBehaviour
{
    public TeaExerciseSequence teaExercise;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TeaCup"))
        {
            teaExercise.CupPlaced();
        }
    }
}