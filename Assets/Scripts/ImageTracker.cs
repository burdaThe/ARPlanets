using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracker : MonoBehaviour
{
    private ARTrackedImageManager _trackedImages;
    public GameObject[] ArPrefabs;

    List<GameObject> ARObjects = new List<GameObject>();


    void Awake()
    {
        _trackedImages = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        // Для UnityEvent используем AddListener
        _trackedImages.trackablesChanged.AddListener(OnTrackablesChanged);
    }

    void OnDisable()
    {
        // Для UnityEvent используем RemoveListener
        _trackedImages.trackablesChanged.RemoveListener(OnTrackablesChanged);
    }


    // Event Handler
    private void OnTrackablesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        //Create object based on image tracked
        foreach (var _trackedImage in eventArgs.added)
        {
            foreach (var arPrefab in ArPrefabs)
            {
                if (_trackedImage.referenceImage.name == arPrefab.name)
                {
                    var newPrefab = Instantiate(arPrefab, _trackedImage.transform);
                    ARObjects.Add(newPrefab);
                }
            }
        }

        //Update tracking position
        foreach (var _trackedImage in eventArgs.updated)
        {
            foreach (var gameObject in ARObjects)
            {
                if (gameObject.name == _trackedImage.name)
                {
                    gameObject.SetActive(_trackedImage.trackingState == TrackingState.Tracking);
                }
            }
        }

    }
}