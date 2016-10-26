using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Scripting : MonoBehaviour {
	void Awake() {
		//Syntax is Ren'Py-like.

		/*Planned program flow:
		Parse line-delimited commands into objects in memory.
		Sequentially execute.

		However, once events and interfacing with player is implemented (clicking to advance text), parsing in run-time is preferable.*/
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
								Debug.LogError(string.Format("Ignoring line `{0}` containing identifier `{1}` beginning with quote", line, identifier));
								return;
							}

							startIndex = index = IndexOfNonWhiteSpace(line, index + 1);
							if (index >= line.Length) {
								goto InsufficientTokens;
							}
							index = IndexOfWhiteSpace(line, index);
							string equalsSign = line.Substring(startIndex, index - startIndex);
							if ((equalsSign.Length > 1) || !(equalsSign.Equals("="))) {
								Debug.LogError(string.Format("Ignoring line `{0}` not containing equals sign", line));
								return;
							}

							index = IndexOfNonWhiteSpace(line, index + 1);
							if (index >= line.Length) {
								goto InsufficientTokens;
							}
							if (!line.Substring(index, Math.Min(9, line.Length - index)).Equals("Character")) {
								Debug.LogError(string.Format("Ignoring line `{0}` not containing `Character`", line));
								return;
							}

							index = index + 9; //9 is length of "Character"
							if (index >= line.Length) {
								goto InsufficientTokens;
							}
							index = IndexOfNonWhiteSpace(line, index);
							if (line[index] != '(') {
								Debug.LogError(string.Format("Ignoring line `{0}` not containing left parenthesis after Character", line));
								return;
							}

							index++;
							if (index >= line.Length) {
								goto InsufficientTokens;
							}
							index = IndexOfNonWhiteSpace(line, index);
							if (line[index] != '"') {
								Debug.LogError(string.Format("Ignoring line `{0}` not containing quote after left parenthesis", line));
								return;
							}

							startIndex = ++index;
							if (index >= line.Length) {
								goto InsufficientTokens;
							}
							//ESCAPED QUOTES MISSING
							index = line.IndexOf('"', index);
							if (index == -1) {
								Debug.LogError(string.Format("Ignoring line `{0}` not containing closing quote", line));
								return;
							}
							string characterName = line.Substring(startIndex, index - startIndex);

							index++;
							if (index >= line.Length) {
								goto InsufficientTokens;
							}
							index = IndexOfNonWhiteSpace(line, index);
							if (line[index] != ')') {
								Debug.LogError(string.Format("Ignoring line `{0}` not containing right parenthesis after closing quote", line));
								return;
							}

							identifiers.Add(identifier, new Character {Name = characterName});
							break;
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
							//Dialogue and narration: https://www.renpy.org/doc/html/dialogue.html
							if (!identifiers.ContainsKey(first) && (line[0] != '"')) {
								Debug.LogError(string.Format("Ignoring line `{0}` containing unknown identifier `{1}`", line, first));
								return;
							}
							Stack<string> dialogueAndNarration = new Stack<string>(2);
							startIndex = index = ((line[0] == '"') ? 0 : index) + 1;
							//Regular expressions can be used instead.
							for (bool escape = false; index < line.Length; index++) {
								if ((line[index] == '\\' ) && (line[index + 1] == '"')) { //Escaped quote
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
							DialogueAndNarration dialogueAndNarrationCommand = new DialogueAndNarration {Text = dialogueAndNarration.Pop()};
							if (identifiers.ContainsKey(first)) {
								dialogueAndNarrationCommand.Character = identifiers[first];
							}
							else if (dialogueAndNarration.Count >= 1) {
								dialogueAndNarrationCommand.Character = new Character {Name = dialogueAndNarration.Pop()};
							}
							commands.Add(dialogueAndNarrationCommand);
							break;
					}
					continue;
					InsufficientTokens:
						Debug.LogWarning(string.Format("Ignoring line `{0}` containing insufficient tokens", line, first));
						return;
				}
			}
		}
		catch (Exception exception) {
			Debug.Log(exception);
		}

		//Following section for testing, which requires a GameObject named "Dialogue and narration" with a Text component.
		//Since the following does not wait for user input to advance text, only last line of text is shown.
		Text dialogueAndNarrationComponent = GameObject.Find("Dialogue and narration").GetComponent<Text>();
		for (int programCounter = 0; programCounter < commands.Count; programCounter++) {
			if (commands[programCounter].GetType() == typeof(DialogueAndNarration)) {
				DialogueAndNarration dialogueAndNarrationCommand = (DialogueAndNarration)commands[programCounter];
				dialogueAndNarrationComponent.text = ((dialogueAndNarrationCommand.Character != null) ? dialogueAndNarrationCommand.Character.Name + "\n\n" : "") + dialogueAndNarrationCommand.Text;
			}
			else {
				Debug.LogWarning("Unknown command");
			}
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
