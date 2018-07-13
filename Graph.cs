using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
namespace Project1
{
    public partial class Graph : Form
    {
        public  int NumofNode = 0;
        public  int NumofEdge = 0;
        public  List<Node> nodes = new List<Node>();
        public  List<Edge> edges = new List<Edge>();
        public  List<Node> result = new List<Node>();
        bool DrawMode,DrawLine = false;
        Font f = new Font("Arial", 9);
        Pen pb = new Pen(Brushes.Black, 5);
        Pen pblue = new Pen(Brushes.SkyBlue, 5);
        public Image img1 = new Bitmap("red-icon.png");// hình vẽ biểu thị node ban đầu
        public Image img2 = new Bitmap("blue-icon.png");// hình biểu thị node đang đc chọn
        public Image img3 = new Bitmap("ricon.png");// hình dùng để vẽ distance lên 
        public Image img4 = new Bitmap("tickicon.png");// hình biểu thị node đã được chọn
        public Graph()
        {
            InitializeComponent();
            this.panel4.Hide();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Graphics g = panel5.CreateGraphics();
            g.Clear(Color.White);
            NumofNode = 0;
            NumofEdge = 0;
            edges.Clear();
            nodes.Clear();
            
        }
        //Mở file đã lưu để vẽ
        private void button2_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog
            {
                Filter = "Text File|*.txt|All File|*.*",
            };
            if (dlg.ShowDialog() != DialogResult.OK) return;
            string[] lines = System.IO.File.ReadAllLines(dlg.FileName);

            int n = lines.Length;
            for (int i = 0; i < n; i++)
                if (lines[i] == "Edge:")
                {
                    NumofNode = i - 1;
                    NumofEdge = n - 2 - NumofNode;
                    break;
                }
            for (int i = 1; i < NumofNode + 1; i++)
            {
                string[] separators = { "(", ")", "," };
                string[] s = lines[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);
                Node temp_nodes = new Node(s[0], Int32.Parse(s[1]), Int32.Parse(s[2]));
                nodes.Add(temp_nodes);
            }
            for (int i = NumofNode + 2; i < n; i++)
            {
                string[] separators = { "-", ":" };
                int sN = 0, eN = 0;
                string[] s = lines[i].Split(separators, StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < NumofNode; j++)
                {
                    if (nodes[j].name == s[0]) sN = j;
                    if (nodes[j].name == s[1]) eN = j;
                }
                Edge temp_edge1 = new Edge(nodes[sN], nodes[eN], Int32.Parse(s[2]));
                
                edges.Add(temp_edge1);
                

            }
            draw();
        }
        //lưu đồ thị vừa vẽ vào Data.txt
        private void button3_Click(object sender, EventArgs e)
        {
            string path = "Data.txt";
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine("Node:");
                    for (int i = 0; i < NumofNode; i++)
                    {
                        sw.WriteLine(nodes[i].name + "(" + nodes[i].x.ToString() + "," + nodes[i].y.ToString()+")");
                    }
                    sw.WriteLine("Edge:");
                    for(int i=0;i<NumofEdge;i++)
                    {
                        sw.WriteLine(edges[i].startNode.name + "--" + edges[i].endNode.name + ":" + edges[i].weight.ToString());

                    }
                    sw.Close();
                }
            }
            
        }
        //Chọn chế độ vẽ điểm
        private void button4_Click(object sender, EventArgs e)
        {
            if (DrawMode == false)
            {
                DrawMode = true;
                
            }
            else DrawMode = false;
        }
        //Chọn chế độ vẽ cạnh
        private void button5_Click(object sender, EventArgs e)
        {
            if (DrawLine == false)
            {
                this.panel4.Show();
                DrawLine = true;
            }
            else
            {
                this.panel4.Hide();
                DrawLine = false;
            }
        }

        //chế độ chạy từng bước
        private void button7_Click(object sender, EventArgs e)
        {
            int sN = 0, eN = 0;
            for (int i = 0; i < Program.MainWindow.NumofNode; i++)
            {
                if (textBox1.Text == Program.MainWindow.nodes[i].name) { sN = i; break; }
                else sN = -1;
            }
            for (int i = 0; i < Program.MainWindow.NumofNode; i++)
            {
                if (textBox2.Text == Program.MainWindow.nodes[i].name) { eN = i; break; }
                else eN = -1;
            }
            if (sN == eN || sN == -1 || eN == -1) MessageBox.Show("Nhập sai điểm");
            else Algorithm.Dijkstra(sN, eN, true);
        }
        // chế độ chạy xuyên suốt 
        private void button8_Click(object sender, EventArgs e)
        {
            int sN = 0, eN = 0;
            for (int i = 0; i < Program.MainWindow.NumofNode; i++)
            {
                if (textBox1.Text == Program.MainWindow.nodes[i].name) { sN = i; break; }
                else sN = -1;
            }
            for (int i = 0; i < Program.MainWindow.NumofNode; i++)
            {
                if (textBox2.Text == Program.MainWindow.nodes[i].name) { eN = i; break; }
                else eN = -1;
            }
            if (sN == eN || sN == -1 || eN == -1) MessageBox.Show("Nhập sai điểm");
            else Algorithm.Dijkstra(sN, eN,false);
        }
        // Add Edge
        private void button6_Click_1(object sender, EventArgs e)
        {
            int sN = 0, eN = 0;
            for (int i = 0; i < Program.MainWindow.NumofNode; i++)
            {
                if (textBox4.Text == Program.MainWindow.nodes[i].name) { sN = i; break; }
                else sN = -1;
            }
            for (int i = 0; i < Program.MainWindow.NumofNode; i++)
            {
                if (textBox3.Text == Program.MainWindow.nodes[i].name) { eN = i; break; }
                else eN = -1;
            }
            if (sN == eN||sN==-1||eN==-1) MessageBox.Show("Nhập sai cạnh!");
            else
            {
                Edge tmp = new Edge(nodes[sN], nodes[eN], Int32.Parse(textBox5.Text));
                edges.Add(tmp);
                NumofEdge++;
            }
            draw();
        }
        // vẽ Node với hình vẽ hiển thị node tùy chọn
        public void drawNode(Node n,Image img)
        {
            Graphics g = panel5.CreateGraphics();
            g.DrawImage(img, n.x - 8, n.y - 8);
            g.DrawString(n.name, f,Brushes.Yellow, n.x - 7, n.y - 7);
        }
        // vẽ Edge với pen tùy chọn
        public void drawEdge(Edge e,Brush br)
        {
            Graphics g = panel5.CreateGraphics();
            var f = new Font("Arial", 9);
            Pen pen2 = new Pen(br, 5);
            System.Drawing.Drawing2D.AdjustableArrowCap bigArrow = new System.Drawing.Drawing2D.AdjustableArrowCap(5, 5);
            pen2.CustomEndCap = bigArrow;
            Point p1 = new Point(e.startNode.x, e.startNode.y);
            Point p2 = new Point(e.endNode.x, e.endNode.y);
            g.DrawLine(pen2, p1, p2);
            int xx = (e.startNode.x + e.endNode.x) / 2 - 15;
            int yy = (e.startNode.y + e.endNode.y) / 2 - 15;
            g.DrawString(e.weight.ToString(), f, Brushes.Black, xx, yy);
            
        }
        // vẽ lại tất cả các node và Distance của nó, hàm này dùng sau khi vẽ lại cạnh vì hàm vẽ cạnh sẽ vẽ chèn vào ảnh biểu thị node
        public void drawAllNode()
        {
            for (int i = 0; i < NumofNode; i++)
            {
                drawValue(nodes[i], nodes[i].Distance);
                if (nodes[i].picked == 0) drawNode(nodes[i], img1);
                if (nodes[i].picked == 1) drawNode(nodes[i], img2);
                if (nodes[i].picked == 2) drawNode(nodes[i], img4);
            }

        }
        //Hàm vẽ tất cả các node và cạnh
        public void draw()
        {
            for (int i = 0; i < NumofEdge; i++)
            {
                drawEdge(edges[i],Brushes.LightGray);
            }
            drawAllNode();
            
        }
        
        //xu li su kien nhap chuot trong panel
        private void panel5_MouseDown(object sender, MouseEventArgs e)
        {
            if (DrawMode == true)
            {
                char c = (char) (NumofNode+65);
                Node tmp = new Node(c.ToString(), e.X, e.Y);
                NumofNode++;
                nodes.Add(tmp);
                drawNode(tmp,img1);
            }
        }

        //vẽ giá trị distance lên node
        public void drawValue(Node n,int x)
        {
            Graphics g = panel5.CreateGraphics();
            g.DrawImage(img3, n.x -8, n.y - 20);
            
                if (x == Int32.MaxValue - Int16.MaxValue) g.DrawString("inf", f, Brushes.DarkBlue, n.x - 8, n.y - 20);
                else g.DrawString(x.ToString(), f, Brushes.DarkBlue, n.x - 6, n.y - 20);
            
        }
        

        
    }
}
