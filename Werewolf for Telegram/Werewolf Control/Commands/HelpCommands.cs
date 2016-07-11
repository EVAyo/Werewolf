﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Database;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Werewolf_Control.Attributes;
using Werewolf_Control.Helpers;

namespace Werewolf_Control
{
    public static partial class Commands
    {
        [Command(Trigger = "grouplist")]
        public static void GroupList(Update update, string[] args)
        {
            //var reply = "";
            //using (var db = new WWContext())
            //{
            //    reply = Enumerable.Aggregate(db.v_PreferredGroups, "", (current, g) => current + $"{GetLanguageName(g.Language)}{(String.IsNullOrEmpty(g.Description) ? "" : $" - {g.Description}")}\n<a href=\"{g.GroupLink}\">{g.Name}</a>\n\n");
            //}
            //try
            //{
            //    var result = Bot.Api.SendTextMessage(update.Message.From.Id, reply, parseMode: ParseMode.Html, disableWebPagePreview: true).Result;
            //    if (update.Message.Chat.Type != ChatType.Private)
            //        Send(GetLocaleString("SentPrivate", GetLanguage(update.Message.From.Id)), update.Message.Chat.Id);
            //}
            //catch (Exception e)
            //{
            //    Send(GetLocaleString("StartPM", GetLanguage(update.Message.Chat.Id)), update.Message.Chat.Id);
            //}

            //new method, fun times....
            //var groups = PublicGroups.GetAll();
            //now determine what languages are available in public groups.
            var langs = PublicGroups.GetBaseLanguages();
            //create a menu out of this
            List<InlineKeyboardButton> buttons = langs.OrderBy(x => x).Select(x => new InlineKeyboardButton(x, $"groups|{update.Message.From.Id}|{x}|null|base")).ToList();

            var baseMenu = new List<InlineKeyboardButton[]>();
            for (var i = 0; i < buttons.Count; i++)
            {
                if (buttons.Count - 1 == i)
                {
                    baseMenu.Add(new[] { buttons[i] });
                }
                else
                    baseMenu.Add(new[] { buttons[i], buttons[i + 1] });
                i++;
            }

            var menu = new InlineKeyboardMarkup(baseMenu.ToArray());


            try
            {
                var result = Bot.Api.SendTextMessage(update.Message.From.Id,
                    GetLocaleString("WhatLangGroup", GetLanguage(update.Message.From.Id)),
                    replyMarkup: menu).Result;
                if (update.Message.Chat.Type != ChatType.Private)
                    Send(GetLocaleString("SentPrivate", GetLanguage(update.Message.From.Id)), update.Message.Chat.Id);
            }
            catch
            {
                Send(GetLocaleString("StartPM", GetLanguage(update.Message.Chat.Id)), update.Message.Chat.Id);
            }

        }

        [Command(Trigger = "rolelist")]
        public static void RoleList(Update update, string[] args)
        {
            var lang = GetLanguage(update.Message.Chat.Id);
            var reply =
                "/AboutVG - Villager\n/AboutSeer - Seer\n/AboutWw - Werewolf\n/AboutHarlot - Harlot\n/AboutDrunk - Drunk\n/AboutCursed - Cursed\n/AboutTraitor - Traitor\n/AboutGA - Guardian Angel\n/AboutDetective - Detective\n/AboutGunner - Gunner\n/AboutTanner - Tanner\n/AboutFool - Fool\n/AboutCult - Cultist\n/AboutCH - Cultist Hunter\n/AboutWC - Wild Child\n/AboutAppS - Apprentice seer\n/AboutBH - Beholder\n/AboutMason - Mason\n/AboutDG - Doppelgänger\n/AboutCupid - Cupid\n/AboutHunter - Hunter\n/AboutSK - Serial Killer";
            try
            {
                var result = Bot.Api.SendTextMessage(update.Message.From.Id, reply).Result;
                if (update.Message.Chat.Type != ChatType.Private)
                    Send(GetLocaleString("SentPrivate", GetLanguage(update.Message.From.Id)), update.Message.Chat.Id);
            }
            catch (Exception e)
            {
                Send(GetLocaleString("StartPM", GetLanguage(update.Message.Chat.Id)), update.Message.Chat.Id);
            }

        }
    }
}
