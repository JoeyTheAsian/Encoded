using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Scripting : MonoBehaviour {
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

	//Start is called multiple times: one during initializing the scripts, and one in DialogueManager. Move initialization to a different method.
	//Call New before using this script.
	public bool New() {
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
		backgroundManager = GameObject.Find("BackgroundManager").GetComponent<BackgroundManager>();
		characterManager = GameObject.Find("CharacterManager").GetComponent<CharacterManager>();
		dialogueManager = GameObject.Find("DialogueManager").GetComponent<DialogueManager>();
		transitions.Add("fade", BackgroundManager.transitions.Fade);
		Debug.Log("----------Read----------");
		Debug.Log(System.IO.Directory.GetCurrentDirectory());
		try {
			using (StreamReader streamReader = new StreamReader(@"Assets\Scripts\Script.txt")) {
				for (string line; (line = streamReader.ReadLine()) != null;) { //Multi-line logical lines are unsupported; logical lines must begin and end on same line. See: https://www.renpy.org/doc/html/language_basics.html#logical-lines
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
							Debug.LogWarning(first + " not supported");
							break;
						case "hide":
							if (index >= line.Length) {
								goto InsufficientTokens;
							}
							commands.Add(new string[] { first, line.Substring(startIndex, line.Length - startIndex) });
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
							arrayCommand = new string[] {"play", play, ""};

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
								if (!transitions.ContainsKey(transition)) {
									Debug.LogError(string.Format("Line `{0}` contains unknown transition `{1}`", line, transition));
									return false;
								}
							}
							commands.Add(new string[] {first, sceneName, transition});
							break;
						case "show":
							//Unlike Ren'Py, no image definition required
							if (index >= line.Length) {
								goto InsufficientTokens;
							}
							commands.Add(new string[] { first, line.Substring(startIndex, line.Length - startIndex) });
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
			}
		} catch (Exception exception) {
			Debug.Log(exception);
		}
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

	//Next command
	public void Next() {
		for (;;) {
			if (programCounter >= commands.Count) {
				Debug.LogWarning("programCounter >= commands.Count");
				return;
			}
			Debug.Log(programCounter);
			if (commands[programCounter].GetType() == typeof(DialogueAndNarration)) {
				DialogueAndNarration dialogueAndNarrationCommand = (DialogueAndNarration)commands[programCounter];
				Debug.Log(dialogueAndNarrationCommand);
				dialogueManager.SetText(((dialogueAndNarrationCommand.Character != null) ? dialogueAndNarrationCommand.Character.Name : "") + "\n\n" + dialogueAndNarrationCommand.Text);
				programCounter++;
				return;
			}
			else if (commands[programCounter].GetType() == typeof(string[])) {
				string[] arrayCommand = (string[])commands[programCounter];
				Debug.Log(string.Join(", ", arrayCommand));
				switch (arrayCommand[0]) {
					case "hide":
						characterManager.RemoveCharacter(arrayCommand[1]);
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
						break;
					case "play":
						audioManager.ChangeMusic(arrayCommand[2], arrayCommand[1].Equals("music"));
						programCounter++;
						continue;
					case "scene":
						if (arrayCommand[2] == null) {
							backgroundManager.ChangeBackground(arrayCommand[1]);
						}
						else {
							backgroundManager.ChangeBackground(arrayCommand[1], transitions[arrayCommand[2]]);
						}
						programCounter++;
						continue;
					case "show":
						characterManager.AddCharacter(arrayCommand[1]);
						programCounter++;
						continue;
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
		for (bool escape = false;; index++) {
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
}
