using BingoMode.BingoSteamworks;
using Expedition;
using MoreSlugcats;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace BingoMode.BingoChallenges
{
    public class BingoCutsceneReenact : BingoChallenge
    {
        public override void UpdateDescription()
        {
            description = ChallengeTools.IGT.Translate("Jump down pit in Outer Expanse that leads to Outskirts");
            base.UpdateDescription();
        }

        public override Phrase ConstructPhrase()
        {
            return new Phrase([
                new Icon("oe_jump", 0.75f, UnityEngine.Color.white, 180f),
                new Icon("deathpiticon", 1f, new Color(1.0f, 0.65f, 0.0f)),
                new Verse("OE_CAVE03")
                ], [1,2]);
        }

        public override bool Duplicable(Challenge challenge)
        {
            return challenge is not BingoCutsceneReenact;
        }

        public override string ChallengeName()
        {
            return ChallengeTools.IGT.Translate("Jump down OE pit");
        }

        public override Challenge Generate()
        {
            BingoCutsceneReenact ch = new BingoCutsceneReenact
            {
            };

            return ch;
        }

        public override void Update()
        {
            base.Update();
            if (completed || revealed || TeamsCompleted[SteamTest.team] || hidden) return;
            for (int i = 0; i < game.Players.Count; i++)
            {
                if (game.Players[i] != null && game.Players[i].realizedCreature is Player player && player.room != null && player.room.abstractRoom.name.ToLowerInvariant() == "oe_pump01")
                {
                    CompleteChallenge();
                    return;
                }
            }
        }

        public override int Points()
        {
            return 20;
        }

        public override bool CombatRequired()
        {
            return false;
        }

        public override bool ValidForThisSlugcat(SlugcatStats.Name slugcat)
        {
            // Scugs that can't get to OE
            if (slugcat == MoreSlugcatsEnums.SlugcatStatsName.Gourmand || slugcat == MoreSlugcatsEnums.SlugcatStatsName.Rivulet || slugcat == SlugcatStats.Name.White || slugcat == SlugcatStats.Name.Yellow)
            {
                return true;
            } return false;
        }

        public override string ToString()
        {
            return string.Concat(new string[]
            {
                "BingoCutsceneReenact",
                "~",
                completed ? "1" : "0",
                "><",
                revealed ? "1" : "0",
            });
        }

        public override void FromString(string args)
        {
            try
            {
                string[] array = Regex.Split(args, "><");
                completed = (array[0] == "1");
                revealed = (array[1] == "1");
                UpdateDescription();
            }
            catch (Exception ex)
            {
                ExpLog.Log("ERROR: BingoCutsceneReenact FromString() encountered an error: " + ex.Message);
                throw ex;
            }
        }

        public override void AddHooks()
        {
        }

        public override void RemoveHooks()
        {
        }
        public override List<object> Settings() => [];
    }
}