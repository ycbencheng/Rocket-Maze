using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
  [SerializeField] float mainThrust = 750F;
  [SerializeField] float rotationThrust = 100f;
  [SerializeField] AudioClip mainEngine;
  [SerializeField] ParticleSystem mainThrusterParticles;

  KeyCode spaceKey;
  KeyCode aKey;
  KeyCode dKey;

  Rigidbody rigidBody;

  AudioSource audioSource;

  // Start is called before the first frame update
  void Start() {
    rigidBody = GetComponent<Rigidbody>();
    audioSource = GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update() {
    spaceKey = KeyCode.Space;
    aKey = KeyCode.A;
    dKey = KeyCode.D;

    ProcessThrust();
    ProcessRotation();
  }

  void ProcessThrust() {
    if (Input.GetKey(spaceKey)) {
      StartThrusting();
    } else {
      audioSource.Stop();
      mainThrusterParticles.Stop();
    }
  }

  void StartThrusting() {
    rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);

    if (!audioSource.isPlaying) {
      audioSource.PlayOneShot(mainEngine);
    }

    if (!mainThrusterParticles.isPlaying) {
      mainThrusterParticles.Play();
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
