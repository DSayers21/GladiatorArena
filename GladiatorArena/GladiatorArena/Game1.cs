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
        Texture2D spr_Duck;
        Texture2D spr_Player;
        Texture2D tile_Grass;
        Texture2D tile_Sand;
        Texture2D tile_Tree;
        Texture2D tile_Water;
        Vector2 scr_dimensions;
        TileMap tilesMappo;
        List<Entity> eList = new List<Entity>();

        Vector2 gameArea = new Vector2(11, 17);
        Vector2 tileDims = new Vector2(64, 64);

        double interval = 33;
        double elapsedTime = 0;


        Player m_player;

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
            base.Initialize();

            graphics.PreferredBackBufferWidth = Convert.ToInt32(tileDims.X * gameArea.Y);
            graphics.PreferredBackBufferHeight = Convert.ToInt32(tileDims.Y * gameArea.X);
            graphics.ApplyChanges();
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
            //load your game content here
            spr_Player = this.Content.Load<Texture2D>("PlayerSprite");


            tile_Grass = this.Content.Load<Texture2D>("GrassTile");
            tile_Sand = this.Content.Load<Texture2D>("SandTile");
            tile_Tree = this.Content.Load<Texture2D>("TreeTile");
            tile_Water = this.Content.Load<Texture2D>("WaterTile");

            //Build level data
            m_player = new Player(spr_Player);

            List<Tile> map = new List<Tile>();

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

            for (int i = 0; i < array.GetLength(1); i++)
                for (int j = 0; j < array.GetLength(0); j++)
                {
                    Tile test = new Tile();
                    if (array[j, i] == 0)
                        test = new Tile(new Entity(tile_Grass, new Vector2(i * tileDims.X, j * tileDims.Y)), 0);
                    else if (array[j, i] == 1)
                        test = new Tile(new Entity(tile_Sand, new Vector2(i * tileDims.X, j * tileDims.Y)), 1);
                    else if (array[j, i] == 2)
                        test = new Tile(new Entity(tile_Tree, new Vector2(i * tileDims.X, j * tileDims.Y)), 2);
                    else if (array[j, i] == 3)
                        test = new Tile(new Entity(tile_Water, new Vector2(i * tileDims.X, j * tileDims.Y)), 3);
                    map.Add(test);

                    int po = (j * Convert.ToInt32(gameArea.X)) + i;
                    Console.WriteLine(i + " : " + j + " = " + po);
                }


            //Do adjacency
            ///..................

            tilesMappo = new TileMap(map, new Vector2(11,17));
           
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

            if (elapsedTime > interval)
            {
                elapsedTime -= interval;
                // TODO: Add your update logic here

                m_player.Update(tilesMappo);

         
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
            tilesMappo.RenderTileMap(tileBatch);
            tileBatch.End();

            // TODO: Add your drawing code here
            spriteBatch.Begin(); 
            m_player.Draw(spriteBatch);
            spriteBatch.End(); 

            base.Draw(gameTime);
        }
    }
}
