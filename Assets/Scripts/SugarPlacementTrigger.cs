using UnityEngine;

public class SugarPlacementTrigger : MonoBehaviour
{
    public TeaExerciseSequence teaExercise;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sugar"))
        {
            GameObject sugarObject = other.attachedRigidbody != null
                ? other.attachedRigidbody.gameObject
                : other.gameObject;

            teaExercise.AddSugar(sugarObject);
        }
    }
}