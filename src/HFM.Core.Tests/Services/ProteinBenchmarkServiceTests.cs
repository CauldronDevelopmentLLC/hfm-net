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

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

using NUnit.Framework;

using HFM.Core.DataTypes;
using HFM.Core.Serializers;

namespace HFM.Core
{
   [TestFixture]
   public class ProteinBenchmarkServiceTests
   {
      [Test]
      public void ReadBinaryTest()
      {
         var container = new ProteinBenchmarkService
         {
            FileName = Path.Combine("..\\..\\TestFiles", Constants.BenchmarkCacheFileName),
         };

         container.Read();
         Assert.AreEqual(1246, container.GetAll().Count);
      }

      [Test]
      public void WriteAndReadBinaryTest()
      {
         var collection = new ProteinBenchmarkService
         {
            FileName = "TestProteinBenchmark.dat",
         };

         collection.Data = CreateTestList();
         collection.Write();
         collection.Data = null;
         collection.Read();
         ValidateTestList(collection.Data);
      }

      [Test]
      public void WriteAndReadXmlTest()
      {
         var data1 = CreateTestList();
         var serializer = new XmlFileSerializer<List<ProteinBenchmark>>();
         serializer.Serialize("TestProteinBenchmark.xml", data1);

         var data2 = serializer.Deserialize("TestProteinBenchmark.xml");
         ValidateTestList(data2);
      }
      
      private static List<ProteinBenchmark> CreateTestList()
      {
         var list = new List<ProteinBenchmark>();
         for (int i = 0; i < 10; i++)
         {
            var benchmark = new ProteinBenchmark
                            {
                               OwningClientName = "TestOwner",
                               OwningClientPath = "TestPath",
                               ProjectID = 100 + i
                            };
            
            for (int j = 1; j < 6; j++)
            {
               benchmark.SetFrameDuration(TimeSpan.FromMinutes(j));
            }
            list.Add(benchmark);
         }

         for (int i = 10; i < 20; i++)
         {
            var benchmark = new ProteinBenchmark
            {
               OwningClientName = "TestOwner2",
               OwningClientPath = "TestPath2",
               OwningSlotId = i - 10,
               ProjectID = 200 + i
            };

            for (int j = 1; j < 6; j++)
            {
               benchmark.SetFrameDuration(TimeSpan.FromMinutes(j + 10));
            }
            list.Add(benchmark);
         }

         return list;
      }

      private static void ValidateTestList(IList<ProteinBenchmark> list)
      {
         for (int i = 0; i < 10; i++)
         {
            ProteinBenchmark benchmark = list[i];
            Assert.AreEqual("TestOwner", benchmark.OwningSlotName);
            Assert.AreEqual("TestOwner", benchmark.OwningClientName);
            Assert.AreEqual("TestPath", benchmark.OwningClientPath);
            Assert.AreEqual(-1, benchmark.OwningSlotId);
            Assert.AreEqual(100 + i, benchmark.ProjectID);
            
            int index = 0;
            for (int j = 5; j > 0; j--)
            {
               Assert.AreEqual(TimeSpan.FromMinutes(j), benchmark.FrameTimes[index].Duration);
               index++;
            }
         }

         for (int i = 10; i < 20; i++)
         {
            ProteinBenchmark benchmark = list[i];
            Assert.AreEqual(String.Format(CultureInfo.InvariantCulture, "TestOwner2 Slot {0:00}", (i - 10)), benchmark.OwningSlotName);
            Assert.AreEqual("TestOwner2", benchmark.OwningClientName);
            Assert.AreEqual("TestPath2", benchmark.OwningClientPath);
            Assert.AreEqual(i - 10, benchmark.OwningSlotId);
            Assert.AreEqual(200 + i, benchmark.ProjectID);

            int index = 0;
            for (int j = 5; j > 0; j--)
            {
               Assert.AreEqual(TimeSpan.FromMinutes(j + 10), benchmark.FrameTimes[index].Duration);
               index++;
            }
         }
      }
   }
}
