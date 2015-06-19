/*
Dialogue System Bark Example - Three NPCs Bark

This folder contains a scene that demonstrates barks between three NPCs.

It uses the SendMessage(OnUse, ,<npc>) sequencer command to prompt the next speaker, 
and a variable named Line to keep track of which line the speaker should bark.

Each NPC has a Bark Trigger set to OnUse, which responds to the previous bark's
SendMessage() command. Adam also has an additional Bark Trigger set to OnStart
to get the barks started.

对话系统树皮的例子——三个npc树皮
这个文件夹包含一个场景,展示了叫三npc。
它使用SendMessage(OnUse, ,<人大>)音序器命令提示下一个演讲者,
和一个名为线跟踪的变量线演讲者应该树皮。
每个人大都有一个叫触发OnUse,这对以前的树皮
SendMessage()命令。 亚当也有一个额外的树皮触发设置为要
叫开始。
*/