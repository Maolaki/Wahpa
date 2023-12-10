using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WindowEngine;

namespace Wahpa.WindowsFolder
{
    internal class ComboBox : GUIElement, IUpdate
    {
        private static DateTime lastClickTime;
        public delegate void doFunc();
        public List<ComboBox> boxes = new List<ComboBox>();
        public bool isActive = false;
        public int number = 0;
        public int comboBoxSize { get; set; } = 0;

        // Конструктор, который принимает высоту и ширину окна
        public ComboBox(float size, float coordX, float coordY, int location, string[] comboTextArray) : base(size, coordX, coordY, Data.GUIDict["ComboBox"], location)
        {
            foreach (string text in comboTextArray)
            {
                if (text == comboTextArray[0])
                {
                    boxes.Add(new ComboBox(0, 0, 0, Data.GUIDict["ComboBoxEdge"], 0));
                }
                else if(text == comboTextArray[comboTextArray.Length - 1])
                {
                    boxes.Add(new ComboBox(0, 0, 0, Data.GUIDict["ComboBoxEdge2"], 0));
                }
                else
                {
                    boxes.Add(new ComboBox(0, 0, 0, Data.GUIDict["ComboBoxMid"], 0));
                }
                
                boxes[comboBoxSize].SetText(comboTextArray[comboBoxSize], Data.font1, 60, Color.Black);
                boxes[comboBoxSize].number = comboBoxSize;
                comboBoxSize += 1;
            }

            foreach (ComboBox box in boxes)
            {
                box.isActive = true;
                box.boxes.Add(this);

                box.DoClickFunc += () =>
                {
                    foreach(ComboBox box in boxes)
                    {
                        ViewHandler.GUIUpdates.Remove(box);
                        ViewHandler.GUIDraw.Remove(box);
                    }
                    ViewHandler.GUIUpdates.Add(this);
                    ViewHandler.GUIDraw.Add(this);
                    this.Text = new Text(box.Text);
                };
            }

            base.Resize();
            base.Relocate();
        }

        public ComboBox(float size, float coordX, float coordY, Texture texture, int location) : base(size, coordX, coordY, texture, location)
        {
            base.Resize();
            base.Relocate();
        }

        public doFunc DoClickFunc;

        public void Update()
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Vector2i mousePos = Mouse.GetPosition(MainWindow.window);
                Vector2f worldPos = MainWindow.window.MapPixelToCoords(mousePos);

                if (this.Sprite.GetGlobalBounds().Contains(worldPos.X, worldPos.Y))
                {
                    DateTime currentTime = DateTime.Now;

                    if ((currentTime - lastClickTime).TotalSeconds >= 0.3)
                    {
                        lastClickTime = currentTime;

                        DoClickFunc();
                    }
                }
            }
        }

        public override void Relocate()
        {
            if (!isActive)
            {
                base.Relocate();
            }
            else
            {
                boxes[0].Relocate();

                Vector2f position = new Vector2f(boxes[0].Sprite.Position.X, boxes[0].Sprite.Position.Y);

                position.Y += boxes[0].Sprite.Texture.Size.Y * boxes[0].Sprite.Scale.Y * number;

                this.Sprite.Position = position;

                float maxLetterHeight = Text.Font.GetLineSpacing(Text.CharacterSize);

                float offsetX = Sprite.GetGlobalBounds().Width / 2 - Text.GetLocalBounds().Width * Text.Scale.X / 2;
                float offsetY = (Sprite.GetGlobalBounds().Height - maxLetterHeight * Text.Scale.Y) / 2;

                Text.Position = new Vector2f(Sprite.Position.X + offsetX, Sprite.Position.Y + offsetY);

            }
        }

        public override void Resize()
        {
            if (!isActive)
            {
                base.Resize();
            }
            else
            {
                boxes[0].Resize();

                this.Size = boxes[0].Size;
                this.Sprite.Scale = boxes[0].Sprite.Scale;
                this.Text.Scale = boxes[0].Text.Scale;
            }

        }
    }
}
