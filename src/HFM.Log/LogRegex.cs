﻿
using System.Text.RegularExpressions;

namespace HFM.Log
{
   internal static class LogRegex
   {
      private const RegexOptions Options = RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.Singleline;

      internal static class Common
      {
         /// <summary>
         /// Regular Expression to match Work Unit Project string.
         /// </summary>
         internal static readonly Regex ProjectIDRegex =
            new Regex("\\[?(?<Timestamp>.{8})[\\]|:].*Project: (?<ProjectNumber>\\d+) \\(Run (?<Run>\\d+), Clone (?<Clone>\\d+), Gen (?<Gen>\\d+)\\)", Options);

         /// <summary>
         /// Regular Expression to match Core Version string.
         /// </summary>
         internal static readonly Regex CoreVersionRegex =
            new Regex("\\[?(?<Timestamp>.{8})[\\]|:].*Version (?<CoreVer>.*) \\(.*\\)", Options);

         /// <summary>
         /// Regular Expression to match Standard and SMP Clients Frame Completion Lines (Gromacs Style).
         /// </summary>
         internal static readonly Regex FramesCompletedRegex =
            new Regex("\\[?(?<Timestamp>.{8})[\\]|:].*Completed (?<Completed>.*) out of (?<Total>.*) steps {1,2}\\((?<Percent>.*)\\)", Options);

         /// <summary>
         /// Regular Expression to match Percent Style 1
         /// </summary>
         internal static readonly Regex Percent1Regex =
            new Regex("(?<Percent>.*) percent", Options);

         /// <summary>
         /// Regular Expression to match Percent Style 2
         /// </summary>
         internal static readonly Regex Percent2Regex =
            new Regex("(?<Percent>.*)%", Options);

         /// <summary>
         /// Regular Expression to match GPU2 Client Frame Completion Lines
         /// </summary>
         internal static readonly Regex FramesCompletedGpuRegex =
            new Regex("\\[?(?<Timestamp>.{8})[\\]|:].*Completed (?<Percent>[0-9]{1,3})%", Options);

         /// <summary>
         /// Regular Expression to match Machine ID string.
         /// </summary>
         internal static readonly Regex CoreShutdownRegex =
            new Regex("\\[?(?<Timestamp>.{8})[\\]|:].*Folding@home Core Shutdown: (?<UnitResult>.*)", Options);
      }

      internal static class Legacy
      {
         // ReSharper disable InconsistentNaming

         /// <summary>
         /// Regular expression to match the log opening data (client start date and time).
         /// </summary>
         internal static readonly Regex LogOpenRegex =
            new Regex(@"--- Opening Log file \[(?<StartTime>.+ ([0-1]\d|2[0-3]):([0-5]\d)(:([0-5]\d))?)(?: UTC)?\]", Options);

         /// <summary>
         /// Regular Expression to match User (Folding ID) and Team string.
         /// </summary>
         internal static readonly Regex UserTeamRegex =
            new Regex("\\[(?<Timestamp>.{8})\\] - User name: (?<Username>.*) \\(Team (?<TeamNumber>.*)\\)", Options);

         /// <summary>
         /// Regular Expression to match User ID string.
         /// </summary>
         internal static readonly Regex ReceivedUserIDRegex =
            new Regex("\\[(?<Timestamp>.{8})\\].*- Received User ID = (?<UserID>.*)", Options);

         /// <summary>
         /// Regular Expression to match User ID string.
         /// </summary>
         internal static readonly Regex UserIDRegex =
            new Regex("\\[(?<Timestamp>.{8})\\] - User ID: (?<UserID>.*)", Options);

         /// <summary>
         /// Regular Expression to match Machine ID string.
         /// </summary>
         internal static readonly Regex MachineIDRegex =
            new Regex("\\[(?<Timestamp>.{8})\\] - Machine ID: (?<MachineID>.*)", Options);

         /// <summary>
         /// Regular Expression to match Unit Index string.
         /// </summary>
         internal static readonly Regex UnitIndexRegex =
            new Regex("\\[(?<Timestamp>.{8})\\] Working on Unit 0(?<QueueIndex>[\\d]) \\[.*\\]", Options);

         /// <summary>
         /// Regular Expression to match Unit Index string.
         /// </summary>
         internal static readonly Regex QueueIndexRegex =
            new Regex("\\[(?<Timestamp>.{8})\\] Working on queue slot 0(?<QueueIndex>[\\d]) \\[.*\\]", Options);

         /*** ProtoMol Only */

         /// <summary>
         /// Regular Expression to match ProtoMol Core Version string.
         /// </summary>
         internal static readonly Regex ProtoMolCoreVersionRegex =
            new Regex("\\[(?<Timestamp>.{8})\\]   Version: (?<CoreVer>.*)", Options);

         /*******************/

         /// <summary>
         /// Regular Expression to match Completed Work Units string.
         /// </summary>
         internal static readonly Regex CompletedWorkUnitsRegex =
            new Regex("\\[(?<Timestamp>.{8})\\] \\+ Number of Units Completed: (?<Completed>.*)", Options);

         /// <summary>
         /// Regular Expression to match Calling Core string.  Only matches the up to the first three digits of "-np X".
         /// For this legacy detection we're only interested in knowing if this -np number exists and is greater than 1.
         /// </summary>
         internal static readonly Regex WorkUnitCallingCore =
            new Regex("\\[(?<Timestamp>.{8})\\].*-np (?<Threads>\\d{1,3}).*", Options);

         // ReSharper restore InconsistentNaming
      }

      internal static class FahClient
      {
         internal static readonly Regex LogOpenRegex =
            new Regex(@"\*{23} Log Started (?<StartTime>.+) \*+", Options);

         // a copy of this regex exists in HFM.Core.DataTypes
         internal static readonly Regex WorkUnitRunningRegex =
            new Regex("(?<Timestamp>.{8}):WU(?<UnitIndex>\\d{2}):FS(?<FoldingSlot>\\d{2}):.*", Options);

         internal static readonly Regex WorkUnitWorkingRegex =
            new Regex("(?<Timestamp>.{8}):WU(?<UnitIndex>\\d{2}):FS(?<FoldingSlot>\\d{2}):Starting", Options);

         internal static readonly Regex WorkUnitCoreReturnRegex =
            new Regex("(?<Timestamp>.{8}):WU(?<UnitIndex>\\d{2}):FS(?<FoldingSlot>\\d{2}):FahCore returned: (?<UnitResult>.*) \\(.*\\)", Options);

         internal static readonly Regex WorkUnitCleanUpRegex =
            new Regex("(?<Timestamp>.{8}):WU(?<UnitIndex>\\d{2}):FS(?<FoldingSlot>\\d{2}):Cleaning up", Options);
      }
   }

   internal static class UnitInfoRegex
   {
      internal static readonly Regex RegexProjectNumberFromTag =
         new Regex("P(?<ProjectNumber>.*)R(?<Run>.*)C(?<Clone>.*)G(?<Gen>.*)", RegexOptions.Compiled | RegexOptions.ExplicitCapture | RegexOptions.Singleline);
   }
}
