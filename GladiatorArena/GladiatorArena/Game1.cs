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

        Vector2 gameArea = new Vector2(17, 11);
        Vector2 tileDims = new Vector2(64, 64);

        Player m_player;
        int AmountofDucks = 0;

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

            graphics.PreferredBackBufferHeight = Convert.ToInt32(tileDims.Y * gameArea.Y);
            graphics.PreferredBackBufferWidth = Convert.ToInt32(tileDims.X * gameArea.X);
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
            spr_Duck = this.Content.Load<Texture2D>("DuckSprite");
            spr_Player = this.Content.Load<Texture2D>("AngryRussellSprite");


            tile_Grass = this.Content.Load<Texture2D>("GrassTile");
            tile_Sand = this.Content.Load<Texture2D>("SandTile");
            tile_Tree = this.Content.Load<Texture2D>("TreeTile");
            tile_Water = this.Content.Load<Texture2D>("WaterTile");
            //eList.Add(new Entity(spr_Duck, new Vector2(0, 0), new Vector2(100, 100), 0.5f));

            //Build level data
            m_player = new Player(spr_Player);

            List<Tile> map = new List<Tile>();

            int[,] array = new int[11, 17] { 
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
            
            for (int i = 0; i < gameArea.Y; i++)
            {
                for (int j = 0; j < gameArea.X; j++)
                {
                    Tile test = new GladiatorArena.Tile();
                    if (array[i, j] == 0)
                        test = new Tile(new Entity(tile_Grass, new Vector2(j * tileDims.Y, i * tileDims.X)), 0);
                    else if (array[i, j] == 1)
                        test = new Tile(new Entity(tile_Sand, new Vector2(j * tileDims.Y, i * tileDims.X)), 1);
                    else if (array[i, j] == 2)
                        test = new Tile(new Entity(tile_Tree, new Vector2(j * tileDims.Y, i * tileDims.X)), 2);
                    else if (array[i, j] == 3)
                        test = new Tile(new Entity(tile_Water, new Vector2(j * tileDims.Y, i * tileDims.X)), 3);
                    map.Add(test);
                }
            }


            //Do adjacency

                ////
            //

            tilesMappo = new TileMap(map, new Vector2(17,11));

            Console.WriteLine(tilesMappo.ConvertTo2D(17).X + ":" + tilesMappo.ConvertTo2D(17).Y);
            Console.WriteLine(tilesMappo.ConvertTo2D(5).X + ":" + tilesMappo.ConvertTo2D(5).Y);
            Console.WriteLine(tilesMappo.ConvertTo2D(101).X + ":" + tilesMappo.ConvertTo2D(101).Y);
            //{ 0,0,0,0,0,0,0,0,0,0,0 }
            //{ 0,0,0,0,0,0,0,0,0,0,0 }
            //{ 0,0,0,0,0,0,0,0,0,0,0 }
            //{ 0,0,0,0,0,0,0,0,0,0,0 }
            //{ 0,0,0,0,0,0,0,0,0,0,0 }
            //{ 0,0,0,0,0,0,0,0,0,0,0 }
            //{ 0,0,0,0,0,0,0,0,0,0,0 }
            //{ 0,0,0,0,0,0,0,0,0,0,0 }
            //{ 0,0,0,0,0,0,0,0,0,0,0 }
            //{ 0,0,0,0,0,0,0,0,0,0,0 }
            //{ 0,0,0,0,0,0,0,0,0,0,0 }

            //

            Random rnd = new Random();
            scr_dimensions = new Vector2(graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height);

            for (int i = 0; i < AmountofDucks; i++)
            {
                eList.Add(new Entity(spr_Duck, new Vector2(rnd.Next(1, (int)scr_dimensions.X - 64), rnd.Next(1, (int)scr_dimensions.Y - 64)), new Vector2(rnd.Next(100, 400), rnd.Next(100, 400)), (float)rnd.Next(1, 5) / 10));
            }
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

            m_player.Update(tilesMappo);

            for (int i = 0; i < eList.Count; i++)
                eList[i].Update(gameTime, eList, graphics.GraphicsDevice);

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
            spriteBatch.Begin(); //Start batch

            m_player.Draw(spriteBatch);

            for (int i = 0; i < eList.Count; i++)
                eList[i].Draw(spriteBatch);

            spriteBatch.End();  //Draw



            base.Draw(gameTime);
        }
    }
}
