using UnityEngine;

namespace ShootEmUp
{
    public sealed class CharacterController : MonoBehaviour
    {
        [SerializeField] private InputManager _inputManager;
        
        [SerializeField] private GameObject character; 
        [SerializeField] private GameManager gameManager;
        [SerializeField] private BulletSystem _bulletSystem;
        [SerializeField] private BulletConfig _bulletConfig;
        
        public bool _fireRequired;

        private IMoveComponent _moveComponent;

        private void Start()
        {
            _moveComponent = character.GetComponent<IMoveComponent>();
        }

        private void OnEnable()
        {
            _inputManager.OnMove += OnMove;
            _inputManager.OnShoot += OnShoot;
            
            this.character.GetComponent<HitPointsComponent>().hpEmpty += this.OnCharacterDeath;
        }

        private void OnDisable()
        {
            _inputManager.OnMove -= OnMove;
            _inputManager.OnShoot -= OnShoot;

            this.character.GetComponent<HitPointsComponent>().hpEmpty -= this.OnCharacterDeath;
        }
        
        private void OnMove(Vector2 direction)
        {
            var offset = new Vector2(direction.x, direction.y) * Time.fixedDeltaTime;
            this._moveComponent.Move(offset);
        }

        private void OnCharacterDeath(GameObject _) => this.gameManager.FinishGame();

        private void OnShoot()
        {
            if (this._fireRequired) return;
            
            this._fireRequired = true;
            this.OnFlyBullet();
            this._fireRequired = false;
        }

        private void OnFlyBullet()
        {
            var weapon = this.character.GetComponent<WeaponComponent>();
            _bulletSystem.FlyBulletByArgs(new BulletSystem.Args
            {
                isPlayer = true,
                physicsLayer = (int) this._bulletConfig.physicsLayer,
                color = this._bulletConfig.color,
                damage = this._bulletConfig.damage,
                position = weapon.Position,
                velocity = weapon.Rotation * Vector3.up * this._bulletConfig.speed
            });
        }
    }
}