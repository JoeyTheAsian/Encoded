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
<b>Joey Tong - 2016/11/24 23:56 - UI Integration</b>
<ul>
<li>Fixed dialogue engine integration</li>
<li>Added UI textures, fixed resizing issues</li>
<li>Added new cursor texture</li>
<li>Other minor changes to  UI, and character rendering</li>
</ul>
