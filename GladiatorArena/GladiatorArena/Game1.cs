using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;



namespace GladiatorArena
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        SpriteBatch tileBatch;

        Texture2D spr_Player;

        Dictionary<TileMap.tileType, Texture2D> tileTextures = new Dictionary<TileMap.tileType, Texture2D>();

        TileMap m_tilesMap;
        List<Entity> eList = new List<Entity>();

        double interval = 25;
        double elapsedTime = 0;


        Player m_player;
        MusicMan m_musicMan;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            m_musicMan = new MusicMan();
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
            tileBatch = new SpriteBatch(GraphicsDevice);

            //Load Sprites
            spr_Player = this.Content.Load<Texture2D>("PlayerSprite");

            //Load Tile Textures
            tileTextures.Add(TileMap.tileType.grass, this.Content.Load<Texture2D>("GrassTile"));
            tileTextures.Add(TileMap.tileType.sand, this.Content.Load<Texture2D>("SandTile"));
            tileTextures.Add(TileMap.tileType.tree, this.Content.Load<Texture2D>("TreeTile"));
            tileTextures.Add(TileMap.tileType.water, this.Content.Load<Texture2D>("WaterTile"));

            //Create level
            int[,] array = new int[,] { 
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 ,0 ,0 ,0 ,0 ,0 },
                { 0, 0, 0, 1, 0, 2, 0, 0, 0, 0, 0, 2 ,0 ,0 ,0 ,0 ,0 },
                { 0, 2, 0, 1, 1, 2, 0, 0, 0, 0, 0, 2 ,0 ,0 ,0 ,0 ,0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 ,0 ,0 ,0 ,0 ,0 },
                { 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 2 ,0 ,0 ,2 ,0 ,0 },
                { 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1 ,0 ,0 ,3 ,3 ,0 },
                { 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 2 ,0 ,0 ,3 ,0 ,0 },
                { 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 2 ,0 ,0 ,0 ,0 ,0 },
                { 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 2 ,0 ,0 ,0 ,0 ,0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2 ,0 ,0 ,0 ,0 ,0 },
                { 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 2 ,0 ,0 ,0 ,0 ,0 }
            };

            m_tilesMap = new TileMap(array, new Vector2(11, 17), new Vector2(64, 64), tileTextures);

            //Create Player
            m_player = new Player(spr_Player, m_tilesMap, new Vector2(1, 1), 10, 4);

            //Update ScreenSize
            graphics.PreferredBackBufferWidth = Convert.ToInt32(m_tilesMap.m_tileDims.X * m_tilesMap.m_mapSize.Y);
            graphics.PreferredBackBufferHeight = Convert.ToInt32(m_tilesMap.m_tileDims.Y * m_tilesMap.m_mapSize.X);
            graphics.ApplyChanges();
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

            m_player.Input(m_tilesMap);
            m_player.Update(m_tilesMap, gameTime);


            if (elapsedTime > interval)
            {
                elapsedTime -= interval;

               

                elapsedTime += gameTime.ElapsedGameTime.TotalMilliseconds;
            }
            elapsedTime++;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            tileBatch.Begin();
            m_tilesMap.RenderTileMap(tileBatch);
            tileBatch.End();

            // TODO: Add your drawing code here
            spriteBatch.Begin(); 
            m_player.Draw(spriteBatch);
            spriteBatch.End(); 

            base.Draw(gameTime);
        }
    }
}
