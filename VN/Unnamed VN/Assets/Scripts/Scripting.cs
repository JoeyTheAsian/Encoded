using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Scripting : MonoBehaviour {
	List<object> commands = new List<object>();
	int programCounter = 0;
	Dictionary<string, Character> identifiers = new Dictionary<string, Character>();
	Dictionary<string, int> labels = new Dictionary<string, int>();

	AudioManager audioManager;
	DialogueManager dialogueManager;

	//Start is called multiple times: one during initializing the scripts, and one in DialogueManager.
	//Call New before using this script.
    public void New() {
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
        Debug.Log(System.IO.Directory.GetCurrentDirectory());
		Debug.Log("----------Read----------");
        try {
            using (StreamReader streamReader = new StreamReader(@"Assets\Scripts\Script.txt")) {
                for (string line; (line = streamReader.ReadLine()) != null;) { //Multi-line logical lines are unsupported; logical lines must begin and end on same line. See: https://www.renpy.org/doc/html/language_basics.html#logical-lines
                    line = line.Trim();
                    Debug.Log("\"" + line + "\"");
                    if (String.IsNullOrEmpty(line)) {
                        continue;
                    }
                    int index = IndexOfWhiteSpace(line);
                    string first = line.Substring(0, index);
                    int startIndex = index = IndexOfNonWhiteSpace(line, index + 1);
                    switch (first) {
                        case "define":
                            if (index >= line.Length) {
                                goto InsufficientTokens;
                            }
                            index = IndexOfWhiteSpace(line, index);
                            string identifier = line.Substring(startIndex, index - startIndex);
                            if (identifier[0] == '"') {
                                Debug.LogError(string.Format("Line `{0}` contains identifier `{1}` beginning with quote", line, identifier));
                                return;
                            }

                            startIndex = index = IndexOfNonWhiteSpace(line, index + 1);
                            if (index >= line.Length) {
                                goto InsufficientTokens;
                            }
                            index = IndexOfWhiteSpace(line, index);
                            string equalsSign = line.Substring(startIndex, index - startIndex);
                            if ((equalsSign.Length > 1) || !(equalsSign.Equals("="))) {
                                Debug.LogError(string.Format("Line `{0}` does not contain equals sign", line));
                                return;
                            }

                            index = IndexOfNonWhiteSpace(line, index + 1);
                            if (index >= line.Length) {
                                goto InsufficientTokens;
                            }
                            if (!line.Substring(index, Math.Min(9, line.Length - index)).Equals("Character")) {
                                Debug.LogError(string.Format("Line `{0}` does not contain `Character`", line));
                                return;
                            }

                            index = index + 9; //9 is length of "Character"
                            if (index >= line.Length) {
                                goto InsufficientTokens;
                            }
                            index = IndexOfNonWhiteSpace(line, index);
                            if (line[index] != '(') {
                                Debug.LogError(string.Format("Line `{0}` does not contain left parenthesis after Character", line));
                                return;
                            }

                            index++;
                            if (index >= line.Length) {
                                goto InsufficientTokens;
                            }
                            index = IndexOfNonWhiteSpace(line, index);
                            if (line[index] != '"') {
                                Debug.LogError(string.Format("Line `{0}` does not contain quote after left parenthesis", line));
                                return;
                            }

                            startIndex = ++index;
                            if (index >= line.Length) {
                                goto InsufficientTokens;
                            }
                            //ESCAPED QUOTES MISSING
                            index = line.IndexOf('"', index);
                            if (index == -1) {
                                Debug.LogError(string.Format("Line `{0}` does not contain closing quote", line));
                                return;
                            }
                            string characterName = line.Substring(startIndex, index - startIndex);

                            index++;
                            if (index >= line.Length) {
                                goto InsufficientTokens;
                            }
                            index = IndexOfNonWhiteSpace(line, index);
                            if (line[index] != ')') {
                                Debug.LogError(string.Format("Line `{0}` does not contain right parenthesis after closing quote", line));
                                return;
                            }

                            identifiers.Add(identifier, new Character { Name = characterName });
                            break;
                        case "if":
                            Debug.Log("Keyword: " + first);
                            break;
                        case "image":
                            Debug.Log("Keyword: " + first);
                            break;
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
                                return;
                            } else if (index == startIndex) {
                                Debug.LogError(string.Format("Line `{0}` contains empty label name", line));
                                return;
                            }

                            labels.Add(line.Substring(startIndex, index - startIndex), commands.Count);
                            break;
                        case "play":
							if (index >= line.Length) {
								goto InsufficientTokens;
							}
							index = IndexOfWhiteSpace(line, index);
							string play = line.Substring(startIndex, index - startIndex);
							if ((!play.Equals("music")) && (!play.Equals("sound"))) {
								Debug.LogError(string.Format("Line `{0}` does not contain play \"music\" or \"sound\"", line));
								return;
							}
							string[] arrayCommand = new string[] {"play", play, ""};

							startIndex = index = IndexOfNonWhiteSpace(line, index + 1);
							if (index >= line.Length) {
								goto InsufficientTokens;
							}
							if (line[index] != '"') {
								Debug.LogError(string.Format("Line `{0}` does not contain quote", line));
								return;
							}
							startIndex = index += 1;
							for (bool escape = false; index < line.Length; index++) {
								if ((line[index] == '\\') && (line[index + 1] == '"')) { //Escaped quote
									escape = true;
									index += 2;
								}
								if (line[index] == '"') {
									arrayCommand[2] = escape ? line.Substring(startIndex, index - startIndex).Replace("\\\"", "\"") : line.Substring(startIndex, index - startIndex);
									break;
								}
							}
							commands.Add(arrayCommand);
                            break;
                        case "return": //1
                            commands.Add(new string[] { first });
                            break;
                        case "scene":
                            Debug.Log("Keyword: " + first);
                            break;
                        case "show":
                            Debug.Log("Keyword: " + first);
                            break;
                        case "stop": //1
                            Debug.Log("Keyword: " + first);
                            break;
                        default:
							//Comment
							if (line[0] == '#') {
								Debug.Log("Comment");
								break;
							}
                            //Dialogue and narration: https://www.renpy.org/doc/html/dialogue.html
                            if (!identifiers.ContainsKey(first) && (line[0] != '"')) {
                                Debug.LogError(string.Format("Line `{0}` contains unknown identifier `{1}`", line, first));
                                return;
                            }
                            Stack<string> dialogueAndNarration = new Stack<string>(2);
                            startIndex = index = ((line[0] == '"') ? 0 : index) + 1;
                            //Regular expressions can be used instead.
                            for (bool escape = false; index < line.Length; index++) {
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
                            }
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
                    return;
                }
            }
        } catch (Exception exception) {
            Debug.Log(exception);
        }
		Debug.Log("----------Commands----------");
		for (int i = 0; i < commands.Count; i++) {
			string commandString = "" + i + ": ";
			if (commands[i].GetType() == typeof(string[])) {
				string[] arrayCommand = (string[])commands[i];
				Debug.Log(commandString + string.Join(",", arrayCommand));
			}
			else {
				Debug.Log(commandString + commands[i]);
			}
		}
		Debug.Log("----------Labels----------");
		foreach (string key in labels.Keys) {
			Debug.Log(key + " = " + labels[key] + ": " + commands[labels[key]]);
		}
		Debug.Log("----------New completed----------");
    }

	//Next command
	//true: call Next again: don't have the user click for a command to jump and play sound 
	//false: do not call Next again
	public bool Next() {
		if (programCounter >= commands.Count) {
			return false;
		}
		Debug.Log(programCounter);
		if (commands[programCounter].GetType() == typeof(DialogueAndNarration)) {
			DialogueAndNarration dialogueAndNarrationCommand = (DialogueAndNarration)commands[programCounter];
			Debug.Log(dialogueAndNarrationCommand);
			dialogueManager.SetText(((dialogueAndNarrationCommand.Character != null) ? dialogueAndNarrationCommand.Character.Name + "\n\n" : "") + dialogueAndNarrationCommand.Text);
			programCounter++;
			return false;
		}
		else if (commands[programCounter].GetType() == typeof(string[])) {
			string[] arrayCommand = (string[])commands[programCounter];
			Debug.Log(string.Join(",", arrayCommand));
			switch (arrayCommand[0]) {
				case "jump":
					if (labels.ContainsKey(arrayCommand[1])) {
						programCounter = labels[arrayCommand[1]];
						return true;
					} else {
						Debug.LogWarning(string.Format("Unknown label `{0}`", arrayCommand[1]));
						programCounter++;
						return false;
					}
					break;
				case "play":
					audioManager.ChangeMusic(arrayCommand[2]);
					programCounter++;
					return true;
				case "return":
					return false;
				default:
					Debug.LogWarning(string.Format("Unknown command `{0}`", arrayCommand[0]));
					return false;
			}
		}
		else {
			Debug.LogWarning(string.Format("Unknown command `{0}`", commands[programCounter]));
			return false;
		}
		return false;
	}

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
}
