﻿/*
 * HFM.NET - Options Data Class
 * Copyright (C) 2009-2016 Ryan Harlamert (harlam357)
 *
 * This program is free software; you can redistribute it and/or
 * modify it under the terms of the GNU General Public License
 * as published by the Free Software Foundation; version 2
 * of the License. See the included file GPLv2.TXT.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
 */

using System.Diagnostics;

using Newtonsoft.Json.Linq;

using HFM.Client.Converters;

namespace HFM.Client.DataTypes
{
   /// <summary>
   /// Folding@Home client options message.
   /// </summary>
   public class Options : TypedMessage
   {
      #region Properties

      #pragma warning disable 1591

      [MessageProperty("assignment-servers")]
      public string AssignmentServers { get; set; }

      [MessageProperty("capture-directory")]
      public string CaptureDirectory { get; set; }

      [MessageProperty("capture-on-error")]
      public bool? CaptureOnError { get; set; }

      [MessageProperty("capture-packets")]
      public bool? CapturePackets { get; set; }

      [MessageProperty("capture-requests")]
      public bool? CaptureRequests { get; set; }

      [MessageProperty("capture-responses")]
      public bool? CaptureResponses { get; set; }

      [MessageProperty("capture-sockets")]
      public bool? CaptureSockets { get; set; }

      [MessageProperty("cause")]
      public string Cause { get; set; }

      //[MessageProperty("certificate-file")]
      //public string CertificateFile { get; set; }

      [MessageProperty("checkpoint")]
      public int? Checkpoint { get; set; }

      [MessageProperty("child")]
      public bool? Child { get; set; }

      [MessageProperty("client-subtype")]
      public string FahClientSubType { get; set; }

      [MessageProperty("client-subtype", typeof(FahClientSubTypeConverter))]
      public FahClientSubType FahClientSubTypeEnum { get; set; }

      [MessageProperty("client-threads")]
      public int? ClientThreads { get; set; }

      [MessageProperty("client-type")]
      public string FahClientType { get; set; }

      [MessageProperty("client-type", typeof(FahClientTypeConverter))]
      public FahClientType FahClientTypeEnum { get; set; }

      // could be IP Address type
      [MessageProperty("command-address")]
      public string CommandAddress { get; set; }

      // could be IP Address type
      [MessageProperty("allow", "command-allow")]
      public string Allow { get; set; }

      // could be IP Address type
      [MessageProperty("command-allow-no-pass")]
      public string CommandAllowNoPass { get; set; }

      // could be IP Address type
      [MessageProperty("deny", "command-deny")]
      public string Deny { get; set; }

      // could be IP Address type
      [MessageProperty("command-deny-no-pass")]
      public string CommandDenyNoPass { get; set; }

      [MessageProperty("command-port")]
      public int? CommandPort { get; set; }

      [MessageProperty("config-rotate")]
      public bool? ConfigRotate { get; set; }

      [MessageProperty("config-rotate-dir")]
      public string ConfigRotateDir { get; set; }

      [MessageProperty("config-rotate-max")]
      public int? ConfigRotateMax { get; set; }

      [MessageProperty("connection-timeout")]
      public int? ConnectionTimeout { get; set; }

      [MessageProperty("core-dir")]
      public string CoreDir { get; set; }

      [MessageProperty("core-key")]
      public string CoreKey { get; set; }

      [MessageProperty("core-prep")]
      public string CorePrep { get; set; }

      [MessageProperty("core-priority")]
      public string CorePriority { get; set; }

      [MessageProperty("core-priority", typeof(CorePriorityConverter))]
      public CorePriority CorePriorityEnum { get; set; }

      [MessageProperty("core-server")]
      public string CoreServer { get; set; }

      [MessageProperty("cpu-affinity")]
      public bool? CpuAffinity { get; set; }

      // could be enum type
      [MessageProperty("cpu-species")]
      public string CpuSpecies { get; set; }

      // could be enum type
      [MessageProperty("cpu-type")]
      public string CpuType { get; set; }

      [MessageProperty("cpu-usage")]
      public int? CpuUsage { get; set; }

      [MessageProperty("cpus")]
      public int? Cpus { get; set; }

      //[MessageProperty("crl-file")]
      //public string CrlFile { get; set; }

      [MessageProperty("cuda-index")]
      public int? CudaIndex { get; set; }

      [MessageProperty("cycle-rate")]
      public int? CycleRate { get; set; }

      [MessageProperty("cycles")]
      public int? Cycles { get; set; }

      [MessageProperty("daemon")]
      public bool? Daemon { get; set; }

      [MessageProperty("data-directory")]
      public string DataDirectory { get; set; }

      [MessageProperty("debug-sockets")]
      public bool? DebugSockets { get; set; }

      // Version 7.2.9 and prior
      [MessageProperty("disable-project-lookup")]
      public bool? DisableProjectLookup { get; set; }

      [MessageProperty("disable-sleep-when-active")]
      public bool? DisableSleepWhenActive { get; set; }

      [MessageProperty("dump-after-deadline")]
      public bool? DumpAfterDeadline { get; set; }

      [MessageProperty("eval")]
      public string Eval { get; set; }

      [MessageProperty("exception-locations")]
      public bool? ExceptionLocations { get; set; }

      [MessageProperty("exec-directory")]
      public string ExecDirectory { get; set; }

      [MessageProperty("exit-when-done")]
      public bool? ExitWhenDone { get; set; }

      [MessageProperty("extra-core-args")]
      public string ExtraCoreArgs { get; set; }

      [MessageProperty("fold-anon")]
      public bool? FoldAnon { get; set; }

      [MessageProperty("force-ws")]
      public string ForceWs { get; set; }

      [MessageProperty("gpu")]
      public bool? Gpu { get; set; }

      [MessageProperty("gpu-assignment-servers")]
      public string GpuAssignmentServers { get; set; }

      // Version 7.1.38 and prior
      [MessageProperty("gpu-device-id")]
      public string GpuDeviceId { get; set; }

      // Version 7.1.38 and prior
      [MessageProperty("gpu-id")]
      public int? GpuId { get; set; }

      // Version 7.1.38 and prior
      [MessageProperty("gpu-vendor-id")]
      public string GpuVendorId { get; set; }

      [MessageProperty("gpu-index")]
      public string GpuIndex { get; set; }

      [MessageProperty("gpu-usage")]
      public int? GpuUsage { get; set; }

      [MessageProperty("http-addresses")]
      public string HttpAddresses { get; set; }

      [MessageProperty("https-addresses")]
      public string HttpsAddresses { get; set; }

      [MessageProperty("idle-seconds")]
      public int? IdleSeconds { get; set; }
      
      [MessageProperty("log")]
      public string Log { get; set; }

      [MessageProperty("log-color")]
      public bool? LogColor { get; set; }

      [MessageProperty("log-crlf")]
      public bool? LogCrlf { get; set; }

      [MessageProperty("log-date")]
      public bool? LogDate { get; set; }

      [MessageProperty("log-date-periodically")]
      public int? LogDatePeriodically { get; set; }

      [MessageProperty("log-debug")]
      public bool? LogDebug { get; set; }

      [MessageProperty("log-domain")]
      public bool? LogDomain { get; set; }

      [MessageProperty("log-domain-levels")]
      public string LogDomainLevels { get; set; }

      [MessageProperty("log-header")]
      public bool? LogHeader { get; set; }

      [MessageProperty("log-level")]
      public bool? LogLevel { get; set; }

      [MessageProperty("log-no-info-header")]
      public bool? LogNoInfoHeader { get; set; }

      [MessageProperty("log-redirect")]
      public bool? LogRedirect { get; set; }

      [MessageProperty("log-rotate")]
      public bool? LogRotate { get; set; }

      [MessageProperty("log-rotate-dir")]
      public string LogRotateDir { get; set; }

      [MessageProperty("log-rotate-max")]
      public int? LogRotateMax { get; set; }

      [MessageProperty("log-short-level")]
      public bool? LogShortLevel { get; set; }

      [MessageProperty("log-simple-domains")]
      public bool? LogSimpleDomains { get; set; }

      [MessageProperty("log-thread-id")]
      public bool? LogThreadId { get; set; }

      [MessageProperty("log-time")]
      public bool? LogTime { get; set; }

      [MessageProperty("log-to-screen")]
      public bool? LogToScreen { get; set; }

      [MessageProperty("log-truncate")]
      public bool? LogTruncate { get; set; }

      [MessageProperty("machine-id")]
      public int? MachineId { get; set; }

      // Version 7.1.38 and prior
      [MessageProperty("max-delay")]
      public int? MaxDelay { get; set; }

      [MessageProperty("max-connect-time")]
      public int? MaxConnectTime { get; set; }

      [MessageProperty("max-connections")]
      public int? MaxConnections { get; set; }

      [MessageProperty("max-packet-size")]
      public string MaxPacketSize { get; set; }

      [MessageProperty("max-packet-size", typeof(MaxPacketSizeConverter))]
      public MaxPacketSize MaxPacketSizeEnum { get; set; }

      [MessageProperty("max-queue")]
      public int? MaxQueue { get; set; }

      [MessageProperty("max-request-length")]
      public int? MaxRequestLength { get; set; }

      [MessageProperty("max-shutdown-wait")]
      public int? MaxShutdownWait { get; set; }

      [MessageProperty("max-slot-errors")]
      public int? MaxSlotErrors { get; set; }

      [MessageProperty("max-unit-errors")]
      public int? MaxUnitErrors { get; set; }

      [MessageProperty("max-units")]
      public int? MaxUnits { get; set; }

      [MessageProperty("memory")]
      public string Memory { get; set; }

      // Version 7.1.38 and prior
      [MessageProperty("min-delay")]
      public int? MinDelay { get; set; }

      [MessageProperty("min-connect-time")]
      public int? MinConnectTime { get; set; }

      [MessageProperty("next-unit-percentage")]
      public int? NextUnitPercentage { get; set; }

      [MessageProperty("priority")]
      public string Priority { get; set; }

      [MessageProperty("no-assembly")]
      public bool? NoAssembly { get; set; }

      [MessageProperty("opencl-index")]
      public int? OpenClIndex { get; set; }

      // could be enum type
      [MessageProperty("os-species")]
      public string OsSpecies { get; set; }

      // could be enum type
      [MessageProperty("os-type")]
      public string OsType { get; set; }

      [MessageProperty("passkey")]
      public string Passkey { get; set; }

      [MessageProperty("password")]
      public string Password { get; set; }

      [MessageProperty("pause-on-battery")]
      public bool? PauseOnBattery { get; set; }

      [MessageProperty("pause-on-start")]
      public bool? PauseOnStart { get; set; }

      [MessageProperty("pid")]
      public bool? Pid { get; set; }

      [MessageProperty("pid-file")]
      public string PidFile { get; set; }

      [MessageProperty("power")]
      public string Power { get; set; }

      //[MessageProperty("private-key-file")]
      //public string PrivateKeyFile { get; set; }

      [MessageProperty("project-key")]
      public int? ProjectKey { get; set; }

      [MessageProperty("proxy")]
      public string Proxy { get; set; }

      [MessageProperty("proxy-enable")]
      public bool? ProxyEnable { get; set; }

      [MessageProperty("proxy-pass")]
      public string ProxyPass { get; set; }

      [MessageProperty("proxy-user")]
      public string ProxyUser { get; set; }

      [MessageProperty("respawn")]
      public bool? Respawn { get; set; }

      [MessageProperty("run-as")]
      public string RunAs { get; set; }

      [MessageProperty("script")]
      public string Script { get; set; }

      [MessageProperty("service")]
      public bool? Service { get; set; }

      [MessageProperty("service-description")]
      public string ServiceDescription { get; set; }

      [MessageProperty("service-restart")]
      public bool? ServiceRestart { get; set; }

      [MessageProperty("service-restart-delay")]
      public int? ServiceRestartDelay { get; set; }

      [MessageProperty("session-timeout")]
      public int? SessionTimeout { get; set; }

      [MessageProperty("smp")]
      public bool? Smp { get; set; }

      [MessageProperty("stack-traces")]
      public bool? StackTraces { get; set; }

      [MessageProperty("team")]
      public int? Team { get; set; }

      [MessageProperty("threads")]
      public int? Threads { get; set; }

      [MessageProperty("user")]
      public string User { get; set; }

      [MessageProperty("verbosity")]
      public int? Verbosity { get; set; }

      // could be IP Address type
      [MessageProperty("web-allow")]
      public string WebAllow { get; set; }

      // could be IP Address type
      [MessageProperty("web-deny")]
      public string WebDeny { get; set; }

      #pragma warning restore 1591

      #endregion

      /// <summary>
      /// Fill the Options object with data from the given JsonMessage.
      /// </summary>
      /// <param name="message">Message object containing JSON value and meta-data.</param>
      public override void Fill(JsonMessage message)
      {
         Debug.Assert(message != null);

         var propertySetter = new MessagePropertySetter(this);
         foreach (var prop in JObject.Parse(message.Value.ToString()).Properties())
         {
            propertySetter.SetProperty(prop);
         }
         SetMessageValues(message);
      }
   }
}
