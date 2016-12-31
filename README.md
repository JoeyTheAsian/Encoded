# Visual-Novel-Project-2016
<b>Joey Tong - 2016/9/19 - 4:29pm - 'Project Created'</b>
<ul>
  <li>Please use this format for logging changes from now on</li>
  <li>Project Schedule will be added shortly</li>
</ul>
<br>
<b>Ziwei - 2016/10/12 -  Started the graphic and sound system with backgrounds and tachi-e</b><br>
<br>
<b>Sean Yip - 2016/10/18 08:21 - Scripting: Add Ren'Py-like dialogue and narration</b>
<ul>
  <li>Dialogue and narration are parsed and displayed but since text does not wait for user input, only last piece of text is visible</li>
</ul>
<b>Sean Yip - 2016/10/18 14:46 - Scripting: Add classes and update tests</b>
<ul>
  <li>Classes: Character and DialogueAndNarration</li>
  <li>Tests: Script is parsed into a `List&ltobject&gt`. Although commands in List are executed sequentially, variable name `programCounter` is chosen to hint at branches and jumps.</li>
</ul>
<b>Joey Tong - 2016/10/20 - 4:43pm - 'Functional Specification'</b>
<ul><li>Added functional specification document</li></ul>
<b> Joey Tong- 2016/10/22 - 7:12pm - 'Added screen resolution change for backgrounds'</b><br>
<br>
<b>Sean Yip - 2016/10/25 21:08 - Scripting: Add character definitions</b>
<ul>
  <li>Example: `define S  = Character("Sylvie")`</li>
  <li>2016-10-19 20:51 - Preliminary code tested on https://repl.it/EA6v/10</li>
  <li>Date listed above - Working version with light testing
</ul>
<b>Sean Yip - 2016/10/26 08:20 - Scripting: Stop on invalid line</b>
<ul>
  <li>Previous behavior was continue</li>
</ul>
<b>Sean Yip - 2016/10/26 11:30 - Scripting: jump, label, return, and ToString</b>
<ul>
  <li>Add support for jump, label, and return</li>
  <li>Add ToString to classes</li>
</ul>
<b>Joey Tong - 2016/10/26 12:38pm - Background integration</b>
<ul><li>Added backgrounds to scripting scene</li>
<li>Fixed camera rendering</li>
<li>Changed camera display mode from Perspective to orthographic</li>
<li>Added placeholder background and script</li>
</ul>
<b>Joey Tong - 2016/10/28 20:46 - Engine Architecture and Implementation</b>
<ul>
<li>Added Fade in effect FadeIn(float duration) for backgrounds</li>
<li>Added ChangeBackground(String) functionality that switches the active background</li>
<li>Added MakeTransparent() that makes backgrounds transparent</li>
<li>Added GameManager object and subobjects for clearer engine architecture</li>
</ul>
<b>Qizhi Zhao - 2016-10-30 13:14 - type one by one text </b>
<ul>
<li>Changed the update and start function </li>
</ul>
<b>Joey Tong - 2016/11/1 23:30 - UI, Rendering, Architecture, Audio</b>
<ul>
<li>Fixed QiZhi's implementation of animated text to be compatible with less than 10ms char buffers and use memory more efficiently</li>
<li>Current implementation of scrolling text will also not freeze the thread, previous implementation used the wait method</li>
<li>Added Dialogue box autosize, adjustable margins, opacity</li>
<li>Added basic audio and draft BGM for computer room</li>
<li>Fixed implementation of Text to inherit size from parent container, with a margin</li>
<li>Sorted scripts into separate folders for UI and Engine</li>
</ul>
<b>Hong Lam To - 2016/11/4 23:00 - UI</b>
<ul>
<li>Added Menu button at top-left corner</li>
<li>Added MenuManager.cs, Menu.cs for trigger UI animation</li>
<li>Added Main Menu (side menu) slide in from right</li>
<li>Added Return, Save, Load, Option, Exit buttons in Main Menu (no function, except Return button)</li>
</ul>
<b>Joey Tong - 2016/11/12 23:33 - Background Transition system, sound effects system</b>
<ul>
<li>Streamlined animation system, can change background with several transitions with a single method call</li>
<li>Changed background rendering to dual background system, active/inactive background</li>
<li>Added text blip sounds</li>
<li>Other minor animation and sound related fixes/implementation</li>
</ul>
<b>Sean Yip - 2016/11/12 15:34 - Click to advance dialogue and narration</b>
<ul>
	<li>Text scrolling variables not properly reset to display next text</li>
	<li>"return" triggers nothing</li>
</ul>
<b>Joey Tong - 2016/11/22 23:56 - Character Rendeirng</b>
<ul>
<li>Added Character rendering and animation framework</li>
<li>Cleaned up asset directories</li>
</ul>
<b>Joey Tong - 2016/11/24 17:38 - UI Integration</b>
<ul>
<li>Fixed dialogue engine integration</li>
<li>Added UI textures, fixed resizing issues</li>
<li>Added new cursor texture</li>
<li>Other minor changes to  UI, and character rendering</li>
</ul>
<b>Sean Yip - 2016/11/24 17:38 - Maintenance</b>
<ul>
	<li>Delete Character's inheritance of MonoBehavior</li>
	<li>Fix multiple calls to Scripting.Start</li>
	<li>Use the actual script</li>
	<li>Scripting: Add comments</li>
	<li>Scripting: Fix Scripting.Next multiple increment of programCounter</li>
</ul>
<b>Sean Yip - 2016/11/25 17:47 - Scripting: add play</b>
<ul>
	<li>Play audio</li>
</ul>
<b>Hong Lam To - 2016/11/27 02:21 - add scene- UI_Main Menu, UI_In Game</b>
<ul>
	<li>added UI_Main Menu, UI_Main Menu</li>
	<li>UI_In Game: clone the buttons from scene "Scripting branched", fixed .cs, animator</li>
	<li>UI_Main Menu: </li>
	<li>Added Slanted Bar (animation)</li>
	<li>Added Audio Spectrum Visualization (script)</li>
	<li>Added Dots (script)</li>
	<li>Added Button Panel (animation)</li>
	<li>Added Center (canvas)</li>
	<li>Added Spectrum Bars (script)</li>
	<li>UI_Main Menu: button- no function</li>
	<li>UI_In Game: button (Save, Load, Exit)- no function</li>
	<li>UI_In Game: Option panel- (blank)no function, no clue</li>
	<li>UI_In Game: Missing Save, Load panels</li>
</ul>
<b>Sean Yip - 2016/11/27 14:57 - Scripting: maintenance</b>
<ul>
	<li>Scripting.Next can loop on its own, not through DialogueManager</li>
	<li>Fix DialogueManager.bufferText NullReference if first command is not dialogue and narration</li>
	<li>Add looping on "play music" and not "play sound"</li>
</ul>
<b>Sean Yip - 2016/11/27 18:53 - Scripting: add background</b>
<ul>
	<li>Add backgrounds through "image" and "scene"</li>
</ul>
<b>Sean Yip - 2016/11/27 21:34 - Scripting: add character and show</b>
<ul>
	<li>"Show" displays a character</li>
</ul>
<b>Sean Yip - 2016/11/27 22:42 - Scripting: add hide and return, and maintenance</b>
<ul>
	<li>"Hide" removes character from display</li>
	<li>"Return" will stop game in editor</li>
</ul>
<b>Joey Tong - 2016/11/27 23:50 - Character Animations</b>
<ul>
<li>Added character animation assets</li>
<li>Implemented character animations blinking and breathing</li>
<li>Heroine blinks randomly every 4-10 seconds</li>
<li>Implemented "add character" and "remove character" functions in CharacterManager</li>
</ul>
<b>Sean Yip - 2016/11/28 10:36 - Scripting: add scene with transition</b>
<ul>
	<li>The only supported transition is "fade"</li>
</ul>
<b>Sean Yip - 2016/11/28 23:47 - Scripting: new lines</b>
<ul>
	<li>"\n" for new line</li>
	<li>Dialogue and narration always start on same line, regardless of character name</li>
</ul>
<b>Joey Tong - 2016/11/28 23:51 - Menu, animations, effects, bug fixes</b>
<ul>
<li>Added scene 2 to script.txt</li>
<li>Added menu UI textures and new animations</li>
<li>Added menu button sounds</li>
<li>Replaced computer lab music with live performed recording</li>
<li>Fixed positioning bug with character autopositioning</li>
<li>Minor bug fixes to scripting.cs</li>
</ul>
<b>Hong Lam To - 2016/12/25 20:00 - Multi scene</b>
<ul>
<li>Code updated SceneLoader.cs</li>
</ul>
<b>Hong Lam To - 2016/12/28 01:00 - Config Menu</b>
<ul>
<li>Added "SceneLoader_InGame.cs" in scene "UI_In Game" "SceneManager"</li>
<li>Added Canvas "Config Menu" with Animation in scene "UI_Main Menu"</li>
<li>Added GameObject (Config Menu): Screen Resolution, Master Volume, Music Volume, SFX Volume, Message Speed,</li>
<li>	Auto Mode Message Speed, (2) On/Off switch (*no function)</li>
<li>Updated "Option Menu" in scene "UI_In Game"</li>
<li>Added function "Quit" button in scene "UI_In Game"</li>
</ul>
<b>Joey Tong - 2016/12/30 22:55 - Tidying up</b>
<ul>
<li>Fixed corrupt background file</li>
<li>Added some animation framework for character</li>
<li>Remastered music track</li>
</ul>
