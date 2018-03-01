using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.IO;
using System.Threading;
using SV360.Data;

namespace SV360.Utils
{
    public class ExcelWriter 
    {
        //Excel workbook
        private Workbook workbook;
        //Excel worksheet
        private static Worksheet worksheet;
        //Application
        private Application app;
        //Excel range
        private static Range range; 
        //Excel file path
        private string path;
        //Excel line counter
        private static int lineCounter;        

        public ExcelWriter()
        {
            workbook = null;
            worksheet = null;
            range = null;             
        }

        public ExcelWriter(String path)
        {
            this.path = path;           
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);                    
                }
                //Create a new worksheet
                app = new Application();
                workbook = app.Workbooks.Add(System.Reflection.Missing.Value);
                worksheet = (Worksheet)workbook.Worksheets.get_Item(1);
                range = (Range)worksheet.get_Range("1:1");                
            }
            catch
            {
                throw new Exception("Cannot create/overwrite the file [" + path + "].\n Does it already opened?");
            }
        }

        public void UpdateSeedResults()
        {
            //Write results in the excel file via another task
            Thread threadExportResults = new Thread(new ThreadStart(ExportResultsTask));
            threadExportResults.Start();             
        }

        internal static void Write(List<Seed> seeds, string path)
        {
            throw new NotImplementedException();
        }

        private static void ExportResultsTask()
        {
        /*    //Get seed object
           // Seed_View seed = Seed_View.Instance();
            int i;
            lineCounter = (seed.id-1) * 11+1;
            //Write seed number
            worksheet.Cells[lineCounter, 1] = "Seed #" + seed.id;
            worksheet.Cells[lineCounter, 1].EntireRow.Font.Bold = true;
            worksheet.Rows[lineCounter].Interior.Color = XlRgbColor.rgbCadetBlue;
            lineCounter++;
            //Write blobs number
            worksheet.Cells[lineCounter, 1] = "Blobs Number";
            worksheet.Cells[lineCounter, 1].Interior.Color = XlRgbColor.rgbWheat;
            worksheet.Cells[lineCounter, 2] = seed.nbBlobs.ToString();
            lineCounter++;
            //Write threshold low applied
            worksheet.Cells[lineCounter, 1] = "ThresholdLo";
            worksheet.Cells[lineCounter, 1].Interior.Color = XlRgbColor.rgbWheat;
            worksheet.Cells[lineCounter, 2] = seed.threshold.ToString();
            lineCounter++;
            //Write seed colors data
            worksheet.Cells[lineCounter, 1] = "Colors";
            worksheet.Cells[lineCounter, 1].Interior.Color = XlRgbColor.rgbWheat;
            i = 2;
            foreach (SeedColor color in seed.colors)
            {
                worksheet.Cells[lineCounter, i] = color.ToString();
                i++;
            }
            worksheet.Cells[lineCounter, 6] = "Average seed color = ("+seed.color.R+" "+seed.color.G+" "+seed.color.B+")";
            worksheet.Cells[lineCounter, 6].Interior.Color = XlRgbColor.rgbIndianRed;
            lineCounter++;
            //Write seed morphology data
            worksheet.Cells[lineCounter, 1] = "Morphology";
            worksheet.Cells[lineCounter, 2] = "Area (mm²)";
            worksheet.Cells[lineCounter, 3] = "Roundness";
            worksheet.Cells[lineCounter, 4] = "Perimeter (mm)";
            worksheet.Cells[lineCounter, 5] = "DMax (mm)";
            worksheet.Cells[lineCounter, 6] = "DMin (mm)";
            worksheet.Cells[lineCounter, 1].Interior.Color = XlRgbColor.rgbWheat;
            lineCounter++;
            i = lineCounter;
            foreach (SeedQuarter quarter in seed.quarters)
            {
                worksheet.Cells[i, 1] = "View " + quarter.index;
                worksheet.Cells[i, 1].Interior.Color = XlRgbColor.rgbBeige;
                worksheet.Cells[i, 2] = quarter.area.ToString();
                worksheet.Cells[i, 3] = quarter.roundness.ToString();
                worksheet.Cells[i, 4] = quarter.perimLength.ToString();
                worksheet.Cells[i, 5] = quarter.feretDiameterMax.ToString();
                worksheet.Cells[i, 6] = quarter.feretDiameterMin.ToString();
                i++;
            }
            lineCounter += 4;
            worksheet.Cells[lineCounter, 2] = "Seed volume (mm³) = " + seed.volume;
            worksheet.Cells[lineCounter, 3] = "Seed roundness = " + seed.roundness;
            worksheet.Cells[lineCounter, 4] = "Seed perimeter (mm) = " + seed.perimeter;
            worksheet.Cells[lineCounter, 5] = "Seed length (mm) = " + seed.length;
            worksheet.Cells[lineCounter, 6] = "Seed thickness (mm) = " + seed.thickness;


            for (int j = 2; j < 7; j++)
            {
                worksheet.Cells[lineCounter - 5, j].Interior.Color = XlRgbColor.rgbBeige;
                worksheet.Cells[lineCounter, j].Interior.Color = XlRgbColor.rgbIndianRed;
            }  */            
        }

        public void Save()
        {
            try
            {
                //Write statistics in the end of the file
                WriteStats();
                //Save the file in the path 
                workbook.SaveAs(path, XlFileFormat.xlWorkbookDefault,
                            Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlExclusive,
                            XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing, Type.Missing);
                workbook.Close(true, System.Reflection.Missing.Value, System.Reflection.Missing.Value);
                //Destroy Excel objects
                app.Quit();
                ReleaseObject(worksheet);
                ReleaseObject(workbook);
                ReleaseObject(app);
            }
            catch
            {
                throw new Exception("Cannot save Excel file ["+path+"]");
            }
        }

        private void WriteStats()
        {
            //Get batch statistics object
            BatchStats batchStats=BatchStats.Instance();
            lineCounter += 4;
            worksheet.Cells[lineCounter, 1] = "Batch Statistics";
            worksheet.Cells[lineCounter, 1].EntireRow.Font.Bold = true;
            worksheet.Rows[lineCounter].Interior.Color = XlRgbColor.rgbDarkOrange;
            lineCounter++;
            //Write seeds number
            worksheet.Cells[lineCounter, 1] = "Seeds Number";
            worksheet.Cells[lineCounter, 1].Interior.Color = XlRgbColor.rgbWheat;
            worksheet.Cells[lineCounter, 2] = batchStats.areaStats.counter.ToString();            
            lineCounter++;            
            worksheet.Cells[lineCounter, 2] = "Average";
            worksheet.Cells[lineCounter, 2].Interior.Color = XlRgbColor.rgbBeige;
            worksheet.Cells[lineCounter, 3] = "Max";
            worksheet.Cells[lineCounter, 3].Interior.Color = XlRgbColor.rgbBeige;
            worksheet.Cells[lineCounter, 4] = "Min";
            worksheet.Cells[lineCounter, 4].Interior.Color = XlRgbColor.rgbBeige;
            worksheet.Cells[lineCounter, 5] = "Standard Deviation";
            worksheet.Cells[lineCounter, 5].Interior.Color = XlRgbColor.rgbBeige;
            lineCounter++;
            worksheet.Cells[lineCounter, 1] = "Color";
            worksheet.Cells[lineCounter, 1].Interior.Color = XlRgbColor.rgbWheat;
            lineCounter++;
            //Write red color statistics
            worksheet.Cells[lineCounter, 1] = "Red";
            worksheet.Cells[lineCounter, 1].Interior.Color = XlRgbColor.rgbBeige;
            worksheet.Cells[lineCounter, 2] = batchStats.redStats.average;
            worksheet.Cells[lineCounter, 3] = batchStats.redStats.max;
            worksheet.Cells[lineCounter, 4] = batchStats.redStats.min;
            worksheet.Cells[lineCounter, 5] = batchStats.redStats.stdDeviation;
            lineCounter++;
            //Write green color statistics
            worksheet.Cells[lineCounter, 1] = "Green";
            worksheet.Cells[lineCounter, 1].Interior.Color = XlRgbColor.rgbBeige;
            worksheet.Cells[lineCounter, 2] = batchStats.greenStats.average;
            worksheet.Cells[lineCounter, 3] = batchStats.greenStats.max;
            worksheet.Cells[lineCounter, 4] = batchStats.greenStats.min;
            worksheet.Cells[lineCounter, 5] = batchStats.greenStats.stdDeviation;
            lineCounter++;
            //Write blue color statistics
            worksheet.Cells[lineCounter, 1] = "Blue";
            worksheet.Cells[lineCounter, 1].Interior.Color = XlRgbColor.rgbBeige;
            worksheet.Cells[lineCounter, 2] = batchStats.blueStats.average;
            worksheet.Cells[lineCounter, 3] = batchStats.blueStats.max;
            worksheet.Cells[lineCounter, 4] = batchStats.blueStats.min;
            worksheet.Cells[lineCounter, 5] = batchStats.blueStats.stdDeviation;
            lineCounter++;
            worksheet.Cells[lineCounter, 1] = "Morphology";
            worksheet.Cells[lineCounter, 1].Interior.Color = XlRgbColor.rgbWheat;
            lineCounter++;
            //Write seeds volume statistics
            worksheet.Cells[lineCounter, 1] = "Volume (mm³)";
            worksheet.Cells[lineCounter, 1].Interior.Color = XlRgbColor.rgbBeige;
            worksheet.Cells[lineCounter, 2] = batchStats.areaStats.average;
            worksheet.Cells[lineCounter, 3] = batchStats.areaStats.max;
            worksheet.Cells[lineCounter, 4] = batchStats.areaStats.min;
            worksheet.Cells[lineCounter, 5] = batchStats.areaStats.stdDeviation;
            lineCounter++;
            //Write seeds roundness statistics
            worksheet.Cells[lineCounter, 1] = "Roundness";
            worksheet.Cells[lineCounter, 1].Interior.Color = XlRgbColor.rgbBeige;
            worksheet.Cells[lineCounter, 2] = batchStats.roundnessStats.average;
            worksheet.Cells[lineCounter, 3] = batchStats.roundnessStats.max;
            worksheet.Cells[lineCounter, 4] = batchStats.roundnessStats.min;
            worksheet.Cells[lineCounter, 5] = batchStats.roundnessStats.stdDeviation;
            lineCounter++;
            //Write seeds perimeter statistics
            worksheet.Cells[lineCounter, 1] = "Perimeter (mm)";
            worksheet.Cells[lineCounter, 1].Interior.Color = XlRgbColor.rgbBeige;
            worksheet.Cells[lineCounter, 2] = batchStats.perimeterStats.average;
            worksheet.Cells[lineCounter, 3] = batchStats.perimeterStats.max;
            worksheet.Cells[lineCounter, 4] = batchStats.perimeterStats.min;
            worksheet.Cells[lineCounter, 5] = batchStats.perimeterStats.stdDeviation;
            lineCounter++;
            //Write seeds length statistics
            worksheet.Cells[lineCounter, 1] = "Length (mm)";
            worksheet.Cells[lineCounter, 1].Interior.Color = XlRgbColor.rgbBeige;
            worksheet.Cells[lineCounter, 2] = batchStats.lengthStats.average;
            worksheet.Cells[lineCounter, 3] = batchStats.lengthStats.max;
            worksheet.Cells[lineCounter, 4] = batchStats.lengthStats.min;
            worksheet.Cells[lineCounter, 5] = batchStats.lengthStats.stdDeviation;
            lineCounter++;
            //Write seeds thickness statistics
            worksheet.Cells[lineCounter, 1] = "Thickness (mm)";
            worksheet.Cells[lineCounter, 1].Interior.Color = XlRgbColor.rgbBeige;
            worksheet.Cells[lineCounter, 2] = batchStats.thicknessStats.average;
            worksheet.Cells[lineCounter, 3] = batchStats.thicknessStats.max;
            worksheet.Cells[lineCounter, 4] = batchStats.thicknessStats.min;
            worksheet.Cells[lineCounter, 5] = batchStats.thicknessStats.stdDeviation;
            //Auto fit column width
            range.EntireColumn.AutoFit();
        }

        private void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch
            {
                obj = null;
                throw new Exception("Cannot save Excel file [" + path + "]");
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
