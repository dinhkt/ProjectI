using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project1
{
    class Algorithm
    {
        public static void Dijkstra(int startNode,int endNode, bool option)
        {
            int inf = Int32.MaxValue-Int16.MaxValue;
            // vẽ giá trị distance ban đầu lên các Node
            Program.MainWindow.nodes[startNode].drawValue(0);
            for(int i=0;i<Program.MainWindow.NumofNode;i++)
            {
                if(i!=startNode)
                {
                    Program.MainWindow.nodes[i].drawValue(inf);
                }
            }
            //
            if(option) System.Threading.Thread.Sleep(2000);
            int pickNode=startNode;
            while (pickNode!=endNode)
            {
                Program.MainWindow.nodes[pickNode].pickNode();// làm nổi bật node đang được chọn
                if (option)  System.Threading.Thread.Sleep(2000);

                for(int i=0;i<Program.MainWindow.NumofEdge;i++)
                {
                    if (Program.MainWindow.edges[i].startNode.name==((char)(pickNode+65)).ToString())
                    {
                        Program.MainWindow.edges[i].pickEdge();
                        
                        if (Program.MainWindow.edges[i].endNode.Distance > Program.MainWindow.edges[i].startNode.Distance + Program.MainWindow.edges[i].weight)
                        {
                            if (option) System.Threading.Thread.Sleep(2000);
                            Program.MainWindow.edges[i].endNode.drawValue(Program.MainWindow.edges[i].startNode.Distance + Program.MainWindow.edges[i].weight);// cập nhật distance
                            Program.MainWindow.edges[i].endNode.prev = Program.MainWindow.edges[i].startNode;// lưu node trước đó vào node liền kề
                        }
                        if (option) System.Threading.Thread.Sleep(2000);
                        Program.MainWindow.edges[i].unpick();
                        Program.MainWindow.drawNode(Program.MainWindow.nodes[pickNode], Program.MainWindow.img2);
                    }
                }
                Program.MainWindow.nodes[pickNode].unpick();
                if (option) System.Threading.Thread.Sleep(2000);
                //Lấy node có distance nhỏ nhất trong số các node chưa được chọn 
                for (int i = 0; i < Program.MainWindow.NumofNode; i++)
                {
                    if (Program.MainWindow.nodes[i].picked == 0)
                    {
                        pickNode = i;
                    }
                }
                for (int i = 0; i < Program.MainWindow.NumofNode; i++)
                {
                    if (Program.MainWindow.nodes[i].picked==0)
                    {
                        if (Program.MainWindow.nodes[i].Distance < Program.MainWindow.nodes[pickNode].Distance)
                            pickNode = i;
                    }
                }
                Program.MainWindow.nodes[pickNode].pickNode();
            }
            // Truy ngược lại đường đi từ Node đích
            Node x=Program.MainWindow.nodes[endNode];
            while(x!=null)
            {
                for (int i = 0; i < Program.MainWindow.NumofEdge; i++)
                    if (Program.MainWindow.edges[i].startNode == x.prev && Program.MainWindow.edges[i].endNode == x)
                        Program.MainWindow.edges[i].pickEdge();
                if (option) System.Threading.Thread.Sleep(1000); 
                x = x.prev;
            }
            for (int i = 0; i < Program.MainWindow.NumofNode; i++)
            {
                Program.MainWindow.drawNode(Program.MainWindow.nodes[i], Program.MainWindow.img1);
            }

        }
    }
}
