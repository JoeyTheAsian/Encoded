using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CharacterManager : MonoBehaviour {
    public List<GameObject> characters = new List<GameObject>();
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
    public void AutoPosition() {

    }
    //attempts to apply the animation passed in
    public void StartAnimation(string animation, string character) {
        GameObject c = GetCharacter(character);
        if (!c.GetComponent<CharacterModel>().StartAnimation(animation))
        {
            Debug.Log("Unable to play animation '" + animation + "' in character '" + character + "'");
        }
    }
    public void StopAnimation(string animation, string character)
    {
        GameObject c = GetCharacter(character);
        c.GetComponent<CharacterModel>().StopAnimation(animation);
    }
    public GameObject GetCharacter(string name)
    {
        return GameObject.Find(name + "(Clone)");
    }
    //autosizes all displayed characters
    public void AutoSize() {
        foreach(GameObject g in characters)
        {
            g.GetComponent<CharacterModel>().AutoSize();
        }
    }
    //attempts to add in the character passed in
    public void AddCharacter(string name)
    {
        GameObject newCharacter = Instantiate<GameObject>(Resources.Load("Prefabs/" + name) as GameObject);
        newCharacter.transform.parent = GameObject.Find("Characters").transform;
        characters.Add(newCharacter);
    }
    public void RemoveCharacter(string name)
    {
		GameObject character = GameObject.Find(name + "(Clone)");
		characters.Remove(character);
		Destroy(character);
    }
}
