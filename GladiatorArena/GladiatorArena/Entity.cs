using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//XNA
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GladiatorArena
{
    class Entity
    {
        //public
        public Entity()
        {
            m_spr_sprite = null;
            m_velocity = Vector2.Zero;
            m_position = Vector2.Zero;

            CalcScale(1);
        }

        public Entity(Texture2D spr_sprite, Vector2 position, float scale)
        {
            m_spr_sprite = spr_sprite;
            m_velocity = Vector2.Zero;
            m_position = position;

            CalcScale(scale);
        }

        public Entity(Texture2D spr_sprite, Vector2 position, Vector2 velocity, float scale)
        {
            m_spr_sprite = spr_sprite;
            m_velocity = velocity;
            m_position = position;

            CalcScale(scale);
        }

        //Functions
        public void ApplyImpulse(Vector2 impulseVel) { m_velocity += impulseVel; }
        

        public void Update(GameTime gameTime, List<Entity> entities, GraphicsDevice gDevice)
        {
            //Check Collisions
            for (int i = 0; i < entities.Count(); i++)
            {
                if (this != entities[i])
                {
                    bool Res = Collider.CheckColliding(this, entities[i]);
                    //Do collision Response
                    if (Res)
                    {
                        //Basic Collision Response, just invert velocity
                        m_velocity *= -1;
                    }
                }
            }

            if (m_position.X < 0 && m_velocity.X < 0)
                m_velocity.X *= -1;
            else if (m_position.Y < 0 && m_velocity.Y < 0)
                m_velocity.Y *= -1;
            else if (m_position.X >= gDevice.Viewport.Width - m_width && m_velocity.X > 0)
                m_velocity.X *= -1;
            else if (m_position.Y >= gDevice.Viewport.Height - m_height && m_velocity.Y > 0)
                m_velocity.Y *= -1;

            //Update Position
            UpdatePosition(gameTime);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(m_spr_sprite, position: m_position, scale: m_scale);
        }

        //Setters
        public void SetPosition(Vector2 position) { m_position = position; }
        public void SetVelocity(Vector2 velocity) { m_velocity = velocity; }

        //Getters
        public Vector2 GetPosition() { return m_position; }
        public Vector2 GetVelocity() { return m_velocity; }
        public float GetWidth() { return m_width; }
        public float GetHeight() { return m_width; }

        public Vector2 GetCentre()
        {
            Vector2 centre;
            centre.X = m_position.X + m_width / 2;
            centre.Y = m_position.Y + m_height / 2;
            return centre;
        }

        public float GetRadius()
        {
            return MathHelper.Max(m_width, m_height) / 2;
        }

        //Privatw
        private void CalcScale(float scale)
        {
            m_width = m_spr_sprite.Width * scale;

            m_scale = new Vector2(m_width / (float)m_spr_sprite.Width, m_width / (float)m_spr_sprite.Width);
            m_height = m_spr_sprite.Height * m_scale.Y;
        }

        private void UpdatePosition(GameTime gameTime)
        {
            m_position += m_velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        //Members
        private Vector2 m_position;
        private Vector2 m_velocity;
        private Texture2D m_spr_sprite;
        //Scale
        private float m_width;
        private float m_height;
        private Vector2 m_scale;
        private int Count = 0;
    }
}
