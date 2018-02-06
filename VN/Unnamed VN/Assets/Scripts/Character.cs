using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System;

public class Character {
	public string Name {get; set;}
	
	public override string ToString() {
		return string.Format("Character {{Name = {0}}}", Name);
	}
}
