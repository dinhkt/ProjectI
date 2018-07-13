using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Project1
{
    public class Edge
    {
        public Node startNode;
        public Node endNode;
        public int weight;
        public Edge()
        {
            this.startNode = new Node();
            this.endNode = new Node();
            this.weight = 0;
        }
        public Edge(Node a, Node b, int weight)
        {
            this.startNode = a;
            this.endNode = b;
            this.weight = weight;
        }
        // làm nổi bật cạnh đang được xử lý
        public void pickEdge()
        {
            Program.MainWindow.drawEdge(this,Brushes.Yellow);
            Program.MainWindow.drawAllNode();
        }
        //Bỏ chọn cạnh 
        public void unpick()
        {
            Program.MainWindow.drawEdge(this, Brushes.LightGray);
            Program.MainWindow.drawAllNode();
        }
    }
}
