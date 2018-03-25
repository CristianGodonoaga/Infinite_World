using Infinite_World.NPC;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
using System;


namespace Infinite_World
{
    class Player : Entity
    {
        public const float PLAYER_MOVE_SPEED = 4f;
        public const float PLAYER_MOVE_SPEED_ACCELERATION = 0.2f;

        RectangleShape rectDirection;

        public Player(World world) : base(world)
        {
            rect = new RectangleShape(new Vector2f(Tile.TILE_SIZE_X * 1.5f, Tile.TILE_SIZE_Y * 2.8f));
            rect.Texture = Content.playerNess;
            //rect.TextureRect = new IntRect(x, y, TILE_SIZE_X, TILE_SIZE_Y);
            rect.Origin = new Vector2f(rect.Size.X / 2, 0);


            rectDirection = new RectangleShape(new Vector2f(50, 3));
            rectDirection.FillColor = Color.Red;
            rectDirection.Position = new Vector2f(0, rect.Size.Y / 2 - 1);
        }

        public override void OnKill()
        {
            Spawn();
            // Efects
        }

        public override void OnWallCollided()
        {

        }

        public override void UpdateNPC()
        {
            updateMovement();
        }

        public override void DrawNPC(RenderTarget target, RenderStates states)
        {
            target.Draw(rectDirection, states);
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
                    if (movement.X > 0)
                        movement.X = 0;
                    movement.X -= PLAYER_MOVE_SPEED_ACCELERATION;
                    Direction = -1;
                }
                if (isMoveRight)
                {
                    if (movement.X < 0)
                        movement.X = 0;
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
