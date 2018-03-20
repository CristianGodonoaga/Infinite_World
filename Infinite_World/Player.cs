using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;

namespace Infinite_World
{
    class Player : Transformable, Drawable
    {
        public const float PLAYER_MOVE_SPEED = 4f;
        public const float PLAYER_MOVE_SPEED_ACCELERATION = 0.2f;

        public Vector2f StartPosition;

        RectangleShape rect;
        RectangleShape rectDirection;
        Vector2f velocity;  //Acceleratia jucatorului
        Vector2f movement;
        World world;

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
        public Player(World world)
        {
            rect = new RectangleShape(new Vector2f(Tile.TILE_SIZE_X * 1.5f, Tile.TILE_SIZE_Y * 2.8f));
            rect.Origin = new Vector2f(rect.Size.X / 2, 0);

            this.world = world;

            rectDirection = new RectangleShape(new Vector2f(50, 3));
            rectDirection.FillColor = Color.Red;
            rectDirection.Position = new Vector2f(0, rect.Size.Y / 2 - 1);
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            states.Transform *= Transform;
            target.Draw(rect, states);
            target.Draw(rectDirection, states);
        }

        public void Spawn()
        {
            Position = StartPosition;
            velocity = new Vector2f();
            //Aici redarea efectelor
        }

        public void Update()
        {
            updatePhysics(); //Refrash fizica
            updateMovement(); //Refrash pozitie

            Position += movement + velocity;

            if (Position.Y > Program.Window.Size.Y)
                Spawn();
        }

        private void updatePhysics()
        {
            bool isFall = true;

            velocity += new Vector2f(0, 0.15f);

            Vector2f nextPos = Position + velocity - rect.Origin;
            FloatRect playerRect = new FloatRect(nextPos, rect.Size);

            int pX = (int)((Position.X - rect.Origin.X + rect.Size.X / 2) / Tile.TILE_SIZE_X);
            int pY = (int)((Position.Y + rect.Size.Y) / Tile.TILE_SIZE_Y);

            Tile tile = world.GetTile(pX, pY); //Verificam textura
            if(tile != null)
            {
                FloatRect tileRect = new FloatRect(tile.Position, new Vector2f(Tile.TILE_SIZE_X, Tile.TILE_SIZE_Y));

                DebugRander.AddRectangle(tileRect, Color.Red); // @debug

                isFall = !playerRect.Intersects(tileRect);
            }

            if (isFall)
            {
                if(velocity.Y <= 10)
                    velocity.Y += 1;
            }else
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

            foreach(Tile tile in walls)
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
                        movement.X = ((tileRect.Left + tileRect.Width) - playerRect.Left);
                        velocity.X = 0;
                    }
                    else if (offset.X < 0)
                    {
                        movement.X = (tileRect.Left - (tileRect.Width + playerRect.Left));
                        velocity.X = 0;
                    }
                }
            }
        }

        private void updateMovement()
        {
            // @todo: De optimizat!!! 
            bool isMoveLeft = Keyboard.IsKeyPressed(Keyboard.Key.A);
            bool isMoveRight = Keyboard.IsKeyPressed(Keyboard.Key.D);
            bool isMove = isMoveLeft || isMoveRight;

            if (isMove)
            {
                // Acceleram viteza personajului
                if (isMoveLeft)
                {
                    //if (movement.X > 0)
                        //movement.X = 0;
                    movement.X -= PLAYER_MOVE_SPEED_ACCELERATION;
                    Direction = -1;
                }
                if (isMoveRight)
                {
                    //if (movement.X < 0)
                        //movement.X = 0;
                    movement.X += PLAYER_MOVE_SPEED_ACCELERATION;
                    Direction = 1;
                }
                // Limitam viteza maxima
                if (movement.X > PLAYER_MOVE_SPEED)
                    movement.X = PLAYER_MOVE_SPEED;
                else if (movement.X < -PLAYER_MOVE_SPEED)
                    movement.X = -PLAYER_MOVE_SPEED;
            }
            else
            {
                movement = new Vector2f();
            }
        }
    }
}
