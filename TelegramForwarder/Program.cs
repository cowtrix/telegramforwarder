using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace TelegramForwarder
{
	public class Config
	{
		public string TelegramAPIKey = "";
		public string Password = "";
		public string ResponseMessage = "We've received your message, thanks!";
	}

	public class Data
	{
		public List<int> Subscribers = new List<int>();
	}

	static class Program
	{
		const string CONFIG_PATH = "config.json";
		const string DATA_PATH = "data.json";
		static TelegramBotClient m_botClient;
		static Config m_config;
		static Data m_data = new Data();
		static string Username;

		static void Main(string[] args)
		{
			var configPath = Path.GetFullPath(CONFIG_PATH);
			if(!File.Exists(configPath))
			{
				File.WriteAllText("sampleconfig.json", JsonConvert.SerializeObject(new Config(), Formatting.Indented));
				throw new Exception("You have to create a file called config.json with all the settings in it. " +
					"A sample of this file has been created called 'sampleconfig.json'. " +
					"Fill in the information, rename the file, and relaunch the application.");
			}
			m_config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(configPath));
			if (string.IsNullOrEmpty(m_config.Password) || m_config.Password.Length < 5)
			{
				throw new Exception("You have to set a password of more than 5 characters");
			}

			if(File.Exists(DATA_PATH))
			{
				m_data = JsonConvert.DeserializeObject<Data>(File.ReadAllText(DATA_PATH));
			}

			m_botClient = new TelegramBotClient(m_config.TelegramAPIKey);
			var me = m_botClient.GetMeAsync().Result;
			Username = me.Username;
			m_botClient.OnMessage += Bot_OnMessage;
			m_botClient.StartReceiving();
			Console.WriteLine($"Started message forwarder for username {Username}. There are currently {m_data.Subscribers.Count} subscribers to this channel");
			while (true) ;
		}

		static async void Bot_OnMessage(object sender, MessageEventArgs e)
		{
			try
			{
				var message = e.Message.Text;
				var userID = e.Message.From.Id;

				if(m_data.Subscribers.Contains(userID))
				{
					Console.WriteLine("Ignored message because it was from an admin");
					await m_botClient.SendTextMessageAsync(userID, "You are currently subscribed to this channel and so your message has been ignored.");
					return;
				}

				// Subscribe user
				if(message.ToUpperInvariant().StartsWith("SUBSCRIBE") && message.EndsWith(m_config.Password))
				{
					AddSubscriber(userID);
					Console.WriteLine("Subscribed new user " + userID);
					await m_botClient.SendTextMessageAsync(userID, "You are successfully subscribed to " + Username);
					return;
				}
				Console.WriteLine($"Received message from {e.Message.From.FirstName}({e.Message.From.Id}), forwarding it to {m_data.Subscribers.Count} subscribers...");
				foreach(var subscriber in m_data.Subscribers)
				{
					await m_botClient.ForwardMessageAsync(subscriber, e.Message.Chat.Id, e.Message.MessageId);
				}
				await m_botClient.SendTextMessageAsync(userID, m_config.ResponseMessage);
			}
			catch (Exception exception)
			{
				Console.WriteLine(exception);
			}
		}

		private static void AddSubscriber(int user)
		{
			m_data.Subscribers.Add(user);
			SaveData();
		}

		private static void SaveData()
		{
			File.WriteAllText(DATA_PATH, JsonConvert.SerializeObject(m_data, Formatting.Indented));
		}
	}
}
