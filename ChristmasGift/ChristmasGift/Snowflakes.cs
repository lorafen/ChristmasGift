using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace ChristmasGift
{
    /// <summary>
    /// A class for a snowflake
    /// </summary>
    class Snowflakes
    {
        #region Fields

        // drawning support
        Texture2D sprite;
        Rectangle drawRectangle;
        Random rand = new Random();

        // velocity information
        Vector2 velocity = new Vector2(0, 0);

        #endregion

        #region Constructors

        /// <summary>
        /// Constructors a snowflake with random direction and speed
        /// </summary>
        /// <param name="sprite">texture of snowflake</param>
        /// <param name="x">the x location of the center of the snowflake</param>
        /// <param name="y">the y location of the center of the snowflake</param>
        /// <param name="windowWidth">the window width</param>
        /// <param name="windowHeigth">thw window height</param>
        public Snowflakes(Texture2D sprite, int x, int y, int windowWidth, int windowHeigth)
        {
            this.sprite = sprite;
            InitializeDrawRectangle(sprite, x, y);

            // generate random velocity
            float speed = (float)(rand.Next(5) + 3) / 16;
            double angle = 2 * Math.PI * rand.NextDouble();

            velocity.X = (float)Math.Cos(angle) * speed;
            velocity.Y = -1 * (float)Math.Sin(angle) * speed;
        }
        
        /// <summary>
        /// Constructs a snowflake with random speed
        /// </summary>
        /// <param name="contentManager">the content manager for loading content</param>
        /// <param name="spriteName">the name of the sprite for the snowflake</param>
        /// <param name="x">the x location of the center of the snowflake</param>
        /// <param name="y"> the y location of the center of the snowflake</param>
        /// <param name="windowWidth">the window width</param>
        /// <param name="windowHeight">the window height</param>
        public Snowflakes(ContentManager contentManager, string spriteName, int x, int y, 
            int windowWidth, int windowHeight)
        {
            // Load the content
            LoadContent(contentManager,spriteName, x, y);
            
            // Generate random velocity
            int speed = rand.Next(5) + 3;
            double angle = 2 * Math.PI * rand.NextDouble();
            velocity.X = (float)Math.Cos(angle) * speed;
            velocity.Y = -1 * (float)Math.Sin(angle) * speed;
            
        }

        /// <summary>
        /// Constructs a snowflake with the given characteristic
        /// </summary>
        /// <param name="sprite">the sprite for the snowflake</param>
        /// <param name="x">the x location of the center of the snowflake</param>
        /// <param name="y">the y location of the center of the snowflake</param>
        /// <param name="velocity">the velocity vector for the snowflake</param>
        /// <param name="windowWidth">the window width</param>
        /// <param name="windowHeigth">the window height</param>
        public Snowflakes(Texture2D sprite, int x, int y, 
            Vector2 velocity, int windowWidth, int windowHeigth)
        {
            this.sprite = sprite;
            InitializeDrawRectangle(sprite, x, y);
            this.velocity = velocity;
        }

        /// <summary>
        /// Constructs a snowflake with the given characteristic
        /// </summary>
        /// <param name="contentManager">the content manager for loading content</param>
        /// <param name="spriteName">the name od the sprite for the snowflake</param>
        /// <param name="x">the x location of the center of the snowflake</param>
        /// <param name="y">the y location of the center of the snowflake</param>
        /// <param name="velocity">the velocity of the snowflake</param>
        /// <param name="windowWidth">the window width</param>
        /// <param name="windowHeight">thw window height</param>
        public Snowflakes(ContentManager contentManager, string spriteName, int x, int y,
            Vector2 velocity, int windowWidth, int windowHeight)
        {
            LoadContent(contentManager, spriteName, x, y);
            this.velocity = velocity;

        }
        #endregion

        #region Properties

        /// <summary>
        /// Activity of snowflake - is it visible or not
        /// </summary>
        public bool Active
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the collision rectangle for the teddy bear
        /// </summary>
        public Rectangle CollisionRectangle
        {
            get { return drawRectangle; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Updates the snowflake's location
        /// </summary>
        public void Update()
        {
            // Moving the snowflake in Y axis
            drawRectangle.Y += (int)velocity.Y + 1;
        }

        /// <summary>
        /// Draws the snowflake
        /// </summary>
        /// <param name="spriteBatch">the sprite batch to use</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, drawRectangle, Color.White);
        }
        

        #endregion

        #region Private methods

        /// <summary>
        /// Loads the content for the snowflake
        /// </summary>
        /// <param name="contentManager">the content manager to use</param>
        /// <param name="spriteName">the name of the sprite for the snowflake</param>
        /// <param name="x">the x location of the center of the snowflake</param>
        /// <param name="y">the y location of the center of the snowflake</param>
        private void LoadContent(ContentManager contentManager, string spriteName, int x, int y)
        {
            // Load content and set reminder of draw rectangle
            sprite = contentManager.Load<Texture2D>(spriteName);
            InitializeDrawRectangle(sprite, x, y);
        }

        /// <summary>
        /// Initializes the draw rectangle
        /// </summary>
        /// <param name="sprite">the sprite for the snowflakes</param>
        /// <param name="x">the x location of the center of the snowflakes</param>
        /// <param name="y">the y location of the center of the snowflakes</param>
        private void InitializeDrawRectangle(Texture2D sprite, int x, int y)
        {
            drawRectangle = new Rectangle(x - sprite.Width / 2,
                y - sprite.Height / 2,
                sprite.Width,
                sprite.Height);
        }

        #endregion

    }
}
