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

        Random rand = new Random();

        // WINDOW SIZE
        const int WINDOW_WIDTH = 800;
        const int WINDOW_HEIGHT = 600;

        // background texture
        Texture2D background;
        Rectangle mainFrame;

        // constant values - types of snowflakes
        const int NUM_SNOWFLAKES = 2;
        
        // sprites saved for efficiancy
        Texture2D snowflakeSprite;

        // game entitles
        List<Snowflakes> snowflakes = new List<Snowflakes>();

        // snowflake spawn support
        const float SNOWFLAKE_SPEED = 0.2F;
        const int TOTAL_SPAWN_MILLISECONDS = 1000;
        int elapsedSpawnMilliseconds = 0;

        // background music
        SoundEffectInstance backgroundMusic;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;

            // mouse visibility
            IsMouseVisible = true;
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

            // load and start playing background music
            SoundEffect backgroundMusicEffect = Content.Load<SoundEffect>("backgroundMusic");
            backgroundMusic = backgroundMusicEffect.CreateInstance();
            backgroundMusic.IsLooped = true;
            backgroundMusic.Play();

            // Load the background content
            background = Content.Load<Texture2D>("background0");
            mainFrame = new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);

            // Create a snowflake object
            int randomTexture = rand.Next(NUM_SNOWFLAKES);
            string assetName = "snowflake" + randomTexture;
            snowflakeSprite = Content.Load<Texture2D>("snowflake2");  
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
            foreach (Snowflakes snowflake in snowflakes)
            {
                snowflake.Update();
            }


            // spawn snowflake as appopriate
            elapsedSpawnMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
            if (elapsedSpawnMilliseconds >= TOTAL_SPAWN_MILLISECONDS)
            {
                elapsedSpawnMilliseconds = 0;

                // Added a new constructor for providing sprote and velocity
                snowflakes.Add(new Snowflakes(snowflakeSprite, rand.Next(WINDOW_WIDTH  - snowflakeSprite.Width + 1),
                    -snowflakeSprite.Height / 2,
                    new Vector2(0, SNOWFLAKE_SPEED),
                    WINDOW_WIDTH,
                    WINDOW_HEIGHT));
            }

            // check for snowflakes leaving window
            foreach (Snowflakes snowflake in snowflakes)
            {
                if (snowflake.CollisionRectangle.Top > WINDOW_HEIGHT)
                {
                    snowflake.Active = false;
                } 
            }

            
           

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            // Drawing snowflakes
            spriteBatch.Begin();

            spriteBatch.Draw(background, mainFrame, Color.White);

            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            foreach (Snowflakes snowflake in snowflakes)
            {
                snowflake.Draw(spriteBatch);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }

        private Rectangle GetRandomDrawRectangle(Texture2D sprite)
        {
            return new Rectangle(rand.Next(0, WINDOW_WIDTH), 0, sprite.Width, sprite.Height);
        }
    }
}
