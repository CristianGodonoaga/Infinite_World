using System;
using SFML.Graphics;
using SFML.System;
using Infinite_World.Views;
using Infinite_World;

namespace Infinite_World.NPC
{
    abstract class Entity : Transformable, Drawable
    {
        public const float NPC_MOVE_GRAVITY = 0.25f;

        public Vector2f StartPosition;

        protected RectangleShape rect;
        protected Vector2f velocity;
        protected Vector2f movement;
        protected World world;
        protected bool isFly = true;

        public int Direction
        {
            set
            {
                int dir = value >= 0 ? 1 : -1;
                Scale = new Vector2f(dir, 1);
            }

            get
            {
                int dir = Scale.X >= 0 ? 1 : -1;
                return dir;
            }
        }

        public Entity(World world)
        {
            this.world = world;
        }

        public void Spawn()
        {
            Position = StartPosition;
            velocity = new Vector2f();
            //Aici redarea efectelor
        }

        public void Update()
        {
            UpdateNPC();

            updatePhysics();

            Position += movement + velocity;

            //daca cade
            if (Position.Y > 400) // !!!!!!!!!
                OnKill();
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            target.Draw(rect, states);

            DrawNPC(target, states);
        }

        private void updatePhysics()
        {
            bool isFall = true;

            //velocity += new Vector2f(0, 0.15f);
            velocity.X *= 0.99f; // !! slow decrement
            velocity.Y += NPC_MOVE_GRAVITY;

            Vector2f nextPos = Position + velocity - rect.Origin;
            FloatRect playerRect = new FloatRect(nextPos, rect.Size);

            int pX = (int)((Position.X - rect.Origin.X + rect.Size.X / 2) / Tile.TILE_SIZE_X);
            int pY = (int)((Position.Y + rect.Size.Y) / Tile.TILE_SIZE_Y);

            Tile tile = world.GetTile(pX, pY); //Verificam textura
            if (tile != null)
            {
                FloatRect tileRect = new FloatRect(tile.Position, new Vector2f(Tile.TILE_SIZE_X, Tile.TILE_SIZE_Y));

                DebugRander.AddRectangle(tileRect, Color.Red); // @debug

                isFall = !playerRect.Intersects(tileRect);
                isFly = isFall;
            }

            if (isFall)
            {
                if (velocity.Y <= 10)
                    velocity.Y += 1;
            }
            else
            {
                velocity.Y = 0;
            }
            updatePhisicsWall(playerRect, pX, pY);
        }

        private void updatePhisicsWall(FloatRect playerRect, int pX, int pY)
        {
            Tile[] walls = new Tile[]
            {
                world.GetTile(pX - 1, pY - 1),
                world.GetTile(pX - 1, pY - 2),
                world.GetTile(pX - 1, pY - 3),
                world.GetTile(pX + 1, pY - 1),
                world.GetTile(pX + 1, pY - 2),
                world.GetTile(pX + 1, pY - 3)
            };

            foreach (Tile tile in walls)
            {
                if (tile == null)
                    continue;
                FloatRect tileRect = new FloatRect(tile.Position, new Vector2f(Tile.TILE_SIZE_X, Tile.TILE_SIZE_Y));

                DebugRander.AddRectangle(tileRect, Color.Yellow); // @debug

                if (playerRect.Intersects(tileRect))
                {
                    Vector2f offset = new Vector2f(playerRect.Left - tileRect.Left, 0);
                    offset.X /= Math.Abs(offset.X);
                    float speed = Math.Abs(movement.X);

                    if (offset.X > 0)
                    {
                        Position = new Vector2f((tileRect.Left + tileRect.Width) + playerRect.Width / 2, Position.Y);
                        //movement.X = ((tileRect.Left + tileRect.Width) - playerRect.Left);
                        movement.X = 0;
                        velocity.X = 0;
                    }
                    else if (offset.X < 0)
                    {
                        Position = new Vector2f(tileRect.Left - playerRect.Width / 2, Position.Y);
                        //movement.X = (tileRect.Left - (tileRect.Width + playerRect.Left));
                        movement.X = 0;
                        velocity.X = 0;
                    }

                    OnWallCollided();
                }
            }
        }
        public abstract void OnKill();
        public abstract void OnWallCollided();
        public abstract void UpdateNPC();
        public abstract void DrawNPC(RenderTarget target, RenderStates states);
    }
}
