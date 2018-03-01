using OfficeOpenXml;
using SV360.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SV360.IHM.Elements.Seuils;
using SV360.Resources;
using OfficeOpenXml.Style;
using System.Drawing;
using OfficeOpenXml.Drawing.Chart;
using SV360.Resources.lang;
using OfficeOpenXml.Table;

namespace SV360.Utils
{
    /// <summary>
    /// Classe permettant d'exporter les données dans un fichier Excel. 
    /// La classe s'occupe de l'écriture et de la mise en forme du fichier excel
    /// </summary>
    public class ExcelController
    {
        /// <summary>
        /// Lecture d'un fichier
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static List<List<double>> Read(String path)
        {
            try
            {
                FileInfo fileInfo = new FileInfo(path);
                ExcelPackage ep = new ExcelPackage(new FileInfo(path));

                if (ep.Workbook.Worksheets.Count < 1) return null;
                ExcelWorksheet ws = ep.Workbook.Worksheets["feuil1"];

                List<List<double>> list = new List<List<double>>();

                for (int rw = 1; rw <= ws.Dimension.End.Row; rw++)
                {
                    List<double> domains = new List<double>();
                    for (int cols = 1; cols <= ws.Dimension.End.Column; cols++)
                    {
                        if (ws.Cells[rw, cols].Value != null)
                            domains.Add(double.Parse(ws.Cells[rw, cols].Value.ToString()));
                    }
                    list.Add(domains);
                }

                /*  for (int i = 0; i < list.Count; i++)
                  {
                      for (int j = 0; j < list[i].Count; j++)
                      {
                          Debug.Write(";" + list[i][j]);
                      }
                      Debug.WriteLine("");
                  }*/

                return list;
            }
            catch
            {
                throw new Exception("Cannot read the file [" + path + "].\n Does it already opened?");
            }
        }

        /// <summary>
        /// Différente catégorie de feuille
        /// </summary>
        public enum SheetCat {
            /// <summary>
            /// feuille générale
            /// </summary>
            General = 0,
            /// <summary>
            /// feuille classe 1
            /// </summary>
            C1 = 1,
            /// <summary>
            /// feuille classe 2
            /// </summary>
            C2 = 2,
            /// <summary>
            /// feuille classe 3
            /// </summary>
            C3 = 3,
            /// <summary>
            /// feuille classe 4
            /// </summary>
            C4 = 4
        }

        /// <summary>
        /// Ecrit dans un fichier, les données relative à la morphologie d'une liste de grains
        /// </summary>
        /// <param name="seeds">liste de grains</param>
        /// <param name="path">chemin du fichier</param>
        /// <returns></returns>
        public static bool Write(List<Seed> seeds, String path)
        {
            try
            {

                seeds = (from s in seeds
                         where s.Length > 1 && s.Length < 15
                                && s.Width > 1 && s.Width < 15
                                && s.Thickness > 1 && s.Thickness < 15
                                && s.NumClass > 0
                         select s).ToList();

                FileInfo fileInfo = new FileInfo(path);
                if (fileInfo.Exists)
                    fileInfo.Delete();

                ExcelPackage ep = new ExcelPackage(new FileInfo(path));

                ExcelWorksheet ws = ep.Workbook.Worksheets.Add("General");
                Sorting sorting = Sorting.Instance();

                var infoClass = CalculateInfoClass(sorting.Criterias, seeds);
                WriteHeader(ws, SheetCat.General, seeds.Count, infoClass);
                WriteStats(ws, seeds);




                for (int i = 0; i < sorting.Criterias.Count; i++)
                {
                    string title = "Classe " + (i + 1);
                    ExcelWorksheet wsC = ep.Workbook.Worksheets.Add(title);
                    // WriteSheets(ep, i, sorting.Criterias[i], seeds);
                    WriteHeader(wsC, (SheetCat)i + 1, seeds.Count, infoClass);
                    var seedsClass = from s in seeds
                                     where s.NumClass == (Seed.ENumClass)i+1
                                     select s;
                    WriteStats(wsC, seedsClass.ToList());
                }

                ep.Save();
                System.Diagnostics.Process.Start(path);
            }
            catch
            {
                return false;
                // throw new Exception("Cannot write in the file [" + path + "].");
            }
            return true;
        }

        [Obsolete("non utilisé")]
        private static void WriteSheets(ExcelPackage ep, int classNumber, List<Criteria> criterias, List<Seed> seeds)
        {
            string title = "Classe " + (classNumber);
            ExcelWorksheet ws = ep.Workbook.Worksheets.Add(title);

            ws.Cells["A3"].Value = "Critères";

            string criteres = "";
            for (int i = 0; i < criterias.Count - 1; i++)
            {
                criteres += criterias[i].ToString() + " ET ";
            }
            criteres += criterias[criterias.Count - 1].ToString();

            ws.Cells["A3"].Value = "Critères";
            ws.Cells["B3"].Value = criteres;

            var seedsClass = from s in seeds
                             where s.NumClass == (Seed.ENumClass)classNumber
                             select s;

            // WriteStats(ws, seedsClass.ToList(), seeds.Count);
        }

        private class InfoClass
        {
            public int number;
            public float pourcentage;
            public string criterias;
            public bool undefined = false;
        }

        //incrementeur de ligne dans le fichier
        static int nbLine = 1;


        /// <summary>
        /// Calcule les infos sur les classes pour l'affichage dans le fichier excel
        /// </summary>
        /// <param name="criterias"></param>
        /// <param name="seeds"></param>
        /// <returns></returns>
        private static List<InfoClass> CalculateInfoClass(List<List<Criteria>> criterias, List<Seed> seeds)
        {
          
            IEnumerable<Seed> seedsClass;

            if (criterias.Count > 0)
            {
                List<InfoClass> classNumber = new List<InfoClass>(criterias.Count);
                for (int i = 0; i < criterias.Count; i++)
                {

                    classNumber.Add(new InfoClass());

                    seedsClass = from s in seeds
                                 where s.NumClass == (Seed.ENumClass)i + 1
                                 select s;

                    classNumber[i].number = seedsClass.Count();
                    classNumber[i].pourcentage = (float)seedsClass.Count() / seeds.Count;
                    string criteres = "";
                    for (int j = 0; j < criterias[i].Count - 1; j++)
                    {
                        criteres += criterias[i][j].ToString() + " ET ";
                    }
                    criteres += criterias[i][criterias[i].Count - 1].ToString();
                    classNumber[i].criterias = criteres;
                }

                /*classNumber.Add(new InfoClass());
                int idx = criterias.Count;
                seedsClass = from s in seeds
                             where s.NumClass == (Seed.ENumClass)0
                             select s;

                classNumber[idx].number = seedsClass.Count();
                classNumber[idx].pourcentage = (float)seedsClass.Count() / seeds.Count;
                classNumber[idx].criterias = "";
                classNumber[idx].undefined = true;
                */
                return classNumber;
            }
            else
            {
                List<InfoClass> classNumber = new List<InfoClass>();
                int i = 0;
                classNumber.Add(new InfoClass());

                seedsClass = from s in seeds
                             where s.NumClass == Seed.ENumClass.class1
                             select s;

                classNumber[i].number = seedsClass.Count();
                classNumber[i].pourcentage = (float)seedsClass.Count() / seeds.Count;

                classNumber.Add(new InfoClass());
                i++;
              /*  seedsClass = from s in seeds
                             where s.NumClass == (Seed.ENumClass)0
                             select s;

                classNumber[i].number = seedsClass.Count();
                classNumber[i].pourcentage = (float)seedsClass.Count() / seeds.Count;
                classNumber[i].criterias = "";
                classNumber[i].undefined = true;*/

                return classNumber;
            }
          
        }

        /// <summary>
        /// Ecriture  de l'entete des fichiers 
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="cat"></param>
        /// <param name="nbSeeds"></param>
        /// <param name="infoClass"></param>
        private static void WriteHeader(ExcelWorksheet ws, SheetCat cat, int nbSeeds, List<InfoClass> infoClass)
        {

            for (int i = 1; i <= 6; i++)
                ws.Column(i).Width = 17;

            ws.Column(7).Width = 0;

            ws.Column(19).Width = 4;
            ws.Column(21).Width = 13;

            nbLine = 1;

            ws.Cells[nbLine, 1].Value = "Date - Heure";
            ws.Cells[nbLine, 2].Value = DateTime.Now.ToLongDateString();
            ws.Cells[nbLine, 4].Value = DateTime.Now.ToLongTimeString();
            nbLine++;

            ws.Cells[nbLine, 1].Value = "Nb total de grains";
            ws.Cells[nbLine, 2].Value = nbSeeds; // nb of seeds
            ws.Cells[nbLine, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            nbLine++;
            nbLine++;

            ws.Cells[nbLine, 1].Value = "poids profil [kg]";
            ws.Cells[nbLine, 1].Style.Font.Size = 9;
            ws.Cells[nbLine, 2].Style.Numberformat.Format = "0";
            ws.Cells[nbLine, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            BorderThin(ws.Cells[nbLine, 1]);
            BorderThin(ws.Cells[nbLine, 2]);
            nbLine++;

            ws.Cells[nbLine, 1].Value = "PMG labo profil [kg]";
            ws.Cells[nbLine, 1].Style.Font.Size = 9;
            ws.Cells[nbLine, 2].Style.Numberformat.Format = "0.00";
            ws.Cells[nbLine, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            BorderThin(ws.Cells[nbLine, 1]);
            BorderThin(ws.Cells[nbLine, 2]);
            nbLine++;

            ws.Cells[nbLine, 1].Value = "nbre grains profil";
           
            ws.Cells[nbLine, 1].Style.Font.Size = 9;
            ws.Cells[nbLine, 2].Style.Numberformat.Format = "0";
            ws.Cells[nbLine, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            ws.Cells[nbLine, 2].Formula = "IF(B5>0,B4*1000/B5,0)";
            BorderThin(ws.Cells[nbLine, 1]);
            BorderThin(ws.Cells[nbLine, 2]);
            nbLine++;


            ws.Cells[nbLine, 1].Value = "nbre dose prév.";
            ws.Cells[nbLine, 1].Style.Font.Size = 9;
            ws.Cells[nbLine, 2].Style.Numberformat.Format = "0";
            ws.Cells[nbLine, 2].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
            ws.Cells[nbLine, 2].Formula = "B" + (nbLine -1 )+ " /5000";
            BorderThin(ws.Cells[nbLine, 1]);
            BorderThin(ws.Cells[nbLine, 2]);
            nbLine++;



            nbLine += 2;

            //white style
            ws.Cells["A1:R10"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["A1:R10"].Style.Fill.BackgroundColor.SetColor(SVColor.snow);
           
            ws.Cells["A4:A7"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);


            if (infoClass.Count > 0)
            {
                ws.Cells[nbLine, 2].Value = "Nb grains";
                ws.Cells[nbLine, 3].Value = "Nb grains [%]";
                ws.Cells[nbLine, 4].Value = "Critères";
                ws.Cells[nbLine, 4, nbLine, 5].Merge = true;
                FormatTitle(ws.Cells[nbLine, 2, nbLine, 4]);

                for (int i = 0; i < infoClass.Count; i++)
                {

                    nbLine++;
                    if (!infoClass[i].undefined)
                    {
                        ws.Cells[nbLine, 1].Value = "Classe " + (i + 1);
                        ws.Cells[nbLine, 2].Value = infoClass[i].number;
                        ws.Cells[nbLine, 3].Value = (float)infoClass[i].pourcentage;
                        ws.Cells[nbLine, 4].Value = infoClass[i].criterias;
                    }
                    else
                    {
                        ws.Cells[nbLine, 1].Value = "Indéfini";
                        ws.Cells[nbLine, 2].Value = infoClass[i].number;
                        ws.Cells[nbLine, 3].Value = (float)infoClass[i].pourcentage;
                        ws.Cells[nbLine, 4].Value = infoClass[i].criterias;
                    }
                                     

                    //style
                    ws.Cells[nbLine, 3].Style.Numberformat.Format = "#0.00%";
                    ws.Cells[nbLine, 4, nbLine, 5].Merge = true;
                    FormatTitle(ws.Cells[nbLine, 1]);
                    ws.Cells[nbLine, 2, nbLine, 3].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    if ((i + 1) == (int)cat)
                    {
                        ws.Cells[nbLine, 2, nbLine, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
                        ws.Cells[nbLine, 2, nbLine, 5].Style.Fill.BackgroundColor.SetColor(SVColor.sunFlower);
                    }
                }



                //Add the Doughnut chart
                var pieChart = ws.Drawings.AddChart("classPourcent", eChartType.Pie) as ExcelPieChart;
                //Set position to row 1 column 7 and 16 pixels offset
                pieChart.SetPosition(nbLine, 6, 1, 14);
                pieChart.SetSize(400, 185);
                pieChart.Series.Add(ws.Cells[11, 3, infoClass.Count + 10, 3], ws.Cells[11, 1, infoClass.Count + 10, 1]);

                //doughtnutChart.Title.Text = "Extension Count";
                pieChart.DataLabel.ShowCategory = true;
                pieChart.DataLabel.ShowPercent = true;
                pieChart.DataLabel.ShowLeaderLines = true;
                pieChart.Legend.Remove();

               // ws.Cells["A4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
               // ws.Cells["A4"].Style.Fill.BackgroundColor.SetColor(SVColor.snow);

                ws.Cells[nbLine + 1, 1, nbLine + 10, 18].Style.Fill.PatternType = ExcelFillStyle.Solid;
                ws.Cells[nbLine + 1, 1, nbLine + 10, 18].Style.Fill.BackgroundColor.SetColor(SVColor.snow);

                nbLine += 11;
            }
        }
        /// <summary>
        /// Mise en page : 
        /// Création d'un bord fin autour d'une zone
        /// </summary>
        /// <param name="cells">zone</param>
        private static void BorderThin(ExcelRange cells)
        {
            // Assign borders
            cells.Style.Border.Top.Style = ExcelBorderStyle.Thin;
            cells.Style.Border.Left.Style = ExcelBorderStyle.Thin;
            cells.Style.Border.Right.Style = ExcelBorderStyle.Thin;
            cells.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
        }

        /// <summary>
        /// Mise en page : 
        /// Création d'un bord moyen autour d'une zone
        /// </summary>
        /// <param name="cells">zone</param>
        private static void BorderMedium(ExcelRange cells)
        {
            int rowS = cells.Start.Row;
            int clS = cells.Start.Column;

            int rowE= cells.End.Row;
            int clE = cells.End.Column;

            cells[rowS, clS].Style.Border.Top.Style = ExcelBorderStyle.Medium;
            cells[rowS, clS].Style.Border.Left.Style = ExcelBorderStyle.Medium;


            cells[rowE, clS].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            cells[rowE, clS].Style.Border.Left.Style = ExcelBorderStyle.Medium;

         
            cells[rowE, clE].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
            cells[rowE, clE].Style.Border.Right.Style = ExcelBorderStyle.Medium;

            cells[rowS, clE].Style.Border.Right.Style = ExcelBorderStyle.Medium;
            cells[rowS, clE].Style.Border.Top.Style = ExcelBorderStyle.Medium;



            for (int i = rowS; i < rowE; i++)
            {
                cells[i, clS].Style.Border.Left.Style = ExcelBorderStyle.Medium;
                cells[rowE  - i + rowS, clE].Style.Border.Right.Style = ExcelBorderStyle.Medium;
            }

            for (int i = clS; i < clE; i++)
            {
                cells[rowE, i].Style.Border.Bottom.Style = ExcelBorderStyle.Medium;
                cells[rowS, clE - i + clS].Style.Border.Top.Style = ExcelBorderStyle.Medium;
            }

            // Assign borders
            /*  cells.Style.Border.Top.Style = ExcelBorderStyle.Medium;
              cells.Style.Border.Left.Style = ExcelBorderStyle.Medium;
              cells.Style.Border.Right.Style = ExcelBorderStyle.Medium;
              cells.Style.Border.Bottom.Style = ExcelBorderStyle.Medium;*/
        }

        /// <summary>
        /// Mise en page :
        /// Couleurs des titres
        /// </summary>
        /// <param name="cells"></param>
        private static void FormatTitle(ExcelRange cells)
        {
            cells.Style.Font.Color.SetColor(Color.White);
            cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
            cells.Style.Fill.BackgroundColor.SetColor(SVColor.orangeUp);
            cells.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
        }

        /// <summary>
        /// Ecriture des statistiques des données d'une liste de seed. Créé 3 graphiques histogramme pour chaque caractéristique morphologique, écrit les détails des données de la liste de grains
        /// </summary>
        /// <param name="ws"></param>
        /// <param name="seeds"></param>
        private static void WriteStats(ExcelWorksheet ws, List<Seed> seeds)
        {


            ws.Cells[nbLine, 1].Value = "Stats Morphologie";
            FormatTitle(ws.Cells[nbLine, 1]);
            ws.Cells[nbLine, 1, nbLine, 5].Merge = true;
            ws.Cells[nbLine, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            nbLine++;


            ws.Cells[nbLine, 2].Value = "Min";
            ws.Cells[nbLine, 3].Value = "Max";
            ws.Cells[nbLine, 4].Value = "Moyenne";
            ws.Cells[nbLine, 5].Value = "Ecart-type";

            ws.Cells[nbLine, 2, nbLine, 5].Style.Font.Bold = true;

            nbLine++; ws.Cells[nbLine, 1].Value = "Largeur";
            ws.Cells[nbLine, 1].Style.Font.Bold = true;
            ws.Cells[nbLine, 2].Value = seeds.Min(s => s.Width); // width min
            ws.Cells[nbLine, 3].Value = seeds.Max(s => s.Width); // width max
            ws.Cells[nbLine, 4].Value = seeds.Average(s => s.Width); // width moy
            ws.Cells[nbLine, 5].Value = seeds.StdDev(s => s.Width);// width Std Dev
            ws.Cells[nbLine, 1, nbLine, 5].Style.Numberformat.Format = "0.00";
            ws.Cells[nbLine, 1, nbLine, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;


            nbLine++; ws.Cells[nbLine, 1].Value = "Epaisseur";
            ws.Cells[nbLine, 1].Style.Font.Bold = true;
            ws.Cells[nbLine, 2].Value = seeds.Min(s => s.Thickness); // thickness min
            ws.Cells[nbLine, 3].Value = seeds.Max(s => s.Thickness); // thickness max
            ws.Cells[nbLine, 4].Value = seeds.Average(s => s.Thickness); // thickness moy
            ws.Cells[nbLine, 5].Value = seeds.StdDev(s => s.Thickness);// thickness Std Dev
            ws.Cells[nbLine, 1, nbLine, 5].Style.Numberformat.Format = "0.00";
            ws.Cells[nbLine, 1, nbLine, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            nbLine++; ws.Cells[nbLine, 1].Value = "Longueur";
            ws.Cells[nbLine, 1].Style.Font.Bold = true;
            ws.Cells[nbLine, 2].Value = seeds.Min(s => s.Length); // length min
            ws.Cells[nbLine, 3].Value = seeds.Max(s => s.Length); // length max
            ws.Cells[nbLine, 4].Value = seeds.Average(s => s.Length); // length moy
            ws.Cells[nbLine, 5].Value = seeds.StdDev(s => s.Length);// length Std Dev
            ws.Cells[nbLine, 1, nbLine, 5].Style.Numberformat.Format = "0.00";
            ws.Cells[nbLine, 1, nbLine, 5].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            ws.Cells[nbLine + 1, 1, nbLine + 1, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells[nbLine + 1, 1, nbLine + 1, 5].Style.Fill.BackgroundColor.SetColor(SVColor.snow);


            nbLine += 2; ws.Cells[nbLine, 1].Value = "Stats Anomalies";
            ws.Cells[nbLine, 1, nbLine, 5].Merge = true;
            ws.Cells[nbLine, 1].Style.Font.Color.SetColor(Color.White);
            ws.Cells[nbLine, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells[nbLine, 1].Style.Fill.BackgroundColor.SetColor(SVColor.orangeUp);
            ws.Cells[nbLine, 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            nbLine++; ws.Cells[nbLine, 1].Value = "% Sains";
            ws.Cells[nbLine, 2].Value = "% Eclatés NC";
            ws.Cells[nbLine, 3].Value = "% Eclatés C";
            ws.Cells[nbLine, 4].Value = "% Germés";
            ws.Cells[nbLine, 5].Value = "% Rongés";
            ws.Cells[nbLine, 1, nbLine, 5].Style.Font.Bold = true;

            ws.Cells[nbLine + 2, 1, nbLine + 2, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells[nbLine + 2, 1, nbLine + 2, 5].Style.Fill.BackgroundColor.SetColor(SVColor.snow);

            nbLine += 3; ws.Cells[nbLine, 1].Value = "Détails";
            ws.Cells[nbLine, 1].Style.Font.Color.SetColor(Color.White);
            ws.Cells[nbLine, 1].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells[nbLine, 1].Style.Fill.BackgroundColor.SetColor(SVColor.orangeUp);
            ws.Cells[nbLine, 1].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            ws.Cells[nbLine, 1, nbLine, 6].Merge = true;

            nbLine++;
            ws.Cells[nbLine, 1].Value = "Largeur";  //  ws.Cells[16,1].Value = "Largeur";
            ws.Cells[nbLine, 2].Value = "Epaisseur";
            ws.Cells[nbLine, 3].Value = "Longueur";
            ws.Cells[nbLine, 4].Value = "Seuil Largeur";
            ws.Cells[nbLine,5].Value = "Seuil Epaisseur";
            ws.Cells[nbLine, 6].Value = "Seuil Longueur";
            ws.Cells[nbLine, 7].Value = "Visibilité";
            ws.Cells[nbLine, 1, nbLine, 8].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

            int sMorpho = nbLine;
            nbLine++;

            // select the range that will be included in the table
            var range = ws.Cells[sMorpho, 1, seeds.Count + sMorpho +100,7];

            // add the excel table entity
            var table = ws.Tables.Add(range, null);
            table.ShowTotal = false;
            table.TableStyle = TableStyles.Light17;
            

            table.Columns[6].CalculatedColumnFormula = "SUBTOTAL(3," + table.Name + "[[#This Row],[Largeur]])";

            for (int i = 0; i < seeds.Count; i++)
            {
                ws.Cells[nbLine + i, 1].Value = seeds[i].Width;
                ws.Cells[nbLine + i, 2].Value = seeds[i].Thickness;
                ws.Cells[nbLine + i, 3].Value = seeds[i].Length;

                ws.Cells[nbLine + i, 4].Value = (float)Math.Floor(seeds[i].Width * 4) / 4;
                ws.Cells[nbLine + i, 5].Value = (float)Math.Floor(seeds[i].Thickness * 4) / 4;
                ws.Cells[nbLine + i, 6].Value = (float)Math.Floor(seeds[i].Length * 4) / 4;


                for (int j = 0; j < 30; j++)
                {
                    ws.Cells[nbLine + i, 1 + j].Style.Numberformat.Format = "0.00";
                    ws.Cells[nbLine + i, 1 + j].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }
            }

            ws.Cells["1:1"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["1:1"].Style.Fill.BackgroundColor.SetColor(SVColor.snow);
            ws.Cells["S:X"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["S:X"].Style.Fill.BackgroundColor.SetColor(SVColor.snow);

            // style de la fenetre calibre
            ws.Cells["T1"].Value = "Nombre de semences";
            ws.Cells["T1:U1"].Style.Font.Bold = true;
            ws.Cells["T4:U4"].Style.Font.Bold = true;
            ws.Cells["T7:U7"].Style.Font.Bold = true;
            ws.Cells["T10:U10"].Style.Font.Bold = true;
            ws.Cells["T13:U13"].Style.Font.Bold = true;

            for (int i = 0; i < 4; i++)
            {
                ws.Cells[2+i*3,20].Value = "% nbre grains";
                ws.Cells[3 + i * 3, 20].Value = "nbre grains";
                ws.Cells[4 + i * 3, 20].Value = "nbre doses";

                ws.Cells[2 + i * 3, 21].Formula = "=$U$1/$B$2 *100";
                ws.Cells[3 + i * 3, 21].Formula = "U" + (2 + i * 3) + "/100*$B$7";
                ws.Cells[4 + i * 3, 21].Formula = "U"+ (3 + i * 3) + " / 50000";
            }

          
            ws.Cells["T4:U4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["T4:U4"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            ws.Cells["T7:U7"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["T7:U7"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            ws.Cells["T10:U10"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["T10:U10"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            ws.Cells["T13:U13"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["T13:U13"].Style.Fill.BackgroundColor.SetColor(Color.LightGray);
            BorderMedium(ws.Cells["S2:U4"]);


            ws.Cells["S2"].Value = "Cal. 1";
            ws.Cells["S2:S4"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["S2:S4"].Style.Fill.BackgroundColor.SetColor(SVColor.wetAlphast);
            ws.Cells["S2:S4"].Style.Font.Color.SetColor(Color.White);
            ws.Cells["S2:S4"].Merge = true;
            ws.Cells["S2:S4"].Style.TextRotation = 90;
            ws.Cells["S2:S4"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["S2:S4"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            BorderMedium(ws.Cells["S5:U7"]);

            ws.Cells["S5"].Value = "Cal. 2";
            ws.Cells["S5:S7"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["S5:S7"].Style.Fill.BackgroundColor.SetColor(SVColor.wetAlphast);
            ws.Cells["S5:S7"].Style.Font.Color.SetColor(Color.White);
            ws.Cells["S5:S7"].Merge = true;
            ws.Cells["S5:S7"].Style.TextRotation = 90;
            ws.Cells["S5:S7"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["S5:S7"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            BorderMedium(ws.Cells["S8:U10"]);

            ws.Cells["S8"].Value = "Cal. 3";
            ws.Cells["S8:S10"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["S8:S10"].Style.Fill.BackgroundColor.SetColor(SVColor.wetAlphast);
            ws.Cells["S8:S10"].Style.Font.Color.SetColor(Color.White);
            ws.Cells["S8:S10"].Merge = true;
            ws.Cells["S8:S10"].Style.TextRotation = 90;
            ws.Cells["S8:S10"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["S8:S10"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;
            BorderMedium(ws.Cells["S11:U13"]);


            ws.Cells["S11"].Value = "Cal. 4";
            ws.Cells["S11:S13"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["S11:S13"].Style.Fill.BackgroundColor.SetColor(SVColor.wetAlphast);
            ws.Cells["S11:S13"].Style.Font.Color.SetColor(Color.White);
            ws.Cells["S11:S13"].Merge = true;
            ws.Cells["S11:S13"].Style.TextRotation = 90;
            ws.Cells["S11:S13"].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells["S11:S13"].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            ws.Column(19).Width = 7;
            ws.Column(20).Width = 21;

          


            //print numbers 
            ws.Cells["U1"].Formula = "SUBTOTAL(3," + table.Name + "[Largeur])";



            Batch batch = new Batch(seeds);

            // print title

            int offset = 9;

           /* for (int i = 19 + offset; i < 22 + offset + batch.MaxIdx; i++)
            {
                ws.Column(i).Width = 5;
            }*/
            


            ws.Cells[1, 19+offset].Value = "Données graphiques";

            int start = 1;
            int idx = start;
            ws.Cells[idx, 20 + offset].Value = "Seuils";
            idx++; ws.Cells[idx, 20 + offset].Value = "Largeur";
            idx++; idx++; ws.Cells[idx, 20 + offset].Value = "Epaisseur";
            idx++; idx++; ws.Cells[idx, 20 + offset].Value = "Longueur";

            idx = 2;
            for (int i = 0; i < 3; i++)
            {
                ws.Cells[idx, 21 + offset].Value = "Nombre"; idx++;
                ws.Cells[idx, 21 + offset].Value = "Poucentage"; idx++;
            }

           


            for (int i = 0; i < batch.MaxIdx; i++)
            {
                ws.Cells[1, 22 + i + offset].Value = batch.Step[i];
                ws.Cells[2, 22 + offset + i].Formula = "COUNTIFS(" + table.Name + "[Seuil Largeur]," + GetExcelColumnName(22 + offset + i) + "1, " + table.Name + "[Visibilité], 1)";
                ws.Cells[3, 22 + offset + i].Formula = GetExcelColumnName(22 + offset + i) + "2 / $U$1";

                ws.Cells[4, 22 + offset + i].Formula = "COUNTIFS(" + table.Name + "[Seuil Epaisseur]," + GetExcelColumnName(22 + offset + i) + "1, " + table.Name + "[Visibilité], 1)";
                ws.Cells[5, 22 + offset + i].Formula = GetExcelColumnName(22 + offset + i) + "4 /  $U$1";


                ws.Cells[6, 22 + offset + i].Formula = "COUNTIFS(" + table.Name + "[Seuil Longueur]," + GetExcelColumnName(22 + offset + i) + "1, " + table.Name + "[Visibilité], 1)";
                ws.Cells[7, 22 + offset + i].Formula = GetExcelColumnName(22 + offset + i) + "6 /  $U$1";
            }

            //style 
            idx = 22 + offset;
            ws.Cells[1, idx, 1, batch.MaxIdx + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[1, idx, 1, batch.MaxIdx + 1].Style.Numberformat.Format = "0.00";
            for (int i = 0; i < 3; i++)
            {
                ws.Cells[2+i*2, idx, 2 + i*2, idx+ batch.MaxIdx + 1].Style.Numberformat.Format = "0";
                ws.Cells[3+i*2, idx,3+i*2, idx + batch.MaxIdx + 1].Style.Numberformat.Format = "#0.00%";
            }

            //Print chart 
            //Width
            var chartW = ws.Drawings.AddChart("Width Seeds Distribution", eChartType.ColumnClustered) as ExcelBarChart;
            chartW.Title.Text = Lang.Text("excel_chart_title_w");
            chartW.SetSize(800, 310);
            chartW.SetPosition(0, 5, 5, 10);
          
            int sCol = batch.MinIdxWidth() < 1 ? 0 : batch.MinIdxWidth() - 1;
            int eCol = batch.MaxIdxWidth() >= batch.MaxIdx ? batch.MaxIdx : batch.MaxIdxWidth() + 1;
            sCol += 22 + offset;
            eCol += 22 + offset;
            var serW = chartW.Series.Add(ws.Cells[3, sCol,3 , eCol], ws.Cells[1, sCol,1 , eCol]);
            serW.Fill.Color = SVColor.emerald;
            chartW.Legend.Remove();

            //Thickness
            var chartT = ws.Drawings.AddChart("Thickness Seeds Distribution", eChartType.ColumnClustered) as ExcelBarChart;
            chartT.Title.Text = Lang.Text("excel_chart_title_t");
            chartT.SetSize(500, 310);
            chartT.SetPosition(16, 5, 5, 10);
            
            sCol = batch.MinIdxThickness() < 1 ? 0 : batch.MinIdxThickness() - 1;
            eCol = batch.MaxIdxThickness() >= batch.MaxIdx ? batch.MaxIdx : batch.MaxIdxThickness() + 1;
            sCol += 22 + offset;
            eCol += 22 + offset;
            var serT = chartT.Series.Add(ws.Cells[5, sCol,5, eCol], ws.Cells[1, sCol, 1, eCol]);
            serT.Fill.Color = SVColor.sunFlower;
            chartT.Legend.Remove();

            //Length
            var chartL = ws.Drawings.AddChart("Length Seeds Distribution", eChartType.ColumnClustered) as ExcelBarChart;
            chartL.Title.Text = Lang.Text("excel_chart_title_l");
            chartL.SetSize(500, 310);
            chartL.SetPosition(16, 5,13, 10);
            sCol = batch.MinIdxLength() < 1 ? 0 : batch.MinIdxLength() - 1;
            eCol = batch.MaxIdxLength() >= batch.MaxIdx ? batch.MaxIdx : batch.MaxIdxLength() + 1;
            sCol += 22 + offset;
            eCol += 22 + offset;
            var serL = chartL.Series.Add(ws.Cells[7, sCol, 7, eCol], ws.Cells[1, sCol, 1, eCol]);
            serL.Fill.Color = SVColor.peterRiver;
            chartL.Legend.Remove();
            // ws.Cells["B1:E1"].Style.Font.Bold = true;

            // Donnees graphiques style
            ws.Cells[1, 19 + offset, 7, 19 + offset].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells[1, 19 + offset, 7, 19 + offset].Style.Fill.BackgroundColor.SetColor(SVColor.wetAlphast);
            ws.Cells[1, 19 + offset, 7, 19 + offset].Style.Font.Color.SetColor(Color.White);
            ws.Cells[1, 19 + offset, 7, 19 + offset].Merge = true;
            ws.Cells[1, 19 + offset, 7, 19 + offset].Style.TextRotation = 90;
            ws.Cells[1, 19 + offset, 7, 19 + offset].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            ws.Cells[1, 19 + offset, 7, 19 + offset].Style.VerticalAlignment = ExcelVerticalAlignment.Center;

            ws.Cells[1, 20 + offset, 7, 20 + offset].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells[1, 20 + offset, 7, 20 + offset].Style.Fill.BackgroundColor.SetColor(SVColor.blueSky);
            ws.Cells[1, 20 + offset, 7, 20 + offset].Style.Font.Color.SetColor(Color.White);

            ws.Cells[1, 21 + offset, 7, 21 + offset].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells[1, 21 + offset, 7, 21 + offset].Style.Fill.BackgroundColor.SetColor(SVColor.blueLight);

            //white style

            ws.Cells[sMorpho - 2, 4, sMorpho + seeds.Count, 5].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells[sMorpho - 2, 4, sMorpho + seeds.Count, 5].Style.Fill.BackgroundColor.SetColor(SVColor.snow);

            ws.Cells["F:R"].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells["F:R"].Style.Fill.BackgroundColor.SetColor(SVColor.snow);

            ws.Cells[14, 19, sMorpho  + seeds.Count, 19 + 62].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells[14, 19, sMorpho  + seeds.Count, 19 + 62].Style.Fill.BackgroundColor.SetColor(SVColor.snow);


            ws.Cells[1, 79, 8, 81].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells[1, 79, 8, 81].Style.Fill.BackgroundColor.SetColor(SVColor.snow);

            ws.Cells[8, 23, sMorpho + seeds.Count, 19 + 62].Style.Fill.PatternType = ExcelFillStyle.Solid;
            ws.Cells[8, 23, sMorpho + seeds.Count, 19 + 62].Style.Fill.BackgroundColor.SetColor(SVColor.snow);
        }


        /// <summary>
        /// Converti la position colonne d'un nombre en une suite de lettre
        /// </summary>
        /// <param name="columnNumber">numéro de colonne</param>
        /// <returns>colonne en lettre</returns>
        private static string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }
    }
 
}
