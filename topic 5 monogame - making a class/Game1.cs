using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace topic_5_monogame___making_a_class
{
    

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        
        List<Texture2D> ghostTextures;

        Texture2D titleBackgroundTexture, endBackgroundTexture, mainBackgroundTexture, background;

      MouseState mouseState;

        Ghost ghost1;

        enum Screen
        {
            title,
            house,
            end
        }

        Screen playScreen;

        Screen screen;
        Rectangle window;

        KeyboardState keyboardState;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = 800;
            _graphics.PreferredBackBufferHeight = 600;
            

           screen = Screen.title;
            IsMouseVisible = false;

            base.Initialize();
            ghost1 = new Ghost(ghostTextures, new Rectangle(150, 250, 40, 40));
            window = new Rectangle(0, 0, 800, 500);
            
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            ghostTextures = new List<Texture2D>();

            

            for (int i = 1; i < 9; i++)
            {
                ghostTextures.Add(Content.Load<Texture2D>("ghostTextures/boo-move-" + i));
            }

            titleBackgroundTexture = Content.Load<Texture2D>("backgrounds/haunted-title");
            mainBackgroundTexture = Content.Load<Texture2D>("backgrounds/haunted-background");
            endBackgroundTexture = Content.Load<Texture2D>("backgrounds/haunted-end-screen");

        }

        protected override void Update(GameTime gameTime)
        {
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
           

            // TODO: Add your update logic here
            if (screen == Screen.title)
            {
                if (keyboardState.IsKeyDown(Keys.Enter))
                {
                    screen = Screen.house;
                }
                else if (screen == Screen.house)
                {
                    ghost1.Update(gameTime, mouseState);
                    if (ghost1.Contains(mouseState.Position))
                    {
                        screen = Screen.end;
                    }
                }
            }


            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            if (screen == Screen.title)
                _spriteBatch.Draw(titleBackgroundTexture, window, Color.White);
            else if (screen == Screen.house)
            {
                _spriteBatch.Draw(mainBackgroundTexture, window, Color.White);
            }
            else
                _spriteBatch.Draw(endBackgroundTexture, window, Color.White);
            


            ghost1.draw(_spriteBatch);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
