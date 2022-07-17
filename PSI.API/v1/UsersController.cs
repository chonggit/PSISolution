using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PSI.Administration.Identity;
using IdentityUser = PSI.Administration.Identity.User;

namespace PSI.API.v1
{
    [ApiController]
    [Route("v1/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// 通过 id 查找用户
        /// </summary>
        /// <param name="id">用户 id</param>
        /// <returns>找到的用户</returns>
        [HttpGet("{id}")]
        public Task<User> Get([FromRoute] string id)
        {
            return _userManager.FindByIdAsync(id);
        }

        /// <summary>
        /// 通过角色查找归属角色的所有用户
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <returns>属于角色的用户</returns>
        [HttpGet("[action]/{roleName}")]
        public Task<IList<User>> InRole(string roleName)
        {
            return _userManager.GetUsersInRoleAsync(roleName);
        }

        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="user">用户信息</param>
        /// <returns>创建用户结棍</returns>
        [HttpPost]
        public async Task<IActionResult> Post(User user)
        {
            IdentityResult result = await _userManager.CreateAsync(user);

            if (result.Succeeded == false)
            {
                return Ok(result);
            }

            return Created(string.Empty, new
            {
                user,
                result.Errors,
                result.Succeeded
            });
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user">需要更新的用户</param>
        /// <returns>更新是结果</returns>
        [HttpPut]
        [HttpPatch]
        public async Task<IActionResult> Patch(User user)
        {
            return Ok(await _userManager.UpdateAsync(user));
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户的 Id</param>
        /// <returns>删除结果</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            User user = await _userManager.FindByIdAsync(id);

            if (user.UserName == IdentityUser.Manager)
            {
                return Ok(IdentityResult.Failed(new IdentityError { Description = "不能删除系统默认管理员！" }));
            }

            if (user == null)
            {
                return Ok(IdentityResult.Failed(new IdentityError { Description = $"找不到 Id：{id}的用户" }));
            }
            return Ok(await _userManager.DeleteAsync(user));
        }

        /// <summary>
        /// 将用户添加到角色
        /// </summary>
        /// <param name="id">用户 id</param>
        /// <param name="roles">角色</param>
        /// <returns>是否添加成功</returns>
        [HttpPost("Roles/{id}")]
        public async Task<IActionResult> AddToRole([FromRoute] string id, [FromBody] string[] roles)
        {
            User user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return Ok(IdentityResult.Failed(new IdentityError { Code = "Not Found", Description = $"找不到 Id：{id}的用户" }));
            }

            return Created(string.Empty, await _userManager.AddToRolesAsync(user, roles));
        }

        /// <summary>
        /// 从角色中删除用户
        /// </summary>
        /// <param name="id">用户 id</param>
        /// <param name="roles">角色</param>
        /// <returns>删除结果</returns>
        [HttpDelete("Roles/{id}")]
        public async Task<IActionResult> RemoveFromRole([FromRoute] string id, [FromBody] string[] roles)
        {
            User user = await _userManager.FindByIdAsync(id);
            //不能删除系统默认管理员的管理角色
            if (user.UserName == IdentityUser.Manager && roles.Any(r => r == Role.Administrators))
            {
                return Ok(IdentityResult.Failed(new IdentityError { Description = $"不能删除系统默认管理员的管理角色" }));
            }

            if (user == null)
            {
                return Ok(IdentityResult.Failed(new IdentityError { Description = $"找不到 Id：{id}的用户" }));
            }

            return Ok(await _userManager.RemoveFromRolesAsync(user, roles));
        }
    }
}
