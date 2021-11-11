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
            if (construction.kernels.Count > numericUpDown1.Value)
            {
                construction.kernels.RemoveAt(construction.kernels.Count - 1);
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
                for (int i = 0; i < 4; i++)
                {
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[i].Value = 1;

                }

                for (int i = 0; i < 4; i++)
                {
                    dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[1].Value = 6;

                }


                dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[0].Value = construction.kernels.Count;
                dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[1].Value = 0;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }


        private void DrawWarning(Graphics g)
        {
            Point pt = new Point(pictureBox1.Width / 2 - 180, pictureBox1.Height - 40);
            Font ft = new Font(label1.Font.Name, 15);
            Brush bh = Brushes.Black;
            string st = "Некоторые нагрузки не отрисованы из-за масштаба";
            g.DrawString(st, ft, bh, pt);
        }
        private void DrawSealing( Graphics g)
        {
            if (checkBox2.Checked)
            {
                construction.leftSealing = true;
                // левый
                if (construction.nodesLoad[0] >= 0)
                {
                    Pen Zp = new Pen(Color.Black);
                    Zp.Width = 3;
                    g.DrawLine(Zp, construction.kernels[0].location.X, axle - 30, construction.kernels[0].location.X, axle + 30);
                    for (int i = 0; i < 6; i++)
                    {
                        int x = construction.kernels[0].location.X;
                        int y = axle - 30 + 10;
                        g.DrawLine(Zp, x - 10, y + (i * 10) + 2, x, y + i * 10 - 10);
                    }
                }
                else
                {
                    construction.leftSealing = false;
                    checkBox2.Checked = false;
                }
            }

            if (checkBox1.Checked)
            {
                construction.rightSealing = true;
                // правый 
                if (construction.nodesLoad[construction.nodesLoad.Count - 1] <= 0)
                {
                    Pen Zp = new Pen(Color.Black);
                    Zp.Width = 3;
                    int x = construction.kernels[construction.kernels.Count - 1].location.X + construction.kernels[construction.kernels.Count - 1].location.Width;
                    int leftborder = pictureBox1.Width - 70;
                    g.DrawLine(Zp, x, axle - 30, x, axle + 30);
                    for (int i = 0; i < 6; i++)
                    {

                        int y = axle - 30 - 10;
                        g.DrawLine(Zp, x + 10, y + (i * 10) + 10, x, y + i * 10 + 20);
                    }
                }
                else
                {
                    construction.rightSealing = false;
                    checkBox1.Checked = false;
                }
                    
            }
        }
        private void DrawNodesLoads(Graphics g)
        {
            if (construction.nodesLoad[0] > 0)
            {
                if (construction.kernels[0].location.Width < 45 || construction.kernels[0].location.Height < 45)
                {
                    DrawWarning(g);
                }
                else
                {
                    int w = construction.kernels[0].location.X;
                    Pen Fpen = new Pen(Color.Black);
                    Fpen.CustomEndCap = new AdjustableArrowCap(6, 3);
                    Fpen.Width = 7;
                    g.DrawLine(Fpen, w, axle, w + 40, axle);
                    Point pt = new Point(construction.kernels[0].location.X, axle - 45);
                    Font ft = new Font(label1.Font.Name, 10);
                    Brush bh = Brushes.Black;
                    string st = "F= " + construction.nodesLoad[0].ToString();
                    g.DrawString(st, ft, bh, pt);
                }
            }

            if (construction.nodesLoad[0] < 0)
            {
                int w = construction.kernels[0].location.X;
                Pen Fpen = new Pen(Color.Black);
                Fpen.CustomEndCap = new AdjustableArrowCap(6, 3);
                Fpen.Width = 7;
                g.DrawLine(Fpen, w, axle, w - 40, axle);

                Point pt = new Point(construction.kernels[0].location.X-45, axle - 45);

                Font ft = new Font(label1.Font.Name, 10);

                Brush bh = Brushes.Black;
                string st = "F= " + construction.nodesLoad[0].ToString();
                g.DrawString(st, ft, bh, pt);

            }

            if (construction.nodesLoad[construction.nodesLoad.Count-1] > 0)
            {
                
                    int w = construction.kernels[construction.kernels.Count - 1].location.X+construction.kernels[construction.kernels.Count - 1].location.Width;
                    Pen Fpen = new Pen(Color.Black);
                    Fpen.CustomEndCap = new AdjustableArrowCap(6, 3);
                    Fpen.Width = 7;
                    g.DrawLine(Fpen, w, axle, w + 40, axle);
                    Point pt = new Point(w, axle - 45);
                    Font ft = new Font(label1.Font.Name, 10);
                    Brush bh = Brushes.Black;
                    string st = "F= " + construction.nodesLoad[construction.nodesLoad.Count-1].ToString();
                    g.DrawString(st, ft, bh, pt);
                
            }

            if (construction.nodesLoad[construction.nodesLoad.Count - 1] < 0)
            {
                if (construction.kernels[construction.kernels.Count - 1].location.Width < 45 || construction.kernels[construction.kernels.Count - 1].location.Height < 45)
                    { 
                    DrawWarning(g);
                    }
                else {
                    int w = construction.kernels[construction.kernels.Count - 1].location.X + construction.kernels[construction.kernels.Count - 1].location.Width;
                    Pen Fpen = new Pen(Color.Black);
                    Fpen.CustomEndCap = new AdjustableArrowCap(6, 3);
                    Fpen.Width = 7;
                    g.DrawLine(Fpen, w, axle, w - 40, axle);

                    Point pt = new Point(w - 45, axle - 45);

                    Font ft = new Font(label1.Font.Name, 10);

                    Brush bh = Brushes.Black;
                    string st = "F= " + construction.nodesLoad[construction.nodesLoad.Count - 1].ToString();
                    g.DrawString(st, ft, bh, pt);
                }
            }


            for (int i = 1; i < construction.nodesLoad.Count-1; i++)
            {

                
                    if (construction.nodesLoad[i] > 0)
                    {
                        if (construction.kernels[i].location.Width < 45 || construction.kernels[i].location.Height < 45)
                        {
                            DrawWarning(g);
                        }
                            else
                            { 
                                int w = construction.kernels[i - 1].location.X + construction.kernels[i - 1].location.Width;
                                Pen Fpen = new Pen(Color.Black);
                                Fpen.CustomEndCap = new AdjustableArrowCap(6, 3);
                                Fpen.Width = 7;
                                g.DrawLine(Fpen, w, axle, w + 40, axle);
                                int x = w + 20;
                                Point pt = new Point(x, axle - 45);
                                Font ft = new Font(label1.Font.Name, 15);
                                Brush bh = Brushes.Black;
                                string st = "F= " + construction.nodesLoad[i].ToString();
                                g.DrawString(st, ft, bh, pt);
                            }

                    }

                if (construction.nodesLoad[i] < 0)
                {
                    if (construction.kernels[i - 1].location.Width < 45 || construction.kernels[i - 1].location.Height < 45)
                    {
                        DrawWarning(g);
                    }
                    else
                    {
                        int w = construction.kernels[i - 1].location.X + construction.kernels[i - 1].location.Width;
                        Pen Fpen = new Pen(Color.Black);
                        Fpen.CustomEndCap = new AdjustableArrowCap(6, 3);
                        Fpen.Width = 7;
                        g.DrawLine(Fpen, w, axle, w - 40, axle);
                        int x = w - 80;
                        Point pt = new Point(x, axle - 45);
                        Font ft = new Font(label1.Font.Name, 15);
                        Brush bh = Brushes.Black;
                        string st = "F= " + construction.nodesLoad[i].ToString();
                        g.DrawString(st, ft, bh, pt);
                    }
                }
                
            }


            
        }
            
            
            
        private void DrawDistibutedLoads(Graphics g)
        {
            for (int i = 0; i < construction.kernels.Count; i++)
            {
                if (construction.kernels[i] != null)
                {

                    if (construction.kernels[i].location.Height < 30)
                    {
                       
                        Point pt = new Point(pictureBox1.Width / 2-180, pictureBox1.Height - 40);
                        

                        Font ft = new Font(label1.Font.Name, 15);

                        Brush bh = Brushes.Black;
                        string st = "Некоторые нагрузки не отрисованы из-за масштаба";
                        g.DrawString(st, ft, bh, pt);
                    }
                    else
                    {
                        if (construction.kernels[i].distributedLoad > 0)
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
                            if (construction.kernels[i].distributedLoad < 0)
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
            }
        }

        private void DrawDistributedLoadsNames(Graphics g)
        {
            for (int i = 0; i < construction.kernels.Count; i++)
            {
                if (construction.kernels[i].location.Width < 40)
                {

                    Point pt = new Point(pictureBox1.Width / 2 - 180, pictureBox1.Height - 40);


                    Font ft = new Font(label1.Font.Name, 10);

                    Brush bh = Brushes.Black;
                    string st = "Некоторые нагрузки не отрисованы из-за масштаба";
                    g.DrawString(st, ft, bh, pt);
                }
                else
                {
                    if (construction.kernels[i].distributedLoad != 0)
                    {
                        int x;
                        if (construction.kernels[i].location.Width > 40)
                        {
                             x = construction.kernels[i].location.X + (construction.kernels[i].location.Width / 2);
                        }
                        else
                        {
                            x = construction.kernels[i].location.X;
                        }
                        Point pt = new Point(x, axle + 40);

                        Font ft = new Font(label1.Font.Name, 10);

                        Brush bh = Brushes.Black;
                        string st = "q= " + construction.kernels[i].distributedLoad.ToString();
                        g.DrawString(st, ft, bh, pt);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool check = false;
            Graphics g = pictureBox1.CreateGraphics();
            g.Clear(Color.White);

            if (construction.kernels.Count > 0)
            {
                if (checkBox1.Checked == false && checkBox2.Checked == false)
                {

                    MessageBox.Show("Должна быть минимум одна активная заделка");
                }
                else 
                { 
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







                    int max = construction.kernels[0].A * 100;
                    double k;

                    for (int i = 0; i < construction.kernels.Count; i++)

                    {
                        construction.kernels[i].location.Height = construction.kernels[i].A * 100;

                        if (construction.kernels[i].location.Height > max)
                        {
                            max = construction.kernels[i].A * 100;
                        }
                    }

                    if (max > 500)
                    {
                        k = 500.0f / max;

                        for (int i = 0; i < construction.kernels.Count; i++)
                        {
                            construction.kernels[i].location.Height = ((int)(construction.kernels[i].location.Height * k));
                            if (construction.kernels[i].location.Height == 0)
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

                    DrawDistibutedLoads(g);
                    DrawSealing(g);
                    DrawNodesLoads(g);
                    DrawDistributedLoadsNames(g);



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

        

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private bool CheckForCorrectFloat(string z)
        {
           
            for (int i=0;i<z.Length;i++)
            {
                if (z[i]=='.')
                {
                    return true;
                }
                
            }
            return false;
        }
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {


            if (e.ColumnIndex == 0)
            {
                int res;
                if (e.FormattedValue.ToString() == string.Empty)
                    return;
                else
                    if (!int.TryParse(e.FormattedValue.ToString(), out res) || e.FormattedValue.ToString().Length > 10)
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1;
                    MessageBox.Show("Введите числовое значение");
                    e.Cancel = true;
                    return;
                }
            }
            if (e.ColumnIndex == 1)
            {
                int res;
                if (e.FormattedValue.ToString() == string.Empty)
                    return;
                else
                    if (!int.TryParse(e.FormattedValue.ToString(), out res) || e.FormattedValue.ToString().Length > 10 )
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1;
                    MessageBox.Show("Введите числовое значение");
                    e.Cancel = true;
                    return;
                }
            }

            if (e.ColumnIndex == 2)
            {
                float res;
                if (e.FormattedValue.ToString() == string.Empty)
                    return;
                else
                    if (!float.TryParse(e.FormattedValue.ToString(), out res) || e.FormattedValue.ToString().Length > 10 || CheckForCorrectFloat(e.FormattedValue.ToString()))
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1;
                    MessageBox.Show("Введите числовое значение");
                    e.Cancel = true;
                    return;
                }
            }

            if (e.ColumnIndex == 3)
            {
                float res;
                if (e.FormattedValue.ToString() == string.Empty)
                    return;
                else
                    if (!float.TryParse(e.FormattedValue.ToString(), out res) || e.FormattedValue.ToString().Length > 10 || CheckForCorrectFloat(e.FormattedValue.ToString()))
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1;
                    MessageBox.Show("Введите числовое значение");
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void dataGridView2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            float res;
           


            if (e.ColumnIndex == 1)
            {
                if (e.FormattedValue.ToString() == string.Empty)
                    return;
                else
                    if (!float.TryParse(e.FormattedValue.ToString(), out res) || e.FormattedValue.ToString().Length > 10 || CheckForCorrectFloat(e.FormattedValue.ToString()))
                {
                    dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1;
                    MessageBox.Show("Введите числовое значение");
                    e.Cancel = true;
                    return;
                }
            }
        }

        private void dataGridView3_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

        }


        private void updateDataGrids()
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            for (int i = 0; i < construction.kernels.Count; i++)
            {
                dataGridView1.Rows.Add();
                dataGridView1.Rows[dataGridView1.Rows.GetLastRow(DataGridViewElementStates.None)].Cells[0].Value = construction.kernels[i].L;
                dataGridView1.Rows[dataGridView1.Rows.GetLastRow(DataGridViewElementStates.None)].Cells[1].Value = construction.kernels[i].A;
                dataGridView1.Rows[dataGridView1.Rows.GetLastRow(DataGridViewElementStates.None)].Cells[2].Value = construction.kernels[i].E;
                dataGridView1.Rows[dataGridView1.Rows.GetLastRow(DataGridViewElementStates.None)].Cells[3].Value = construction.kernels[i].b;
                
            }

            for (int i=0;i<construction.nodesLoad.Count;i++)
            {
                dataGridView2.Rows.Add();
                dataGridView2.Rows[dataGridView2.Rows.GetLastRow(DataGridViewElementStates.None)].Cells[0].Value = i + 1;
                dataGridView2.Rows[dataGridView2.Rows.GetLastRow(DataGridViewElementStates.None)].Cells[1].Value = construction.nodesLoad[i];
            }

            for (int i = 0; i < construction.kernels.Count; i++)
            {
                dataGridView3.Rows.Add();
                dataGridView3.Rows[dataGridView3.Rows.GetLastRow(DataGridViewElementStates.None)].Cells[0].Value = i + 1;
                dataGridView3.Rows[dataGridView3.Rows.GetLastRow(DataGridViewElementStates.None)].Cells[1].Value = construction.kernels[i].distributedLoad;
            }

            checkBox2.Checked = construction.leftSealing;
            checkBox2.Checked = construction.rightSealing;

            dataGridView1.Update();
            dataGridView2.Update();
            dataGridView3.Update();

        }
        private void button2_Click(object sender, EventArgs e)
        {
            
            SaveFileDialog sfd=new SaveFileDialog();
            sfd.Filter = "Json files (*.json)|*.json";
           
            if (sfd.ShowDialog()==DialogResult.OK)
            construction.SaveToJson(sfd.FileName);
        }

        private void Load_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Json files (*.json)|*.json";
           
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                construction.LoadFromJson(ofd.FileName);

                

                updateDataGrids();
                button1_Click(sender, e);
                
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
   
}
