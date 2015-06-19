/*
Bark Conversation Example

This folder contains a scene that demonstrates how to run a conversation
using characters' bark UIs instead of the regular dialogue UI. In some
cases you may find this preferable to chaining barks using BarkOnDialogueEvent.

In this scene, Private Hart:
	- Now has a bark UI
	- Has an Override Display Settings component that:
		- Uses a dialogue UI named BarkDialogueUI (described below)
  		- Enables PC subtitles (so we can see the player's barks)
  		- Sets the default sequence to Delay({{end}}) so it doesn't touch the camera.

BarkDialogueUI is an empty GameObject that contains the BarkDialogueUI script. 
This implementation of IDialogueUI simply uses the participants' bark UIs to 
display lines in a conversation. If the player has multiple responses, it
automatically chooses the first response.

A new GameObject named Private Hart Conversation Trigger is the trigger for the
conversation. When the player enters the trigger area, the conversation starts.
Stop Conversation On Trigger Exit is ticked, so if the player leaves the trigger
area the conversation still stop.

The Player:
	- Now has a bark UI
	- Only disables gameplay control for the terminal and dead guard, not for
	  the conversation with Private Hart.


	  树皮谈话的例子
这个文件夹包含一个场景,演示了如何运行一个对话
使用角色的树皮UI代替常规的UI对话。在一些
情况下,你会发现这比链接使用BarkOnDialogueEvent吠叫。
在这个场景中,私人哈特:
——现在有树皮UI
——有一个覆盖显示设置组件:
——使用UI对话叫BarkDialogueUI(在下面描述)
使个人电脑字幕(所以我们可以看到玩家的叫)
设置默认序列延迟({ {结束} })因此它不碰相机。
包含BarkDialogueUI BarkDialogueUI是一个空GameObject脚本。
这个实现的IDialogueUI仅仅使用参与者的树皮ui
在一次谈话显示行。如果玩家有多个响应,它
自动选择的第一反应。
一个名为私人哈特谈话的新GameObject触发器的触发
谈话。当玩家进入触发区域,对话开始。
停止谈话引发出口,所以如果玩家离开扳机
区域对话仍然停止。
玩家:
——现在有树皮UI
——只有禁用游戏控制终端和死,不是
哈特与私人对话。


*/