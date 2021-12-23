using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace YazlaB2Son
{
    public partial class Form1 : Form
    {
        char[,] Sudoku = new char[21, 21];
        public Form1()
        {



          



           Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            txtOku();
            createCells();
            timer1.Enabled = true;
            timer1.Interval = 1;
            thStart();
          /*  for (int i = 0; i < 10; i++)
            {
                string a = "th1";
                Solve(0, 0, a);
                Solve(12, 0, a);
                Solve(0, 12, a);
                Solve(12, 12, a);
                Solve(6, 6, a);



            }*/



            
         /*   thread5.Start();
            thread1.Start();
            
           thread2.Start();
         
            thread3.Start();
          
            thread4.Start();*/
           



          
          


        }





        class cellDenetle
        {
            public int Value { get; set; }

            public List<int> listTut = new List<int>();
            public int X { get; set; }
            public int Y { get; set; }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
         
         
           
        }
        class SudokuCell : Button
        {
            public int Value { get; set; }
            public List<int> listGelemezler = new List<int>();
            public int X { get; set; }
            public int Y { get; set; }
            public bool İslemGordumu = false;
            public bool İslemGordumuSütün = false;
            public bool İslemGordumuSatir = false;
        }

        static List<SudokuCell> KritikListe = new List<SudokuCell>();

        static SudokuCell[,] cells = new SudokuCell[21, 21];
        private void createCells()
        {
            List<int> tamList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int i = 0; i < 21; i++)
            {
                for (int j = 0; j < 21; j++)
                {

                    cells[i, j] = new SudokuCell();

                    cells[i, j].Size = new Size(30, 30);
                    cells[i, j].ForeColor = SystemColors.ControlDarkDark;
                    cells[i, j].Location = new Point(i * 30, j * 30);
                    cells[i, j].FlatStyle = FlatStyle.Flat;
                    cells[i, j].BackColor = ((i / 3) + (j / 3)) % 2 == 0 ? SystemColors.Control : Color.CadetBlue;
                    cells[i, j].FlatStyle = FlatStyle.Flat;
                    cells[i, j].FlatAppearance.BorderColor = Color.Black;
                    cells[i, j].Font = new Font("Georgia", 10, FontStyle.Bold);
                    cells[i, j].X = i;
                    cells[i, j].Y = j;


                    cells[i, j].Text = Sudoku[j, i].ToString();
                    panel1.Controls.Add(cells[i, j]);

                    if (cells[i, j].Text.Equals("0"))
                    {
                        cells[i, j].Visible = false;
                        cells[i, j].Value = 0;

                    }
                    else if (cells[i, j].Text.Equals("*"))
                    {
                        cells[i, j].Text = "";
                        cells[i, j].Value = 0;


                    }
                    else if (!cells[i, j].Text.Equals("*") || !cells[i, j].Text.Equals("0") || !cells[i, j].Text.Equals(""))
                    {

                        cells[i, j].Value = Convert.ToInt32(cells[i, j].Text);
                        cells[i, j].listGelemezler = tamList;

                    }





                }

            }

            KritikListe.Add(cells[6, 6]); KritikListe.Add(cells[7, 6]); KritikListe.Add(cells[8, 6]);
            KritikListe.Add(cells[6, 7]); KritikListe.Add(cells[7, 7]); KritikListe.Add(cells[8, 7]);
            KritikListe.Add(cells[6, 8]); KritikListe.Add(cells[7, 8]); KritikListe.Add(cells[8, 8]);

            KritikListe.Add(cells[12, 6]); KritikListe.Add(cells[13, 6]); KritikListe.Add(cells[14, 6]);
            KritikListe.Add(cells[12, 7]); KritikListe.Add(cells[13, 7]); KritikListe.Add(cells[14, 7]);
            KritikListe.Add(cells[12, 8]); KritikListe.Add(cells[13, 8]); KritikListe.Add(cells[14, 8]);

            KritikListe.Add(cells[6, 12]); KritikListe.Add(cells[7, 12]); KritikListe.Add(cells[8, 12]);
            KritikListe.Add(cells[6, 13]); KritikListe.Add(cells[7, 13]); KritikListe.Add(cells[8, 13]);
            KritikListe.Add(cells[6, 14]); KritikListe.Add(cells[7, 14]); KritikListe.Add(cells[8, 14]);

            KritikListe.Add(cells[12, 12]); KritikListe.Add(cells[13, 12]); KritikListe.Add(cells[14, 12]);
            KritikListe.Add(cells[12, 13]); KritikListe.Add(cells[13, 13]); KritikListe.Add(cells[14, 13]);
            KritikListe.Add(cells[12, 14]); KritikListe.Add(cells[13, 14]); KritikListe.Add(cells[14, 14]);



        }



        static Thread thread1 = new Thread(t =>
       {
           string a = "Th1";
           Solve(0, 0, a);

       });

        static Thread thread2 = new Thread(t =>
     {
         string a = "Th2";

         Solve(12, 0, a);

     });
        static Thread thread3 = new Thread(t =>
         {
             string a = "Th3";
             Solve(0, 12, a);
         });
        static Thread thread4 = new Thread(t =>
         {
             string a = "Th4";
             Solve(12, 12, a);

         });
        static Thread thread5 = new Thread(t =>
         {

             string a = "Th5";
             Thread.Sleep(100);
             Solve(6, 6, a);

         });





         void Veriyaz1(List<veri> veriList)
        {


            cartesianChart1.Series = new LiveCharts.SeriesCollection
           {

               new LineSeries
               {


                   Values=new ChartValues<ObservablePoint>
                   {
                       new ObservablePoint(veriList[0].zaman,veriList[0].bulunan),
                       new ObservablePoint(veriList[1].zaman,veriList[1].bulunan),
                       new ObservablePoint(veriList[2].zaman,veriList[2].bulunan),
                       new ObservablePoint(veriList[3].zaman,veriList[3].bulunan),
                       new ObservablePoint(veriList[4].zaman,veriList[4].bulunan),
                       new ObservablePoint(veriList[5].zaman,veriList[5].bulunan),
                       new ObservablePoint(veriList[6].zaman,veriList[6].bulunan),
                        new ObservablePoint(veriList[7].zaman,veriList[7].bulunan),
                       new ObservablePoint(veriList[8].zaman,veriList[8].bulunan),
                      /* new ObservablePoint(veriList[9].zaman,veriList[9].bulunan),
                       new ObservablePoint(veriList[10].zaman,veriList[10].bulunan),
                       new ObservablePoint(veriList[11].zaman,veriList[11].bulunan),
                       new ObservablePoint(veriList[12].zaman,veriList[12].bulunan),
                       new ObservablePoint(veriList[13].zaman,veriList[13].bulunan),*/







                   },


                   PointGeometrySize=15



               }



           };





        }



            



      

        static public void thStart()
        {









            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();
            thread5.Start();
          
                




        }
     
        

        static int BulunanNokta=0;
        static void txtYaz(string yazilacak,string tür)
        {



            BulunanNokta++;
            
          

            


            if (tür == "Th1")
            {
                StreamWriter Yaz1 = File.AppendText( "C:\\Users\\90505\\Desktop\\th1.txt");
                Yaz1.WriteLine(yazilacak);
                Yaz1.Close();
            }
            if (tür == "Th2")
            {
                StreamWriter Yaz2 = File.AppendText( "C:\\Users\\90505\\Desktop\\th2.txt");
                Yaz2.WriteLine(yazilacak);
                Yaz2.Close();
            }
            if (tür == "Th3")
            {
                StreamWriter Yaz3 = File.AppendText( "C:\\Users\\90505\\Desktop\\th3.txt");
                Yaz3.WriteLine(yazilacak);
                Yaz3.Close();
            }
            if (tür == "Th4")
            {
                StreamWriter Yaz4 = File.AppendText( "C:\\Users\\90505\\Desktop\\th4.txt");
                Yaz4.WriteLine(yazilacak);
                Yaz4.Close();

            }
            if (tür == "Th5")
            {
                StreamWriter Yaz5 = File.AppendText( "C:\\Users\\90505\\Desktop\\th5.txt");
                Yaz5.WriteLine(yazilacak);
                Yaz5.Close();
            }
           
            

        }




        




    static void satirSutunDenetle(int k, int m)
        {
            for (int i = k; i < k + 9; i++)
            {
                for (int j = m; j < m + 9; j++)
                {
                    for (int x = m; x < m+ 9; x++)
                    {
                        if (j != x)
                        {
                            //  MessageBox.Show(cells[i, x].Value.ToString());
                            if (cells[i, x].Value != 0 && !cells[i, j].listGelemezler.Contains(cells[i, x].Value))
                            {
                               
                               
                                    cells[i, j].listGelemezler.Add(cells[i, x].Value);
                                
                               

                            }


                        }
                      
                    }




                }
            }
            for (int i = k; i < k + 9; i++)
            {
                for (int j = m; j < m + 9; j++)
                {
                    for (int x = k; x < k + 9; x++)
                    {
                       
                        if (i != x)
                        {
                            if (cells[x, j].Value != 0 && !cells[i, j].listGelemezler.Contains(cells[x, j].Value))
                            {
                                if (KritikListe.Contains(cells[i, j]))
                                {

                                    for (int n = 0; n < 100; n++)
                                    {

                                    }




                                    cells[i, j].listGelemezler.Add(cells[x, j].Value);
                                    
                                       
                                    


                                }
                                else
                                {
                                    cells[i, j].listGelemezler.Add(cells[x, j].Value);

                                }
                            }



                        }
                    }




                }
            }





        }



      static  public void DokuzTamamla(int k,int m,string TH)
        {
            List<int> tamList = new List<int>() {1,2,3,4,5,6,7,8,9 };

            for (int i = k; i <k+ 9; i++)
            {
                for (int j = m; j <m+ 9; j++)
                {
                    if (cells[i, j].listGelemezler.Count == 8 && cells[i, j].Value == 0)
                    {



                      


                            var eklenecek = tamList.Except(cells[i, j].listGelemezler).ToList();
                            cells[i, j].Value = eklenecek[0];
                            cells[i, j].Text = cells[i, j].Value.ToString();
                            cells[i, j].listGelemezler = tamList;

                      string A =  TH+ " "+ i.ToString()+ "," + j.ToString() + ":" + eklenecek[0].ToString() ;
                        txtYaz(A,TH);

                      






                    }



                }

            }



        }
        static public void Solve(int k,int m,string TH)
        {
            for (int i = 0; i < 100; i++)                   
            {
               
               Thread.Sleep(10);
               

                satirSutunDenetle(k, m);
                Thread.Sleep(10);
                KareDenetle(k, m);           
                Thread.Sleep(10);
                KareTamamla(k, m,TH);

                Thread.Sleep(10);
                satirSutunDenetle(k, m);
               Thread.Sleep(10);
                KareDenetle(k, m);
               Thread.Sleep(10);
                DokuzTamamla(k, m,TH);
                Thread.Sleep(10);
                satirSutunDenetle(k, m);
                Thread.Sleep(10);
                KareDenetle(k, m);
                Thread.Sleep(10);
                satirTamamla(k, m,TH);
                Thread.Sleep(10);
                satirSutunDenetle(k, m);
                Thread.Sleep(10);
                KareDenetle(k, m);
                Thread.Sleep(10);

                sütünTamamla(k, m,TH);

                //  satirSutunDenetle(k,m);
                // KareDenetle(k,m);


                // sütünTamamla2(k,m);


                // satirSutunDenetle(k,m);
                //  KareDenetle(k,m);



                //  satirTamamla2(k,m);


            }
        }
      
        static  public void Denetle2(int k,int m)
        {
            for (int i = k; i <k+ 9; i++)
            {
                for (int j = m; j < m+9; j++)
                {

                    if (i % 3 == 0 && j % 3 == 0)
                    {
                        List<int> sayilar = new List<int>();

                        for (int sayi = 1; sayi <= 9; sayi++)
                        {
                            int sayacNew = 0;


                            for (int x = 0; x < 3; x++)
                            {
                                for (int y = 0; y < 3; y++)
                                {
                                    if (cells[i + x, j + y].listGelemezler.Contains(sayi))
                                    {
                                        //eklenemeyecek listesini tutmak ıcın olusturuldu
                                        sayacNew++;


                                    }

                                }

                            }
                            if (sayacNew == 7)
                            {
                                if (!sayilar.Contains(sayi))
                                {
                                    sayilar.Add(sayi);
                                }




                            }


                        }

                        if (sayilar.Count == 2)
                        {
                            for (int a = 0; a < 3; a++)
                            {

                                for (int b = 0; b < 3; b++)
                                {


                                    for (int x = 0; x < 3; x++)
                                    {
                                        for (int y = 0; y < 3; y++)
                                        {

                                            if (a != x || y != b)
                                            {
                                                int sayac = 0;

                                                List<cellDenetle> denemeDouble = new List<cellDenetle>();
                                                for (int t = 0; t < sayilar.Count; t++)
                                                {

                                                    if (!cells[a + i, b + j].listGelemezler.Contains(sayilar[t]) && !cells[x + i, y + j].listGelemezler.Contains(sayilar[t]))
                                                    {
                                                        sayac++;
                                                        cellDenetle cellDenetle = new cellDenetle();
                                                        cellDenetle cellDenetle2 = new cellDenetle();
                                                        cellDenetle.X = i + a;
                                                        cellDenetle.Y = b + j;
                                                        cellDenetle2.X = i + x;
                                                        cellDenetle2.Y = j + y;
                                                        cellDenetle.listTut.Add(sayilar[t]);
                                                        cellDenetle2.listTut.Add(sayilar[t]);
                                                        denemeDouble.Add(cellDenetle);
                                                        denemeDouble.Add(cellDenetle2);

                                                    }
                                                }
                                                if (sayac == 2 && denemeDouble.Count == 4)
                                                {



                                                  



                                                    if (cells[denemeDouble[0].X, denemeDouble[0].Y].Value == 0)
                                                    {
                                                        List<int> yeniList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                                                        yeniList.Remove(denemeDouble[0].listTut[0]);
                                                        yeniList.Remove(denemeDouble[2].listTut[0]);
                                                        cells[denemeDouble[0].X, denemeDouble[0].Y].listGelemezler = yeniList;
                                                        cells[denemeDouble[0].X, denemeDouble[0].Y].İslemGordumu = true;
                                                      



                                                    }
                                                    if (cells[denemeDouble[1].X, denemeDouble[1].Y].Value == 0)
                                                    {
                                                        List<int> yeniList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                                                        yeniList.Remove(denemeDouble[0].listTut[0]);
                                                        yeniList.Remove(denemeDouble[2].listTut[0]);
                                                        cells[denemeDouble[1].X, denemeDouble[1].Y].İslemGordumu = true;
                                                        cells[denemeDouble[1].X, denemeDouble[1].Y].listGelemezler = yeniList;
                                                        
                                                    }












                                                }

                                                }
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
            }


     static   public void denetle3Satir(int k,int m)
        {

            for (int i = k; i <k+ 9; i++)
            {

                List<int> sayilar = new List<int>();

                for (int sayi = 1; sayi <= 9; sayi++)
                {
                    int sayacNew = 0;
                    for (int j = m; j < m+ 9; j++)
                    {

                        if (cells[j, i].listGelemezler.Contains(sayi))
                        {
                            //eklenemeyecek listesini tutmak ıcın olusturuldu
                            sayacNew++;


                        }
                    }

                    if (sayacNew == 7)
                    {

                        if (!sayilar.Contains(sayi))
                        {
                            sayilar.Add(sayi);
                        }


                    }

                }
                if (sayilar.Count >= 2)
                {

                    for (int j = m; j <m+ 9; j++)
                    {
                        for (int a = m; a <m+ 9; a++)
                        {

                            if (j != a)
                            {
                                int sayac = 0;
                                List<int> abc = new List<int>();
                                List<cellDenetle> denetle = new List<cellDenetle>();
                                for (int t = 0; t < sayilar.Count; t++)
                                {

                                    if (!cells[j, i].listGelemezler.Contains(sayilar[t]) && !cells[a, i].listGelemezler.Contains(sayilar[t]))
                                    {
                                        //denetle count 4 cunku 2 sayi var 2*2 den 4 eleman eklemesi yaplıyor
                                        sayac++;
                                        cellDenetle cellDenetle = new cellDenetle();
                                        cellDenetle cellDenetle2 = new cellDenetle();
                                        cellDenetle.X = j;
                                        cellDenetle.Y = i;
                                        cellDenetle2.X = a;
                                        cellDenetle2.Y = i;
                                        abc.Add(sayilar[t]);
                                        denetle.Add(cellDenetle);
                                        denetle.Add(cellDenetle2);


                                    }
                                }
                                if (sayac == 2 && denetle.Count == 4)
                                {

                                    // cells[denetle[0].X, denetle[0].Y].İslemGordumuSatir == false
                                    if (true)
                                    {

                                        if (cells[denetle[0].X, denetle[0].Y].Value == 0)
                                        {




                                            List<int> yeniList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

                                            yeniList.Remove(abc[0]);
                                            yeniList.Remove(abc[1]);
                                            cells[denetle[0].X, denetle[0].Y].İslemGordumuSatir = true;
                                            cells[denetle[0].X, denetle[0].Y].listGelemezler = yeniList;
                                          

                                        }

                                    }

                                }





                            }

                        }






                    }


                }



            }








        }














        static public void denetle3Sütün(int k,int m)
        {

            for (int i = k; i <k+ 9; i++)
            {

                List<int> sayilar = new List<int>();

                for (int sayi = 1; sayi <= 9; sayi++)
                {
                    int sayacNew = 0;
                    for (int j = m; j < m + 9; j++)
                    {

                        if (cells[i, j].listGelemezler.Contains(sayi))
                        {
                            
                            sayacNew++;


                        }
                    }

                    if (sayacNew == 7)
                    {

                        if (!sayilar.Contains(sayi))
                        {
                            sayilar.Add(sayi);
                        }


                    }

                }
                if (sayilar.Count >= 2)
                {

                    for (int j = m; j <m+ 9; j++)
                    {
                        for (int a = m; a < m+9; a++)
                        {

                            if (j != a)
                            {
                                int sayac = 0;
                                List<int> abc = new List<int>();
                                List<cellDenetle> denetle = new List<cellDenetle>();
                                for (int t = 0; t < sayilar.Count; t++)
                                {

                                    if (!cells[i, j].listGelemezler.Contains(sayilar[t]) && !cells[i, a].listGelemezler.Contains(sayilar[t]))
                                    {
                                       
                                        //denetle count 4 cunku 2 sayi var 2*2 den 4 eleman eklemesi yaplıyor
                                        sayac++;
                                        cellDenetle cellDenetle = new cellDenetle();
                                        cellDenetle cellDenetle2 = new cellDenetle();
                                        cellDenetle.X = i;
                                        cellDenetle.Y = j;
                                        cellDenetle2.X = i;
                                        cellDenetle2.Y = a;
                                        abc.Add(sayilar[t]);
                                        denetle.Add(cellDenetle);
                                        denetle.Add(cellDenetle2);


                                    }
                                }
                                if (sayac == 2 && denetle.Count == 4)
                                {


                                    if (true)
                                    {

                                        if (cells[denetle[0].X, denetle[0].Y].Value == 0 && cells[denetle[0].X, denetle[0].Y].X==cells[denetle[1].X, denetle[1].Y].X)
                                        {
                                            List<int> yeniList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                                            yeniList.Remove(abc[0]);
                                            yeniList.Remove(abc[1]);
                                            cells[denetle[0].X, denetle[0].Y].İslemGordumuSatir = true;
                                            cells[denetle[0].X, denetle[0].Y].listGelemezler = yeniList;

                                            
                                               
                                               
                                           

                                        }



                                    }

                                }





                            }

                        }

                    }


                }



            }
        }

        static  public void Denetle1(int sayi,int k,int m)
        {


            for (int i = k; i <k+ 9; i++)
            {
                for (int j = m; j <m+ 9; j++)
                {
                    if (i % 3 == 0 && j % 3 == 0)
                    {

                        int sayacNew = 0;
                        List<cellDenetle> cellsDenetle = new List<cellDenetle>();
                        for (int x = 0; x < 3; x++)
                        {
                            for (int y = 0; y < 3; y++)
                            {
                                //i+x j+y
                                if (cells[i + x, j + y].listGelemezler.Contains(sayi))
                                {
                                    sayacNew++;

                                }
                                else if (!cells[i + x, j + y].listGelemezler.Contains(sayi))
                                {
                                    cellDenetle cellsAdd = new cellDenetle();
                                    cellsAdd.X = i + x;
                                    cellsAdd.Y = j + y;
                                    cellsAdd.Value = sayi;
                                    cellsDenetle.Add(cellsAdd);

                                }


                            }

                        }
                        if (sayacNew == 7 && cellsDenetle.Count == 2)
                        {
                            //ayni satırdaysa 
                            if (cellsDenetle[0].X == cellsDenetle[1].X)
                            {
                                for (int sütün = m; sütün < m+9; sütün++)
                                {
                                    //ve veya karısık duzeltilebilir
                                    if (sütün != cellsDenetle[0].Y && sütün != cellsDenetle[1].Y)
                                    {
                                        if (!cells[cellsDenetle[0].X, sütün].listGelemezler.Contains(cellsDenetle[0].Value))
                                        {
                                            if (KritikListe.Contains(cells[cellsDenetle[0].X, sütün]))
                                            {
                                               





                                                cells[cellsDenetle[0].X, sütün].listGelemezler.Add(cellsDenetle[0].Value);
                                              


                                            }
                                            else
                                            {
                                                cells[cellsDenetle[0].X, sütün].listGelemezler.Add(cellsDenetle[0].Value);
                                            }
                                          
                                            //   MessageBox.Show(cellsDenetle[0].X + " "+sütün+" "+ cellsDenetle[0].Value);
                                        }

                                    }


                                }

                            }
                            if (cellsDenetle[0].Y == cellsDenetle[1].Y)
                            {
                                for (int satir = k; satir <k+ 9; satir++)
                                {

                                    if (satir != cellsDenetle[0].X && satir != cellsDenetle[1].X)
                                    {
                                        if (!cells[satir, cellsDenetle[0].Y].listGelemezler.Contains(cellsDenetle[0].Value))
                                        {
                                            if (KritikListe.Contains(cells[satir, cellsDenetle[0].Y]))
                                            {
                                              


                                                cells[satir, cellsDenetle[0].Y].listGelemezler.Add(cellsDenetle[0].Value);
                                             
                                              

                                            }
                                            else
                                            {
                                                cells[satir, cellsDenetle[0].Y].listGelemezler.Add(cellsDenetle[0].Value);
                                            }
                                           
                                        }

                                    }
                                }


                            }


                        }




                    }

                }

            }







        }










        static public void sütünTamamla(int k,int m,string TH)
        {

            List<int> tamList = new List<int>() { 1,2,3,4,5,6,7,8,9};
            for (int i = k; i < k+9; i++)
            {
                int index = 0;
                int sayac = 0;

                for (int t = m; t <m+ 9; t++)
                {

                    if (cells[t, i].listGelemezler.Count() != 9)
                    {
                        sayac++;
                        index = t;

                    }


                }
                if (sayac == 1)
                {

                    if (cells[index, i].Value == 0)
                    {


                        var eklenecek = tamList.Except(cells[index, i].listGelemezler).ToList();
                        cells[index, i].Value = eklenecek[0];
                        cells[index, i].Text = cells[index, i].Value.ToString();

                        //Şüpheli satır hata cıkarabilir
                        cells[index, i].listGelemezler = tamList;

                        string A = TH+ " " +index.ToString()+ "," + i.ToString() + ":"+ eklenecek[0].ToString() ;
                        txtYaz(A, TH);

                       
                    
                       

                        
                       


                    }
                }


            }



        }


      static  public void satirTamamla(int k,int m,string TH)
        {
            List<int> tamList = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            for (int i = k; i <k+ 9; i++)
            {
                int index = 0;
                int sayac = 0;

                for (int t = m; t <m+ 9; t++)
                {

                    if (cells[i, t].listGelemezler.Count() != 9)
                    {
                        sayac++;
                        index = t;

                    }


                }
                if (sayac == 1)
                {

                    if (cells[i, index].Value == 0)
                    {
                       
                        
                        
                            var eklenecek = tamList.Except(cells[i, index].listGelemezler).ToList();
                            cells[i, index].Value = eklenecek[0];
                            cells[i, index].Text = cells[i, index].Value.ToString();

                            //Şüpheli satır hata cıkarabilir
                            cells[i, index].listGelemezler = tamList;
                        string A =  TH+ " "+ i.ToString()+ "," + index.ToString() + ":"+ eklenecek[0].ToString() ;
                        txtYaz(A, TH);

                      


                    }
                }


            }

        }


































        static public void KareTamamla(int k,int m,string TH)
        {
            for (int i = k; i <k+ 9; i++)
            {


                for (int j = m; j <m+ 9; j++)
                {

                    int a = i;
                    int countX = 0;
                    List<int> olmayan = new List<int>();
                    olmayan.Clear();
                    int ilkAdim = 0;
                    for (int x = 0; x < 3; x++)
                    {

                        int b = j;
                        //MessageBox.Show("Bdir b" + b.ToString());
                        if (ilkAdim == 0)
                        {
                            ilkAdim++;

                            //denemee

                        }
                        else if (ilkAdim != 0)
                        {
                            if (i % 3 == 2)
                            {
                                a--;
                            }
                            else if (i % 3 == 1)
                            {
                                if (countX == 0)
                                {
                                    a++;
                                    countX = 1;
                                }
                                else if (countX == 1)
                                {
                                    a = a - 2;
                                    countX = 0;
                                }

                            }
                            else if (i % 3 == 0)
                            {
                                a++;
                            }
                        }
                        int count = 0;

                        for (int y = 0; y < 3; y++)
                        {
                            
                            var list = cells[a, b].listGelemezler.Except(cells[i, j].listGelemezler).ToList();
                            olmayan.AddRange(list);
                            if (j % 3 == 2)
                            {
                                b--;


                            }

                            else if (j % 3 == 1)
                            {
                                if (count == 0)
                                {
                                    b++;
                                    count = 1;
                                }
                                else if (count == 1)
                                {
                                    b = b - 2;
                                    count = 0;
                                }

                            }
                            else if (j % 3 == 0)
                            {
                                b++;
                            }



                            /* for (int p = 0; p < list.Count; p++)
                             {
                                 MessageBox.Show(list[p].ToString());
                             }*/





                        }
                        //bir elemandan 8 tane varsa o elemanı yerleştiryoruz
                        //  MessageBox.Show(olmayan.Count.ToString());

                    }
                    for (int sayi = 1; sayi <= 9; sayi++)
                    {
                        int sayac = 0;
                        for (int q = 0; q < olmayan.Count; q++)
                        {

                            // MessageBox.Show(olmayan[q].ToString());
                            if (olmayan[q] == sayi)
                            {
                                sayac++;
                            }


                        }
                        if (sayac == 8)
                        {
                            if (cells[i, j].Value == 0)
                            {

                              

                                    cells[i, j].Value = sayi;
                                    cells[i, j].Text = sayi.ToString();

                                    cells[i, j].listGelemezler.Clear();
                                    cells[i, j].listGelemezler.Add(1);
                                    cells[i, j].listGelemezler.Add(2);
                                    cells[i, j].listGelemezler.Add(3);
                                    cells[i, j].listGelemezler.Add(4);
                                    cells[i, j].listGelemezler.Add(5);
                                    cells[i, j].listGelemezler.Add(6);
                                    cells[i, j].listGelemezler.Add(7);
                                    cells[i, j].listGelemezler.Add(8);
                                    cells[i, j].listGelemezler.Add(9);
                                string A =  TH+ " "+ i.ToString()+ "," + j.ToString() + ":"+ sayi.ToString() ;
                                txtYaz(A, TH);

                                







                            }




                        }

                    }



                }


            }


        }

        static void KareDenetle(int k, int m)
        {

            for (int i = k; i < k + 9; i++)
            {
                // int a = i;
                // int ilkAdim = 0;
                for (int j = m; j < m + 9; j++)
                {
                    // int b = j;
                    int a = i;
                    int countX = 0;
                    int ilkAdim = 0;
                    for (int x = 0; x < 3; x++)
                    {
                        int b = j;
                        if (ilkAdim == 0)
                        {
                            ilkAdim++;

                            //denemee

                        }
                        else if (ilkAdim != 0)
                        {
                            if (i % 3 == 2)
                            {
                                a--;
                            }
                            else if (i % 3 == 1)
                            {
                                if (countX == 0)
                                {
                                    a++;
                                    countX = 1;
                                }
                                else if (countX == 1)
                                {
                                    a = a - 2;
                                    countX = 0;
                                }

                            }
                            else if (i % 3 == 0)
                            {
                                a++;
                            }
                        }
                        int count = 0;

                        for (int y = 0; y < 3; y++)
                        {


                            if (!cells[i, j].listGelemezler.Contains(cells[a, b].Value) && cells[a, b].Value != 0)
                            {
                                if (KritikListe.Contains(cells[i,j]))
                                {
                                    for (int n = 0; n < 100; n++)
                                    {

                                    }


                                    cells[i, j].listGelemezler.Add(cells[a, b].Value);
                                   
                                }
                                else
                                {
                                    cells[i, j].listGelemezler.Add(cells[a, b].Value);
                                }
                            }






                            if (j % 3 == 2)
                            {
                                b--;


                            }

                            else if (j % 3 == 1)
                            {
                                if (count == 0)
                                {
                                    b++;
                                    count = 1;
                                }
                                else if (count == 1)
                                {
                                    b = b - 2;
                                    count = 0;
                                }

                            }
                            else if (j % 3 == 0)
                            {
                                b++;
                            }
                        }
                    }


                }

            }





        }






        public void txtOku()
        {


            string path = @"C:\Users\90505\Desktop\Sudoku.txt";
            string[] txt = System.IO.File.ReadAllLines(path);



            for (int x = 0; x < 21; x++)
            {
                char[] furkan = txt[x].ToCharArray();


                for (int y = 0; y < 21; y++)
                {




                    if (furkan.Length == 18)
                    {

                        if (y < 9)
                        {
                            Sudoku[x, y] = furkan[y];
                        }

                        else if (y >= 12 && y < 21)
                        {

                            Sudoku[x, y] = furkan[y - 3];


                        }
                        else
                        {
                            Sudoku[x, y] = '0';
                        }


                    }
                    else if (furkan.Length == 21)
                    {


                        Sudoku[x, y] = furkan[y];

                    }
                    else if (furkan.Length == 9)
                    {
                        if (y < 6)
                        {
                            Sudoku[x, y] = '0';

                        }
                        else if (y >= 6 && y < 15)
                        {
                            Sudoku[x, y] = furkan[y - 6];
                        }
                        else
                        {
                            Sudoku[x, y] = '0';
                        }
                    }
                    else
                    {
                        Console.WriteLine("Txt formatı hatalı");
                    }


                }
            }


        }
       
   






        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void cartesianChart1_ChildChanged(object sender, System.Windows.Forms.Integration.ChildChangedEventArgs e)
        {

        }

        int zaman = 0;
        class veri
        {


            public int bulunan;
                public int zaman;
        }
      List<veri> veriList=new List<veri>();
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (zaman >= 10)
            {

            }
            else
            {
                veri veri = new veri();
                veri.bulunan = BulunanNokta;
                veri.zaman = zaman++;
                veriList.Add(veri);
                BulunanNokta = 0;
                if (zaman == 10)
                {
                    Veriyaz1(veriList);
                }
            }
            


            
                
               
            
           

        }
    }
}

