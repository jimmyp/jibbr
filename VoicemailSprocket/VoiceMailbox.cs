using System.Collections.Generic;
using System.Text.RegularExpressions;
using Jabbot;
using Jabbot.Models;
using Jabbot.Sprockets;

namespace VoicemailSprocket
{
    internal class VoiceMailbox : RegexSprocket
    {
        internal const string RecordCommand = "record \'.*\'";
        public IList<string> Voicemails = new List<string>();

        public override Regex Pattern
        {
            get { return new Regex(RecordCommand);}
        }

        protected override void ProcessMatch(Match match, ChatMessage message, IBot bot)
        {
            Voicemails.Add(message.Content.Split('\'')[1]);
        }

        public void SendVoiceMails(string recipient, IBot bot)
        {
            foreach (var voicemail in Voicemails)
                bot.PrivateReply(recipient, string.Format("@{0} said '{1}'", recipient, voicemail));
        }
    }
}