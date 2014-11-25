using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ChristmasGift
{
    class Sight
    {
        #region Fields

        // Window dimension
        int WINDOW_WIDTH;
        int WINDOW_HEIGHT;

        // Graphics and drawning info
        Texture2D sprite;
        Rectangle drawRectangle = new Rectangle();
        Rectangle sourceRectangle;

        const int IMAGES_PER_ROW = 2;
        int frameWidth;

        // Stuff movement
        const int STUFF_MOVE_AMOUNT = 5;

        // wheter or not the stuff is active
        bool active = true;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructs a fish with the given characteristics
        /// </summary>
        /// <param name="contentManager">the content manager</param>
        /// <param name="spriteName">the name of the sprite for the fish</param>
        /// <param name="x">the x location of the center of the fish</param>
        /// <param name="y">the y location of the center of the fish</param>
        /// <param name="windowWidth">the window width</param>
        /// <param name="windowHeight">the window height</param>
        public Sight(ContentManager contentManager, string spriteName, int x, int y,
            int windowWidth, int windowHeight)
        {
            // set window dimensions
            this.WINDOW_WIDTH = windowWidth;
            this.WINDOW_HEIGHT = windowHeight;

            // set temporary draw rectangle location and load content
            drawRectangle.X = x;
            drawRectangle.Y = y;

            // load content
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the collision rectangle for the fish
        /// </summary>
        public Rectangle CollisionRectangle
        {
            get { return drawRectangle; }
        }

        /// <summary>
        /// Gets and sets whether or not the fish is active
        /// </summary>
        public bool Active
        {
            get { return this.active; }
            set { this.active = value; }
        }

        #endregion

        #region Private properties

        /// <summary>
        /// Gets and sets the x location of the center of the fish
        /// </summary>
        private int X
        {
            get { return drawRectangle.X + drawRectangle.Width / 2; }
            set
            {
                drawRectangle.X = value - drawRectangle.Width / 2;

                // clamp to keep in range
                if (drawRectangle.Left < 0)
                {
                    drawRectangle.X = 0;
                }
                else if (drawRectangle.Right > WINDOW_WIDTH)
                {
                    drawRectangle.X = WINDOW_WIDTH - drawRectangle.Width;
                }
            }
        }

        /// <summary>
        /// Gets and sets the y location of the center of the fish
        /// </summary>
        private int Y
        {
            get { return drawRectangle.Y + drawRectangle.Height / 2; }
            set
            {
                drawRectangle.Y = value - drawRectangle.Height / 2;

                // clamp to keep in range
                if (drawRectangle.Top < 0)
                {
                    drawRectangle.Y = 0;
                }
                else if (drawRectangle.Bottom > WINDOW_HEIGHT)
                {
                    drawRectangle.Y = WINDOW_HEIGHT - drawRectangle.Height;
                }
            }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Updates the stuff's location, stopping at edges if necessary
        /// </summary>
        /// <param name="keyboard">current keyboard state</param>
        public void Update(KeyboardState keyboard)
        {
            // move the stuff based on the keyboard state
            if (keyboard.IsKeyDown(Keys.Right))
            {
                X += STUFF_MOVE_AMOUNT;

                // set source rectangle for right image
                sourceRectangle.X = 0;

            }
        }

        #endregion
    }
}
