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

using Microsoft.Xna.Framework.Input;

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
        Texture2D background, menuBackground, instructionBackground, userBackground, endBackground;
        Texture2D audioOn, audioOff;
        Rectangle mainFrame;

        // constant values - types of snowflakes
        const int NUM_SNOWFLAKES = 2;
        
        // sprites saved for efficiancy
        Texture2D snowflakeSprite;

        // game entitles
        List<Snowflakes> snowflakes = new List<Snowflakes>();

        // snowflake spawn support
        const float SNOWFLAKE_SPEED = 0.2F;
        const int TOTAL_SPAWN_MILLISECONDS = 3000;
        int elapsedSpawnMilliseconds = 0;

        // background music
        SoundEffectInstance backgroundMusic;
        // other sound effects
        SoundEffect shoot;

        // changing cursor look
        SpriteBatch cursorSprite;
        Texture2D cursorTex;
        Vector2 cursorPos;

        // Game object
        List<ChristmasStuff> christmasStuffs = new List<ChristmasStuff>();
        Texture2D christmasTexture;
        string baseSpriteName;
        float STUFF_SPEED = 0.3F;
        const int NUM_STUFF = 9;

        List<Student> students = new List<Student>();
        Texture2D studentTexture;
        string studentName;
        float STUDENT_SPEED = 1.0F;
        const int NUM_STUDENT = 4;
        int DISPLAY_STUDENT_NUM = 1;

        // field to keep track of game state
        static GameState state;

        // menu
        Menu mainMenu, instructionSite, userSite, endSite;

        // Font
        SpriteFont font, endFont;
        const string SCORE_PREFIX = "Score: ";
        const int DISPLAY_OFFSET = 35;
        public static readonly Vector2 SCORE_LOCATION = 
            new Vector2(DISPLAY_OFFSET, DISPLAY_OFFSET);
        int score = 0;
        const int STUDENT_SCORE = 20;
        const int GIFT_SCORE = 10;
        const int MAX_SCORE = 1000;

        // Visibility of shoot cursor
        bool visibilityOfMouse = true;

        // Active sound variable
        bool soundOn = true;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;

            // mouse visibility
            IsMouseVisible = false;
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

            // initialize menu object
            mainMenu = new Menu(Content, WINDOW_WIDTH, WINDOW_HEIGHT);
            instructionSite = new Menu(Content, WINDOW_WIDTH, WINDOW_HEIGHT);
            //userSite = new Menu(Content, WINDOW_WIDTH, WINDOW_HEIGHT);
            endSite = new Menu(Content, WINDOW_WIDTH, WINDOW_HEIGHT);

            // load and start playing background music
            SoundEffect backgroundMusicEffect = Content.Load<SoundEffect>("backgroundMusic");
            backgroundMusic = backgroundMusicEffect.CreateInstance();
            backgroundMusic.IsLooped = true;

            //if (soundOn == true)
            //{
            //    backgroundMusic.Play();
            //}
            //else
            //{
            //    backgroundMusic.Dispose();
            //}

            // load other sound
            shoot = Content.Load<SoundEffect>("shotgun");

            // Load the background content
            background = Content.Load<Texture2D>("background0");
            menuBackground = Content.Load<Texture2D>("background1");
            instructionBackground = Content.Load<Texture2D>("background3");
            userBackground = Content.Load<Texture2D>("background4");
            endBackground = Content.Load<Texture2D>("background5");

            audioOn = Content.Load<Texture2D>("audioOn");
            audioOff = Content.Load<Texture2D>("audioOff");

            mainFrame = new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);

            // Create a snowflake object
            int randomTexture = rand.Next(NUM_SNOWFLAKES);
            string assetName = "snowflake" + randomTexture;
            snowflakeSprite = Content.Load<Texture2D>(assetName);  

            // Load the cursor
            cursorSprite = new SpriteBatch(GraphicsDevice);
            cursorTex = Content.Load<Texture2D>("gunSight");

            // Load the christmas stuff
            baseSpriteName = "stuff" + rand.Next(NUM_STUFF);
            christmasTexture = Content.Load<Texture2D>(baseSpriteName);

            christmasStuffs.Add(new ChristmasStuff(Content, baseSpriteName, rand.Next(0, WINDOW_WIDTH),
                rand.Next(0, WINDOW_HEIGHT), WINDOW_WIDTH, WINDOW_HEIGHT));

            // Load students
            studentName = "lady" + rand.Next(NUM_STUDENT);
            studentTexture = Content.Load<Texture2D>(studentName);

            students.Add(new Student(Content, studentName, rand.Next(0, WINDOW_WIDTH),
                rand.Next(0, WINDOW_HEIGHT), WINDOW_WIDTH, WINDOW_HEIGHT)); 

            // Load sprite font
            font = Content.Load<SpriteFont>("Arial");
            endFont = Content.Load<SpriteFont>("Copperplate");
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

            // Sound
            MouseState currentMouse = Mouse.GetState();

            // let's add some action
            if ((currentMouse.X > (720 - audioOn.Width)) && (currentMouse.X < (720 + audioOn.Width)) &&
                (currentMouse.Y > (10 - audioOn.Height)) && (currentMouse.Y < (10 + audioOn.Height)) &&
                currentMouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                soundOn = !soundOn;
            }
            

            // From instruction site to game
            KeyboardState kbState = Keyboard.GetState();
            if ((state == GameState.Instruction) &&
                (kbState.IsKeyDown(Keys.Enter)))
            {
                state = GameState.Play;
                //kbState.GetPressedKeys()
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                Exit();
            }
            
            if (state == GameState.MainMenu)
            {
                // Update main menu
                mainMenu.Update(Mouse.GetState());
                // Cursor update
                cursorPos = new Vector2(Mouse.GetState().X - cursorTex.Width / 2, Mouse.GetState().Y - cursorTex.Height / 2);
            }
            else if (state == GameState.Instruction)
            {
                // Update menu
                instructionSite.Update(Mouse.GetState());
                // mainMenu.Update(Mouse.GetState());
            }
            else if (state == GameState.User)
            {
                if (kbState.IsKeyDown(Keys.Enter))
                {
                    state = GameState.Play;
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    Exit();
                }
            }
            else if (state == GameState.Play)
            {
                // Cursor update
                cursorPos = new Vector2(Mouse.GetState().X - cursorTex.Width / 2, Mouse.GetState().Y - cursorTex.Height / 2);

                // Updating the snowflake
                foreach (Snowflakes snowflake in snowflakes)
                {
                    snowflake.Update();
                }

                // Christmas stuff update
                foreach (ChristmasStuff stuff in christmasStuffs)
                {
                    stuff.Update();
                }

                // Students update
                foreach (Student student in students)
                {
                    student.Update();
                }

                // spawn snowflake as appopriate
                elapsedSpawnMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
                if (elapsedSpawnMilliseconds >= TOTAL_SPAWN_MILLISECONDS)
                {
                    elapsedSpawnMilliseconds = 0;

                    // Added a new constructor for providing sprite and velocity
                    snowflakes.Add(new Snowflakes(snowflakeSprite, rand.Next(WINDOW_WIDTH - snowflakeSprite.Width + 1),
                        -snowflakeSprite.Height / 2,
                        new Vector2(0, SNOWFLAKE_SPEED),
                        WINDOW_WIDTH,
                        WINDOW_HEIGHT));
                }

                // spawn christmas items
                elapsedSpawnMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
                if (elapsedSpawnMilliseconds >= TOTAL_SPAWN_MILLISECONDS)
                {
                    elapsedSpawnMilliseconds = 0;

                    // Added a new constructor for christmas stuff
                    christmasStuffs.Add(new ChristmasStuff(Content.Load<Texture2D>("stuff" + rand.Next(NUM_STUFF)), rand.Next(WINDOW_WIDTH - christmasTexture.Width),
                        -christmasTexture.Height / 2,
                        new Vector2(0, STUFF_SPEED),
                        WINDOW_WIDTH, WINDOW_HEIGHT));
                }

                // spawn students
                elapsedSpawnMilliseconds += gameTime.ElapsedGameTime.Milliseconds;
                if (elapsedSpawnMilliseconds >= TOTAL_SPAWN_MILLISECONDS)
                {
                    elapsedSpawnMilliseconds = 0;

                    for (int i = 0; i < DISPLAY_STUDENT_NUM; i++)
                    {
                        students.Add(new Student(Content.Load<Texture2D>("lady" + rand.Next(NUM_STUDENT)), rand.Next(0, WINDOW_WIDTH - studentTexture.Width),
                        rand.Next(0, WINDOW_HEIGHT - studentTexture.Height / 2),
                        new Vector2(0, STUDENT_SPEED),
                        WINDOW_WIDTH,
                        WINDOW_HEIGHT));
                    }  
                }

                // check for snowflakes leaving window
                foreach (Snowflakes snowflake in snowflakes)
                {
                    if (snowflake.CollisionRectangle.Top > WINDOW_HEIGHT)
                    {
                        snowflake.Active = false;
                    }
                }

                // check for stuff leaving window
                foreach (ChristmasStuff stuff in christmasStuffs)
                {
                    if (stuff.CollisionRectangle.Top > WINDOW_HEIGHT)
                    {
                        stuff.Active = false;
                    }
                }

                // check for student leaving window
                foreach (Student student in students)
                {
                    if (student.CollisionRectangle.Top > WINDOW_HEIGHT)
                    {
                        student.Active = false;
                    }
                }

                // check if the student is tracked, than shoot
                MouseState currentMouseState = Mouse.GetState();

                // kod czy myszka znajduje siê na studentce, jeœli tak 
                // sprawdzamy czy jest wciœniêty lewy klawisz myszki
                // studentka umiera - mo¿na zostawiæ œlad po kuli, sygna³ dŸwiêkowy i znika z ekranu
                foreach (Student student in students)
                {
                    if (student.ShootDown(gameTime, currentMouseState))
                    {
                        student.Active = false;
                        if (soundOn == true)
                        {
                            shoot.Play();
                        }
                        score += STUDENT_SCORE;
                       
                    }
                }

                foreach (ChristmasStuff stuff in christmasStuffs)
                {
                    if (stuff.ShootDown(gameTime, currentMouseState))
                    {
                        stuff.Active = false;
                        score -= GIFT_SCORE;
                    }
                }

                int full_value = score/100;
                // Let's make it harder
                if (full_value > 0) 
                {
                    int old_display_num = DISPLAY_STUDENT_NUM;
                    DISPLAY_STUDENT_NUM = full_value + 1;

                    if (old_display_num != DISPLAY_STUDENT_NUM)
                    {
                        STUDENT_SPEED += 1.0f;
                    }
                    
                }

                // finish the game if your score is about 1000
                if (score >= MAX_SCORE)
                {
                    state = GameState.EndGame;
                }

                // extra option
                if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    state = GameState.EndGame;
                }
            }
            else if (state == GameState.EndGame)
            {
                visibilityOfMouse = false;
                endSite.Update(Mouse.GetState());
            }
            else
            {
                this.Exit();
            }

            // clean out inactive students and christmas items
            students.RemoveAll(item => item.Active == false);
            christmasStuffs.RemoveAll(item => item.Active == false);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.MidnightBlue);

            // Drawing background and snowflakes
            spriteBatch.Begin();

            if (state == GameState.MainMenu)
            {
                // draw the menu
                spriteBatch.Draw(menuBackground, mainFrame, Color.White);
                mainMenu.Draw(spriteBatch);

            }
            else if (state == GameState.Instruction)
            {
                spriteBatch.Draw(instructionBackground, mainFrame, Color.White);
            }
            //else if (state == GameState.User)
            //{
            //    // write user name
            //    spriteBatch.Draw(userBackground, mainFrame, Color.White);
                
            //}
            else if (state == GameState.Play)
            {
                spriteBatch.Draw(background, mainFrame, Color.White);

                // draw score
                spriteBatch.DrawString(font, SCORE_PREFIX + score, SCORE_LOCATION, Color.White);
                //spriteBatch.DrawString(font, SCORE_PREFIX + DISPLAY_STUDENT_NUM, new Vector2(DISPLAY_OFFSET, DISPLAY_OFFSET + 30), Color.White);


                // -------------------------------------- //
                foreach (Snowflakes snowflake in snowflakes)
                {
                    snowflake.Draw(spriteBatch);
                }

                foreach (ChristmasStuff stuff in christmasStuffs)
                {
                    stuff.Draw(spriteBatch);
                }

                foreach (Student student in students)
                {
                    student.Draw(spriteBatch);
                }
            }
            else if (state == GameState.EndGame)
            {
                spriteBatch.Draw(endBackground, mainFrame, Color.White);
                spriteBatch.DrawString(endFont, "The End", SCORE_LOCATION, Color.White);

                string output = "Your score is ";
                // find the center of the string
                Vector2 FontOrgin = endFont.MeasureString(output);
                spriteBatch.DrawString(endFont, output + score, new Vector2(600,500), Color.White, 0.0f, FontOrgin, 0.5f, SpriteEffects.None, 0.0f);
                spriteBatch.DrawString(endFont, "Press <ESC> to close the window", new Vector2(200, 600), Color.White, 0.0f, FontOrgin, 0.25f, SpriteEffects.None, 0.0f);
            }

            Sound();

            spriteBatch.End();

            // Drawing cursor
            if (state != GameState.Instruction)
            {
                cursorSprite.Begin();
                if (visibilityOfMouse == true)
                {
                    cursorSprite.Draw(cursorTex, cursorPos, Color.White);
                }
                cursorSprite.End();
            }

            base.Draw(gameTime);
        }

        private Rectangle GetRandomDrawRectangle(Texture2D sprite)
        {
            return new Rectangle(rand.Next(0, WINDOW_WIDTH), 0, sprite.Width, sprite.Height);
        }

        /// <summary>
        /// Changes the state of the game
        /// </summary>
        /// <param name="newState">the new game state</param>
        public static void ChangeState(GameState newState)
        {
            state = newState;
        }

        public void Sound()
        {
            // picture of sound
            if ((state == GameState.MainMenu) || (state == GameState.Instruction) || (state == GameState.Play))
            {
                if (soundOn == true)
                {
                    spriteBatch.Draw(audioOn, new Vector2(720, 10), Color.White);
                    backgroundMusic.Play();
                    
                }
                else
                {
                    spriteBatch.Draw(audioOff, new Vector2(720, 10), Color.White);
                    backgroundMusic.Pause();
                }
            }

            
        }
    }
}
