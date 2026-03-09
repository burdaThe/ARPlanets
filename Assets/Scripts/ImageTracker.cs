using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ImageTracker : MonoBehaviour
{
    private ARTrackedImageManager _trackedImages;
    public GameObject[] ArPrefabs;

    List<GameObject> ARObjects = new List<GameObject>();

    public TMP_Text infoBox;

    void Awake()
    {
        _trackedImages = GetComponent<ARTrackedImageManager>();
    }

    private void Update()
    {
        OutputTracking();
    }

    private void OutputTracking()
    {
        int i = 0;
        foreach (var trackedImage in _trackedImages.trackables)
        {
            if (trackedImage.trackingState == TrackingState.Limited)
            {
                ARObjects[i].SetActive(false);
            }
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                ARObjects[i].SetActive(true);
            }
            i++;
        }

        infoBox.text = "Tracking Data: \n";
        
        foreach (var trackedImage in _trackedImages.trackables)
        {
            infoBox.text += "Image: " + trackedImage.referenceImage.name + " " + trackedImage.trackingState + "\n";
        }

    }

    void OnEnable()
    {
        _trackedImages.trackablesChanged.AddListener(OnTrackablesChanged);
    }

    void OnDisable()
    {
        _trackedImages.trackablesChanged.RemoveListener(OnTrackablesChanged);
    }

    private void OnTrackablesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> eventArgs)
    {
        // создать объект
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

        // апдейт поза объекта
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