using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ChristmasGift
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // WINDOW SIZE
        const int WINDOW_WIDTH = 800;
        const int WINDOW_HEIGHT = 600;

        // random texture support
        const int NUM_TEXTURES = 2;
        Random rand = new Random();
        List<Texture2D> textures = new List<Texture2D>();

        // drawing support
        const int MAX_TEXTURES = 100;
        List<Texture2D> drawTextures = new List<Texture2D>();
        List<Rectangle> drawRectangles = new List<Rectangle>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Create a snowflake object
            // load each  of snowflake to snowflake textures and add them to the textures list
            for (int i = 0; i < NUM_TEXTURES; i++)
            {
                string nameOfPicture = "snowflake" + i;
                textures.Add(Content.Load<Texture2D>(nameOfPicture));
            }

            // add one of each of the textures to the drawTextures list, 
            // adding a corresponding random draw rectangle to the drawRectangles list 
            // by calling the GetRandomDrawRectangle method
            for (int i = 0; i < textures.Count; i++)
            {
                drawTextures.Add(textures[i]);
                drawRectangles.Add(GetRandomDrawRectangle(drawTextures[i]));
            }

            
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // Updating the snowflake
            //snowflake.Update();



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Drawing snowflakes
            spriteBatch.Begin();

            //snowflake.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private Rectangle GetRandomDrawRectangle(Texture2D sprite)
        {
            return new Rectangle(rand.Next(0, WINDOW_WIDTH), rand.Next(0, WINDOW_HEIGHT), sprite.Width, sprite.Height);
        }
    }
}
