using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {
  [SerializeField] float levelLoadDelay = 3f;
  [SerializeField] AudioClip collision;
  [SerializeField] AudioClip success;
  [SerializeField] ParticleSystem collisionParticles;
  [SerializeField] ParticleSystem successParticles;

  AudioSource audioSource;

  bool isTransitioning = false;
  bool collisionDisable = false;

  void Start() {
    audioSource = GetComponent<AudioSource>();
  }
  void Update() {
    ResponseToDebugKeys();
  }

  void ResponseToDebugKeys() {
    if (Input.GetKeyDown(KeyCode.L)) {
      LoadNextLevel();
    }

    if (Input.GetKeyDown(KeyCode.C)) {
      collisionDisable = !collisionDisable;
    }
  }

  void OnCollisionEnter(Collision other) {
    if (isTransitioning || collisionDisable) {
      return;
    }

    switch (other.gameObject.tag) {
      case "Friendly":
        Debug.Log("Friendly");
      break;

      case "Finish":
        SuccessSequence();
      break;

      case "Fuel":
        Debug.Log("Fuel");
      break;

      default:
        CrashSequence();
      break;
    }
  }

  void SuccessSequence() {
    isTransitioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(success);
    successParticles.Play();
    GetComponent<Movement>().enabled = false;
    Invoke("LoadNextLevel", levelLoadDelay);
  }

  void CrashSequence() {
    isTransitioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(collision);
    collisionParticles.Play();
    GetComponent<Movement>().enabled = false;
    Invoke("ReloadLevel", levelLoadDelay);
  }


  void ReloadLevel() {
    int currentSceneindex = SceneManager.GetActiveScene().buildIndex;

    SceneManager.LoadScene(currentSceneindex);
  }

  void LoadNextLevel() {
    int currentSceneindex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneindex = currentSceneindex + 1;

    if (nextSceneindex == SceneManager.sceneCountInBuildSettings) {
      nextSceneindex = 0;
    }

    SceneManager.LoadScene(nextSceneindex);
  }
}
