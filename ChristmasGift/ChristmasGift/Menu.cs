using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ChristmasGift
{
    class Menu
    {
        #region Fields

        // fields for buttons
        MenuButton instructionButton;
        MenuButton playButton;
        MenuButton quitButton;

        #endregion

        #region Constructor

        public Menu(ContentManager contentManager, int windowWidth, int windowHeight)
        {
            // Button placement
            int centerX = windowWidth / 2;
            int topCenterY = windowHeight / 6;
            Vector2 buttonCenter = new Vector2(centerX, topCenterY);

            // Create buttons
            instructionButton = new MenuButton(contentManager, contentManager.Load<Texture2D>("instructionButton"),
                buttonCenter, GameState.MainMenu);
            buttonCenter.Y += windowHeight / 3;
            
            playButton = new MenuButton(contentManager, contentManager.Load<Texture2D>("playbutton"),
                buttonCenter, GameState.Play);
            buttonCenter.Y += windowHeight / 3;
            
            quitButton = new MenuButton(contentManager, contentManager.Load<Texture2D>("quitbutton"),
                buttonCenter, GameState.Quit);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Updates the menu
        /// </summary>
        /// <param name="mouse">the current mouse state</param>
        /// <param name="soundbank">the sound bank for sound effects</param>
        public void Update(MouseState mouse)
        {
            // update buttons
            instructionButton.Update(mouse);
            playButton.Update(mouse);
            quitButton.Update(mouse);
        }

        /// <summary>
        /// Draws the menu
        /// </summary>
        /// <param name="spriteBatch">the spritebatch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            // draw buttons
            instructionButton.Draw(spriteBatch);
            playButton.Draw(spriteBatch);
            quitButton.Draw(spriteBatch);
        }

        #endregion
    }
}
