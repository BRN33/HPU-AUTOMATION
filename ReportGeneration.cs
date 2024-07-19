using HPU_OTOMASYON;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Forms.DataVisualization.Charting;
//using System.Windows.Forms.DataVisualization.Charting;

namespace HPU_OTOMASYON
{
    class ReportGeneration
    {


        string reportType { get; set; }

        public string dateTime { get; set; }
        public string itemNo { get; set; }
        public string itemType { get; set; }
        public string operatorName { get; set; }
        public string isControlled { get; set; }
        public string sapNo { get; set; }

        public string oilLevel { get; set; }
        public string oilLeakage { get; set; }

        public string path { get; set; }



        iTextSharp.text.Font validFont;
        iTextSharp.text.Font warningFont;
        iTextSharp.text.Font invalidFont;






        int pageNumber = 1;

        int totalPageNumber;



        public void GenerateReportMotor(ObservableConcurrentDictionary<string, string> values, Dictionary<double, double> tco24V, Dictionary<double, double> tco16_8V, Dictionary<double, double> tt1Increase, Dictionary<double, double> tt1Decrease, Dictionary<double, double> pg0Increase, Dictionary<double, double> pg0Decrease, Dictionary<double, double> spr, Dictionary<double, double> pg1Increase, Dictionary<double, double> pg1Decrease)
        {
            try

            {

                //// pg0Increase ve pg0Decrease değerlerini dosyaya yazdırıyor.

                //using (StreamWriter file = new StreamWriter("pg0Inc.txt"))
                //    foreach (var entry in pg0Increase)
                //        file.WriteLine("[{0} {1}]", entry.Key, entry.Value);

                //using (StreamWriter file = new StreamWriter("pg0Dec.txt"))
                //    foreach (var entry in pg0Decrease)
                //        file.WriteLine("[{0} {1}]", entry.Key, entry.Value);

                reportType = "Motor";

                totalPageNumber = 5;


                FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);


                Document doc = new Document(PageSize.A4, 20, 20, 20, 20);

                PdfWriter writer = PdfWriter.GetInstance(doc, fs);

                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, "Cp1254", BaseFont.EMBEDDED);

                validFont = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                warningFont = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.BOLD, new BaseColor(255, 103, 0));
                invalidFont = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.BOLD, new BaseColor(255, 0, 0));

                doc.Open();




                // pdf
                iTextSharp.text.Font normalFont_new = new iTextSharp.text.Font(bf, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);


                iTextSharp.text.Font normalFont = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                iTextSharp.text.Font boldFont = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                iTextSharp.text.Font headerFont = new iTextSharp.text.Font(bf, 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK);






                #region sayfa 1

                AddMfilesTitleForMotor(doc);


                #region Title Table

                try
                {


                    PdfPTable table = new PdfPTable(4);

                    table.TotalWidth = 216f;

                    float[] widths = new float[] { 1f, 6f, 8f, 10f };
                    table.SetWidths(widths);

                    PdfPCell cell = new PdfPCell(new Phrase(" "));
                    cell.DisableBorderSide(iTextSharp.text.Rectangle.BOTTOM_BORDER);
                    cell.Colspan = 4;
                    cell.Rowspan = 1;
                    cell.BorderWidth = 3;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);





                    //3. satur
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Test Tarihi ve Saati", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    string strDate = dateTime + "-" + DateTime.Now.ToShortTimeString();

                    // Date Time
                    cell = new PdfPCell((new Phrase(strDate, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);



                    // 4. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Ürün Seri No", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    // Date Time
                    cell = new PdfPCell((new Phrase(itemNo, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);


                    //5. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Bogi Tipi", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    // Date Time
                    cell = new PdfPCell((new Phrase(itemType, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    //5. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Operator", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    cell = new PdfPCell((new Phrase(operatorName, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    // 7. satır

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Sipariş No (SAP)", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    cell = new PdfPCell((new Phrase(sapNo, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    // 8. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);


                    doc.Add(table);


                    doc.Add(new Paragraph(" "));


                }
                catch (Exception)
                {

                    throw;
                }


                #endregion

                #region Result Table 

                try
                {
                    PdfPTable table = new PdfPTable(4);

                    float[] widths = new float[] { 1f, 6f, 8f, 10f };

                    table.TotalWidth = 216f;

                    table.SetWidths(widths);

                    // 1. satır


                    Paragraph prg = new Paragraph("TEST ADIMLARI", boldFont);
                    prg.Alignment = 1;
                    doc.Add(prg);
                    doc.Add(new Paragraph(" "));

                    PdfPCell cell = new PdfPCell(new Phrase("Test Adı", boldFont));
                    cell.Colspan = 2;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Hedef", boldFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Sonuç", boldFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);


                    //1.satır

                    iTextSharp.text.Font rowFont = ValidateResult("Görsel", isControlled);


                    cell = new PdfPCell(new Phrase("1. Görsel Kontrol", normalFont));
                    cell.Colspan = 2;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Evet", normalFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(isControlled, rowFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);


                    //2.satır

                    rowFont = ValidateResult("Yağ Seviyesi", oilLevel);

                    cell = new PdfPCell(new Phrase("2. Yağ Seviyesi", normalFont));
                    cell.Colspan = 2;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("İyi", normalFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(oilLevel, rowFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    //3.satır
                    rowFont = ValidateResult("Yağ Kaçağı", oilLeakage);
                    cell = new PdfPCell(new Phrase("3. Yağ Kaçağı", normalFont));
                    cell.Colspan = 2;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("İyi", normalFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(oilLeakage, rowFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);


                    // 4. satır

                    Phrase brpsPhrase = new Phrase();

                    brpsPhrase.Add(new Chunk("\n\n", normalFont));

                    rowFont = ValidateResult("BRPS Change Contact_3", values["BRPS Change Contact_3"]);

                    brpsPhrase.Add(new Chunk(values["BRPS Change Contact_3"] + " bar \n", rowFont));

                    rowFont = ValidateResult("BRPS Change Contact_1", values["BRPS Change Contact_1"]);

                    brpsPhrase.Add(new Chunk(values["BRPS Change Contact_1"] + " bar \n", rowFont));

                    rowFont = ValidateResult("BRPS Change Contact_2", values["BRPS Change Contact_2"]);

                    brpsPhrase.Add(new Chunk(values["BRPS Change Contact_2"] + " bar \n", rowFont));

                    rowFont = ValidateResult("BRPS Change Contact_4", values["BRPS Change Contact_4"]);

                    brpsPhrase.Add(new Chunk(values["BRPS Change Contact_4"] + " bar \n", rowFont));


                    //string brps = ("\n\n" + values["BRPS Change Contact_3"] + " bar \n" +
                    //                             values["BRPS Change Contact_1"] + " bar \n" +
                    //                             values["BRPS Change Contact_2"] + " bar \n" +
                    //                             values["BRPS Change Contact_4"] + " bar \n");


                    cell = new PdfPCell(new Phrase("4. BRPS\n\n \t  BAPS: 24V->> 0V \n\t   BRPS: 0V->> 24V \n\t   BRPS: 24V->> 0V \n\t   BAPS: 0V->> 24V \n", normalFont));
                    cell.Colspan = 2;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("\n\n 27,5 bar < P < 32,5 bar \n 92,5 bar < P < 97,5 bar \n 77,5 bar < P < 82,5 bar \n  15,5 bar < P < 20,5 bar \n", normalFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);




                    cell = new PdfPCell(brpsPhrase);
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right

                    table.AddCell(cell);


                    // 5. satır

                    rowFont = ValidateResult("PRLIMIT", values["PRLIMIT"]);

                    string pr = values["PRLIMIT"] + " bar";


                    cell = new PdfPCell(new Phrase("5. PR Basınç Limit Valfi", normalFont));
                    cell.Colspan = 2;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("162 bar < P <172 bar", normalFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(pr, rowFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);


                    // 6. satır

                    Phrase pg0Phrase = new Phrase();

                    pg0Phrase.Add(new Chunk("\n\n", normalFont));

                    rowFont = ValidateResult("PG0 INCREASING_1", values["PG0 INCREASING_1"]);
                    pg0Phrase.Add(new Chunk(values["PG0 INCREASING_1"] + " mA \n", rowFont));

                    rowFont = ValidateResult("PG0 INCREASING_2", values["PG0 INCREASING_2"]);
                    pg0Phrase.Add(new Chunk(values["PG0 INCREASING_2"] + " mA \n", rowFont));

                    rowFont = ValidateResult("PG0 DECREASING", values["PG0 DECREASING"]);
                    pg0Phrase.Add(new Chunk(values["PG0 DECREASING"] + " mA \n", rowFont));



                    //string pg0 = ("\n\n" + values["PG0 INCREASING_1"] + " mA \n" +
                    //                         values["PG0 INCREASING_2"] + " mA \n" +
                    //                         values["PG0 DECREASING"] + " mA \n");


                    cell = new PdfPCell(new Phrase("6. PG0\n\n    P Artan    P = 0 bar\n    P Artan    P = 150 bar\n    P Azalan  P = 0 bar", normalFont));
                    cell.Colspan = 2;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);


                    string pg0Reference = "\n\n 3,84 mA < I < 4,16 mA \n 15,84 mA < I < 16,16 mA \n 3,84 mA < I < 4,16 mA";


                    cell = new PdfPCell(new Phrase(pg0Reference, normalFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(pg0Phrase));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);



                    // 7. satır

                    string pressTime = values["YAG DOLDURMA SURESI"] + " s";

                    rowFont = ValidateResult("YAG DOLDURMA SURESI", values["YAG DOLDURMA SURESI"]);


                    cell = new PdfPCell(new Phrase("7. Yağ Doldurma Süresi", normalFont));
                    cell.Colspan = 2;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("T < 35   U=24 VDC", normalFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(pressTime, rowFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);


                    // 8. satır


                    string tco = values["TCO Basınç Farkı"] + " bar";

                    rowFont = ValidateResult("TCO Basınç Farkı", values["TCO Basınç Farkı"]);


                    cell = new PdfPCell(new Phrase("8. Sızdırmazlık Ölçümü", normalFont));
                    cell.Colspan = 2;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("∆P < 25 bar  (60s.)", normalFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(tco, rowFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);


                    // 9. satır

                    string sv = values["SV Valve"] + " bar";

                    rowFont = ValidateResult("SV Valve", values["SV Valve"]);


                    cell = new PdfPCell(new Phrase("9. SV Değeri", normalFont));
                    cell.Colspan = 2;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("  P < 2 bar   OK", normalFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(sv, rowFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    // 10.satır



                    string oldCell = " < ±1,5 bar/period 2s \n\n Hyst < 5 bar \n 0 bar < P < 3 bar \n 19 bar < P < 29 bar \n 44 bar < P < 54 bar"
                            + "\n" + "69 bar < P < 79 bar  \n 94 bar < P < 104 bar \n 104 bar < P < 120 bar \n + Tps equiv < 0,8s ";
                    string level = string.Empty;
                    double tempValue = Convert.ToDouble(values["Oil Temp_Record"]);
                    iTextSharp.text.Font tempFont;

                    if (tempValue <= 10)
                    {
                        level = "T > 1180 s         t<=10°";
                        tempFont = tempValue <= 10 ? validFont : invalidFont;


                    }
                    else if (tempValue <= 20)
                    {
                        level = "T > 330 s         10°<t<=20°";
                        tempFont = (tempValue > 10 && tempValue <= 20) ? validFont : invalidFont;
                    }
                    else if (tempValue <= 25)
                    {
                        level = "T > 242 s         20°<t<=25°";
                        tempFont = (tempValue > 20 && tempValue <= 25) ? validFont : invalidFont;
                    }
                    else if (tempValue <= 30)
                    {
                        level = "T > 192 s         25°<t<=30°";
                        tempFont = (tempValue > 25 && tempValue <= 30) ? validFont : invalidFont;
                    }
                    else if (tempValue <= 35)
                    {
                        level = "T > 159 s         30°<t<=35°";
                        tempFont = (tempValue > 30 && tempValue <= 35) ? validFont : invalidFont;

                    }
                    else if (tempValue <= 40)
                    {
                        level = "T > 135 s         35°<t<=40°";
                        tempFont = (tempValue > 35 && tempValue <= 40) ? validFont : invalidFont;
                    }
                    else if (tempValue <= 45)
                    {
                        level = "T > 118 s         40°<t<=45°";
                        tempFont = (tempValue > 40 && tempValue <= 45) ? validFont : invalidFont;
                    }
                    else if (tempValue <= 50)
                    {
                        level = "T > 105 s         45°<t<=50°";
                        tempFont = (tempValue > 45 && tempValue <= 50) ? validFont : invalidFont;
                    }
                    else if (tempValue <= 55)
                    {
                        level = "T > 94 s         50°<t<=55°";
                        tempFont = (tempValue > 50 && tempValue <= 55) ? validFont : invalidFont;
                    }
                    else
                    {
                        level = "T > 85 s         55°<t<=60°";
                        tempFont = (tempValue > 55 && tempValue <= 60) ? validFont : invalidFont;
                    }

                    string reference = "\n " + level + "\n" + oldCell;


                    string motorTime = Convert.ToBoolean(values["Restart Motor Time"]) ? "OK" : "NOK";
                    iTextSharp.text.Font motorFont = Convert.ToBoolean(values["Restart Motor Time"]) ? validFont : invalidFont;

                    string pressure = Convert.ToBoolean(values["Steadiness Pressure"]) ? "OK" : "NOK";
                    iTextSharp.text.Font pressureFont = Convert.ToBoolean(values["Steadiness Pressure"]) ? validFont : invalidFont;

                    Phrase pcValvePhrase = new Phrase();
                    pcValvePhrase.Add(new Chunk("\n", normalFont));
                    pcValvePhrase.Add(new Chunk(motorTime + "   ", motorFont));
                    pcValvePhrase.Add(new Chunk(values["Oil Temp_Record"] + " °C" + "\n", tempFont));
                    pcValvePhrase.Add(new Chunk(pressure + "\n\n\n", pressureFont));

                    rowFont = ValidateResult("Valf1 High", values["Valf1 High"]);
                    pcValvePhrase.Add(new Chunk(values["Valf1 High"] + " bar   ", rowFont));
                    rowFont = ValidateResult("Valf1 Low", values["Valf1 Low"]);
                    pcValvePhrase.Add(new Chunk(values["Valf1 Low"] + " bar   ", rowFont));
                    rowFont = ValidateResult("Valf1 Diff", values["Valf1 Diff"]);
                    pcValvePhrase.Add(new Chunk(values["Valf1 Diff"] + " bar \n", rowFont));

                    rowFont = ValidateResult("Valf2 High", values["Valf2 High"]);
                    pcValvePhrase.Add(new Chunk(values["Valf2 High"] + " bar   ", rowFont));
                    rowFont = ValidateResult("Valf2 Low", values["Valf2 Low"]);
                    pcValvePhrase.Add(new Chunk(values["Valf2 Low"] + " bar   ", rowFont));
                    rowFont = ValidateResult("Valf2 Diff", values["Valf2 Diff"]);
                    pcValvePhrase.Add(new Chunk(values["Valf2 Diff"] + " bar \n", rowFont));

                    rowFont = ValidateResult("Valf3 High", values["Valf3 High"]);
                    pcValvePhrase.Add(new Chunk(values["Valf3 High"] + " bar   ", rowFont));
                    rowFont = ValidateResult("Valf3 Low", values["Valf3 Low"]);
                    pcValvePhrase.Add(new Chunk(values["Valf3 Low"] + " bar   ", rowFont));
                    rowFont = ValidateResult("Valf3 Diff", values["Valf3 Diff"]);
                    pcValvePhrase.Add(new Chunk(values["Valf3 Diff"] + " bar \n", rowFont));

                    rowFont = ValidateResult("Valf4 High", values["Valf4 High"]);
                    pcValvePhrase.Add(new Chunk(values["Valf4 High"] + " bar   ", rowFont));
                    rowFont = ValidateResult("Valf4 Low", values["Valf4 Low"]);
                    pcValvePhrase.Add(new Chunk(values["Valf4 Low"] + " bar   ", rowFont));
                    rowFont = ValidateResult("Valf4 Diff", values["Valf4 Diff"]);
                    pcValvePhrase.Add(new Chunk(values["Valf4 Diff"] + " bar \n", rowFont));

                    rowFont = ValidateResult("Valf5 High", values["Valf5 High"]);
                    pcValvePhrase.Add(new Chunk(values["Valf5 High"] + " bar   ", rowFont));
                    rowFont = ValidateResult("Valf5 Low", values["Valf5 Low"]);
                    pcValvePhrase.Add(new Chunk(values["Valf5 Low"] + " bar   ", rowFont));
                    rowFont = ValidateResult("Valf5 Diff", values["Valf5 Diff"]);
                    pcValvePhrase.Add(new Chunk(values["Valf5 Diff"] + " bar \n", rowFont));

                    rowFont = ValidateResult("Valf6 High", values["Valf6 High"]);
                    pcValvePhrase.Add(new Chunk(values["Valf6 High"] + " bar   ", rowFont));
                    rowFont = ValidateResult("Valf6 Low", values["Valf6 Low"]);
                    pcValvePhrase.Add(new Chunk(values["Valf6 Low"] + " bar   ", rowFont));
                    rowFont = ValidateResult("Valf6 Diff", values["Valf6 Diff"]);
                    pcValvePhrase.Add(new Chunk(values["Valf6 Diff"] + " bar \n", rowFont));




                    //string result = ("\n" + motorTime + "   " + values["Oil Temp_Record"] + " °C" + "\n" +
                    //          pressure + "\n\n\n" +
                    //         values["Valf1 High"] + " bar   " + values["Valf1 Low"] + " bar   " + values["Valf1 Diff"] + " bar \n" +
                    //          values["Valf2 High"] + " bar   " + values["Valf2 Low"] + " bar   " + values["Valf2 Diff"] + " bar \n" +
                    //           values["Valf3 High"] + " bar   " + values["Valf3 Low"] + " bar   " + values["Valf3 Diff"] + " bar \n" +
                    //             values["Valf4 High"] + " bar   " + values["Valf4 Low"] + " bar   " + values["Valf4 Diff"] + " bar \n" +
                    //               values["Valf5 High"] + " bar   " + values["Valf5 Low"] + " bar   " + values["Valf5 Diff"] + " bar \n" +
                    //                values["Valf6 High"] + " bar   " + values["Valf6 Low"] + " bar   " + values["Valf6 Diff"] + " bar \n"

                    //         );// +

                    cell = new PdfPCell(new Phrase("10. PC Valf Testi \n    Motor Restart Süresi \n    Çıkış Basıncı İstikrarı \n\n\n" +
                "    I1 = 0 A  \n    I2 = 0,207 A \n    I3 = 0,371 A \n    I4 = 0,535 A \n    I5 = 0,699 A \n    I6 = 0,800 A", normalFont));
                    cell.Colspan = 2;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(reference, normalFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(pcValvePhrase);
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    //11. satır

                    string sprReference = string.Empty;

                    Phrase sprPhrase = new Phrase();

                    rowFont = ValidateResult("SPRLIMIT", values["SPRLIMIT"]);

                    sprPhrase.Add(new Chunk(values["SPRLIMIT"] + " bar    ", rowFont));

                    rowFont = ValidateResult("SPRLIMIT_2", values["SPRLIMIT_2"]);

                    sprPhrase.Add(new Chunk(values["SPRLIMIT_2"] + " bar \n", rowFont));

                    rowFont = ValidateResult("Pressure Limit", values["Pressure Limit"]);

                    sprPhrase.Add(new Chunk(values["Pressure Limit"] + " ms ", rowFont));

                    sprReference = "  5 bar < P < 10 bar \n Tps equiv < 500 ms";


                    string sprValue = values["SPRLIMIT"] + " bar" + "  " + values["SPRLIMIT_2"] + " bar";



                    cell = new PdfPCell(new Phrase("11. SPR Limiti", normalFont));
                    cell.Colspan = 2;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);



                    cell = new PdfPCell(new Phrase(sprReference, normalFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(sprPhrase);
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    doc.Add(table);

                    doc.Add(new Paragraph(" "));

                    table = new PdfPTable(3);

                    table.TotalWidth = 216f;

                    widths = new float[] { 1f, 3f, 2f };
                    table.SetWidths(widths);

                    cell = new PdfPCell(new Phrase("Açıklamalar", boldFont));

                    cell.Rowspan = 1;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell.VerticalAlignment = 1;
                    cell.FixedHeight = 50f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", normalFont));

                    cell.Rowspan = 1;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell.VerticalAlignment = 1;
                    cell.FixedHeight = 50f;
                    table.AddCell(cell);

                    Phrase infoPhrase = new Phrase();
                    infoPhrase.Add(new Chunk("SİYAH ", boldFont));
                    infoPhrase.Add(new Chunk("- Normal \n", rowFont));
                    infoPhrase.Add(new Chunk("TURUNCU ", warningFont));
                    infoPhrase.Add(new Chunk("- Kabul Edilebilir \n", warningFont));
                    infoPhrase.Add(new Chunk("KIRMIZI ", invalidFont));
                    infoPhrase.Add(new Chunk("- Hata \n", invalidFont));

                    cell = new PdfPCell(infoPhrase);

                    cell.Rowspan = 1;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell.VerticalAlignment = 1;
                    cell.FixedHeight = 50f;
                    table.AddCell(cell);

                    doc.Add(table);


                    doc.Add(new Paragraph(" "));

                    table = new PdfPTable(4);

                    table.TotalWidth = 216f;

                    widths = new float[] { 1f, 2f, 1f, 2f };
                    table.SetWidths(widths);

                    cell = new PdfPCell(new Phrase("Kontrol Eden \n Sicil-İmza", boldFont));

                    cell.Rowspan = 1;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell.VerticalAlignment = 1;
                    cell.FixedHeight = 30f;

                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", normalFont));

                    cell.Rowspan = 1;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell.VerticalAlignment = 1;
                    cell.FixedHeight = 30f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Onaylayan \n Sicil-İmza", boldFont));

                    cell.Rowspan = 1;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell.VerticalAlignment = 1;
                    cell.FixedHeight = 25f;

                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", normalFont));

                    cell.Rowspan = 1;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell.VerticalAlignment = 1;
                    cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    doc.Add(table);
                }
                catch (Exception ex)
                {
                    ShowError(ex.ToString());
                }

                #endregion

                #region footer 

                string ekler = "EKLER:Sızdırmazlık (SV) Grafiği, Oransal Valf (PC) Grafiği, PG0 Grafiği, Acil Fren Basınç Limit Valfı (SPR) Grafiği";

                string page = pageNumber.ToString() + "/" + totalPageNumber.ToString();
                Paragraph ek = new Paragraph(ekler, normalFont_new);
                Paragraph footer = new Paragraph(page, normalFont);
                ek.Alignment = 1;
                // footer.Alignment = 2;
                doc.Add(ek);
                //doc.Add(footer);

                pageNumber++;

                #endregion

                #endregion

                #region sayfa 2

                doc.SetPageSize(PageSize.A4.Rotate());

                doc.NewPage();

                #region Title Table

                try
                {


                    PdfPTable table = new PdfPTable(4);

                    table.TotalWidth = 150f;

                    float[] widths = new float[] { 1f, 6f, 10f, 8f };
                    table.SetWidths(widths);

                    PdfPCell cell = new PdfPCell(new Phrase(" "));
                    cell.DisableBorderSide(iTextSharp.text.Rectangle.BOTTOM_BORDER);
                    cell.Colspan = 4;
                    cell.Rowspan = 1;
                    cell.BorderWidth = 3;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);





                    //3. satur
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Test Tarihi ve Saati", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    string strDate = dateTime + "-" + DateTime.Now.ToShortTimeString();

                    // Date Time
                    cell = new PdfPCell((new Phrase(strDate, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);



                    // 4. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Ürün Seri No", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    // Date Time
                    cell = new PdfPCell((new Phrase(itemNo, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);


                    //5. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Bogi Tipi", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    // Date Time
                    cell = new PdfPCell((new Phrase(itemType, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    //5. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Operator", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    cell = new PdfPCell((new Phrase(operatorName, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    // 7. satır

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Sipariş No (SAP)", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    cell = new PdfPCell((new Phrase(sapNo, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    // 8. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);


                    doc.Add(table);

                    Paragraph prg = new Paragraph("SIZDIRMAZLIK (SV) GRAFİĞİ", headerFont);
                    prg.Alignment = 1;
                    doc.Add(prg);
                    doc.Add(new Paragraph(" "));


                }
                catch (Exception ex)
                {
                    ShowError(ex.ToString());
                }


                #endregion

                //System.Windows.Forms.DataVisualization.Charting.Series s1 = new System.Windows.Forms.DataVisualization.Charting.Series("24V");
                //System.Windows.Forms.DataVisualization.Charting.Series s2 = new System.Windows.Forms.DataVisualization.Charting.Series("16.8V");

                //for (int i = 0; i < tco24V.Count; i++)
                //    s1.Points.AddXY(tco24V.ElementAt(i).Key, tco24V.ElementAt(i).Value);

                //for (int i = 0; i < tco16_8V.Count; i++)
                //    s2.Points.AddXY(tco16_8V.ElementAt(i).Key, tco16_8V.ElementAt(i).Value);



                //s1.Color = Color.DarkRed;
                //s2.Color = Color.Green;

                //List<System.Windows.Forms.DataVisualization.Charting.Series> list = new List<System.Windows.Forms.DataVisualization.Charting.Series>();
                //list.Add(s1);
                //list.Add(s2);

                //AddChartToDocument(doc, list, "Zaman (s)", "Basınç (Bar)", 160, 170, 10, 20);

                #region footer 
                page = pageNumber.ToString() + "/" + totalPageNumber.ToString();

                footer = new Paragraph(page, normalFont);

                footer.Alignment = 2;

                doc.Add(footer);

                pageNumber++;

                #endregion

                #endregion

                #region sayfa 3


                doc.SetPageSize(PageSize.A4.Rotate());

                doc.NewPage();

                #region Title Table

                try
                {


                    PdfPTable table = new PdfPTable(4);

                    table.TotalWidth = 216f;

                    float[] widths = new float[] { 1f, 6f, 8f, 10f };
                    table.SetWidths(widths);

                    PdfPCell cell = new PdfPCell(new Phrase(" "));
                    cell.DisableBorderSide(iTextSharp.text.Rectangle.BOTTOM_BORDER);
                    cell.Colspan = 4;
                    cell.Rowspan = 1;
                    cell.BorderWidth = 3;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);





                    //3. satur
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Test Tarihi ve Saati", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    string strDate = dateTime + "-" + DateTime.Now.ToShortTimeString();

                    // Date Time
                    cell = new PdfPCell((new Phrase(strDate, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);



                    // 4. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Ürün Seri No", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    // Date Time
                    cell = new PdfPCell((new Phrase(itemNo, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);


                    //5. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Bogi Tipi", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    // Date Time
                    cell = new PdfPCell((new Phrase(itemType, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    //5. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Operator", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    cell = new PdfPCell((new Phrase(operatorName, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    // 7. satır

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Sipariş No (SAP)", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    cell = new PdfPCell((new Phrase(sapNo, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    // 8. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);


                    doc.Add(table);

                    Paragraph prg = new Paragraph("ORANSAL VALF (PC) GRAFİĞİ", headerFont);
                    prg.Alignment = 1;
                    doc.Add(prg);
                    doc.Add(new Paragraph(" "));


                }
                catch (Exception )
                {

                    throw;
                }


                #endregion



                //s1 = new System.Windows.Forms.DataVisualization.Charting.Series("TT1_Artan");
                //s1.Color = Color.DarkRed;
                //s1.ChartType = SeriesChartType.Line;

                //s2 = new System.Windows.Forms.DataVisualization.Charting.Series("TT1_Azalan");
                //s2.Color = Color.Green;
                //s2.ChartType = SeriesChartType.Line;

                //System.Windows.Forms.DataVisualization.Charting.Series s3 = new System.Windows.Forms.DataVisualization.Charting.Series("Pt1");
                //System.Windows.Forms.DataVisualization.Charting.Series s4 = new System.Windows.Forms.DataVisualization.Charting.Series("Pt2");
                //System.Windows.Forms.DataVisualization.Charting.Series s5 = new System.Windows.Forms.DataVisualization.Charting.Series("Pt3");
                //System.Windows.Forms.DataVisualization.Charting.Series s6 = new System.Windows.Forms.DataVisualization.Charting.Series("Pt4");
                //System.Windows.Forms.DataVisualization.Charting.Series s7 = new System.Windows.Forms.DataVisualization.Charting.Series("Pt5");
                //System.Windows.Forms.DataVisualization.Charting.Series s8 = new System.Windows.Forms.DataVisualization.Charting.Series("Pt6");


                //for (int i = 0; i <= 3; i++)
                //    s3.Points.AddXY(i, 0);

                //for (int i = 19; i <= 29; i++)
                //    s4.Points.AddXY(i, 207);

                //for (int i = 44; i <= 54; i++)
                //    s5.Points.AddXY(i, 371);


                //for (int i = 69; i <= 79; i++)
                //    s6.Points.AddXY(i, 535);

                //for (int i = 94; i <= 104; i++)
                //    s7.Points.AddXY(i, 699);

                //for (int i = 104; i <= 120; i++)
                //    s8.Points.AddXY(i, 800);

                //for (int i = 0; i < tt1Increase.Count; i++)
                //    s1.Points.AddXY(tt1Increase.ElementAt(i).Key, tt1Increase.ElementAt(i).Value);

                //for (int i = 0; i < tt1Decrease.Count; i++)
                //    s2.Points.AddXY(tt1Decrease.ElementAt(i).Key, tt1Decrease.ElementAt(i).Value);


                //s1.Color = Color.Green;
                //s2.Color = Color.Blue;
                //s3.Color = Color.Red;
                //s4.Color = Color.Red;
                //s5.Color = Color.Red;
                //s6.Color = Color.Red;
                //s7.Color = Color.Red;
                //s8.Color = Color.Red;




                //list = new List<System.Windows.Forms.DataVisualization.Charting.Series>();
                //list.Add(s1);
                //list.Add(s2);
                //list.Add(s3);
                //list.Add(s4);
                //list.Add(s5);
                //list.Add(s6);
                //list.Add(s7);
                //list.Add(s8);


                //AddChartToDocument(doc, list, "Basınç (Bar)", "Akım (mA)", 120, 1000, 20, 200);

                #region footer 
                page = pageNumber.ToString() + "/" + totalPageNumber.ToString();

                footer = new Paragraph(page, normalFont);

                footer.Alignment = 2;

                doc.Add(footer);

                pageNumber++;

                #endregion

                #endregion

                #region sayfa 4 

                //// 4. sayfa

                doc.SetPageSize(PageSize.A4.Rotate());

                doc.NewPage();

                #region Title Table

                try
                {


                    PdfPTable table = new PdfPTable(4);

                    table.TotalWidth = 216f;

                    float[] widths = new float[] { 1f, 6f, 8f, 10f };
                    table.SetWidths(widths);

                    PdfPCell cell = new PdfPCell(new Phrase(" "));
                    cell.DisableBorderSide(iTextSharp.text.Rectangle.BOTTOM_BORDER);
                    cell.Colspan = 4;
                    cell.Rowspan = 1;
                    cell.BorderWidth = 3;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);





                    //3. satur
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Test Tarihi ve Saati", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    string strDate = dateTime + "-" + DateTime.Now.ToShortTimeString();

                    // Date Time
                    cell = new PdfPCell((new Phrase(strDate, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);



                    // 4. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Ürün Seri No", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    // Date Time
                    cell = new PdfPCell((new Phrase(itemNo, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);


                    //5. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Bogi Tipi", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    // Date Time
                    cell = new PdfPCell((new Phrase(itemType, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    //5. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Operator", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    cell = new PdfPCell((new Phrase(operatorName, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    // 7. satır

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Sipariş No (SAP)", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    cell = new PdfPCell((new Phrase(sapNo, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    // 8. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);


                    doc.Add(table);

                    Paragraph prg = new Paragraph("PG0 GRAFİĞİ", headerFont);
                    prg.Alignment = 1;
                    doc.Add(prg);
                    doc.Add(new Paragraph(" "));


                }
                catch (Exception )
                {

                    throw;
                }


                #endregion

                //s1 = new System.Windows.Forms.DataVisualization.Charting.Series("PG0_Artan");
                //s2 = new System.Windows.Forms.DataVisualization.Charting.Series("PG0_Azalan");

                //s1.Color = Color.DarkRed;
                //s2.Color = Color.Green;

                //for (int i = 0; i < pg0Increase.Count; i++)
                //    s1.Points.AddXY(pg0Increase.ElementAt(i).Key, pg0Increase.ElementAt(i).Value);

                //for (int i = 0; i < pg0Decrease.Count; i++)
                //    s2.Points.AddXY(pg0Decrease.ElementAt(i).Key, pg0Decrease.ElementAt(i).Value);

                //list = new List<System.Windows.Forms.DataVisualization.Charting.Series>();
                //list.Add(s1);
                //list.Add(s2);

                //AddChartToDocument(doc, list, "Basınç (Bar)", "Akım (mA)", 160, 18, 10, 1);


                #region footer 
                page = pageNumber.ToString() + "/" + totalPageNumber.ToString();

                footer = new Paragraph(page, normalFont);

                footer.Alignment = 2;

                doc.Add(footer);

                pageNumber++;

                #endregion

                #endregion

                #region sayfa 5

                //// 5. sayfa

                doc.SetPageSize(PageSize.A4.Rotate());

                doc.NewPage();

                #region Title Table

                try
                {


                    PdfPTable table = new PdfPTable(4);

                    table.TotalWidth = 216f;

                    float[] widths = new float[] { 1f, 6f, 8f, 10f };
                    table.SetWidths(widths);

                    PdfPCell cell = new PdfPCell(new Phrase(" "));
                    cell.DisableBorderSide(iTextSharp.text.Rectangle.BOTTOM_BORDER);
                    cell.Colspan = 4;
                    cell.Rowspan = 1;
                    cell.BorderWidth = 3;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);





                    //3. satur
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Test Tarihi ve Saati", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    string strDate = dateTime + "-" + DateTime.Now.ToShortTimeString();

                    // Date Time
                    cell = new PdfPCell((new Phrase(strDate, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);



                    // 4. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Ürün Seri No", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    // Date Time
                    cell = new PdfPCell((new Phrase(itemNo, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);


                    //5. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Bogi Tipi", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    // Date Time
                    cell = new PdfPCell((new Phrase(itemType, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    //5. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Operator", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    cell = new PdfPCell((new Phrase(operatorName, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    // 7. satır

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Sipariş No (SAP)", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    cell = new PdfPCell((new Phrase(sapNo, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    // 8. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);


                    doc.Add(table);

                    Paragraph prg = new Paragraph("ACİL FREN BASINÇ LİMİT VALFİ (SPR) GRAFİĞİ", headerFont);
                    prg.Alignment = 1;
                    doc.Add(prg);
                    doc.Add(new Paragraph(" "));


                }
                catch (Exception )
                {
                    throw;
                }


                #endregion

                //s1 = new System.Windows.Forms.DataVisualization.Charting.Series("TT1");

                //for (int i = 0; i < spr.Count; i++)
                //    s1.Points.AddXY(spr.ElementAt(i).Key, spr.ElementAt(i).Value);

                //s1.Color = Color.DarkRed;

                //list = new List<System.Windows.Forms.DataVisualization.Charting.Series>();
                //list.Add(s1);

                //AddChartToDocument(doc, list, "Zaman (s)", "Basınç (Bar)", 20, 160, 10, 20);


                #region footer 
                page = pageNumber.ToString() + "/" + totalPageNumber.ToString();

                footer = new Paragraph(page, normalFont);

                footer.Alignment = 2;

                doc.Add(footer);

                pageNumber++;

                #endregion

                #endregion




                doc.Close();

                ShowError("Rapor başarıyla kaydedildi");




            }
            catch (Exception ex)
            {
                ShowError("Rapor kaydedilemedi");
                Console.WriteLine(ex.ToString());
            }




        }

        System.Windows.Forms.DialogResult ShowError(string message)  //I call this function in another thread
        {
            System.Windows.Forms.DialogResult result = System.Windows.Forms.DialogResult.None;

            //FlexibleMessageBox.FONT = new System.Drawing.Font("Arial", 16);

            //result = FlexibleMessageBox.Show(message, "Uyarı", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand);

            return result;
        }

        public void GenerateReportTrailer(ObservableConcurrentDictionary<string, string> values, Dictionary<double, double> tco24V, Dictionary<double, double> tco16_8V, Dictionary<double, double> tt1Increase, Dictionary<double, double> tt1Decrease, Dictionary<double, double> pg0Increase, Dictionary<double, double> pg0Decrease, Dictionary<double, double> spr, Dictionary<double, double> pg1Increase, Dictionary<double, double> pg1Decrease)
        {
            try

            {

                //// pg0Increase ve pg0Decrease değerlerini dosyaya yazdırıyor.

                //using (StreamWriter file = new StreamWriter("pg0Inc.txt"))
                //    foreach (var entry in pg0Increase)
                //        file.WriteLine("[{0} {1}]", entry.Key, entry.Value);

                //using (StreamWriter file = new StreamWriter("pg0Dec.txt"))
                //    foreach (var entry in pg0Decrease)
                //        file.WriteLine("[{0} {1}]", entry.Key, entry.Value);

                //LogtoFile(tco24V, tco16_8V, tt1Increase, tt1Decrease, pg0Increase, pg0Decrease, spr, pg0Increase, pg0Decrease);

                reportType = "Taşıyıcı";
                totalPageNumber = 6;



                FileStream fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite);


                Document doc = new Document(PageSize.A4, 20, 20, 20, 20);

                PdfWriter writer = PdfWriter.GetInstance(doc, fs);

                BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, "Cp1254", BaseFont.EMBEDDED);


                validFont = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                invalidFont = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.BOLD, new BaseColor(255, 0, 0));
                warningFont = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.BOLD, new BaseColor(255, 103, 0));

                doc.Open();




                // pdf


                iTextSharp.text.Font normalFont_new = new iTextSharp.text.Font(bf, 9, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                iTextSharp.text.Font normalFont = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                iTextSharp.text.Font boldFont = new iTextSharp.text.Font(bf, 11, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

                iTextSharp.text.Font headerFont = new iTextSharp.text.Font(bf, 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK);




                #region sayfa 1

                AddMfilesTitleForTasiyici(doc);


                #region Title Table

                try
                {


                    PdfPTable table = new PdfPTable(4);

                    table.TotalWidth = 216f;

                    float[] widths = new float[] { 1f, 6f, 8f, 10f };
                    table.SetWidths(widths);

                    PdfPCell cell = new PdfPCell(new Phrase(" "));
                    cell.DisableBorderSide(iTextSharp.text.Rectangle.BOTTOM_BORDER);
                    cell.Colspan = 4;
                    cell.Rowspan = 1;
                    cell.BorderWidth = 3;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);





                    //3. satur
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Test Tarihi ve Saati", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    string strDate = dateTime + "-" + DateTime.Now.ToShortTimeString();

                    // Date Time
                    cell = new PdfPCell((new Phrase(strDate, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);



                    // 4. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Ürün Seri No", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    // Date Time
                    cell = new PdfPCell((new Phrase(itemNo, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);


                    //5. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Bogi Tipi", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    // Date Time
                    cell = new PdfPCell((new Phrase(itemType, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    //5. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Operator", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    cell = new PdfPCell((new Phrase(operatorName, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    // 7. satır

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Sipariş No (SAP)", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    cell = new PdfPCell((new Phrase(sapNo, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    // 8. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);


                    doc.Add(table);


                    doc.Add(new Paragraph(" "));


                }
                catch (Exception )
                {

                    throw;
                }


                #endregion

                #region Result Table 

                try
                {
                    PdfPTable table = new PdfPTable(4);

                    float[] widths = new float[] { 1f, 6f, 8f, 10f };

                    table.TotalWidth = 216f;

                    table.SetWidths(widths);

                    // 1. satır


                    Paragraph prg = new Paragraph("TEST ADIMLARI", boldFont);
                    prg.Alignment = 1;
                    doc.Add(prg);
                    doc.Add(new Paragraph(" "));

                    PdfPCell cell = new PdfPCell(new Phrase("Test Adı", boldFont));
                    cell.Colspan = 2;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Hedef", boldFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Sonuç", boldFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);


                    //2.satır

                    iTextSharp.text.Font rowFont = ValidateResult("Görsel", isControlled);

                    cell = new PdfPCell(new Phrase("1. Görsel Kontrol", normalFont));
                    cell.Colspan = 2;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Evet", normalFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(isControlled, rowFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);


                    //2.satır
                    rowFont = ValidateResult("Yağ Seviyesi", oilLevel);

                    cell = new PdfPCell(new Phrase("2. Yağ Seviyesi", normalFont));
                    cell.Colspan = 2;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("İyi", normalFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(oilLevel, rowFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    //3.satır
                    rowFont = ValidateResult("Yağ Kaçağı", oilLeakage);
                    cell = new PdfPCell(new Phrase("3. Yağ Kaçağı", normalFont));
                    cell.Colspan = 2;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("İyi", normalFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(oilLeakage, rowFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);




                    // 5. satır

                    string pr = values["PRLIMIT"] + " bar";


                    rowFont = ValidateResult("PRLIMIT", values["PRLIMIT"]);

                    cell = new PdfPCell(new Phrase("4. PR Basınç Limit Valfi", normalFont));
                    cell.Colspan = 2;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("162 bar < P < 172 bar", normalFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(pr, rowFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);


                    // 6. satır

                    Phrase pg0Phrase = new Phrase();

                    pg0Phrase.Add(new Chunk("\n\n", normalFont));

                    rowFont = ValidateResult("PG0 INCREASING_1", values["PG0 INCREASING_1"]);
                    pg0Phrase.Add(new Chunk(values["PG0 INCREASING_1"] + " mA \n", rowFont));

                    rowFont = ValidateResult("PG0 INCREASING_2", values["PG0 INCREASING_2"]);
                    pg0Phrase.Add(new Chunk(values["PG0 INCREASING_2"] + " mA \n", rowFont));

                    rowFont = ValidateResult("PG0 DECREASING", values["PG0 DECREASING"]);
                    pg0Phrase.Add(new Chunk(values["PG0 DECREASING"] + " mA \n", rowFont));

                    //string pg0 = ("\n\n" + values["PG0 INCREASING_1"] + " mA \n" +
                    //                         values["PG0 INCREASING_2"] + " mA \n" +
                    //                         values["PG0 DECREASING"] + " mA \n");


                    cell = new PdfPCell(new Phrase("5. PG0\n\n    P Artan    P = 0 bar\n    P Artan    P = 150 bar\n    P Azalan  P = 0 bar", normalFont));
                    cell.Colspan = 2;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    string pg0Reference = "";

                    pg0Reference = "\n\n 3,84 mA < I < 4,16 mA \n 15,84 mA < I < 16,16 mA \n 3,84 mA < I < 4,16 mA";


                    cell = new PdfPCell(new Phrase(pg0Reference, normalFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(pg0Phrase);
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);


                    // 7. satır
                    // Taşıyıcı ise pg1 eklencek

                    Phrase pg1Phrase = new Phrase();

                    pg1Phrase.Add(new Chunk("\n\n", normalFont));

                    rowFont = ValidateResult("PG1 Increasing_1", values["PG1 Increasing_1"]);
                    pg1Phrase.Add(new Chunk(values["PG1 Increasing_1"] + " mA \n", rowFont));

                    rowFont = ValidateResult("PG1 Increasing_2", values["PG1 Increasing_2"]);
                    pg1Phrase.Add(new Chunk(values["PG1 Increasing_2"] + " mA \n", rowFont));

                    rowFont = ValidateResult("PG1 Decreasing", values["PG1 Decreasing"]);
                    pg1Phrase.Add(new Chunk(values["PG1 Decreasing"] + " mA \n", rowFont));


                    //string pg1 = ("\n\n" + values["PG1 Increasing_1"] + " mA \n" +
                    //               values["PG1 Increasing_2"] + " mA \n" +
                    //               values["PG1 Decreasing"] + " mA \n");


                    cell = new PdfPCell(new Phrase("6. PG1\n\n    P Artan    P = 0 bar\n    P Artan    P = 150 bar\n    P Azalan  P = 0 bar", normalFont));
                    cell.Colspan = 2;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    string pg1Reference = "\n\n 3,84 mA < I < 4,16 mA \n 11,80 mA < I < 12,20 mA \n 3,84 mA < I < 4,16 mA";


                    cell = new PdfPCell(new Phrase(pg1Reference, normalFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(pg1Phrase);
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);



                    // 7. satır

                    string pressTime = values["YAG DOLDURMA SURESI"] + " s";

                    rowFont = ValidateResult("YAG DOLDURMA SURESI", values["YAG DOLDURMA SURESI"]);

                    cell = new PdfPCell(new Phrase("7. Yağ Doldurma Süresi", normalFont));
                    cell.Colspan = 2;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("T < 35   U=24 VDC", normalFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(pressTime, rowFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    // 8. satır

                    string tco = values["TCO Basınç Farkı"] + " bar";

                    rowFont = ValidateResult("TCO Basınç Farkı", values["TCO Basınç Farkı"]);


                    cell = new PdfPCell(new Phrase("8. Sızdırmazlık Ölçümü", normalFont));
                    cell.Colspan = 2;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("∆P < 25 bar  (60s)", normalFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(tco, rowFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);



                    // 9. satır

                    rowFont = ValidateResult("SV Valve", values["SV Valve"]);
                    string sv = values["SV Valve"] + " bar";
                    cell = new PdfPCell(new Phrase("9. SV Değeri", normalFont));
                    cell.Colspan = 2;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("  P < 2 bar   OK", normalFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(sv, rowFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);



                    string oldCell = " <± 1,5 bar/period 2s \n\n Hyst < 5 bar \n 0 bar < P < 3 bar \n 19 bar < P < 29 bar \n 44 bar < P < 54 bar"
                              + "\n" + "69 bar < P < 79 bar  \n 94 bar < P < 104 bar \n 104 bar < P < 120 bar \n + Tps equiv < 0,8s ";
                    string level = string.Empty;
                    double tempValue = Convert.ToDouble(values["Oil Temp_Record"]);
                    iTextSharp.text.Font tempFont;

                    if (tempValue <= 10)
                    {
                        level = "T > 1180 s         t<=10°";
                        tempFont = tempValue <= 10 ? validFont : invalidFont;


                    }
                    else if (tempValue <= 20)
                    {
                        level = "T > 330 s         10°<t<=20°";
                        tempFont = (tempValue > 10 && tempValue <= 20) ? validFont : invalidFont;
                    }
                    else if (tempValue <= 25)
                    {
                        level = "T > 242 s         20°<t<=25°";
                        tempFont = (tempValue > 20 && tempValue <= 25) ? validFont : invalidFont;
                    }
                    else if (tempValue <= 30)
                    {
                        level = "T > 192 s         25°<t<=30°";
                        tempFont = (tempValue > 25 && tempValue <= 30) ? validFont : invalidFont;
                    }
                    else if (tempValue <= 35)
                    {
                        level = "T > 159 s         30°<t<=35°";
                        tempFont = (tempValue > 30 && tempValue <= 35) ? validFont : invalidFont;

                    }
                    else if (tempValue <= 40)
                    {
                        level = "T > 135 s         35°<t<=40°";
                        tempFont = (tempValue > 35 && tempValue <= 40) ? validFont : invalidFont;
                    }
                    else if (tempValue <= 45)
                    {
                        level = "T > 118 s         40°<t<=45°";
                        tempFont = (tempValue > 40 && tempValue <= 45) ? validFont : invalidFont;
                    }
                    else if (tempValue <= 50)
                    {
                        level = "T > 105 s         45°<t<=50°";
                        tempFont = (tempValue > 45 && tempValue <= 50) ? validFont : invalidFont;
                    }
                    else if (tempValue <= 55)
                    {
                        level = "T > 94 s         50°<t<=55°";
                        tempFont = (tempValue > 50 && tempValue <= 55) ? validFont : invalidFont;
                    }
                    else
                    {
                        level = "T > 85 s         55°<t<=60°";
                        tempFont = (tempValue > 55 && tempValue <= 60) ? validFont : invalidFont;
                    }

                    string reference = "\n " + level + "\n" + oldCell;


                    string motorTime = Convert.ToBoolean(values["Restart Motor Time"]) ? "OK" : "NOK";
                    iTextSharp.text.Font motorFont = Convert.ToBoolean(values["Restart Motor Time"]) ? validFont : invalidFont;

                    string pressure = Convert.ToBoolean(values["Steadiness Pressure"]) ? "OK" : "NOK";
                    iTextSharp.text.Font pressureFont = Convert.ToBoolean(values["Steadiness Pressure"]) ? validFont : invalidFont;

                    Phrase pcValvePhrase = new Phrase();
                    pcValvePhrase.Add(new Chunk("\n", normalFont));
                    pcValvePhrase.Add(new Chunk(motorTime + "   ", motorFont));
                    pcValvePhrase.Add(new Chunk(values["Oil Temp_Record"] + " °C" + "\n", tempFont));
                    pcValvePhrase.Add(new Chunk(pressure + "\n\n\n", pressureFont));

                    rowFont = ValidateResult("Valf1 High", values["Valf1 High"]);
                    pcValvePhrase.Add(new Chunk(values["Valf1 High"] + " bar   ", rowFont));
                    rowFont = ValidateResult("Valf1 Low", values["Valf1 Low"]);
                    pcValvePhrase.Add(new Chunk(values["Valf1 Low"] + " bar   ", rowFont));
                    rowFont = ValidateResult("Valf1 Diff", values["Valf1 Diff"]);
                    pcValvePhrase.Add(new Chunk(values["Valf1 Diff"] + " bar \n", rowFont));

                    rowFont = ValidateResult("Valf2 High", values["Valf2 High"]);
                    pcValvePhrase.Add(new Chunk(values["Valf2 High"] + " bar   ", rowFont));
                    rowFont = ValidateResult("Valf2 Low", values["Valf2 Low"]);
                    pcValvePhrase.Add(new Chunk(values["Valf2 Low"] + " bar   ", rowFont));
                    rowFont = ValidateResult("Valf2 Diff", values["Valf2 Diff"]);
                    pcValvePhrase.Add(new Chunk(values["Valf2 Diff"] + " bar \n", rowFont));

                    rowFont = ValidateResult("Valf3 High", values["Valf3 High"]);
                    pcValvePhrase.Add(new Chunk(values["Valf3 High"] + " bar   ", rowFont));
                    rowFont = ValidateResult("Valf3 Low", values["Valf3 Low"]);
                    pcValvePhrase.Add(new Chunk(values["Valf3 Low"] + " bar   ", rowFont));
                    rowFont = ValidateResult("Valf3 Diff", values["Valf3 Diff"]);
                    pcValvePhrase.Add(new Chunk(values["Valf3 Diff"] + " bar \n", rowFont));

                    rowFont = ValidateResult("Valf4 High", values["Valf4 High"]);
                    pcValvePhrase.Add(new Chunk(values["Valf4 High"] + " bar   ", rowFont));
                    rowFont = ValidateResult("Valf4 Low", values["Valf4 Low"]);
                    pcValvePhrase.Add(new Chunk(values["Valf4 Low"] + " bar   ", rowFont));
                    rowFont = ValidateResult("Valf4 Diff", values["Valf4 Diff"]);
                    pcValvePhrase.Add(new Chunk(values["Valf4 Diff"] + " bar \n", rowFont));

                    rowFont = ValidateResult("Valf5 High", values["Valf5 High"]);
                    pcValvePhrase.Add(new Chunk(values["Valf5 High"] + " bar   ", rowFont));
                    rowFont = ValidateResult("Valf5 Low", values["Valf5 Low"]);
                    pcValvePhrase.Add(new Chunk(values["Valf5 Low"] + " bar   ", rowFont));
                    rowFont = ValidateResult("Valf5 Diff", values["Valf5 Diff"]);
                    pcValvePhrase.Add(new Chunk(values["Valf5 Diff"] + " bar \n", rowFont));

                    rowFont = ValidateResult("Valf6 High", values["Valf6 High"]);
                    pcValvePhrase.Add(new Chunk(values["Valf6 High"] + " bar   ", rowFont));
                    rowFont = ValidateResult("Valf6 Low", values["Valf6 Low"]);
                    pcValvePhrase.Add(new Chunk(values["Valf6 Low"] + " bar   ", rowFont));
                    rowFont = ValidateResult("Valf6 Diff", values["Valf6 Diff"]);
                    pcValvePhrase.Add(new Chunk(values["Valf6 Diff"] + " bar \n", rowFont));




                    //string result = ("\n" + motorTime + "   " + values["Oil Temp_Record"] + " °C" + "\n" +
                    //          pressure + "\n\n\n" +
                    //         values["Valf1 High"] + " bar   " + values["Valf1 Low"] + " bar   " + values["Valf1 Diff"] + " bar \n" +
                    //          values["Valf2 High"] + " bar   " + values["Valf2 Low"] + " bar   " + values["Valf2 Diff"] + " bar \n" +
                    //           values["Valf3 High"] + " bar   " + values["Valf3 Low"] + " bar   " + values["Valf3 Diff"] + " bar \n" +
                    //             values["Valf4 High"] + " bar   " + values["Valf4 Low"] + " bar   " + values["Valf4 Diff"] + " bar \n" +
                    //               values["Valf5 High"] + " bar   " + values["Valf5 Low"] + " bar   " + values["Valf5 Diff"] + " bar \n" +
                    //                values["Valf6 High"] + " bar   " + values["Valf6 Low"] + " bar   " + values["Valf6 Diff"] + " bar \n"

                    //         );// +

                    cell = new PdfPCell(new Phrase("10. PC Valf Testi \n    Motor Restart Süresi \n    Çıkış Basıncı İstikrarı \n\n\n" +
                "    I1 = 0 A  \n    I2 = 0,207 A \n    I3 = 0,371 A \n    I4 = 0,535 A \n    I5 = 0,699 A \n    I6 = 0,800 A", normalFont));
                    cell.Colspan = 2;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(reference, normalFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(pcValvePhrase);
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    //SPR Limit

                    string sprReference = string.Empty;

                    Phrase sprPhrase = new Phrase();

                    rowFont = ValidateResult("SPRLIMIT", values["SPRLIMIT"]);

                    sprPhrase.Add(new Chunk(values["SPRLIMIT"] + " bar    ", rowFont));

                    rowFont = ValidateResult("SPRLIMIT_2", values["SPRLIMIT_2"]);

                    sprPhrase.Add(new Chunk(values["SPRLIMIT_2"] + " bar \n", rowFont));

                    rowFont = ValidateResult("Pressure Limit", values["Pressure Limit"]);

                    sprPhrase.Add(new Chunk(values["Pressure Limit"] + " ms ", rowFont));

                    sprReference = "  30 bar < P < 40 bar  \n Tps equiv < 500 ms";


                    string sprValue = values["SPRLIMIT"] + " bar" + "  " + values["SPRLIMIT_2"] + " bar";



                    cell = new PdfPCell(new Phrase("11. SPR Limiti", normalFont));
                    cell.Colspan = 2;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 0; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);



                    cell = new PdfPCell(new Phrase(sprReference, normalFont));
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    cell = new PdfPCell(sprPhrase);
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);

                    doc.Add(table);

                    doc.Add(new Paragraph(" "));

                    table = new PdfPTable(3);

                    table.TotalWidth = 216f;

                    widths = new float[] { 1f, 3f, 2f };
                    table.SetWidths(widths);

                    cell = new PdfPCell(new Phrase("Açıklamalar", boldFont));

                    cell.Rowspan = 1;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell.VerticalAlignment = 1;
                    cell.FixedHeight = 50f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", normalFont));

                    cell.Rowspan = 1;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell.VerticalAlignment = 1;
                    cell.FixedHeight = 50f;
                    table.AddCell(cell);

                    Phrase infoPhrase = new Phrase();
                    infoPhrase.Add(new Chunk("SİYAH ", boldFont));
                    infoPhrase.Add(new Chunk("- Normal \n", rowFont));
                    infoPhrase.Add(new Chunk("TURUNCU ", warningFont));
                    infoPhrase.Add(new Chunk("- Kabul Edilebilir \n", warningFont));
                    infoPhrase.Add(new Chunk("KIRMIZI ", invalidFont));
                    infoPhrase.Add(new Chunk("- Hata \n", invalidFont));

                    cell = new PdfPCell(infoPhrase);

                    cell.Rowspan = 1;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell.VerticalAlignment = 1;
                    cell.FixedHeight = 50f;
                    table.AddCell(cell);

                    doc.Add(table);


                    doc.Add(new Paragraph(" "));

                    table = new PdfPTable(4);

                    table.TotalWidth = 216f;

                    widths = new float[] { 1f, 2f, 1f, 2f };
                    table.SetWidths(widths);

                    cell = new PdfPCell(new Phrase("Kontrol Eden \n Sicil-İmza", boldFont));

                    cell.Rowspan = 1;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell.VerticalAlignment = 1;
                    cell.FixedHeight = 30f;

                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", normalFont));

                    cell.Rowspan = 1;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell.VerticalAlignment = 1;
                    cell.FixedHeight = 30f;
                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Onaylayan \n Sicil-İmza", boldFont));

                    cell.Rowspan = 1;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell.VerticalAlignment = 1;
                    cell.FixedHeight = 25f;

                    table.AddCell(cell);

                    cell = new PdfPCell(new Phrase(" ", normalFont));

                    cell.Rowspan = 1;
                    cell.BorderWidth = 1;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    cell.VerticalAlignment = 1;
                    cell.FixedHeight = 25f;
                    table.AddCell(cell);

                    doc.Add(table);
                }
                catch (Exception ex)
                {
                    ShowError(ex.ToString());
                }

                #endregion

                #region footer 


                string ekler = "EKLER:Sızdırmazlık(SV) Grafiği, Oransal Valf (PC) Grafiği, PG0 ve PG1 Grafiği, Acil Fren Basınç Limit Valfı (SPR) Grafiği";

                string page = pageNumber.ToString() + "/" + totalPageNumber.ToString();
                Paragraph ek = new Paragraph(ekler, normalFont_new);
                Paragraph footer = new Paragraph(page, normalFont);
                ek.Alignment = 1;
                // footer.Alignment = 2;
                doc.Add(ek);
                //doc.Add(footer);

                //string page = pageNumber.ToString() + "/" + totalPageNumber.ToString();

                //Paragraph footer = new Paragraph(page, normalFont);

                //footer.Alignment = 2;

                //doc.Add(footer);

                pageNumber++;

                #endregion

                #endregion

                #region sayfa 2

                doc.SetPageSize(PageSize.A4.Rotate());

                doc.NewPage();

                #region Title Table

                try
                {


                    PdfPTable table = new PdfPTable(4);

                    table.TotalWidth = 150f;

                    float[] widths = new float[] { 1f, 6f, 10f, 8f };
                    table.SetWidths(widths);

                    PdfPCell cell = new PdfPCell(new Phrase(" "));
                    cell.DisableBorderSide(iTextSharp.text.Rectangle.BOTTOM_BORDER);
                    cell.Colspan = 4;
                    cell.Rowspan = 1;
                    cell.BorderWidth = 3;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);





                    //3. satur
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Test Tarihi ve Saati", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    string strDate = dateTime + "-" + DateTime.Now.ToShortTimeString();

                    // Date Time
                    cell = new PdfPCell((new Phrase(strDate, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);



                    // 4. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Ürün Seri No", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    // Date Time
                    cell = new PdfPCell((new Phrase(itemNo, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);


                    //5. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Bogi Tipi", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    // Date Time
                    cell = new PdfPCell((new Phrase(itemType, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    //5. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Operator", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    cell = new PdfPCell((new Phrase(operatorName, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    // 7. satır

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Sipariş No (SAP)", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    cell = new PdfPCell((new Phrase(sapNo, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    // 8. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);


                    doc.Add(table);

                    Paragraph prg = new Paragraph("SIZDIRMAZLIK (SV) GRAFİĞİ", headerFont);
                    prg.Alignment = 1;
                    doc.Add(prg);
                    doc.Add(new Paragraph(" "));


                }
                catch (Exception ex)
                {
                    ShowError(ex.ToString());
                }


                #endregion

                //System.Windows.Forms.DataVisualization.Charting.Series s1 = new System.Windows.Forms.DataVisualization.Charting.Series("24V");
                //System.Windows.Forms.DataVisualization.Charting.Series s2 = new System.Windows.Forms.DataVisualization.Charting.Series("16.8V");

                //for (int i = 0; i < tco24V.Count; i++)
                //    s1.Points.AddXY(tco24V.ElementAt(i).Key, tco24V.ElementAt(i).Value);

                //for (int i = 0; i < tco16_8V.Count; i++)
                //    s2.Points.AddXY(tco16_8V.ElementAt(i).Key, tco16_8V.ElementAt(i).Value);



                //s1.Color = Color.DarkRed;
                //s2.Color = Color.Green;

                //List<System.Windows.Forms.DataVisualization.Charting.Series> list = new List<System.Windows.Forms.DataVisualization.Charting.Series>();
                //list.Add(s1);
                //list.Add(s2);

                //AddChartToDocument(doc, list, "Zaman (s)", "Basınç (Bar)", 160, 170, 10, 20);

                #region footer 
                page = pageNumber.ToString() + "/" + totalPageNumber.ToString();

                footer = new Paragraph(page, normalFont);

                footer.Alignment = 2;

                doc.Add(footer);
                pageNumber++;

                #endregion

                #endregion

                #region sayfa 3


                doc.SetPageSize(PageSize.A4.Rotate());

                doc.NewPage();

                #region Title Table

                try
                {


                    PdfPTable table = new PdfPTable(4);

                    table.TotalWidth = 216f;

                    float[] widths = new float[] { 1f, 6f, 8f, 10f };
                    table.SetWidths(widths);

                    PdfPCell cell = new PdfPCell(new Phrase(" "));
                    cell.DisableBorderSide(iTextSharp.text.Rectangle.BOTTOM_BORDER);
                    cell.Colspan = 4;
                    cell.Rowspan = 1;
                    cell.BorderWidth = 3;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);





                    //3. satur
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Test Tarihi ve Saati", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    string strDate = dateTime + "-" + DateTime.Now.ToShortTimeString();

                    // Date Time
                    cell = new PdfPCell((new Phrase(strDate, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);



                    // 4. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Ürün Seri No", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    // Date Time
                    cell = new PdfPCell((new Phrase(itemNo, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);


                    //5. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Bogi Tipi", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    // Date Time
                    cell = new PdfPCell((new Phrase(itemType, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    //5. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Operator", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    cell = new PdfPCell((new Phrase(operatorName, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    // 7. satır

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Sipariş No (SAP)", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    cell = new PdfPCell((new Phrase(sapNo, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    // 8. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);


                    doc.Add(table);

                    Paragraph prg = new Paragraph("ORANSAL VALF (PC) GRAFİĞİ", headerFont);
                    prg.Alignment = 1;
                    doc.Add(prg);
                    doc.Add(new Paragraph(" "));


                }
                catch (Exception ex)
                {
                    ShowError(ex.ToString());
                }


                #endregion



                //s1 = new System.Windows.Forms.DataVisualization.Charting.Series("TT1_Artan");
                //s2 = new System.Windows.Forms.DataVisualization.Charting.Series("TT1_Azalan");
                //System.Windows.Forms.DataVisualization.Charting.Series s3 = new System.Windows.Forms.DataVisualization.Charting.Series("Pt1");
                //System.Windows.Forms.DataVisualization.Charting.Series s4 = new System.Windows.Forms.DataVisualization.Charting.Series("Pt2");
                //System.Windows.Forms.DataVisualization.Charting.Series s5 = new System.Windows.Forms.DataVisualization.Charting.Series("Pt3");
                //System.Windows.Forms.DataVisualization.Charting.Series s6 = new System.Windows.Forms.DataVisualization.Charting.Series("Pt4");
                //System.Windows.Forms.DataVisualization.Charting.Series s7 = new System.Windows.Forms.DataVisualization.Charting.Series("Pt5");
                //System.Windows.Forms.DataVisualization.Charting.Series s8 = new System.Windows.Forms.DataVisualization.Charting.Series("Pt6");


                //for (int i = 0; i <= 3; i++)
                //    s3.Points.AddXY(i, 0);

                //for (int i = 19; i <= 29; i++)
                //    s4.Points.AddXY(i, 207);

                //for (int i = 44; i <= 54; i++)
                //    s5.Points.AddXY(i, 371);


                //for (int i = 69; i <= 79; i++)
                //    s6.Points.AddXY(i, 535);

                //for (int i = 94; i <= 104; i++)
                //    s7.Points.AddXY(i, 699);

                //for (int i = 104; i <= 120; i++)
                //    s8.Points.AddXY(i, 800);

                //for (int i = 0; i < tt1Increase.Count; i++)
                //    s1.Points.AddXY(tt1Increase.ElementAt(i).Key, tt1Increase.ElementAt(i).Value);

                //for (int i = 0; i < tt1Decrease.Count; i++)
                //    s2.Points.AddXY(tt1Decrease.ElementAt(i).Key, tt1Decrease.ElementAt(i).Value);


                //s1.Color = Color.DarkRed;
                //s2.Color = Color.Green;

                //s3.Color = Color.Red;
                //s4.Color = Color.Red;
                //s5.Color = Color.Red;
                //s6.Color = Color.Red;
                //s7.Color = Color.Red;
                //s8.Color = Color.Red;




                //list = new List<System.Windows.Forms.DataVisualization.Charting.Series>();
                //list.Add(s1);
                //list.Add(s2);

                //list.Add(s3);
                //list.Add(s4);
                //list.Add(s5);
                //list.Add(s6);
                //list.Add(s7);
                //list.Add(s8);


                //AddChartToDocument(doc, list, "Basınç (Bar)", "Akım (mA)", 120, 1000, 20, 200);

                #region footer 
                page = pageNumber.ToString() + "/" + totalPageNumber.ToString();

                footer = new Paragraph(page, normalFont);

                footer.Alignment = 2;

                doc.Add(footer);

                pageNumber++;

                #endregion

                #endregion

                #region sayfa 4 

                //// 4. sayfa

                doc.SetPageSize(PageSize.A4.Rotate());

                doc.NewPage();

                #region Title Table

                try
                {


                    PdfPTable table = new PdfPTable(4);

                    table.TotalWidth = 216f;

                    float[] widths = new float[] { 1f, 6f, 8f, 10f };
                    table.SetWidths(widths);

                    PdfPCell cell = new PdfPCell(new Phrase(" "));
                    cell.DisableBorderSide(iTextSharp.text.Rectangle.BOTTOM_BORDER);
                    cell.Colspan = 4;
                    cell.Rowspan = 1;
                    cell.BorderWidth = 3;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);





                    //3. satur
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Test Tarihi ve Saati", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    string strDate = dateTime + "-" + DateTime.Now.ToShortTimeString();

                    // Date Time
                    cell = new PdfPCell((new Phrase(strDate, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);



                    // 4. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Ürün Seri No", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    // Date Time
                    cell = new PdfPCell((new Phrase(itemNo, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);


                    //5. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Bogi Tipi", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    // Date Time
                    cell = new PdfPCell((new Phrase(itemType, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    //5. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Operator", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    cell = new PdfPCell((new Phrase(operatorName, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    // 7. satır

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Sipariş No (SAP)", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    cell = new PdfPCell((new Phrase(sapNo, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    // 8. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);


                    doc.Add(table);

                    Paragraph prg = new Paragraph("PG0 GRAFİĞİ", headerFont);
                    prg.Alignment = 1;
                    doc.Add(prg);
                    doc.Add(new Paragraph(" "));


                }
                catch (Exception ex)
                {
                    ShowError(ex.ToString());
                }


                #endregion

                //s1 = new System.Windows.Forms.DataVisualization.Charting.Series("PG0_Artan");
                //s2 = new System.Windows.Forms.DataVisualization.Charting.Series("PG0_Azalan");

                //s1.Color = Color.DarkRed;
                //s2.Color = Color.Green;

                //for (int i = 0; i < pg0Increase.Count; i++)
                //    s1.Points.AddXY(pg0Increase.ElementAt(i).Key, pg0Increase.ElementAt(i).Value);

                //for (int i = 0; i < pg0Decrease.Count; i++)
                //    s2.Points.AddXY(pg0Decrease.ElementAt(i).Key, pg0Decrease.ElementAt(i).Value);

                //list = new List<System.Windows.Forms.DataVisualization.Charting.Series>();
                //list.Add(s1);
                //list.Add(s2);

                //AddChartToDocument(doc, list, "Basınç (Bar)", "Akım (mA)", 160, 18, 10, 1);


                #region footer 
                page = pageNumber.ToString() + "/" + totalPageNumber.ToString();

                footer = new Paragraph(page, normalFont);

                footer.Alignment = 2;

                doc.Add(footer);

                pageNumber++;

                #endregion

                #endregion

                #region sayfa 5

                //// 5. sayfa

                doc.SetPageSize(PageSize.A4.Rotate());

                doc.NewPage();

                #region Title Table

                try
                {


                    PdfPTable table = new PdfPTable(4);

                    table.TotalWidth = 216f;

                    float[] widths = new float[] { 1f, 6f, 8f, 10f };
                    table.SetWidths(widths);

                    PdfPCell cell = new PdfPCell(new Phrase(" "));
                    cell.DisableBorderSide(iTextSharp.text.Rectangle.BOTTOM_BORDER);
                    cell.Colspan = 4;
                    cell.Rowspan = 1;
                    cell.BorderWidth = 3;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);





                    //3. satur
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Test Tarihi ve Saati", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    string strDate = dateTime + "-" + DateTime.Now.ToShortTimeString();

                    // Date Time
                    cell = new PdfPCell((new Phrase(strDate, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);



                    // 4. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Ürün Seri No", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    // Date Time
                    cell = new PdfPCell((new Phrase(itemNo, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);


                    //5. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Bogi Tipi", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    // Date Time
                    cell = new PdfPCell((new Phrase(itemType, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    //5. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Operator", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    cell = new PdfPCell((new Phrase(operatorName, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    // 7. satır

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Sipariş No (SAP)", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    cell = new PdfPCell((new Phrase(sapNo, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    // 8. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);


                    doc.Add(table);

                    Paragraph prg = new Paragraph("ACİL FREN BASINÇ LİMİT VALFİ (SPR) GRAFİĞİ", headerFont);
                    prg.Alignment = 1;
                    doc.Add(prg);
                    doc.Add(new Paragraph(" "));


                }
                catch (Exception ex)
                {
                    ShowError(ex.ToString());
                }


                #endregion

                //s1 = new System.Windows.Forms.DataVisualization.Charting.Series("TT1");

                //for (int i = 0; i < spr.Count; i++)
                //    s1.Points.AddXY(spr.ElementAt(i).Key, spr.ElementAt(i).Value);

                //s1.Color = Color.DarkRed;

                //list = new List<System.Windows.Forms.DataVisualization.Charting.Series>();
                //list.Add(s1);

                //AddChartToDocument(doc, list, "Zaman (s)", "Basınç (Bar)", 20, 160, 10, 20);


                #region footer 
                page = pageNumber.ToString() + "/" + totalPageNumber.ToString();

                footer = new Paragraph(page, normalFont);

                footer.Alignment = 2;

                doc.Add(footer);

                pageNumber++;

                #endregion

                #endregion

                #region sayfa 6 

                //// 6. sayfa


                doc.SetPageSize(PageSize.A4.Rotate());

                doc.NewPage();

                #region Title Table

                try
                {


                    PdfPTable table = new PdfPTable(4);

                    table.TotalWidth = 216f;

                    float[] widths = new float[] { 1f, 6f, 8f, 10f };
                    table.SetWidths(widths);

                    PdfPCell cell = new PdfPCell(new Phrase(" "));
                    cell.DisableBorderSide(iTextSharp.text.Rectangle.BOTTOM_BORDER);
                    cell.Colspan = 4;
                    cell.Rowspan = 1;
                    cell.BorderWidth = 3;
                    cell.HorizontalAlignment = 1; //0=Left, 1=Centre, 2=Right
                    table.AddCell(cell);





                    //3. satur
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Test Tarihi ve Saati", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    string strDate = dateTime + "-" + DateTime.Now.ToShortTimeString();

                    // Date Time
                    cell = new PdfPCell((new Phrase(strDate, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);



                    // 4. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Ürün Seri No", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    // Date Time
                    cell = new PdfPCell((new Phrase(itemNo, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);


                    //5. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Bogi Tipi", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    // Date Time
                    cell = new PdfPCell((new Phrase(itemType, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    //5. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Operator", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    cell = new PdfPCell((new Phrase(operatorName, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    // 7. satır

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase("Sipariş No (SAP)", normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);


                    cell = new PdfPCell((new Phrase(sapNo, normalFont)));
                    cell.BorderWidth = 1;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);

                    // 8. satır
                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.LEFT_BORDER | iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER;
                    table.AddCell(cell);

                    cell = new PdfPCell((new Phrase(" ")));
                    cell.BorderWidth = 3;
                    cell.Border = iTextSharp.text.Rectangle.BOTTOM_BORDER | iTextSharp.text.Rectangle.RIGHT_BORDER;
                    table.AddCell(cell);


                    doc.Add(table);

                    Paragraph prg = new Paragraph("PG1 GRAFİĞİ", headerFont);
                    prg.Alignment = 1;
                    doc.Add(prg);
                    doc.Add(new Paragraph(" "));


                }
                catch (Exception ex)
                {
                    ShowError(ex.ToString());
                }


                #endregion

                //s1 = new System.Windows.Forms.DataVisualization.Charting.Series("PG1_Artan");
                //s2 = new System.Windows.Forms.DataVisualization.Charting.Series("PG1_Azalan");

                //s1.Color = Color.DarkRed;
                //s2.Color = Color.Green;

                //for (int i = 0; i < pg1Increase.Count; i++)
                //    s1.Points.AddXY(pg1Increase.ElementAt(i).Key, pg1Increase.ElementAt(i).Value);

                //for (int i = 0; i < pg1Decrease.Count; i++)
                //    s2.Points.AddXY(pg1Decrease.ElementAt(i).Key, pg1Decrease.ElementAt(i).Value);

                //list = new List<System.Windows.Forms.DataVisualization.Charting.Series>();
                //list.Add(s1);
                //list.Add(s2);

                //AddChartToDocument(doc, list, "Basınç (Bar)", "Akım (mA)", 120, 15, 10, 1);

                #region footer 

                page = pageNumber.ToString() + "/" + totalPageNumber.ToString();

                footer = new Paragraph(page, normalFont);

                footer.Alignment = 2;

                doc.Add(footer);

                pageNumber++;

                #endregion



                #endregion


                doc.Close();


                ShowError("Rapor başarıyla kaydedildi!");

            }
            catch (Exception ex)
            {

                ShowError("Rapor kaydedilirken hata oluştu." + ex.ToString());
            }




        }


        public void LogtoFile(Dictionary<double, double> tco24V, Dictionary<double, double> tco16_8V, Dictionary<double, double> tt1Increase, Dictionary<double, double> tt1Decrease, Dictionary<double, double> pg0Increase, Dictionary<double, double> pg0Decrease, Dictionary<double, double> spr, Dictionary<double, double> pg1Increase, Dictionary<double, double> pg1Decrease)
        {
            try
            {
                string logPath = path.Remove(path.Length - 4, 4);
                logPath += ".txt";

                using (FileStream file = new FileStream(logPath, FileMode.Append, FileAccess.Write, FileShare.Read))
                using (StreamWriter writer = new StreamWriter(file, Encoding.Unicode))
                {
                    writer.WriteLine("tc024V değerleri:");

                    for (int i = 0; i < tco24V.Count; i++)
                        writer.WriteLine(tco24V.ElementAt(i).Key + "     " + tco24V.ElementAt(i).Value);

                    writer.WriteLine("tc016.8V değerleri:");

                    for (int i = 0; i < tco16_8V.Count; i++)
                        writer.WriteLine(tco16_8V.ElementAt(i).Key + "     " + tco16_8V.ElementAt(i).Value);

                    writer.WriteLine("TT1 artan değerleri:");

                    for (int i = 0; i < tt1Increase.Count; i++)
                        writer.WriteLine(tt1Increase.ElementAt(i).Key + "     " + tt1Increase.ElementAt(i).Value);

                    writer.WriteLine("TT1 azalan değerleri:");

                    for (int i = 0; i < tt1Decrease.Count; i++)
                        writer.WriteLine(tt1Decrease.ElementAt(i).Key + "     " + tt1Decrease.ElementAt(i).Value);

                    writer.WriteLine("pg0 artan değerleri:");

                    for (int i = 0; i < pg0Increase.Count; i++)
                        writer.WriteLine(pg0Increase.ElementAt(i).Key + "     " + pg0Increase.ElementAt(i).Value);

                    writer.WriteLine("pg0 azalan değerleri:");

                    for (int i = 0; i < pg0Decrease.Count; i++)
                        writer.WriteLine(pg0Decrease.ElementAt(i).Key + "     " + pg0Decrease.ElementAt(i).Value);

                    writer.WriteLine("spr değerleri:");

                    for (int i = 0; i < spr.Count; i++)
                        writer.WriteLine(spr.ElementAt(i).Key + "     " + spr.ElementAt(i).Value);

                    writer.WriteLine("pg1 artan değerleri:");

                    for (int i = 0; i < pg1Increase.Count; i++)
                        writer.WriteLine(pg1Increase.ElementAt(i).Key + "     " + pg1Increase.ElementAt(i).Value);

                    writer.WriteLine("pg1 azalan değerleri:");

                    for (int i = 0; i < pg1Decrease.Count; i++)
                        writer.WriteLine(pg1Decrease.ElementAt(i).Key + "     " + pg1Decrease.ElementAt(i).Value);






                }


            }
            catch
            {

            }

        }

        public void AddMfilesTitleForMotor(Document doc)
        {

            try
            {



                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("motor_title1.png");

                jpg.ScalePercent(30);


                doc.Add(jpg);

                doc.Add(new Paragraph(" "));

            }

            catch (Exception ex)
            {
                ShowError("Rapor kaydedilirken hata oluştu!" + ex.ToString());

            }

        }
        public void AddMfilesTitleForTasiyici(Document doc)
        {

            try
            {



                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("tasiyici_title1.png");

                jpg.ScalePercent(30);


                doc.Add(jpg);

                doc.Add(new Paragraph(" "));

            }

            catch (Exception ex)
            {
                ShowError("Rapor kaydedilirken hata oluştu!" + ex.ToString());

            }

        }

        public void AddChartToDocument(Document doc, List<System.Windows.Forms.DataVisualization.Charting.Series> series, string xTitle, string yTitle, int xMax, int yMax, int xInterval, int yInterval)
        {


            try
            {
                foreach (System.Windows.Forms.DataVisualization.Charting.Series s in series)
                    s.SetCustomProperty("LineTension", "0.0");

                //prepare chart control....
                System.Windows.Forms.DataVisualization.Charting.Chart chart = new System.Windows.Forms.DataVisualization.Charting.Chart();

                chart.Width = 850;
                chart.Height = 350;


                for (int i = 0; i < series.Count; i++)
                {
                    chart.Series.Add(series[i]);

                    chart.Series[i].ChartType = SeriesChartType.Line;
                    chart.Series[i].IsVisibleInLegend = true;

                }


                //create legend
                chart.Legends.Add("test");
                chart.Legends[0].BorderWidth = 3;
                chart.Legends[0].BorderColor = Color.Black;



                //create chartareas...
                System.Windows.Forms.DataVisualization.Charting.ChartArea ca = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
                ca.Name = "ChartArea1";

                ca.BackColor = Color.White;

                ca.BorderColor = Color.FromArgb(26, 59, 105);
                ca.BorderWidth = 0;
                ca.BorderDashStyle = ChartDashStyle.Solid;

                ca.AxisX = new System.Windows.Forms.DataVisualization.Charting.Axis();
                ca.AxisX.Title = xTitle;
                ca.AxisX.MajorGrid.LineColor = Color.LightGray;
                ca.AxisX.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;
                ca.AxisX.TitleFont = new System.Drawing.Font("Verdana", 11, System.Drawing.FontStyle.Bold);

                ca.AxisY = new System.Windows.Forms.DataVisualization.Charting.Axis();
                ca.AxisY.Title = yTitle;
                ca.AxisY.MajorGrid.LineColor = Color.LightGray;
                ca.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.DashDotDot;
                ca.AxisY.TitleFont = new System.Drawing.Font("Verdana", 11, System.Drawing.FontStyle.Bold);


                chart.ChartAreas.Add(ca);

                chart.ChartAreas[0].AxisX.Minimum = 0;
                chart.ChartAreas[0].AxisX.Maximum = xMax;

                chart.ChartAreas[0].AxisY.Minimum = 0;
                chart.ChartAreas[0].AxisY.Maximum = yMax;

                chart.ChartAreas[0].AxisX.Interval = xInterval;
                chart.ChartAreas[0].AxisY.Interval = yInterval;

                chart.IsSoftShadows = true;

                Bitmap chartImage;


                using (MemoryStream ms = new MemoryStream())
                {
                    chart.SaveImage(ms, ChartImageFormat.Png);
                    chartImage = new Bitmap(ms);
                }


                iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(chartImage, BaseColor.WHITE, false);

                jpg.Alignment = 1;

                doc.Add(jpg);

            }
            catch (Exception ex)
            {
                ShowError(ex.ToString());
                doc.Close();
            }


        }

        public iTextSharp.text.Font ValidateResult(string parameterName, string value)
        {

            try
            {
                if (parameterName == "Görsel")
                {
                    return (value == "Evet" ? validFont : invalidFont);
                }
                else if (parameterName == "Yağ Seviyesi")
                {
                    return (value == "İyi" ? validFont : invalidFont);
                }
                else if (parameterName == "Yağ Kaçağı")
                {
                    return (value == "İyi" ? validFont : invalidFont);
                }
                else if (parameterName == "BRPS Change Contact_3")
                {
                    double doubleValue = Convert.ToDouble(value);

                    if (27.5 < doubleValue && doubleValue < 32.5)
                        return validFont;
                    else if (26.5 < doubleValue && doubleValue < 33.5)
                        return warningFont;
                    else
                        return invalidFont;
                }
                else if (parameterName == "BRPS Change Contact_1")
                {
                    double doubleValue = Convert.ToDouble(value);

                    if (92.5 < doubleValue && doubleValue < 97.5)
                        return validFont;
                    else if (91.5 < doubleValue && doubleValue < 98.5)
                        return warningFont;
                    else
                        return invalidFont;
                }
                else if (parameterName == "BRPS Change Contact_2")
                {
                    double doubleValue = Convert.ToDouble(value);

                    if (77.5 < doubleValue && doubleValue < 82.5)
                        return validFont;
                    else if (76.5 < doubleValue && doubleValue < 83.5)
                        return warningFont;
                    else
                        return invalidFont;
                }
                else if (parameterName == "BRPS Change Contact_4")
                {
                    double doubleValue = Convert.ToDouble(value);

                    if (15.5 < doubleValue && doubleValue < 20.5)
                        return validFont;
                    else if (14.5 < doubleValue && doubleValue < 21.5)
                        return warningFont;
                    else
                        return invalidFont;
                }

                else if (parameterName == "PRLIMIT")
                {
                    double doubleValue = Convert.ToDouble(value);

                    return (doubleValue > 162 && doubleValue < 172 ? validFont : invalidFont);
                }



                else if (parameterName == "PG0 INCREASING_1")
                {
                    double doubleValue = Convert.ToDouble(value);
                    if (3.84 < doubleValue && doubleValue < 4.16)
                        return validFont;
                    else if (3.80 < doubleValue && doubleValue < 4.20)
                        return warningFont;
                    else
                        return invalidFont;
                }

                else if (parameterName == "PG0 INCREASING_2")
                {
                    double doubleValue = Convert.ToDouble(value);

                    if (15.84 < doubleValue && doubleValue < 16.16)
                        return validFont;
                    else if (15.70 < doubleValue && doubleValue < 16.30)
                        return warningFont;
                    else
                        return invalidFont;
                }

                else if (parameterName == "PG0 DECREASING")
                {
                    double doubleValue = Convert.ToDouble(value);
                    if (3.84 < doubleValue && doubleValue < 4.16)
                        return validFont;
                    else if (3.80 < doubleValue && doubleValue < 4.20)
                        return warningFont;
                    else
                        return invalidFont;
                }
                else if (parameterName == "YAG DOLDURMA SURESI")
                {
                    double doubleValue = Convert.ToDouble(value);
                    return (doubleValue < 35 ? validFont : invalidFont);

                }
                else if (parameterName == "TCO Basınç Farkı")
                {
                    double doubleValue = Convert.ToDouble(value);
                    return (doubleValue < 35 ? validFont : invalidFont);

                }


                else if (parameterName == "SV Valve")
                {
                    double doubleValue = Convert.ToDouble(value);
                    return (doubleValue < 2 ? validFont : invalidFont);

                }

                else if (parameterName == "Valf1 High" || parameterName == "Valf1 Low")
                {
                    double doubleValue = Convert.ToDouble(value);
                    if (0 < doubleValue && doubleValue < 3)
                        return validFont;
                    else if (0 < doubleValue && doubleValue < 6)
                        return warningFont;
                    else
                        return invalidFont;
                }

                else if (parameterName == "Valf2 High" || parameterName == "Valf2 Low")
                {
                    double doubleValue = Convert.ToDouble(value);
                    if (19 < doubleValue && doubleValue < 29)
                        return validFont;
                    else if (16 < doubleValue && doubleValue < 32)
                        return warningFont;
                    else
                        return invalidFont;
                }
                else if (parameterName == "Valf3 High" || parameterName == "Valf3 Low")
                {
                    double doubleValue = Convert.ToDouble(value);
                    if (44 < doubleValue && doubleValue < 54)
                        return validFont;
                    else if (41 < doubleValue && doubleValue < 57)
                        return warningFont;
                    else
                        return invalidFont;
                }
                else if (parameterName == "Valf4 High" || parameterName == "Valf4 Low")
                {
                    double doubleValue = Convert.ToDouble(value);
                    if (69 < doubleValue && doubleValue < 79)
                        return validFont;
                    else if (66 < doubleValue && doubleValue < 82)
                        return warningFont;
                    else
                        return invalidFont;
                }
                else if (parameterName == "Valf5 High" || parameterName == "Valf5 Low")
                {
                    double doubleValue = Convert.ToDouble(value);
                    if (94 < doubleValue && doubleValue < 104)
                        return validFont;
                    else if (91 < doubleValue && doubleValue < 107)
                        return warningFont;
                    else
                        return invalidFont;
                }

                else if (parameterName == "Valf6 High" || parameterName == "Valf6 Low")
                {
                    double doubleValue = Convert.ToDouble(value);
                    if (doubleValue > 104)
                        return validFont;
                    else if (doubleValue > 101)
                        return warningFont;
                    else
                        return invalidFont;
                }

                else if (parameterName == "SPRLIMIT" || parameterName == "SPRLIMIT_2")
                {
                    double doubleValue = Convert.ToDouble(value);
                    if (reportType == "Motor")
                    {
                        if (5 < doubleValue && doubleValue < 10)
                            return validFont;
                        else if (4 < doubleValue && doubleValue < 11)
                            return warningFont;
                        else
                            return invalidFont;
                    }
                    else
                    {
                        if (33 < doubleValue && doubleValue < 37)
                            return validFont;
                        else if (32 < doubleValue && doubleValue < 38)
                            return warningFont;
                        else
                            return invalidFont;
                    }

                }
                else if (parameterName == "Pressure Limit")
                {
                    double doubleValue = Convert.ToDouble(value);
                    if (doubleValue < 500)
                        return validFont;
                    else if (doubleValue < 700)
                        return warningFont;
                    else
                        return invalidFont;
                }
                else if (parameterName.Contains("Diff"))
                {
                    double doubleValue = Convert.ToDouble(value);
                    if (doubleValue < 5)
                        return validFont;
                    else if (doubleValue < 6)
                        return warningFont;
                    else
                        return invalidFont;
                }
                else if (parameterName == "PG1 Increasing_1")
                {
                    double doubleValue = Convert.ToDouble(value);
                    if (3.84 < doubleValue && doubleValue < 4.16)
                        return validFont;
                    else if (3.80 < doubleValue && doubleValue < 4.20)
                        return warningFont;
                    else
                        return invalidFont;
                }

                else if (parameterName == "PG1 Increasing_2")
                {
                    double doubleValue = Convert.ToDouble(value);
                    if (11.80 < doubleValue && doubleValue < 12.20)
                        return validFont;
                    else if (11.60 < doubleValue && doubleValue < 12.40)
                        return warningFont;
                    else
                        return invalidFont;

                }

                else if (parameterName == "PG1 Decreasing")
                {
                    double doubleValue = Convert.ToDouble(value);
                    if (3.84 < doubleValue && doubleValue < 4.16)
                        return validFont;
                    else if (3.80 < doubleValue && doubleValue < 4.20)
                        return warningFont;
                    else
                        return invalidFont;
                }







                return invalidFont;


            }
            catch (Exception ex)
            {
                ShowError(ex.ToString());
                return invalidFont;
            }

        }

        public void Log()
        {

        }
    }
}
