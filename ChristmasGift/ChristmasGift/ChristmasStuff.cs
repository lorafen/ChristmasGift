using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Sight
{
    class ChristmasStuff
    {
        #region Fields

        // graphic and drawing info
        Texture2D sprite;
        Rectangle drawRectangle = new Rectangle();

        // velocity information
        Vector2 velocity = new Vector2(0, 0);

        // whether or not the Christmas stuff is active
        bool active = true;

        // bouncing support
        int windowWidth;
        int windowHeight;

        #endregion

        #region Constructors

        /// <summary>
        ///  Constructs a Christmas stuff with random velocity
        /// </summary>
        /// <param name="contentManager">the content manager</param>
        /// <param name="spriteName">the name of the sprite for the Christmas stuff</param>
        /// <param name="x">the x location of the center of the Christmas stuff</param>
        /// <param name="y">the y location of the center of the Christmas stuff</param>
        /// <param name="windowWidth">the window width</param>
        /// <param name="windowHeight">the window height</param>
        public ChristmasStuff(ContentManager contentManager, string spriteName, int x, int y,
            int windowWidth, int windowHeight)
        {
            this.windowWidth = windowWidth;
            this.windowHeight = windowHeight;

            LoadContent(contentManager, spriteName, x, y);

            // generate random angle and x and y direction components
            Random rand = new Random();
            
            double angle = 2 * Math.PI * rand.NextDouble();
            double xDirection = Math.Cos(angle);
            double yDirection = -1 * Math.Sin(angle);
            
            double speed = rand.NextDouble();

            // set random velocity
            velocity.X = (float)(speed * xDirection);
            velocity.Y = (float)(speed * yDirection);
        }

        /// <summary>
        /// Constructs a snowflake with the given characteristic
        /// </summary>
        /// <param name="sprite">the sprite for the stuff</param>
        /// <param name="x">the x location of the center of the stuff</param>
        /// <param name="y">the x location of the center of the stuff</param>
        /// <param name="velocity">the velocity vector for the stuff</param>
        /// <param name="windowWidth">the window width</param>
        /// <param name="windowHeigth">the window height</param>
        public ChristmasStuff(Texture2D sprite, int x, int y, 
            Vector2 velocity, int windowWidth, int windowHeigth)
        {
            this.sprite = sprite;
            InitializeDrawRectangle(sprite, x, y);
            this.velocity = velocity;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the collision rectangle for the Christmas stuff
        /// </summary>
        public Rectangle CollisionRectangle
        {
            get { return drawRectangle; }
        }

        /// <summary>
        /// Gets and sets whether or not the Christmas stuff is active
        /// </summary>
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Updates the Christmas stuff's location, bouncing if necessary
        /// </summary>
        public void Update()
        {
            if (active)
            {
                // move the Christmas stuff
                //drawRectangle.X += (int)(velocity.X);
                drawRectangle.Y += (int)(velocity.Y) + 1;

                // bounce as necessary
                //BounceTopBottom();
                //BounceLeftRight();
            }
        }

        /// <summary>
        /// Draws the Christmas stuff
        /// </summary>
        /// <param name="spriteBatch">the sprite batch to use</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            if (active)
            {
                spriteBatch.Draw(sprite, drawRectangle, Color.White);
            }
        }

        /// <summary>
        /// Bounces the Christmas stuff by reversing the x and y velocities
        /// </summary>
        public void Bounce()
        {
            velocity.X *= -1;
            velocity.Y *= -1;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Loads the content for the Christmas stuff
        /// </summary>
        /// <param name="contentManager">the content manager to use</param>
        /// <param name="spriteName">the name of the sprite for the Christmas stuff</param>
        /// <param name="x">the x location of the center of the Christmas stuff</param>
        /// <param name="y">the y location of the center of the Christmas stuff</param>
        private void LoadContent(ContentManager contentManager, string spriteName,
            int x, int y)
        {
            // load content and set draw rectangle
            sprite = contentManager.Load<Texture2D>(spriteName);
            drawRectangle = new Rectangle(x - sprite.Width / 2,
                y - sprite.Height / 2, sprite.Width,
                sprite.Height);
        }

        /// <summary>
        /// Bounces the Christmas stuff off the top and bottom window borders if necessary
        /// </summary>
        private void BounceTopBottom()
        {
            if (drawRectangle.Top < 0)
            {
                // bounce off top
                drawRectangle.Y = 0;
                velocity.Y *= -1;
            }
            else if (drawRectangle.Bottom > windowHeight)
            {
                // bounce off bottom
                drawRectangle.Y = windowHeight - drawRectangle.Height;
                velocity.Y *= -1;
            }
        }

        /// <summary>
        /// Bounces the Christmas stuff off the left and right window borders if necessary
        /// </summary>
        private void BounceLeftRight()
        {
            if (drawRectangle.Left < 0)
            {
                // bounc off left
                drawRectangle.X = 0;
                velocity.X *= -1;
            }
            else if (drawRectangle.Right > windowWidth)
            {
                // bounce off right
                drawRectangle.X = windowWidth - drawRectangle.Width;
                velocity.X *= -1;
            }
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
