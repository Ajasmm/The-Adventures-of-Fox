using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AndroidController : MonoBehaviour
{
    [SerializeField] Button interactionButton;
    [SerializeField] Button throwButton;

    private void Awake()
    {
        if (Application.platform != RuntimePlatform.Android) gameObject.SetActive(false);
    }
    private void OnEnable()
    {
        GameManager.Instance.androidController = this;
    }

    public void SetInteractionButton(bool state) => interactionButton.interactable = state;
    public void SetThrowButton(bool state) => throwButton.interactable = state;


}
