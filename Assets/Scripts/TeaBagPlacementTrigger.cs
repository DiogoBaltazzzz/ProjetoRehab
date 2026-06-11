using UnityEngine;

public class TeaBagPlacementTrigger : MonoBehaviour
{
    public TeaExerciseSequence teaExercise;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TeaBag"))
        {
            GameObject teaObject = other.attachedRigidbody != null
                ? other.attachedRigidbody.gameObject
                : other.gameObject;

            teaExercise.AddTea(teaObject);
        }
    }
}