namespace Snake.Components {
    public enum CollisionType {
        None,
        PowerUp,
        Fatal
    }

    public class CollisionManager {
        public CollisionType Collision(Grid grid, Snake snake) {
            if (snake.InTail(snake.X, snake.Y))
                return CollisionType.Fatal;
            if (grid.Tile(snake.X, snake.Y) == Grid.WALL_TILE)
                return CollisionType.Fatal;
            if (grid.Tile(snake.X, snake.Y) == Grid.POWERUP_TILE)
                return CollisionType.PowerUp;
            return CollisionType.None;
        }
    }
}
