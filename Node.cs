using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace Project1
{
    public class Node
    {
        public string name;
        public int x;
        public int y;
        public int Distance;// giá trị distance của node so với node bắt đầu
        public Node prev = null;// nút liền kề trước nó trong thuật toán dijkstra
        public int picked = 0;// trạng thái đã được xử lý hay chưa; 0 là chưa được chọn, 1 là đang được chọn , 2 là đã được chọn
        public Node()
        {
            this.name = "";
            this.x = 0;
            this.y = 0;
        }
        public Node(string name,int x, int y)
        {
            this.name = name;
            this.x = x;
            this.y = y;
        }
        //làm nổi bật node đang được chọn
        public void pickNode()
        {
            
            Program.MainWindow.drawNode(this,Program.MainWindow.img2);
            this.picked = 1;
        }
        // Bỏ chọn Node
        public void unpick()
        {

            Program.MainWindow.drawNode(this,Program.MainWindow.img4);
            this.picked = 2;
        }
        // Vẽ giá trị distance lên Node
        public void drawValue(int x)
        {
            this.Distance = x;
            Program.MainWindow.drawValue(this,this.Distance);
        }
    }
}
