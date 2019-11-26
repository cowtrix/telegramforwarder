# WHAT THIS DOES

This will run a Telegram Bot that will receive messages, and forward them
to a list of Telegram contacts that have subscribed to it.

# GETTING STARTED

## 1 - SETTING UP YOUR NEW BOT TO RECEIVE MESSAGES

The first thing you need to do is create a new Telegram Bot that is going
to receive these messages. This is very easy to do and you do it just by
using Telegram itself to talk to a Bot called the "BotFather".

Read the guide on how to do this here:
https://core.telegram.org/bots#6-botfather

You'll give the Bot a name, and you'll then be able to add that new Bot
as a contact and send it messages.

When you make the bot, the BotFather will send you a message with a "token"
in it. A token is a code that you use to control the Bot you have just made.
It looks something like this:

	681229787:AAFZx6lbU3I6o8r6tTCN1TpZeI_xpKkD5rE

It's extremely important that you don't share this token. If someone has this
token, they will be able to control your new Bot. On the other hand, if you
lose it, you won't be able to control the Bot anymore. So keep it secret,
keep it safe!

## 2 - EDITING CONFIG.JSON

There will be a file called "config.json" next to this text file. Open it up
with a text editor like Notepad. You will see something like this:

	{
		"TelegramAPIKey": "",
		"Password": "",
		"ResponseMessage": ""
	}

The token you just received from the BotFather needs to go inside the quotes
to the right of "TelegramAPIKey".

You then need to pick a password, which needs to be longer than 5 characters.
We'll explain what this password is for soon.

Finally, write the message that you want the Bot to reply to people when
they send it a message.

So, for instance, a filled out config file might look like this:

	{
		"TelegramAPIKey": "681229787:AAFZx6lbU3I6o8r6tTCN1TpZeI_xpKkD5rE",
		"Password": "wobblyleopard",
		"ResponseMessage": "We've received your message, thanks!"
	}

Save the file and close your text editor.

## 3 - RUN TELEGRAMFORWARDER.EXE

Now you are ready to run the program itself. Just run the file:

	telegramforwarder.exe

Which will be located next to this file. If a window comes up and then
disappears, try to go through the earlier steps again. The window should
stay up and show when people send the Bot messages.

All you have to do is leave this program running on a computer that has
internet access. If the computer restarts, just rerun the program.

# SUBSCRIBING

So you've got the program running, and now you want to receive any
messages that other users send to it. Easy! All you need to do is send
it a message that says:

	SUBSCRIBE <the password you chose>

Replace the second bit with the password you put in your config.json,
so in the example case it would:

	SUBSCRIBE wobblyleopard

The Bot will tell you that you are subscribed. Now, when anyone else
messages the Bot, it will forward that message to you and to anyone
else who has subscribed to it.

# SUPPORT

Contact seandgfinnegan@gmail.com for support