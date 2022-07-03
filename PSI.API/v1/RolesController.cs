using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PSI.Administration.Identity;

namespace PSI.API.v1
{
    [Route("v1/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;

        public RolesController(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        /// <summary>
        /// 通过角色 Id 查找角色
        /// </summary>
        /// <param name="id">角色 Id</param>
        /// <returns>查询到的角色</returns>
        [HttpGet("{id}")]
        public Task<Role> Get([FromRoute] string id)
        {
            return _roleManager.FindByIdAsync(id);
        }

        /// <summary>
        /// 获取所有角色
        /// </summary>
        /// <returns>所有角色</returns>
        [HttpGet]
        public Task<IEnumerable<Role>> Get()
        {
            if (_roleManager.Roles == null)
            {
                return Task.FromResult(Enumerable.Empty<Role>());
            }
            return Task.FromResult(_roleManager.Roles.AsEnumerable());
        }


        /// <summary>
        /// 创建角色
        /// </summary>
        /// <param name="role">将要创建的角色</param>
        /// <returns>返是否创建成功</returns>
        [HttpPost]
        public async Task<IdentityResult> Post(Role role)
        {
            if (await _roleManager.RoleExistsAsync(role.Name))
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Description = $"角色{role.Name}已存在！"
                });
            }
            var result = await _roleManager.CreateAsync(role);
            return result;
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">删除的角色id</param>
        /// <returns>删除操作结果</returns>
        [HttpDelete("{id}")]
        public async Task<IdentityResult> Delete([FromRoute] string id)
        {
            Role role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return IdentityResult.Failed(new IdentityError() { Description = "找不到要删除的角色" });
            }
            else if (role.NormalizedName == Role.Administrators)
            {
                return IdentityResult.Failed(new IdentityError() { Description = $"{Role.Administrators}为系统默认管理员，不允许删除默认管理员角色" });
            }
            return await _roleManager.DeleteAsync(role);
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role">将要更新的角色</param>
        /// <returns>更新结果</returns>
        [HttpPut]
        [HttpPatch]
        public async Task<IdentityResult> Patch(Role role)
        {
            return await _roleManager.UpdateAsync(role);
        }

        /// <summary>
        /// 检测角色是否已存在
        /// </summary>
        /// <param name="normalizedName">角色名</param>
        /// <returns>true：已存在，false：不存在</returns>
        [HttpGet("RoleExists/{name}")]
        public Task<bool> RoleExists([FromRoute] string name)
        {
            return _roleManager.RoleExistsAsync(name);
        }
    }
}
