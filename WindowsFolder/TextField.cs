using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System.Runtime.InteropServices;
using System.Text;

namespace WindowEngine
{
    class TextField : GUIElement, IUpdate
    {
        private Clock clock;
        public string text;
        public char passwordChar;
        public bool isFocused;
        public bool isMasked; // Добавил новое поле для хранения значения, которое определяет, нужно ли заменять символы на *
        public Keyboard.Key lastKey;
        public bool cursorVisible; // Добавил новое поле для хранения состояния видимости курсора
        public Text cursor; // Добавил новое поле для хранения текста курсора

        public TextField(float size, float coordX, float coordY, Texture texture, int location, bool isMasked) : base(size, coordX, coordY, texture, location)
        {
            this.clock = new Clock();
            this.text = "";
            this.passwordChar = '*';
            this.isFocused = false;
            this.isMasked = isMasked; // Инициализировал новое поле в конструкторе
            this.lastKey = Keyboard.Key.Unknown;
            this.cursorVisible = true; // Инициализировал новое состояние видимости курсора в конструкторе
            this.Text = new Text("", Data.font2, 60)
            {
                Color = Color.Black,
                Style = Text.Styles.Regular
            };
            this.cursor = new Text("|", Data.font2, 60) // Инициализировал новый текст курсора в конструкторе
            {
                Color = Color.Black,
                Style = Text.Styles.Bold
            };
        }

        // Метод для обновления состояния поля ввода пароля
        public void Update()
        {
            if (Mouse.IsButtonPressed(Mouse.Button.Left))
            {
                Vector2i mousePos = Mouse.GetPosition(MainWindow.window);
                Vector2f worldPos = MainWindow.window.MapPixelToCoords(mousePos);

                if (this.Sprite.GetGlobalBounds().Contains(worldPos.X, worldPos.Y))
                {
                    this.isFocused = true;
                }
                else
                {
                    this.isFocused = false;
                }
            }

            if (this.isFocused)
            {
                Keyboard.Key key = Keyboard.Key.Unknown;
                foreach (Keyboard.Key k in Enum.GetValues(typeof(Keyboard.Key)))
                {
                    if (Keyboard.IsKeyPressed(k))
                    {
                        key = k;
                        break;
                    }
                }

                if (key != this.lastKey)
                {
                    this.lastKey = key;
                    switch (key)
                    {
                        case Keyboard.Key.Backspace:
                            if (this.text.Length > 0)
                            {
                                this.text = this.text.Remove(this.text.Length - 1);
                            }
                            break;
                        default: 
                            if (((key >= Keyboard.Key.A && key <= Keyboard.Key.Z) || (key >= Keyboard.Key.Num0 && key <= Keyboard.Key.Num9)) && this.text.Length < 9)
                            {
                                char c = KeyToChar(key);
                                if (!Keyboard.IsKeyPressed(Keyboard.Key.LShift) && !Keyboard.IsKeyPressed(Keyboard.Key.RShift) && !Console.CapsLock)
                                {
                                    c = char.ToLower(c);
                                }
                                this.text += c;
                            }
                            break;
                    }
                }



                if (this.clock.ElapsedTime.AsSeconds() > 0.5f) 
                {
                    this.cursorVisible = !this.cursorVisible;
                    this.clock.Restart(); 
                }
            }

            if (this.isMasked) 
            {
                this.Text.DisplayedString = new string(this.passwordChar, this.text.Length); 
            }
            else
            {
                this.Text.DisplayedString = this.text;
            }


            this.Resize();
            this.Relocate();
        }

        public override void Resize()
        {
            Vector2u windowSize = MainWindow.window.Size;

            float minScale = Math.Min((float)windowSize.X / 100 * Size / Sprite.Texture.Size.X, (float)windowSize.Y / 100 * Size / Sprite.Texture.Size.Y);

            Sprite.Scale = new Vector2f(minScale, minScale);

            if (Text != null && Sprite != null)
            {
                float maxLetterHeight = Text.Font.GetLineSpacing(Text.CharacterSize);

                float scaleY = Sprite.GetGlobalBounds().Height * 1f / maxLetterHeight;

                Text.Scale = new Vector2f(scaleY, scaleY);
                cursor.Scale = new Vector2f(scaleY, scaleY);
            }
        }

        public override void Relocate()
        {
            Vector2u windowSize = MainWindow.window.Size;
            View view = MainWindow.window.GetView();
            Vector2f viewCenter = view.Center;
            Vector2f viewSize = view.Size;

            // Использовал enum для обозначения положения элемента
            switch ((LocationEnum)Location)
            {
                case LocationEnum.TopLeft:
                    float coordX = (float)windowSize.X / 100 * CoordinateX;
                    float coordY = (float)windowSize.Y / 100 * CoordinateY;

                    Vector2f topLeft = viewCenter - viewSize / 2f;

                    Sprite.Position = new Vector2f(topLeft.X + coordX, topLeft.Y + coordY);
                    break;
                case LocationEnum.TopRight:
                    coordX = (float)windowSize.X / 100 * CoordinateX;
                    coordY = (float)windowSize.Y / 100 * CoordinateY;

                    Vector2f topRight = viewCenter + viewSize / 2f;

                    Sprite.Position = new Vector2f(topRight.X - coordX, topRight.Y - coordY);
                    break;
                case LocationEnum.Center:
                    coordX = (float)windowSize.X / 2 - Sprite.Texture.Size.X * Sprite.Scale.X / 2 + (float)windowSize.X / 100 * CoordinateX;
                    coordY = (float)windowSize.Y / 100 * CoordinateY;

                    topLeft = viewCenter - viewSize / 2f;

                    Sprite.Position = new Vector2f(topLeft.X + coordX, topLeft.Y + coordY);
                    break;
            }

            if (Text != null)
            {
                FloatRect textBounds = Text.GetGlobalBounds();

                float maxLetterHeight = Text.Font.GetLineSpacing(Text.CharacterSize);

                // Вычисляем смещение текста по X и Y, чтобы он был по центру спрайта по Y и с небольшим отступом от левой грани спрайта по X
                float offsetX = (maxLetterHeight * 0.9f - Sprite.GetGlobalBounds().Height);
                float offsetY = (Sprite.GetGlobalBounds().Height - maxLetterHeight * Text.Scale.Y) / 2;

                // Устанавливаем позицию текста относительно позиции спрайта
                Text.Position = new Vector2f(Sprite.Position.X + offsetX, Sprite.Position.Y + offsetY);
                cursor.Position = new Vector2f(Sprite.Position.X + offsetX + this.Text.GetGlobalBounds().Width + 5, Sprite.Position.Y + offsetY);
            }
        }

        public override void Draw()
        {
            base.Draw();
            if (this.cursorVisible && this.isFocused) 
            {
                MainWindow.window.Draw(cursor);
            }
        }

        static char KeyToChar(Keyboard.Key key)
        {
            // Проверяем, является ли key буквой от A до Z
            if (key >= Keyboard.Key.A && key <= Keyboard.Key.Z)
            {
                // Преобразуем Keyboard.Key в int и добавляем смещение ASCII для букв
                int ascii = (int)key + 65;
                // Преобразуем int в char и возвращаем его
                return (char)ascii;
            }
            // Проверяем, является ли key цифрой от 0 до 9
            else if (key >= Keyboard.Key.Num0 && key <= Keyboard.Key.Num9)
            {
                // Преобразуем Keyboard.Key в int и добавляем смещение ASCII для цифр
                int ascii = (int)key + 22;
                // Преобразуем int в char и возвращаем его
                return (char)ascii;
            }
            // В противном случае возвращаем символ '?'
            else
            {
                return '?';
            }
        }
    }
}
