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
<b>Joey Tong - 2016/10/28 8:45pm - Engine Architecture and Implementation</b>
<ul>
<li>Added Fade in effect FadeIn(float duration) for backgrounds</li>
<li>Added ChangeBackground(String) functionality that switches the active background</li>
<li>Added MakeTransparent() that makes backgrounds transparent</li>
<li>Added GameManager object and subobjects for clearer engine architecture</li>
</ul>

