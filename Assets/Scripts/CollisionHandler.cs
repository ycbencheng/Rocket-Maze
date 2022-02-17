using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour {
  [SerializeField] float levelLoadDelay = 3f;
  [SerializeField] AudioClip collision;
  [SerializeField] AudioClip success;

  AudioSource audioSource;

  bool isTransitioning = false;

  void Start() {
    audioSource = GetComponent<AudioSource>();
  }

  void OnCollisionEnter(Collision other) {
    if (isTransitioning) {
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
    GetComponent<Movement>().enabled = false;
    Invoke("LoadNextLevel", levelLoadDelay);
  }

  void CrashSequence() {
    isTransitioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(collision);
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
