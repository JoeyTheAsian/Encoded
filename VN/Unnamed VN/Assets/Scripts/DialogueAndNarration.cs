using System;

public class DialogueAndNarration {
	public Character Character {get; set;}
	public string Text {get; set;}

	public override string ToString() {
		return string.Format("DialogueAndNarration {{Character = {0}, Text = {1}}}", this.Character, Text);
	}
}
