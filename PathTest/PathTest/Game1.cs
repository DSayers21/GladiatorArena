using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace PathTest
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        GameGrid gGrid = null;

        PathFinder pathFinder = null;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
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
            gGrid = new GameGrid(40, 40, 8, 8, graphics.GraphicsDevice);
            graphics.PreferredBackBufferWidth = gGrid.GetCols() * (int)gGrid.GetTileDims().X;  // set this value to the desired width of your window
            graphics.PreferredBackBufferHeight = gGrid.GetRows() * (int)gGrid.GetTileDims().Y;   // set this value to the desired height of your window
            graphics.ApplyChanges();

            base.Initialize();
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            pathFinder = new DFS(gGrid);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            gGrid.Update(GraphicsDevice);

            if (Keyboard.GetState().IsKeyDown(Keys.RightShift))
                pathFinder.Init();
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
                pathFinder.Start();
            if (Keyboard.GetState().IsKeyDown(Keys.Back))
                pathFinder.Stop();
            if (Keyboard.GetState().IsKeyDown(Keys.Delete))
                pathFinder.Reset();

            pathFinder.Update();

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            gGrid.Draw(spriteBatch);

            pathFinder.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}