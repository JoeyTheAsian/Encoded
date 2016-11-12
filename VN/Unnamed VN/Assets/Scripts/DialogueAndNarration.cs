using System;

public class DialogueAndNarration {
	public Character Character {get; set;}
	public string Text {get; set;}
    //THIS SHOULD MOVE THE DIALOGUE TO THE NEXT LINE OF ENGINE ACTION 
    //**CAN BE DIALOGUE, OR CAN INVOKE ANIMATIONS/TRANSITIONS/ETC***
    public void Next() {

    }

    public override string ToString() {
        return string.Format("DialogueAndNarration {{Character = {0}, Text = {1}}}", this.Character, Text);
    }

}
