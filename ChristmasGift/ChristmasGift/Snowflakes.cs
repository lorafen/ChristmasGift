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

        // velocity information
        Vector2 velocity = new Vector2(0, 0);

        #endregion

        #region Constructors

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
            // Generate random velocity in Y axis
            Random rand = new Random();
            velocity.Y = (float)rand.NextDouble(); 
            
        }

        /// <summary>
        /// Constructs a snowflake with the given characteristics
        /// </summary>
        /// <param name="contentManager">the content manager for loading content</param>
        /// <param name="spriteName">the name of the sprite for the snowflake</param>
        /// <param name="x">the x location of the center of the snowflake</param>
        /// <param name="y">the y location of the center of the snowflake</param>
        /// <param name="velocity">the velocity vector for the snowflake</param>
        /// <param name="windowWidth">the window width</param>
        /// <param name="windowHeigth">the window height</param>
        public Snowflakes(ContentManager contentManager, string spriteName, int x, int y, 
            Vector2 velocity, int windowWidth, int windowHeigth)
        {
            // Load the content
            LoadContent(contentManager, spriteName, x, y);
            // Set the velocity
            this.velocity = velocity;
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
            spriteBatch.Draw(sprite, drawRectangle, Color.CornflowerBlue);
        }
        /// <summary>
        /// Activity of snowflake - is it visible or not
        /// </summary>
        public bool Active
        {
            get;
            set;
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
            drawRectangle = new Rectangle(x - sprite.Width / 2,
                y - sprite.Height / 2, 
                sprite.Width, 
                sprite.Height);
        }

        #endregion

    }
}
