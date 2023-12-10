using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace WindowEngine
{
    internal class TableView : GUIElement
    {
        public List<List<GUIElement>> Columns;
        public int rowSize {  get; set; }

        public TableView(float size, float coordX, float coordY, Texture texture, int location, int columnSize, int rowSize) : base(size, coordX, coordY, texture = null, location)
        {
            this.Columns = new List<List<GUIElement>>(columnSize);
            this.rowSize = rowSize;
        }

        public void AddColumn(float size, float coordX, float coordY, uint columnNumber, GUIElement[] elementsArray)
        {
            if (columnNumber >= Columns.Count)
                return;

            for (int i = 0; i < rowSize; i++)
            {
                GUIElement element = elementsArray[i];
                Columns[(int)columnNumber].Add(element);
                element.CoordinateX = coordX;
                element.CoordinateY = coordY;
                element.Size = size;
            }
        }

        public override void Resize()
        {
            for (int i = 0; i < Columns.Count; i++)
            {
                for (int j = 0; j < rowSize; i++)
                {
                    Columns[i][j].Resize();
                }
            }
        }

        public override void Relocate()
        {
            for (int i = 0; i < Columns.Count; i++)
            {
                for (int j = 0; j < rowSize; i++)
                {
                    Columns[i][j].Relocate();
                }
            }
        }

        public override void Draw()
        {
            for (int i = 0; i < Columns.Count; i++)
            {
                for (int j = 0; j < rowSize; i++)
                {
                    Columns[i][j].Draw();
                }
            }
        }
    }
}
