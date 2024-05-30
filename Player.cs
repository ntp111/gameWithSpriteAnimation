using SplashKitSDK;

public class Player 
{    
    private Sprite _playerRight;
    private Sprite _playerLeft;
    private const int Speed = 5;
    private const int JumpSpeed = 10;
    private const float Gravity = 0.5f;
    private float _verticalVelocity = 0;
    private bool _isJumping = false;
    private bool _facingRight = true;
    private bool _isMoving = false;
    public double X { get; set; }
    public double Y { get; set; }
    public int Width
    {
        get
        {
            return _playerRight.Width;
        }
    }
    public int Height
    {
        get
        {
            return _playerRight.Height;
        }
    }

    public Player(Window window, Rectangle ground)
    {
        Bitmap characterRight = SplashKit.LoadBitmap("PlayerRight", "Player.png");
        characterRight.SetCellDetails(characterRight.Width / 8, characterRight.Height / 9, 8, 9, 72); // cell width, height, cols, rows, count
        
        Bitmap characterLeft = SplashKit.LoadBitmap("PlayerLeft", "PlayerFlipped.png");
        characterLeft.SetCellDetails(characterLeft.Width / 8, characterLeft.Height / 9, 8, 9, 72); // cell width, height, cols, rows, count

        AnimationScript actionScript = SplashKit.LoadAnimationScript("ActionScript", "actionScript.txt");

        _playerRight = SplashKit.CreateSprite(characterRight, actionScript);
        _playerLeft = SplashKit.CreateSprite(characterLeft, actionScript);

        _playerRight.StartAnimation("Idle");
        _playerLeft.StartAnimation("IdleLeft");

        X = 20;
        Y = window.Height - ground.Height - _playerRight.Height;
    }

    public void Draw()
    {
        if (_facingRight)
        {
            SplashKit.DrawSprite(_playerRight, X, Y);
        }else{
            SplashKit.DrawSprite(_playerLeft, X, Y);
        }

    }

    public void Update(Rectangle ground)
    {
        if (_isJumping)
        {
            _verticalVelocity += Gravity;
            Y += (int)_verticalVelocity;
        }
        if (IsOnGround(ground))
        {
            ResetJump(ground);
        }
        SplashKit.UpdateSpriteAnimation(_playerRight);
        SplashKit.UpdateSpriteAnimation(_playerLeft);
    }

    public void HandleInput()
    {
        if (SplashKit.KeyTyped(KeyCode.RightKey))
        {
            _playerRight.StartAnimation("Run");
        }
        if (SplashKit.KeyTyped(KeyCode.LeftKey))
        {
            _playerLeft.StartAnimation("Run");
        }
        if (SplashKit.KeyTyped(KeyCode.SpaceKey) && !_isJumping)
        {
            _playerLeft.StartAnimation("JumpLeft");
            _playerRight.StartAnimation("Jump");
        }

        if (SplashKit.KeyDown(KeyCode.RightKey))
        {
            _facingRight = true;
            _isMoving = true;
            _playerRight.X += Speed;
            _playerLeft.X += Speed;
        }
        if (SplashKit.KeyDown(KeyCode.LeftKey))
        {
            _facingRight = false;
            _isMoving = true;
            _playerRight.X -= Speed;
            _playerLeft.X -= Speed;
        }
        if (SplashKit.KeyDown(KeyCode.SpaceKey))
        {
            _isJumping = true;            
            _verticalVelocity = -JumpSpeed;
        }

        if (!SplashKit.KeyDown(KeyCode.LeftKey) && !SplashKit.KeyDown(KeyCode.RightKey) && !SplashKit.KeyDown(KeyCode.SpaceKey) && _isMoving)
        {
            _isMoving = false;
            _playerRight.StartAnimation("Idle");
            _playerLeft.StartAnimation("IdleLeft");
        }
    }

    public bool IsOnGround(Rectangle ground){
        return Y + Height >= ground.Y;
    }

    public void ResetJump(Rectangle ground)
    {
        if (_isJumping){
            _isJumping = false;
            _verticalVelocity = 0;
            Y = ground.Y - Height;

            if (_facingRight)
            {
                _playerRight.StartAnimation("Idle");
            }
            else
            {
                _playerLeft.StartAnimation("IdleLeft");
            }
        }
    }
}