using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Scripting : MonoBehaviour {
	void Awake() {
		//Syntax is Ren'Py.

		/*Planned program flow:
		Parse line-delimited commands into objects in memory.
		Sequentially execute.*/
		Debug.Log(System.IO.Directory.GetCurrentDirectory());
		List<object> commands = new List<object>(); //String can be changed to different types to represent different commands
		Dictionary<String, Character> identifiers = new Dictionary<String, Character>();
		try {
			using (StreamReader streamReader = new StreamReader(@"Assets\Script\Script.txt")) {
				for (string line; (line = streamReader.ReadLine()) != null; ) { //Multi-line logical lines are unsupported; logical lines must begin and end on same line. See: https://www.renpy.org/doc/html/language_basics.html#logical-lines
					line = line.Trim();
					Debug.Log("Line: \"" + line + "\"");
					if (String.IsNullOrEmpty(line)) {
						continue;
					}
					//Dialogue and narration: https://www.renpy.org/doc/html/dialogue.html
					if (line[0] == '"') {
						//Dialogue accepts 2 quoted strings
						string[] dialogueAndNarrationTemporary = new string[2];
						bool escape = false;
						//Regular expressions can be used instead.
						for (int startIndex = 1, index = 1; index < line.Length; index++) {
							if ((line[index] == '\\' ) && (line[index + 1] == '"')) { //Escaped quote
								escape = true;
								index += 2;
							}
							if (line[index] == '"') {
								dialogueAndNarrationTemporary[(dialogueAndNarrationTemporary[0] == null) ? 0 : 1] = escape ? line.Substring(startIndex, index - startIndex).Replace("\\\"", "\"") : line.Substring(startIndex, index - startIndex);
								if (dialogueAndNarrationTemporary[1] != null) {
									break;
								}
								int nextIndex = line.IndexOf('"', index + 1);
								if (nextIndex == -1) {
									break;
								}
								startIndex = index = nextIndex + 1;
							}
						}
						DialogueAndNarration dialogueAndNarrationCommand = new DialogueAndNarration {Text = dialogueAndNarrationTemporary[1]};
						if (dialogueAndNarrationTemporary[0] != null) {
							dialogueAndNarrationCommand.Character = new Character {Name = dialogueAndNarrationTemporary[0]};
						}
						commands.Add(dialogueAndNarrationCommand);
					}
					else {
						int index = IndexOfWhiteSpace(line);
						string first = line.Substring(0, index);
						int startIndex = index = IndexOfNonWhiteSpace(line, index + 1);
						switch (first) {
							//Keywords:
							case "define":
								if (index >= line.Length) {
									goto InsufficientTokens;
								}
								index = IndexOfWhiteSpace(line, index);
								string identifier = line.Substring(startIndex, index - startIndex);
								if (identifier[0] == '"') {
									Debug.LogWarning(String.Format("Ignoring line `{0}` containing identifier `{1}` beginning with quote", line, identifier));
									return;
								}
								Debug.Log(identifier);

								startIndex = index = IndexOfNonWhiteSpace(line, index + 1);
								if (index >= line.Length) {
									goto InsufficientTokens;
								}
								index = IndexOfWhiteSpace(line, index);
								string equalsSign = line.Substring(startIndex, index - startIndex);
								if ((equalsSign.Length > 1) || !(equalsSign.Equals("="))) {
									Debug.LogWarning(String.Format("Ignoring line `{0}` not containing equals sign", line));
									return;
								}
								Debug.Log(equalsSign);

								index = IndexOfNonWhiteSpace(line, index + 1);
								if (index >= line.Length) {
									goto InsufficientTokens;
								}
								if (!line.Substring(index, Math.Min(9, line.Length - index)).Equals("Character")) {
									Debug.LogWarning(string.Format("Ignoring line `{0}` not containing `Character`", line));
									return;
								}
								Debug.Log("Character");

								index = index + 9; //9 is length of "Character"
								if (index >= line.Length) {
									goto InsufficientTokens;
								}
								index = IndexOfNonWhiteSpace(line, index);
								if (line[index] != '(') {
									Debug.LogWarning(string.Format("Ignoring line `{0}` not containing left parenthesis after Character", line));
									return;
								}
								Debug.Log(line[index]);

								index++;
								if (index >= line.Length) {
									goto InsufficientTokens;
								}
								index = IndexOfNonWhiteSpace(line, index);
								if (line[index] != '"') {
									Debug.LogWarning(string.Format("Ignoring line `{0}` not containing quote after left parenthesis", line));
									return;
								}
								Debug.Log(line[index]);

								startIndex = ++index;
								if (index >= line.Length) {
									goto InsufficientTokens;
								}
								index = line.IndexOf('"', index);
								if (index == -1) {
									Debug.LogWarning(string.Format("Ignoring line `{0}` not containing closing quote", line));
									return;
								}
								string characterName = line.Substring(startIndex, index - startIndex);
								Debug.Log(characterName);
								Debug.Log(line[index]);

								index++;
								if (index >= line.Length) {
									goto InsufficientTokens;
								}
								index = IndexOfNonWhiteSpace(line, index);
								if (line[index] != ')') {
									Debug.LogWarning(string.Format("Ignoring line `{0}` not containing right parenthesis after closing quote", line));
									return;
								}
								Debug.Log(line[index]);

								return;
								InsufficientTokens:
									Debug.LogWarning(string.Format("Ignoring line `{0}` containing insufficient tokens", line, first));
									return;
							case "if":
								Debug.Log("Keyword: " + first);
								break;
							case "image":
								Debug.Log("Keyword: " + first);
								break;
							case "jump":
								Debug.Log("Keyword: " + first);
								break;
							case "label":
								Debug.Log("Keyword: " + first);
								break;
							case "play":
								Debug.Log("Keyword: " + first);
								break;
							case "return": //1
								Debug.Log("Keyword: " + first);
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
								Debug.Log("Identifier: " + first);
								//What to do for identifier: go to dialogue and narration.
								break;
						}
					}
				}
			}
		}
		catch (Exception exception) {
			Debug.Log(exception);
		}
		//Following section for testing, which requires a GameObject named "Dialogue and narration" with a Text component.
		//Since the following does not wait for user input to advance text, only last line of text is shown.
		Text dialogueAndNarration = GameObject.Find("Dialogue and narration").GetComponent<Text>();
		for (int programCounter = 0; programCounter < commands.Count; programCounter++) {
			//For now and this test, commands only contain DialogueAndNarration
			DialogueAndNarration dialogueAndNarrationCommand = (DialogueAndNarration) commands[programCounter];
			dialogueAndNarration.text = ((dialogueAndNarrationCommand.Character != null) ? dialogueAndNarrationCommand.Character.Name + "\n\n" : "") + dialogueAndNarrationCommand.Text;
		}
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
