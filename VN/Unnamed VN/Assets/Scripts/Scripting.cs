using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Scripting : MonoBehaviour {
    //static save data of current session
    public static List<string> choiceData = new List<string>();

    AudioManager audioManager;
	BackgroundManager backgroundManager;
	CharacterManager characterManager;
	DialogueManager dialogueManager;
	List<object> commands = new List<object>();
	int programCounter = 0;
	Dictionary<string, Character> identifiers = new Dictionary<string, Character>();
	//Dictionary<string, string> images = new Dictionary<string, string>();
	Dictionary<string, int> labels = new Dictionary<string, int>();
	Dictionary<string, BackgroundManager.transitions> transitions = new Dictionary<string, BackgroundManager.transitions>();
    static int IndexOfNonWhiteSpace(string s) {
        return IndexOfNonWhiteSpace(s, 0);
    }

    static int IndexOfNonWhiteSpace(string s, int index) {
        for (; index < s.Length; index++) {
            if (!Char.IsWhiteSpace(s[index])) {
                return index;
            }
        }
        return index;
    }

    static int IndexOfWhiteSpace(string s) {
        return IndexOfWhiteSpace(s, 0);
    }

    static int IndexOfWhiteSpace(string s, int index) {
        for (; index < s.Length; index++) {
            if (Char.IsWhiteSpace(s[index])) {
                return index;
            }
        }
        return index;
    }

    static string QuotedSubstring(string s) {
        return QuotedSubstring(s, 0);
    }

    static string QuotedSubstring(string s, int index) {
        if (s[index] != '"') {
            return null;
        }
        int startIndex = ++index;
        for (bool escape = false; ; index++) {
            index = s.IndexOf('"', index);
            if (index == -1) {
                return null;
            }
            if (s[index - 1] == '\\') {//Escaped quote
                escape = true;
                continue;
            }
            return escape ? s.Substring(startIndex, index - startIndex).Replace("\\\"", "\"") : s.Substring(startIndex, index - startIndex);
        }
    }

    //Start is called multiple times: one during initializing the scripts, and one in DialogueManager. Move initialization to a different method.
    //Call New before using this script.
    public bool New() {
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		backgroundManager = GameObject.Find("BackgroundManager").GetComponent<BackgroundManager>();
		characterManager = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();
		dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
		transitions.Add("fade", BackgroundManager.transitions.Fade);
        transitions.Add("fadeout", BackgroundManager.transitions.Fade);
        Debug.Log("----------Read----------");
		Debug.Log(System.IO.Directory.GetCurrentDirectory());
		//try {
            string[] streamReader = (Resources.Load("Script") as TextAsset).text.Split('\n');
                string line = "";
                    for (int i = 0 ; i < streamReader.Length; i++) { //Multi-line logical lines are unsupported; logical lines must begin and end on same line. See: https://www.renpy.org/doc/html/language_basics.html#logical-lines
                        line = streamReader[i];
                        line = line.Trim(); 
                        Debug.Log("\"" + line + "\"");
                        if (String.IsNullOrEmpty(line)) {
                            continue;
                        }
                        if (line[0] == '#') {
                            Debug.Log("Comment");
                            continue;
                        }
                        int index = IndexOfWhiteSpace(line);
                        string first = line.Substring(0, index);
                        int startIndex = index = IndexOfNonWhiteSpace(line, index + 1);
                        string[] arrayCommand;
                        switch (first) {
                            case "define":
                                if (index >= line.Length) {
                                    goto InsufficientTokens;
                                }
                                index = IndexOfWhiteSpace(line, index);
                                string identifier = line.Substring(startIndex, index - startIndex);
                                if (identifier[0] == '"') {
                                    Debug.LogError(string.Format("Line `{0}` contains identifier `{1}` beginning with quote", line, identifier));
                                    return false;
                                }

                                startIndex = index = IndexOfNonWhiteSpace(line, index + 1);
                                if (index >= line.Length) {
                                    goto InsufficientTokens;
                                }
                                index = IndexOfWhiteSpace(line, index);
                                string equalsSign = line.Substring(startIndex, index - startIndex);
                                if ((equalsSign.Length > 1) || !(equalsSign.Equals("="))) {
                                    Debug.LogError(string.Format("Line `{0}` does not contain equals sign", line));
                                    return false;
                                }

                                index = IndexOfNonWhiteSpace(line, index + 1);
                                if (index >= line.Length) {
                                    goto InsufficientTokens;
                                }
                                if (!line.Substring(index, Math.Min(9, line.Length - index)).Equals("Character")) {
                                    Debug.LogError(string.Format("Line `{0}` does not contain `Character`", line));
                                    return false;
                                }

                                index = index + 9; //9 is length of "Character"
                                if (index >= line.Length) {
                                    goto InsufficientTokens;
                                }
                                index = IndexOfNonWhiteSpace(line, index);
                                if (line[index] != '(') {
                                    Debug.LogError(string.Format("Line `{0}` does not contain left parenthesis after Character", line));
                                    return false;
                                }

                                index++;
                                if (index >= line.Length) {
                                    goto InsufficientTokens;
                                }
                                index = IndexOfNonWhiteSpace(line, index);
                                if (line[index] != '"') {
                                    Debug.LogError(string.Format("Line `{0}` does not contain quote after left parenthesis", line));
                                    return false;
                                }

                                string characterName = QuotedSubstring(line, index);
                                if (characterName == null) {
                                    Debug.LogError(string.Format("Line `{0}` does not contain quoted string", line));
                                    return false;
                                }

                                index += characterName.Length + 2;
                                if (index >= line.Length) {
                                    goto InsufficientTokens;
                                }
                                index = IndexOfNonWhiteSpace(line, index);
                                if (line[index] != ')') {
                                    Debug.LogError(string.Format("Line `{0}` does not contain right parenthesis after closing quote", line));
                                    return false;
                                }

                                identifiers.Add(identifier, new Character { Name = characterName });
                                break;
                            case "if":
                                startIndex = index = IndexOfNonWhiteSpace(line, index + 1);
                                if (index >= line.Length) {
                                    goto InsufficientTokens;
                                }
                                index = IndexOfWhiteSpace(line, index);
                                if (line[index] != '(') {
                                    Debug.LogError(string.Format("Line `{0}` does not contain left parenthesis after Character", line));
                                    return false;
                                }
                                index++;

                                if (index >= line.Length) {
                                    goto InsufficientTokens;
                                }
                                index = IndexOfNonWhiteSpace(line, index);
                                if (line[index] != ')') {
                                    Debug.LogError(string.Format("Line `{0}` does not contain right parenthesis after conditional", line));
                                    return false;
                                }
                                //string conditional = QuotedSubstring(line, index);
                                Debug.LogError("If statement not supported.");
                                break;
                            case "hide":
                                if (index >= line.Length) {
                                    goto InsufficientTokens;
                                }
                                string[] temp = line.Substring(startIndex, line.Length - startIndex).Split(' ');
                                if (temp.Length == 1) {
                                    commands.Add(new string[] { first });
                                } else if (temp.Length == 3) {
                                    if (temp[1] == "with") {
                                        if (!transitions.ContainsKey(temp[2])) {
                                            Debug.LogError(string.Format("Line `{0}` contains unknown transition `{1}`", line, temp[2]));
                                            return false;
                                        }
                                        commands.Add(new string[] { first, temp[0], temp[2] });
                                    } else {
                                        Debug.LogError(string.Format("Line `{0}` does not contain `with`", line));
                                        return false;
                                    }
                                } else if (temp.Length == 4) {
                                    if (temp[1] == "with") {
                                        if (!transitions.ContainsKey(temp[2])) {
                                            Debug.LogError(string.Format("Line `{0}` contains unknown transition `{1}`", line, temp[2]));
                                            return false;
                                        }
                                        if (int.Parse(temp[3]) != null) {
                                            commands.Add(new string[] { first, temp[0], temp[2], temp[3] });
                                        } else {
                                            Debug.LogError(string.Format("Line `{0}` contains invalid transition time", line));
                                        }
                                    } else {
                                        Debug.LogError(string.Format("Line `{0}` does not contain `with`", line));
                                        return false;
                                    }
                                } else {
                                    goto InsufficientTokens;
                                }
                                break;
                            /*case "image":
                                if (index >= line.Length) {
                                    goto InsufficientTokens;
                                }
                                index = line.IndexOf('=');
                                if (index == -1) {
                                    Debug.LogError(string.Format("Line `{0}` does not contain equals sign", line));
                                    return false;
                                }
                                string image = line.Substring(startIndex, index - startIndex).Trim();

                                startIndex = index = IndexOfNonWhiteSpace(line, index + 1);
                                if (index >= line.Length) {
                                    goto InsufficientTokens;
                                }
                                if (line[index] != '"') {
                                    Debug.LogError(string.Format("Line `{0}` does not contain quote", line));
                                    return false;
                                }
                                string imageName = QuotedSubstring(line, index);
                                if (imageName == null) {
                                    Debug.LogError(string.Format("Line `{0}` does not contain quoted string", line));
                                    return false;
                                }
                                images.Add(image, imageName);
                                break;*/
                            case "jump":
                                if (index >= line.Length) {
                                    goto InsufficientTokens;
                                }
                                commands.Add(new string[] { first, line.Substring(startIndex, line.Length - startIndex) });
                                break;
                            case "label":
                                index = line.IndexOf(':');
                                if (index == -1) {
                                    Debug.LogError(string.Format("Line `{0}` does not contain colon for label", line));
                                    return false;
                                } else if (index == startIndex) {
                                    Debug.LogError(string.Format("Line `{0}` contains empty label name", line));
                                    return false;
                                }

                                labels.Add(line.Substring(startIndex, index - startIndex), commands.Count);
                                break;
                            case "play":
                                //Unlike Ren'Py, no file extension required
                                if (index >= line.Length) {
                                    goto InsufficientTokens;
                                }
                                index = IndexOfWhiteSpace(line, index);
                                string play = line.Substring(startIndex, index - startIndex);
                                if ((!play.Equals("music")) && (!play.Equals("sound"))) {
                                    Debug.LogError(string.Format("Line `{0}` does not contain play `music` or `sound`", line));
                                    return false;
                                }
                                arrayCommand = new string[] { "play", play, "" };

                                startIndex = index = IndexOfNonWhiteSpace(line, index + 1);
                                if (index >= line.Length) {
                                    goto InsufficientTokens;
                                }
                                if (line[index] != '"') {
                                    Debug.LogError(string.Format("Line `{0}` does not contain quote", line));
                                    return false;
                                }
                                arrayCommand[2] = QuotedSubstring(line, index);
                                if (String.IsNullOrEmpty(arrayCommand[2])) {
                                    Debug.LogError(string.Format("Line `{0}` does not contain quoted string", line));
                                    return false;
                                }
                                commands.Add(arrayCommand);
                                break;
                            case "return":
                                commands.Add(new string[] { first });
                                break;
                            case "scene":
                                //Unlike Ren'Py, no image definition required
                                if (index >= line.Length) {
                                    goto InsufficientTokens;
                                }
                                //commands.Add(new string[] {"scene", line.Substring(startIndex, line.Length - startIndex).Trim()});
                                //string sceneName = line.Substring(startIndex, line.Length - startIndex).Trim();
                                index = IndexOfWhiteSpace(line, index + 1);
                                string sceneName = line.Substring(startIndex, index - startIndex);
                                startIndex = index = IndexOfNonWhiteSpace(line, index);
                                string transition = null;
                                if (index < line.Length) { //Optional "with"
                                    index = IndexOfWhiteSpace(line, index);
                                    string with = line.Substring(startIndex, index - startIndex);
                                    if (!with.Equals("with")) {
                                        Debug.LogError(string.Format("Line `{0}` does not contain `with`", line));
                                        return false;
                                    }
                                    startIndex = index = IndexOfNonWhiteSpace(line, index);
                                    if (index >= line.Length) {
                                        goto InsufficientTokens;
                                    }
                                    transition = line.Substring(startIndex, line.Length - startIndex).Trim();
                                    if (!transitions.ContainsKey(transition.Split(' ')[0])) {
                                        Debug.LogError(string.Format("Line `{0}` contains unknown transition `{1}`", line, transition));
                                        return false;
                                    }
                                }
                                commands.Add(new string[] { first, sceneName, transition });
                                break;
                            case "choices":
                                List<string> finalCommand = new List<string>();
                                List<string> splitLine = new List<string>(line.Split('"'));
                                
                                for (int j = 0; j < splitLine.Count; j++) {
                                    splitLine[j].Trim();
                                    if (string.IsNullOrEmpty(splitLine[j])) {
                                        splitLine.RemoveAt(j);
                                        j--;
                                    }
                                }
                                for(int k = 0; k < splitLine.Count; k+= 2) {
                                    List<string> tempLine = new List<string>(splitLine[k].Split(' '));
                                    for (int s = 0; s < tempLine.Count; s++) {
                                        tempLine[s].Trim();
                                        if (string.IsNullOrEmpty(tempLine[s])) {
                                            tempLine.RemoveAt(s);
                                            s--;
                                        }
                                    }
                                    finalCommand.AddRange(tempLine);
                                    finalCommand.Add(splitLine[k+1]);
                                }
                                commands.Add(finalCommand.ToArray());
                                break;
                            case "show":
                                //Unlike Ren'Py, no image definition required
                                if (index >= line.Length) {
                                    goto InsufficientTokens;
                                }
                                commands.Add(new string[] { first, line.Substring(startIndex, line.Length - startIndex) });
                                //Debug.Log(first + line.Substring(startIndex, line.Length - startIndex));
                                break;
                            case "stop":
                                Debug.LogWarning(first + " not supported");
                                break;
                            default:
                                if (!identifiers.ContainsKey(first) && (line[0] != '"')) {
                                    Debug.LogError(string.Format("Line `{0}` contains unknown identifier `{1}`", line, first));
                                    return false;
                                }
                                Stack<string> dialogueAndNarration = new Stack<string>(2);
                                startIndex = index = (line[0] == '"') ? 0 : index;
                                string quotedString = QuotedSubstring(line, index);
                                if (quotedString == null) {
                                    Debug.LogError(string.Format("Line `{0}` does not contain quoted string", line));
                                    return false;
                                }
                                dialogueAndNarration.Push(quotedString.Replace("\\n", "\n"));
                                index += quotedString.Length + 2;
                                index = IndexOfNonWhiteSpace(line, index);
                                if (index < line.Length) {
                                    if (line[index] != '"') {
                                        Debug.LogError(string.Format("Line `{0}` contains illegal characters between quoted strings", line));
                                        return false;
                                    }
                                    quotedString = QuotedSubstring(line, index);
                                    if (quotedString == null) {
                                        Debug.LogError(string.Format("Line `{0}` does not contain quoted string", line));
                                        return false;
                                    }
                                    dialogueAndNarration.Push(quotedString.Replace("\\n", "\n"));
                                }
                                /*for (bool escape = false; index < line.Length; index++) {
                                    if ((line[index] == '\\') && (line[index + 1] == '"')) { //Escaped quote
                                        escape = true;
                                        index += 2;
                                    }
                                    if (line[index] == '"') {
                                        dialogueAndNarration.Push(escape ? line.Substring(startIndex, index - startIndex).Replace("\\\"", "\"") : line.Substring(startIndex, index - startIndex));
                                        index = line.IndexOf('"', index + 1); //Characters between quoted strings permitted!
                                        if (index == -1) {
                                            break;
                                        }
                                        startIndex = index = index + 1;
                                    }
                                }*/
                                if (dialogueAndNarration.Count < 1) {
                                    goto InsufficientTokens;
                                }
                                DialogueAndNarration dialogueAndNarrationCommand = new DialogueAndNarration { Text = dialogueAndNarration.Pop() };
                                if (identifiers.ContainsKey(first)) {
                                    dialogueAndNarrationCommand.Character = identifiers[first];
                                } else if (dialogueAndNarration.Count >= 1) {
                                    dialogueAndNarrationCommand.Character = new Character { Name = dialogueAndNarration.Pop() };
                                }
                                commands.Add(dialogueAndNarrationCommand);
                                break;
                        }
                        continue;
                    InsufficientTokens:
                        Debug.LogError(string.Format("Line `{0}` contains insufficient tokens", line, first));
                        return false;
                    }
            /*} catch (Exception exception) {
			Debug.Log(exception);
		}*/
		Debug.Log("----------Commands----------");
		for (int i = 0; i < commands.Count; i++) {
			string commandString = "" + i + ": ";
			if (commands[i].GetType() == typeof(string[])) {
				string[] arrayCommand = (string[])commands[i];
				Debug.Log(commandString + string.Join(", ", arrayCommand));
			}
			else {
				Debug.Log(commandString + commands[i]);
			}
		}
		/*Debug.Log("----------Images----------");
		foreach (string key in images.Keys) {
			Debug.Log(key + " = " + images[key]);
		}*/
		Debug.Log("----------Labels----------");
		foreach (string key in labels.Keys) {
			Debug.Log(key + " = " + labels[key] + ": " + commands[labels[key]]);
		}
		Debug.Log("----------New completed----------");
		return true;
	}
    public void Save() {
        string fileName = "file";
        if (Resources.Load("Saves/" + fileName as string) != null) {
            //return fileName + " already exists.";
            Debug.LogError("File Already exists");
        } else {
            Debug.LogError("lol");
            StreamWriter sw;
            sw = new StreamWriter(new FileStream("Assets/Resources/Saves/" + fileName + ".txt", FileMode.Create));
            foreach (string s in choiceData) {
                sw.WriteLine(s);
            }
            sw.Close();
            //return fileName + " was successfully saved.";
        }

    }

    //Next command
    public void Next() {
		for (;;) {
			if (programCounter >= commands.Count) {
				Debug.LogWarning("programCounter >= commands.Count");
                Debug.LogWarning(programCounter);
				return;
			}
			Debug.Log(programCounter);
			if (commands[programCounter].GetType() == typeof(DialogueAndNarration)) {
				DialogueAndNarration dialogueAndNarrationCommand = (DialogueAndNarration)commands[programCounter];
				//Debug.Log(dialogueAndNarrationCommand);
				dialogueManager.SetText(((dialogueAndNarrationCommand.Character != null) ? dialogueAndNarrationCommand.Character.Name : "") + "\n\n" + dialogueAndNarrationCommand.Text);
				programCounter++;
				return;
			}
			else if (commands[programCounter].GetType() == typeof(string[])) {
				string[] arrayCommand = (string[])commands[programCounter];
				Debug.Log(string.Join(", ", arrayCommand));
				switch (arrayCommand[0]) {
					case "hide":
                        if(arrayCommand[2] == null) {
                            characterManager.RemoveCharacter(arrayCommand[1]);
                        }else if(arrayCommand[3] == null) {
                            if (arrayCommand[2].ToUpper().Equals("FADEOUT")) {
                                characterManager.GetCharacter(arrayCommand[1]).GetComponent<CharacterModel>().FadeOutInit(1f);
                            }
                        }else {
                            if (arrayCommand[2].ToUpper().Equals("FADEOUT")) {
                                characterManager.GetCharacter(arrayCommand[1]).GetComponent<CharacterModel>().FadeOutInit(int.Parse(arrayCommand[3]));
                            }
                        }
						programCounter++;
						continue;
					case "jump":
						if (labels.ContainsKey(arrayCommand[1])) {
							programCounter = labels[arrayCommand[1]];
							continue;
						}
						else {
							Debug.LogWarning(string.Format("Unknown label `{0}`", arrayCommand[1]));
							programCounter++;
							return;
						}
					case "play":
                        if (arrayCommand[1].ToUpper().Equals("MUSIC")) {
                            audioManager.ChangeMusic(arrayCommand[2], true);
                        }
                        else if(arrayCommand[1].ToUpper().Equals("SOUND")) {
                            audioManager.PlaySound(arrayCommand[2]);
                        }
						programCounter++;
						continue;
					case "scene":
						if (arrayCommand[2] == null) {
							backgroundManager.ChangeBackground(arrayCommand[1]);
						}
						else{
                            string[] trans = arrayCommand[2].Split(' ');
                            if(trans.Length == 1)
                            {
                                backgroundManager.ChangeBackground(arrayCommand[1], transitions[trans[0]]);
                            }else if(trans.Length == 2)
                            {
                                backgroundManager.ChangeBackground(arrayCommand[1], transitions[trans[0]], int.Parse(trans[1]));
                            }else
                            {
                                Debug.Log(string.Format("Excessive Transition modifiers at line '{0}'."));
                            }
                        }
						programCounter++;
						continue;
                    case "show":
                        string[] anims = arrayCommand[1].Split(' ');
                        characterManager.AddCharacter(anims[0]);
                        if(anims.Length > 1)
                        {
                            for(int i = 1; i < anims.Length; i++)
                            {
                                characterManager.StartAnimation(anims[i], anims[0]);
                            }
                        }else {
                            characterManager.StartAnimation(anims[1], anims[0]);
                        }
						programCounter++;
						continue;
                    case "choices":
                        if (!dialogueManager.choiceState && !dialogueManager.choiceBuffer) {
                            int numChoices = int.Parse(arrayCommand[1]);
                            string[] choiceTexts = new string[numChoices];
                            Debug.LogError("Choices: " + numChoices);
                            for(int l = 1; l <= numChoices; l++) {
                                choiceTexts[l-1] = arrayCommand[1 + l * 3];
                            }
                            dialogueManager.ChoiceInit(choiceTexts);
                            return;
                        }
                        else if(dialogueManager.choiceBuffer) {
                            if(dialogueManager.GetSelectedChoice() < 1 || dialogueManager.GetSelectedChoice() > 5) {
                                Debug.LogError("Invalid choice index returned: " + dialogueManager.GetSelectedChoice() + " Choice aborted");
                                programCounter++;
                                dialogueManager.ResetChoice();
                                return;
                            }else {
                                int choiceIndex = dialogueManager.GetSelectedChoice() * 3;
                                choiceData.Add("" + choiceIndex/3);
                                if (labels.ContainsKey(arrayCommand[choiceIndex])) {
                                    programCounter = labels[arrayCommand[choiceIndex]];
                                    dialogueManager.ResetChoice();
                                    continue;
                                } else {
                                    Debug.LogWarning(string.Format("Unknown label assigned to choice `{0}`", arrayCommand[choiceIndex]));
                                    programCounter++;
                                    dialogueManager.ResetChoice();
                                    return;
                                }
                            }
                        }else {
                            return;
                        }
					case "return":
						//UnityEditor.EditorApplication.isPlaying = false;
						programCounter++;
						return;
					default:
						Debug.LogWarning(string.Format("Unknown command `{0}`", arrayCommand[0]));
						programCounter++;
						continue;
				}
			}
			else {
				Debug.LogWarning(string.Format("Unknown command `{0}`", commands[programCounter]));
				programCounter++;
				continue;
			}
		}
	}
}
