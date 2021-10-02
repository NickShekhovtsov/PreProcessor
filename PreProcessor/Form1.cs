using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PreProcessor
{
    public partial class Form1 : Form
    {
        Construction construction = new Construction();
        int axle = 350;
        public Form1()
        {
            InitializeComponent();
            dataGridView2.Rows.Add();
            dataGridView2.Rows[0].Cells[0].Value = 1;
            dataGridView2.Rows[0].Cells[1].Value = 0;
            construction.nodesLoad.Add(0);

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (construction.kernels.Count>numericUpDown1.Value)
            {
                construction.kernels.RemoveAt(construction.kernels.Count-1);
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
                dataGridView2.Rows.RemoveAt(dataGridView2.Rows.Count - 1);
                dataGridView3.Rows.RemoveAt(dataGridView3.Rows.Count - 1);
                construction.nodesLoad.RemoveAt(construction.nodesLoad.Count - 1);
            }
            else
            {
                construction.kernels.Add(new Kernel());
                dataGridView1.Rows.Add();
                dataGridView2.Rows.Add();
                dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[0].Value = dataGridView2.Rows.Count;
                dataGridView2.Rows[dataGridView2.Rows.Count - 1].Cells[1].Value = 0;
                construction.nodesLoad.Add(0);
               
                dataGridView3.Rows.Add();
                for (int i=0;i<4;i++)
                {
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[i].Value = 1;
                   
                }

                for (int i = 0; i < 4; i++)
                {
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = 600;

                }


                dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[0].Value = construction.kernels.Count;
                dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[1].Value = 0;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
      
        }



        private void button1_Click(object sender, EventArgs e)
        {
            bool check = false;
            for (int i = 0; i < construction.kernels.Count; i++)
            {
                if (dataGridView1.Rows[i].Cells[0].Value == null |
                     dataGridView1.Rows[i].Cells[1].Value == null |
                     dataGridView1.Rows[i].Cells[2].Value == null |
                     dataGridView1.Rows[i].Cells[3].Value == null)
                {
                    check = true;
                }

            }

            if (!check)
            {
                for (int i = 0; i < construction.kernels.Count; i++)
                {
                    construction.kernels[i].L = Int32.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
                    construction.kernels[i].A = Int32.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                    construction.kernels[i].E = double.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                    construction.kernels[i].b = double.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                    construction.kernels[i].distributedLoad = double.Parse(dataGridView3.Rows[i].Cells[1].Value.ToString());
                }

                construction.nodesLoad.Clear();
                for (int i = 0; i < dataGridView2.Rows.Count; i++)
                { 
                    construction.nodesLoad.Add(double.Parse(dataGridView2.Rows[i].Cells[1].Value.ToString()));
                }
            }

            construction.totalWidth = 0;
            for (int i = 0; i < construction.kernels.Count; i++)
            {
                
                construction.totalWidth += construction.kernels[i].L;

            }


            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);




            int max = construction.kernels[0].A;
            double k;

            for (int i = 0; i < construction.kernels.Count; i++)

            {
                construction.kernels[i].location.Height = construction.kernels[i].A;

                if (construction.kernels[i].location.Height > max)
                {
                    max = construction.kernels[i].A;
                }
            }

            if (max > 500)
            {
                k = 500.0f / max;

                for (int i = 0; i < construction.kernels.Count; i++)
                {
                    construction.kernels[i].location.Height = ((int)(construction.kernels[i].location.Height * k));
                    if (construction.kernels[i].location.Height==0)
                    {
                        construction.kernels[i].location.Height = 1;
                    }
                }
            }





            for (int i = 0; i < construction.kernels.Count; i++)
            {
                if (construction.kernels[i] != null)
                {

                    int h = construction.kernels[i].location.Height;

                    if (i == 0)
                        construction.kernels[i].location = new Rectangle(50, axle - h / 2, (pictureBox1.Width - 120) / construction.totalWidth * construction.kernels[i].L, h);
                    else
                    {
                        construction.kernels[i].location = new Rectangle(construction.kernels[i - 1].location.Width + construction.kernels[i - 1].location.X, axle
                            - h / 2, (pictureBox1.Width - 120) / construction.totalWidth * construction.kernels[i].L, h);
                    }
                }
            }

           

            for (int i = 0; i < construction.kernels.Count; i++)
            {
                if (construction.kernels[i] != null)
                    g.DrawRectangle(Pens.Black, construction.kernels[i].location);
            }



            for (int i=0;i<construction.kernels.Count;i++)
            {

            }





            for (int i = 0; i < construction.kernels.Count; i++)
            {
                if (construction.kernels[i] != null)
                {
                    if (construction.kernels[i].distributedLoad>0)
                    {
                        Pen pen = new Pen(Color.Black);
                       // pen.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;

                       // pen.CustomEndCap = new AdjustableArrowCap(construction.kernels[i].location.Height / 30, construction.kernels[i].location.Width/90);
                        pen.CustomEndCap = new AdjustableArrowCap(6, 3);
                        pen.Width = 3;
                        int j = 0;
                        int ost = construction.kernels[i].location.Width % 30;
                        if (ost != 0)
                        {
                            while (j < construction.kernels[i].location.Width - 30)
                            {
                                // g.DrawLine(pen, 50, 75, kernels[i].location.Width + 50, 75);
                                g.DrawLine(pen, construction.kernels[i].location.X + j, axle, construction.kernels[i].location.X + 30 + j, axle);
                                j += 30;
                            }
                            pen.Color = Color.Red;
                            g.DrawLine(pen, construction.kernels[i].location.X + construction.kernels[i].location.Width - ost, axle, construction.kernels[i].location.X + construction.kernels[i].location.Width, axle);

                        }
                        if (ost == 0)
                        {
                            while (j < construction.kernels[i].location.Width)
                            {
                                // g.DrawLine(pen, 50, 75, kernels[i].location.Width + 50, 75);
                                g.DrawLine(pen, construction.kernels[i].location.X + j, axle, construction.kernels[i].location.X + 30 + j, axle);
                                j += 30;
                            }
                        }
                    }
                    else
                    {
                        if (construction.kernels[i].distributedLoad<0)
                        {
                            Pen pen = new Pen(Color.Black);
                            //pen.StartCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
                            pen.CustomStartCap = new AdjustableArrowCap(6, 3);
                            pen.Width = 3;
                            int j = 0;
                            int ost = construction.kernels[i].location.Width % 20;
                            if (ost != 0)
                            {
                                while (j < construction.kernels[i].location.Width - 20)
                                {
                                    // g.DrawLine(pen, 50, 75, kernels[i].location.Width + 50, 75);
                                    g.DrawLine(pen, construction.kernels[i].location.X + j, axle, construction.kernels[i].location.X + 20 + j, axle);
                                    j += 20;
                                }
                                pen.Color = Color.Red;
                                g.DrawLine(pen, construction.kernels[i].location.X + construction.kernels[i].location.Width - ost, axle, construction.kernels[i].location.X + construction.kernels[i].location.Width, axle);

                            }
                            if (ost == 0)
                            {
                                while (j < construction.kernels[i].location.Width)
                                {
                                    // g.DrawLine(pen, 50, 75, kernels[i].location.Width + 50, 75);
                                    g.DrawLine(pen, construction.kernels[i].location.X + j, axle, construction.kernels[i].location.X + 20 + j, axle);
                                    j += 20;
                                }
                            }

                        }
                    }
                }
            }



            if (construction.nodesLoad[0]>0)
            {
                int w = construction.kernels[0].location.X;
                Pen Fpen = new Pen(Color.Black);
                Fpen.CustomEndCap = new AdjustableArrowCap(6, 3);
                Fpen.Width = 7;
                g.DrawLine(Fpen, w, axle, w + 40, axle);
            }

            if (construction.nodesLoad[0]<0)
            {
                int w = construction.kernels[0].location.X;
                Pen Fpen = new Pen(Color.Black);
                Fpen.CustomEndCap = new AdjustableArrowCap(6, 3);
                Fpen.Width = 7;
                g.DrawLine(Fpen, w, axle, w - 40, axle);

            }
            for (int i=1;i<construction.nodesLoad.Count;i++)
            {
                if (construction.nodesLoad[i]>0)
                {
                    int w=construction.kernels[i - 1].location.X + construction.kernels[i - 1].location.Width;
                    Pen Fpen = new Pen(Color.Black);
                    Fpen.CustomEndCap = new AdjustableArrowCap(6, 3);
                    Fpen.Width = 7;
                    g.DrawLine(Fpen, w, axle, w + 40, axle);
                }

                if (construction.nodesLoad[i] < 0)
                {
                    int w = construction.kernels[i - 1].location.X + construction.kernels[i - 1].location.Width;
                    Pen Fpen = new Pen(Color.Black);
                    Fpen.CustomEndCap = new AdjustableArrowCap(6, 3);
                    Fpen.Width = 7;
                    g.DrawLine(Fpen, w, axle, w - 40, axle);
                }

            }

            if (checkBox2.Checked)
            {
                // левый
                if (construction.nodesLoad[0] >= 0)
                {
                    Pen Zp = new Pen(Color.Black);
                    Zp.Width = 2;
                    g.DrawLine(Zp, 48, axle - 30, 48, axle + 30);
                    for (int i = 0; i < 6; i++)
                    {
                        int x = 43;
                        int y = axle - 30 + 10;
                        g.DrawLine(Zp, x - 2, y + (i * 10) + 2, x + 5, y + i * 10 - 10);
                    }
                }
                else
                    checkBox2.Checked = false;
            }

            if (checkBox1.Checked)
            {
                // правый 
                if (construction.nodesLoad[construction.nodesLoad.Count - 1] <= 0)
                {
                    Pen Zp = new Pen(Color.Black);
                    Zp.Width = 2;
                    int leftborder = pictureBox1.Width - 70;
                    g.DrawLine(Zp, leftborder + 2, axle - 30, leftborder + 2, axle + 30);
                    for (int i = 0; i < 6; i++)
                    {
                        int x = leftborder;
                        int y = axle - 30 - 10;
                        g.DrawLine(Zp, x + 10, y + (i * 10) + 10, x, y + i * 10 + 20);
                    }
                }
                else
                    checkBox1.Checked = false;
            }


            // отрисовка текста нагрузок
            for (int i = 0; i < construction.kernels.Count; i++)
            {
                if (construction.kernels[i].distributedLoad != 0)
                {
                    int x = construction.kernels[i].location.X + (construction.kernels[i].location.Width / 2);
               
                    Point pt = new Point(x, axle - 40);
                   
                    Font ft = new Font(label1.Font.Name,15);
                    
                    Brush bh = Brushes.Black;
                    string st = "q= "+ construction.kernels[i].distributedLoad.ToString();
                    g.DrawString(st, ft,bh, pt);
                }
            }



        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
