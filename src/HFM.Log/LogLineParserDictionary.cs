﻿
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

using HFM.Log.Internal;

namespace HFM.Log
{
   namespace Internal
   {
      internal static class CommonLogLineParser
      {
         internal static void AddToDictionary(IDictionary<LogLineType, LogLineParser> dictionary)
         {
            dictionary.Add(LogLineType.WorkUnitProject, ParseWorkUnitProject);
            dictionary.Add(LogLineType.WorkUnitFrame, ParseWorkUnitFrame);
            dictionary.Add(LogLineType.WorkUnitCoreShutdown, ParseWorkUnitCoreShutdown);
         }

         internal static Tuple<int, int, int, int> ParseWorkUnitProject(LogLine logLine)
         {
            Match projectIdMatch;
            if ((projectIdMatch = FahLogRegex.Common.ProjectIDRegex.Match(logLine.Raw)).Success)
            {
               return Tuple.Create(
                  Int32.Parse(projectIdMatch.Groups["ProjectNumber"].Value),
                  Int32.Parse(projectIdMatch.Groups["Run"].Value),
                  Int32.Parse(projectIdMatch.Groups["Clone"].Value),
                  Int32.Parse(projectIdMatch.Groups["Gen"].Value)
               );
            }

            return null;
         }

         internal static UnitFrame ParseWorkUnitFrame(LogLine logLine)
         {
            UnitFrame frame = GetUnitFrame(logLine);
            if (frame != null)
            {
               return frame;
            }

            frame = GetGpuUnitFrame(logLine);
            if (frame != null)
            {
               return frame;
            }

            return null;
         }

         private static UnitFrame GetUnitFrame(LogLine logLine)
         {
            Debug.Assert(logLine != null);

            Match framesCompleted = FahLogRegex.Common.FramesCompletedRegex.Match(logLine.Raw);
            if (framesCompleted.Success)
            {
               var frame = new UnitFrame();

               int result;
               if (Int32.TryParse(framesCompleted.Result("${Completed}"), out result))
               {
                  frame.RawFramesComplete = result;
               }
               else
               {
                  return null;
               }

               if (Int32.TryParse(framesCompleted.Result("${Total}"), out result))
               {
                  frame.RawFramesTotal = result;
               }
               else
               {
                  return null;
               }

               string percentString = framesCompleted.Result("${Percent}");

               Match mPercent1 = FahLogRegex.Common.Percent1Regex.Match(percentString);
               Match mPercent2 = FahLogRegex.Common.Percent2Regex.Match(percentString);

               int framePercent;
               if (mPercent1.Success)
               {
                  framePercent = Int32.Parse(mPercent1.Result("${Percent}"));
               }
               else if (mPercent2.Success)
               {
                  framePercent = Int32.Parse(mPercent2.Result("${Percent}"));
               }
               // Try to parse a percentage from in between the parentheses (for older single core clients like v5.02) - Issue 36
               else if (!Int32.TryParse(percentString, out framePercent))
               {
                  return null;
               }

               // Validate the steps are in tolerance with the detected frame percent - Issue 98
               double calculatedPercent = ((double)frame.RawFramesComplete / frame.RawFramesTotal) * 100;
               // ex. [00:19:40] Completed 82499 out of 250000 steps  (33%) - Would Validate
               //     [00:19:40] Completed 82750 out of 250000 steps  (33%) - Would Validate
               // 10% frame step tolerance. In the example the completed must be within 250 steps.
               if (Math.Abs(calculatedPercent - framePercent) <= 0.1)
               {
                  frame.TimeStamp = ParseTimeStamp(logLine);
                  frame.ID = framePercent;

                  return frame;
               }

               /*** ProtoMol Only */
               // Issue 191 - New ProtoMol Projects don't report frame progress on the precent boundry.
               if (Math.Abs(calculatedPercent - (framePercent + 1)) <= 0.1)
               {
                  frame.TimeStamp = ParseTimeStamp(logLine);
                  frame.ID = framePercent + 1;

                  return frame;
               }
               /*******************/

               return null;
            }

            return null;
         }

         private static UnitFrame GetGpuUnitFrame(LogLine logLine)
         {
            Debug.Assert(logLine != null);

            Match framesCompletedGpu = FahLogRegex.Common.FramesCompletedGpuRegex.Match(logLine.Raw);
            if (framesCompletedGpu.Success)
            {
               var frame = new UnitFrame();

               frame.RawFramesComplete = Int32.Parse(framesCompletedGpu.Result("${Percent}"));
               frame.RawFramesTotal = 100; //Instance.CurrentProtein.Frames
               // I get this from the project data but what's the point. 100% is 100%.

               frame.TimeStamp = ParseTimeStamp(logLine);
               frame.ID = frame.RawFramesComplete;

               return frame;
            }

            return null;
         }

         internal static object ParseWorkUnitCoreShutdown(LogLine logLine)
         {
            Match coreShutdownMatch;
            if ((coreShutdownMatch = FahLogRegex.Common.CoreShutdownRegex.Match(logLine.Raw)).Success)
            {
               // remove any carriage returns from fahclient log lines - 12/30/11
               string unitResultValue = coreShutdownMatch.Result("${UnitResult}").Replace("\r", String.Empty);
               return WorkUnitResultString.ToWorkUnitResult(unitResultValue);
            }

            return default(WorkUnitResult);
         }

         internal static TimeSpan? GetTimeStamp(LogLine logLine)
         {
            if (logLine.TimeStamp != null)
            {
               return logLine.TimeStamp;
            }

            Match timeStampMatch;
            if ((timeStampMatch = FahLogRegex.Common.TimeStampRegex.Match(logLine.Raw)).Success)
            {
               return GetTimeStamp(timeStampMatch.Groups["Timestamp"].Value);
            }

            return null;
         }

         internal static TimeSpan? GetTimeStamp(string value)
         {
            DateTime result;
            if (DateTime.TryParseExact(value, "HH:mm:ss",
                                       DateTimeFormatInfo.InvariantInfo,
                                       DateTimeParse.Styles, out result))
            {
               return result.TimeOfDay;
            }

            return null;
         }

         internal static TimeSpan ParseTimeStamp(LogLine logLine)
         {
            if (logLine.TimeStamp != null)
            {
               return logLine.TimeStamp.Value;
            }

            Match timeStampMatch;
            if ((timeStampMatch = FahLogRegex.Common.TimeStampRegex.Match(logLine.Raw)).Success)
            {
               return ParseTimeStamp(timeStampMatch.Groups["Timestamp"].Value);
            }

            throw new FormatException(String.Format("Failed to parse time stamp from '{0}'", logLine.Raw));
         }

         internal static TimeSpan ParseTimeStamp(string value)
         {
            return DateTime.ParseExact(value, "HH:mm:ss",
                                       DateTimeFormatInfo.InvariantInfo,
                                       DateTimeParse.Styles).TimeOfDay;
         }
      }
   }

   public delegate object LogLineParser(LogLine logLine);

   public interface ILogLineParserDictionary : IReadOnlyDictionary<LogLineType, LogLineParser>
   {

   }

   namespace Legacy
   {
      public class LegacyLogLineParserDictionary : Dictionary<LogLineType, LogLineParser>, ILogLineParserDictionary
      {
         public LegacyLogLineParserDictionary()
         {
            CommonLogLineParser.AddToDictionary(this);

            Add(LogLineType.LogOpen, ParseLogOpen);
            Add(LogLineType.ClientVersion, ParseClientVersion);
            Add(LogLineType.ClientArguments, ParseClientArguments);
            Add(LogLineType.ClientUserNameTeam, ParseClientUserNameTeam);
            Add(LogLineType.ClientReceivedUserID, ParseClientReceivedUserID);
            Add(LogLineType.ClientUserID, ParseClientUserID);
            Add(LogLineType.ClientMachineID, ParseClientMachineID);
            Add(LogLineType.WorkUnitIndex, ParseWorkUnitIndex);
            Add(LogLineType.WorkUnitQueueIndex, ParseWorkUnitQueueIndex);
            Add(LogLineType.WorkUnitCallingCore, ParseWorkUnitCallingCore);
            Add(LogLineType.WorkUnitCoreVersion, ParseWorkUnitCoreVersion);
            Add(LogLineType.ClientNumberOfUnitsCompleted, ParseClientNumberOfUnitsCompleted);
            Add(LogLineType.ClientCoreCommunicationsError, ParseClientCoreCommunicationsError);
         }

         internal static object ParseLogOpen(LogLine logLine)
         {
            Match logOpenMatch;
            if ((logOpenMatch = FahLogRegex.Legacy.LogOpenRegex.Match(logLine.Raw)).Success)
            {
               string startTime = logOpenMatch.Groups["StartTime"].Value;
               return DateTime.SpecifyKind(DateTime.ParseExact(startTime,
                  "MMMM d HH:mm:ss", CultureInfo.InvariantCulture), DateTimeKind.Utc);
            }
            return null;
         }

         internal static string ParseClientVersion(LogLine logLine)
         {
            int versionIndex = logLine.Raw.IndexOf("Version", StringComparison.Ordinal) + 8;
            if (versionIndex < logLine.Raw.Length)
            {
               return logLine.Raw.Substring(versionIndex).Trim();
            }
            return null;
         }

         internal static string ParseClientArguments(LogLine logLine)
         {
            int argumentIndex = logLine.Raw.IndexOf("Arguments:", StringComparison.Ordinal) + 11;
            if (argumentIndex < logLine.Raw.Length)
            {
               return logLine.Raw.Substring(argumentIndex).Trim();
            }
            return null;
         }

         internal static Tuple<string, int> ParseClientUserNameTeam(LogLine logLine)
         {
            Match userTeamMatch;
            if ((userTeamMatch = FahLogRegex.Legacy.UserTeamRegex.Match(logLine.Raw)).Success)
            {
               return Tuple.Create(userTeamMatch.Groups["Username"].Value, Int32.Parse(userTeamMatch.Groups["TeamNumber"].Value));
            }
            return null;
         }

         // ReSharper disable once InconsistentNaming
         internal static string ParseClientReceivedUserID(LogLine logLine)
         {
            Match receivedUserIdMatch;
            if ((receivedUserIdMatch = FahLogRegex.Legacy.ReceivedUserIDRegex.Match(logLine.Raw)).Success)
            {
               return receivedUserIdMatch.Groups["UserID"].Value;
            }
            return null;
         }

         // ReSharper disable once InconsistentNaming
         internal static string ParseClientUserID(LogLine logLine)
         {
            Match userIdMatch;
            if ((userIdMatch = FahLogRegex.Legacy.UserIDRegex.Match(logLine.Raw)).Success)
            {
               return userIdMatch.Result("${UserID}");
            }
            return null;
         }

         // ReSharper disable once InconsistentNaming
         internal static object ParseClientMachineID(LogLine logLine)
         {
            Match machineIdMatch;
            if ((machineIdMatch = FahLogRegex.Legacy.MachineIDRegex.Match(logLine.Raw)).Success)
            {
               return Int32.Parse(machineIdMatch.Result("${MachineID}"));
            }
            return null;
         }

         internal static object ParseWorkUnitIndex(LogLine logLine)
         {
            Match unitIndexMatch;
            if ((unitIndexMatch = FahLogRegex.Legacy.UnitIndexRegex.Match(logLine.Raw)).Success)
            {
               return Int32.Parse(unitIndexMatch.Result("${QueueIndex}"));
            }
            return null;
         }

         internal static object ParseWorkUnitQueueIndex(LogLine logLine)
         {
            Match queueIndexMatch;
            if ((queueIndexMatch = FahLogRegex.Legacy.QueueIndexRegex.Match(logLine.Raw)).Success)
            {
               return Int32.Parse(queueIndexMatch.Result("${QueueIndex}"));
            }
            return null;
         }

         internal static object ParseWorkUnitCallingCore(LogLine logLine)
         {
            Match workUnitCallingCoreMatch;
            if ((workUnitCallingCoreMatch = FahLogRegex.Legacy.WorkUnitCallingCore.Match(logLine.Raw)).Success)
            {
               return Int32.Parse(workUnitCallingCoreMatch.Result("${Threads}"));
            }
            // not an error - not all "Calling" lines will have a "-np X" in the line
            return 0;
         }

         internal static object ParseWorkUnitCoreVersion(LogLine logLine)
         {
            Match coreVersionMatch;
            if ((coreVersionMatch = FahLogRegex.Common.CoreVersionRegex.Match(logLine.Raw)).Success)
            {
               float value;
               if (Single.TryParse(coreVersionMatch.Result("${CoreVer}").Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out value))
               {
                  return value;
               }
            }
            /*** ProtoMol Only */
            if ((coreVersionMatch = FahLogRegex.Legacy.ProtoMolCoreVersionRegex.Match(logLine.Raw)).Success)
            {
               float value;
               if (Single.TryParse(coreVersionMatch.Result("${CoreVer}").Trim(), NumberStyles.Number, CultureInfo.InvariantCulture, out value))
               {
                  return value;
               }
            }
            /*******************/
            return null;
         }

         internal static object ParseClientNumberOfUnitsCompleted(LogLine logLine)
         {
            Match completedWorkUnitsMatch;
            if ((completedWorkUnitsMatch = FahLogRegex.Legacy.CompletedWorkUnitsRegex.Match(logLine.Raw)).Success)
            {
               return Int32.Parse(completedWorkUnitsMatch.Result("${Completed}"));
            }
            return null;
         }

         internal static object ParseClientCoreCommunicationsError(LogLine logLine)
         {
            return WorkUnitResult.ClientCoreError;
         }
      }
   }

   namespace FahClient
   {
      public class FahClientLogLineParserDictionary : Dictionary<LogLineType, LogLineParser>, ILogLineParserDictionary
      {
         public FahClientLogLineParserDictionary()
         {
            CommonLogLineParser.AddToDictionary(this);

            Add(LogLineType.LogOpen, ParseLogOpen);
            Add(LogLineType.WorkUnitCoreVersion, ParseWorkUnitCoreVersion);
            Add(LogLineType.WorkUnitCoreReturn, ParseWorkUnitCoreReturn);
         }

         internal static object ParseLogOpen(LogLine logLine)
         {
            Match logOpenMatch;
            if ((logOpenMatch = FahLogRegex.FahClient.LogOpenRegex.Match(logLine.Raw)).Success)
            {
               string startTime = logOpenMatch.Result("${StartTime}");
               // Similar code found in HFM.Client.Converters.DateTimeConverter
               // ISO 8601
               DateTime value;
               if (DateTime.TryParse(startTime, CultureInfo.InvariantCulture,
                  DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out value))
               {
                  return value;
               }

               // custom format for older v7 clients
               if (DateTime.TryParseExact(startTime, "dd/MMM/yyyy-HH:mm:ss", CultureInfo.InvariantCulture,
                  DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal, out value))
               {
                  return value;
               }
            }
            return null;
         }

         internal static object ParseWorkUnitCoreVersion(LogLine logLine)
         {
            Match coreVersionMatch;
            if ((coreVersionMatch = FahLogRegex.Common.CoreVersionRegex.Match(logLine.Raw)).Success)
            {
               string coreVer = coreVersionMatch.Groups["CoreVer"].Value.Trim();
               float value;
               if (Single.TryParse(coreVer, NumberStyles.Number, CultureInfo.InvariantCulture, out value))
               {
                  return value;
               }
               // Try to parse Core Versions in the 0.#.## format
               if (coreVer.StartsWith("0."))
               {
                  if (Single.TryParse(coreVer.Substring(2), NumberStyles.Number, CultureInfo.InvariantCulture, out value))
                  {
                     return value;
                  }
               }
            }
            return null;
         }

         internal static object ParseWorkUnitCoreReturn(LogLine logLine)
         {
            Match coreReturnMatch;
            if ((coreReturnMatch = FahLogRegex.FahClient.WorkUnitCoreReturnRegex.Match(logLine.Raw)).Success)
            {
               return WorkUnitResultString.ToWorkUnitResult(coreReturnMatch.Groups["UnitResult"].Value);
            }
            return null;
         }
      }
   }
}
