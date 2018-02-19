using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class CharacterManager : MonoBehaviour {
    public List<GameObject> characters = new List<GameObject>();
    public enum transitions {
        //fades in active, fades out inactive simultaneously
        FadeIn,
        FadeOut
    }
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
    public void AutoPosition() {

    }
    //attempts to apply the animation passed in
    public void StartAnimation(string animation, string character) {
        GameObject c = GetCharacter(character);
        if (!c.GetComponent<CharacterModel>().StartAnimation(animation)) {
            Debug.LogError("Unable to play animation '" + animation + "' in character '" + character + "'");
        }
    }
    public void StopAnimation(string animation, string character) {
        GameObject c = GetCharacter(character);
        c.GetComponent<CharacterModel>().StopAnimation(animation);
    }
    public GameObject GetCharacter(string name) {
        return GameObject.Find(name + "(Clone)");
    }
    //autosizes all displayed characters
    public void AutoSize() {
        foreach (GameObject g in characters) {
            g.GetComponent<CharacterModel>().AutoSize();
        }
    }
    //attempts to add in the character passed in
    public void AddCharacter(string name) {
        GameObject newCharacter = Instantiate<GameObject>(Resources.Load("Prefabs/" + name) as GameObject);
        newCharacter.transform.parent = GameObject.Find("Characters").transform;
        characters.Add(newCharacter);
        
        int charCount = characters.Count;
        for(int i = 0; i < charCount; i++) {
            characters[i].GetComponent<CharacterModel>().offsetPercentage.x = 100/(charCount + 1) * (i+1);
        }

    }
    public void RemoveCharacter(string name) {
        if (name.ToUpper() == "ALL") {
            for (int i = 0; i < characters.Count; i++) {
                Destroy(characters[i]);
                characters.Remove(characters[i]);
            }
        } else {
            GameObject character = GameObject.Find(name + "(Clone)");
            characters.Remove(character);
            Destroy(character);
        }
    }

    public void Transition(transitions t) {
        switch (t) {
            case transitions.FadeIn:

                break;
            case transitions.FadeOut:

                break;
        }
    }
    public void Transition(transitions t, int time) {
        switch (t) {
            case transitions.FadeIn:

                break;
            case transitions.FadeOut:

                break;
        }
    }
}
