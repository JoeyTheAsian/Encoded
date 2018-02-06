using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class configScript : MonoBehaviour {
    //private Animator _animator;
    private CanvasGroup _canvasGroup;
    public GameObject configObj;
    public bool IsOpen
    {
        get { return configObj.activeSelf; }
        set { configObj.SetActive(value); }
        /*get { return _animator.GetBool("IsOpen"); }
        set { _animator.SetBool("IsOpen", value); }*/
    }
    public void Start() {
        // _animator.SetBool("IsOpen", false);
        configObj.SetActive(false);
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
    }
    public void Awake()
    {
       // _animator = GetComponent<Animator>();
        //_canvasGroup = GetComponent<CanvasGroup>();
    }

    public void Update()
    {
        /*if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            _canvasGroup.blocksRaycasts = _canvasGroup.interactable = false;
        }
        else
        {
            _canvasGroup.blocksRaycasts = _canvasGroup.interactable = true;
        }*/
    }
}
