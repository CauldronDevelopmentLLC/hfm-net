﻿/*
 * HFM.NET
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

namespace HFM.Client.DataTypes
{
#pragma warning disable 1591

   /// <summary>
   /// Folding@Home client type.
   /// </summary>
   public enum FahClientType
   {
      Unknown,
      Normal,
      Advanced,
      BigAdv,
      Beta,
      BigBeta
   }

   // ReSharper disable InconsistentNaming

   /// <summary>
   /// Folding@Home sub-client type (CPU or GPU).
   /// </summary>
   public enum FahClientSubType
   {
      Unknown = 0,
      CPU = 1,
      GPU = 2,
   }

   // ReSharper restore InconsistentNaming

   /// <summary>
   /// Folding@Home slot status.
   /// </summary>
   public enum FahSlotStatus
   {
      Unknown,
      Paused,
      Running,
      Finishing,
      Ready,
      Stopping,
      Failed
   }

   /// <summary>
   /// Folding@Home work unit status.
   /// </summary>
   public enum FahUnitStatus
   {
      Unknown,
      Running,
      Download,
      Send,
      Ready
   }

   /// <summary>
   /// Folding@Home maximum packet size.
   /// </summary>
   public enum MaxPacketSize
   {
      Unknown,
      Small,
      Normal,
      Big
   }

   /// <summary>
   /// Folding@Home core priority.
   /// </summary>
   public enum CorePriority
   {
      Unknown,
      Idle,
      Low
   }

   /// <summary>
   /// Operating system types.
   /// </summary>
   public enum OperatingSystemType
   {
      Unknown,
      Windows,
      WindowsXP,
      WindowsVista,
      Windows7,
      Windows8,
      Windows10,
      Linux,
      OSX
   }

   /// <summary>
   /// Operating system architecture types.
   /// </summary>
   public enum OperatingSystemArchitectureType
   {
      Unknown,
      x86,
      x64
   }

   /// <summary>
   /// CPU manufacturers.
   /// </summary>
   public enum CpuManufacturer
   {
      Unknown,
      Intel,
      AMD
   }

   /// <summary>
   /// CPU types.
   /// </summary>
   public enum CpuType
   {
      Unknown,
      Core2,
      Corei7,
      Corei5,
      Corei3,
      PhenomII,
      Phenom,
      Athlon
   }

   /// <summary>
   /// GPU manufacturers.
   /// </summary>
   public enum GpuManufacturer
   {
      Unknown,
      ATI,
      Nvidia
   }

#pragma warning restore 1591
}
