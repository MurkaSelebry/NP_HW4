using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdpShapesLib {
    public enum Shape { Circle, Square, Triangle, Diamond }
    public enum ShapeSize { Small = 1, Medium, Big }

    public class Player {
        public string Name { get; }
        public Shape Shape { get; }
        public ShapeSize ShapeSize { get; }

        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }

        public int X { get; set; }
        public int Y { get; set; }

        public Player (string name, Shape shape, ShapeSize shapeSize, byte red, byte green, byte blue) {
            Name = name;
            Shape = shape;
            ShapeSize = shapeSize; 

            Red = red;
            Green = green;
            Blue = blue;
        }
        public Player (BinaryReader reader) {
            Name = reader.ReadString ();
            Shape = (Shape) reader.ReadByte ();
            ShapeSize = (ShapeSize) reader.ReadByte ();

            Red = reader.ReadByte ();
            Green = reader.ReadByte ();
            Blue = reader.ReadByte ();

            X = reader.ReadInt32 ();
            Y = reader.ReadInt32 ();
        }

        public void Move (BinaryReader reader) {
            X = reader.ReadInt32 ();
            Y = reader.ReadInt32 ();
        }

        public void ChangeColor(BinaryReader reader)
        {
            Red = reader.ReadByte();
            Green = reader.ReadByte();
            Blue = reader.ReadByte();
        }
        public void Draw (Graphics g) {
            Brush brush = new SolidBrush (Color.FromArgb (Red, Green, Blue));

            g.ResetTransform ();
            g.TranslateTransform (X, Y);
            g.DrawString(Name, new Font("Arial", 16), brush,0,-25);
            if (Shape == Shape.Circle)  
                g.FillEllipse(brush, 0, 0, 50 * (int)ShapeSize, 50 * (int)ShapeSize);
            else if (Shape == Shape.Square)
                g.FillRectangle(brush, 0, 0, 50 * (int)ShapeSize, 50 * (int)ShapeSize);
            else if (Shape == Shape.Triangle)
                g.FillPolygon(brush, new[] { new Point(25 * (int)ShapeSize, 0), new Point(50 * (int)ShapeSize, 50 * (int)ShapeSize), new Point(0, 50 * (int)ShapeSize) });
            else if (Shape == Shape.Diamond)
                g.FillPolygon(brush, new[] { new Point(25 * (int)ShapeSize, 0), new Point(50 * (int)ShapeSize, 25 * (int)ShapeSize), new Point(25 * (int)ShapeSize, 50 * (int)ShapeSize), new Point(0, 25 * (int)ShapeSize) });
        }

        public byte[] EnterMessage () {
            MemoryStream stream = new MemoryStream ();
            BinaryWriter writer = new BinaryWriter (stream);
            writer.Write (Message.Enter);
            writer.Write (Name);
            writer.Write ((byte) Shape);
            writer.Write ((byte) ShapeSize);
            writer.Write (Red);
            writer.Write (Green);
            writer.Write (Blue);
            writer.Write (X);
            writer.Write (Y);
            return stream.ToArray ();
        }

        public byte[] ExistMessage () {
            MemoryStream stream = new MemoryStream ();
            BinaryWriter writer = new BinaryWriter (stream);
            writer.Write (Message.Exist);
            writer.Write (Name);
            writer.Write ((byte) Shape);
            writer.Write ((byte) ShapeSize);
            writer.Write (Red);
            writer.Write (Green);
            writer.Write (Blue);
            writer.Write (X);
            writer.Write (Y);
            return stream.ToArray ();
        }

        public byte[] MoveMessage () {
            MemoryStream stream = new MemoryStream ();
            BinaryWriter writer = new BinaryWriter (stream);
            writer.Write (Message.Move);
            writer.Write (Name);
            writer.Write (X);
            writer.Write (Y);
            return stream.ToArray ();
        }

        public byte[] ChangeColorMessage()
        {
            MemoryStream stream = new MemoryStream();
            BinaryWriter writer = new BinaryWriter(stream);
            writer.Write(Message.ChangeColor);
            writer.Write(Name);
            writer.Write(Red);
            writer.Write(Green);
            writer.Write(Blue);
            return stream.ToArray();
        }

        public byte[] LeaveMessage () {
            MemoryStream stream = new MemoryStream ();
            BinaryWriter writer = new BinaryWriter (stream);
            writer.Write (Message.Leave);
            writer.Write (Name);
            return stream.ToArray ();
        }

        public bool Contains (Point point) =>
            point.X >= X && point.X < X + 50 &&
            point.Y >= Y && point.Y < Y + 50;
    }
}
