using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public void JumpAnimation() {

    }
    public void AutoSize() {

    }
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
