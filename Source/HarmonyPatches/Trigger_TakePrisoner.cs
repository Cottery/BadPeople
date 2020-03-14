﻿using HarmonyLib;
using RimWorld;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Verse;

namespace BadPeople.HarmonyPatches
{
    [HarmonyPatch(typeof(Pawn_GuestTracker), "SetGuestStatus")]
    static class Trigger_TakePrisoner
    {
        static void Prefix(Pawn_GuestTracker __instance, Faction newHost, bool prisoner)
        {
            if (prisoner == true && newHost != null && (newHost != __instance.HostFaction || !__instance.IsPrisoner))
            {
                var pawnProperty = typeof(Pawn_GuestTracker).GetField("pawn", BindingFlags.Instance | BindingFlags.NonPublic);
                BadPeopleUtility.NotifyPawnGetCaptured(pawnProperty.GetValue(__instance) as Pawn);
            }
        }
    }
}
