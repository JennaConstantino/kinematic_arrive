using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class KinematicArrive : MonoBehaviour
{
    
    private float moveSpeed;
    private float radiusOfSatisfaction;

    [SerializeField] private Transform myTransform;
    [SerializeField] private Transform targetTransform;

    void Start() {
        moveSpeed = 5f;
        radiusOfSatisfaction = 1f;
    }

    void Update() {
        getTarget();
        RunKinematicArrive ();
    }

    private void RunKinematicArrive () {

        // Create vector from character to target
        Vector3 towardsTarget = targetTransform.position - myTransform.position;

        // Check to see if the character is close enough to the target
        if (towardsTarget.magnitude <= radiusOfSatisfaction) {
            // Close enough to stop
            return;
        }

        // Normalize vector so we only use the direction
        towardsTarget = towardsTarget.normalized;

        // Face the target
        Quaternion targetRotation = Quaternion.LookRotation (towardsTarget);
        myTransform.rotation = Quaternion.Slerp (myTransform.rotation, targetRotation, 0.1f);

        // Move along our forward vector (the direction we're facing)
        Vector3 newPosition = myTransform.position;
        newPosition += myTransform.forward * moveSpeed * Time.deltaTime;

        myTransform.position = newPosition;

    }

    void getTarget() {
        if (Input.GetMouseButton(0)) {
            Ray dir = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast (dir, out hit)){
                if (Vector3.Distance(targetTransform.position, hit.point) > 0.1f){
                    targetTransform.position = hit.point;
                }
            }
        }
        
    }

}
