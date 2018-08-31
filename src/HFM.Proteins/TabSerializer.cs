﻿
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;

namespace HFM.Proteins
{
   public sealed class TabSerializer : IProteinSerializer
   {
      public List<Protein> Deserialize(string fileName)
      {
         using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
         {
            return Deserialize(stream);
         }
      }

      public List<Protein> Deserialize(Stream stream)
      {
         var proteins = new List<Protein>();

         using (var reader = new StreamReader(stream))
         {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
               try
               {
                  // Parse the current line from the CSV file
                  var p = new Protein();
                  string[] lineData = line.Split(new[] { '\t' }, StringSplitOptions.None);
                  p.ProjectNumber = Int32.Parse(lineData[0], CultureInfo.InvariantCulture);
                  p.ServerIP = lineData[1].Trim();
                  p.WorkUnitName = lineData[2].Trim();
                  p.NumberOfAtoms = Int32.Parse(lineData[3], CultureInfo.InvariantCulture);
                  p.PreferredDays = Double.Parse(lineData[4], CultureInfo.InvariantCulture);
                  p.MaximumDays = Double.Parse(lineData[5], CultureInfo.InvariantCulture);
                  p.Credit = Double.Parse(lineData[6], CultureInfo.InvariantCulture);
                  p.Frames = Int32.Parse(lineData[7], CultureInfo.InvariantCulture);
                  p.Core = lineData[8];
                  p.Description = lineData[9];
                  p.Contact = lineData[10];
                  p.KFactor = Double.Parse(lineData[11], CultureInfo.InvariantCulture);

                  if (p.IsValid)
                  {
                     proteins.Add(p);
                  }
               }
               catch (Exception)
               {
                  Debug.Assert(false);
               }
            }
         }

         return proteins;
      }

      public void Serialize(string fileName, List<Protein> value)
      {
         using (var stream = File.Create(fileName))
         {
            Serialize(stream, value);
         }
      }

      public void Serialize(Stream stream, List<Protein> value)
      { 
         using (var writer = new StreamWriter(stream, Encoding.ASCII))
         { 
            foreach (Protein protein in value)
            {
               // Project Number, Server IP, Work Unit Name, Number of Atoms, Preferred (days),
               // Final Deadline (days), Credit, Frames, Code, Description, Contact, KFactor

               string line = String.Format(CultureInfo.InvariantCulture,
                  "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}",
                  /*  0 */ protein.ProjectNumber,    /*  1 */ protein.ServerIP,
                  /*  2 */ protein.WorkUnitName,     /*  3 */ protein.NumberOfAtoms,
                  /*  4 */ protein.PreferredDays,    /*  5 */ protein.MaximumDays,
                  /*  6 */ protein.Credit,           /*  7 */ protein.Frames,
                  /*  8 */ protein.Core,             /*  9 */ protein.Description,
                  /* 10 */ protein.Contact,          /* 11 */ protein.KFactor);

               writer.WriteLine(line);
            }
         }
      }
   }
}
