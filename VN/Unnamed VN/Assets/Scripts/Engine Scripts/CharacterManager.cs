using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CharacterManager : MonoBehaviour {
    public List<GameObject> characters = new List<GameObject>();
	// Use this for initialization
	void Start () {
        AddCharacter("Heroine");
        AddCharacter("Heroine");
        AddCharacter("Heroine");
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
        characters.Add(Instantiate<GameObject>(Resources.Load("Prefabs/" + name) as GameObject));
    }
}
