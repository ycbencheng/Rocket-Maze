using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
  [SerializeField] float mainThrust = 1000f;
  [SerializeField] float rotationThrust = 50f;

  KeyCode spaceKey;
  KeyCode aKey;
  KeyCode dKey;

  Rigidbody rigidBody;

  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {
    spaceKey = KeyCode.Space;
    aKey = KeyCode.A;
    dKey = KeyCode.D;

    rigidBody = GetComponent<Rigidbody>();

    ProcessThrust();
    ProcessRotation();
    //
    //
  }

  void ProcessThrust() {
    if (Input.GetKey(spaceKey)) {
      rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
    }
  }

  void ProcessRotation() {
    if (Input.GetKey(aKey)) {
      ApplyRotation(rotationThrust);
    } else if (Input.GetKey(dKey)) {
      ApplyRotation(-rotationThrust);
    }
  }

  void ApplyRotation(float rotationByFrame) {
    rigidBody.freezeRotation = true;
    transform.Rotate(Vector3.forward * rotationByFrame * Time.deltaTime);
    rigidBody.freezeRotation = false;
  }
}
