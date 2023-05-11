using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;

public class Interactable : MonoBehaviour
{

    public string stationName;
    
    [SerializeField] private Canvas _canvas;
    [SerializeField] private TMP_Text _prompt;
    [SerializeField] private GameObject _panel;
    private UIManager _uim;
    //private bool _panelActive = true;
    private bool _canInteract = false;
    

    void Start()
    {
        _uim = GameObject.FindWithTag("GameController").GetComponentInChildren<UIManager>();
    }
    
    protected virtual void OnActivated()
    {
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        //draw prompt here, have overwrite script in other interactables
        if (other.gameObject.layer == gameObject.layer)
        {
            Debug.Log("Interacted");
            _canInteract = true;
            _prompt.gameObject.SetActive(true);
            _prompt.text = "Interact with " + stationName;
        }
    }

    void Update()
    {
        if (_canInteract)
        {
            if (Input.GetButtonDown("Interact"))
            {
                //invoke UI manager to enable a specified panel and disable player input + any other special cases
                _uim.OpenPanel(_panel);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //draw prompt here, have overwrite script in other interactables
        if (other.gameObject.layer == gameObject.layer)
        {
            _panel.SetActive(false);
            _canInteract = false;
            _prompt.gameObject.SetActive(false);
        }
    }
}
