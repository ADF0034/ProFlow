using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ProFlow.Data;
using ProFlow.Models;

namespace ProFlow.Controllers
{
    public class ProjectModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public ProjectModelsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }





        // GET: ProjectModels
        public async Task<IActionResult> Index()
        {
            //User
            var userid = _userManager.GetUserId(User);
            if (userid != null)
            {
                List<ProjectModel> list = new List<ProjectModel>();
                var projectse = await _context.userToProjects.Include(x=>x.project).Include(x=>x.role).Where(x => x.UserId == userid).ToListAsync();
                foreach (var item in projectse)
                {
                    ProjectModel newitem = item.project;
                    newitem.Role = item.role.Title;
                    list.Add(newitem); 
                }
                ViewData["Data"] = list as IEnumerable<ProjectModel>;
                return View();
            }
            else
            {
                List<ProjectModel> project = new List<ProjectModel>();
                return View(project);
            }
        }

        // GET: ProjectModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.projects == null)
            {
                return NotFound();
            }

            var projectModel = await _context.projects.FirstOrDefaultAsync(m => m.ProjectId == id);
            if (projectModel == null)
            {
                return NotFound();
            }

            return View(projectModel);
        }

        // GET: ProjectModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProjectModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProjectId,ProjctName,description,StartDate,TimeEntry,EndDate,Budget,Owner,Password")] ProjectModel projectModel)
        {
            projectModel.CreateDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                projectModel.ProjectId = Guid.NewGuid();
                _context.Add(projectModel);
                var role =  _context.roles.Where(x=>x.Title=="Owner");
                ApplicationUserToProject userToProject = new ApplicationUserToProject();
                var userid = await _userManager.GetUserAsync(User);
                userToProject.ProjectId = projectModel.ProjectId;
                userToProject.UserId = userid.Id;
                userToProject.roleid =  role.FirstOrDefault().id;
                _context.userToProjects.Add(userToProject);
                await _context.SaveChangesAsync();
                //TempData["StatusMessage"] = new ResponseMessage(StatusCodes.Status200OK,"Projekt er blevet oprette");
                return RedirectToAction(nameof(Index));
            }
            return View(projectModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> JoinProject([Bind("ProjctName,Password")] ProjektNameAndPassword projectModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var res = _context.projects.Where(x => x.ProjctName == projectModel.ProjctName && x.Password == projectModel.Password).FirstOrDefault().ProjectId;
                    if (res != null)
                    {
                        ApplicationUserToProject userToProject = new ApplicationUserToProject();
                        userToProject.ProjectId = res;
                        var userid = await _userManager.GetUserAsync(User);
                        userToProject.UserId = userid.Id;
                        _context.userToProjects.Add(userToProject);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {

                    return RedirectToAction(nameof(Index));
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }


        // GET: ProjectModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.projects == null)
            {
                return NotFound();
            }

            var projectModel = await _context.projects.FindAsync(id);
            if (projectModel == null)
            {
                return NotFound();
            }
            return View(projectModel);
        }

        // POST: ProjectModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProjectId,ProjctName,CreateDate,TimeEntry,EndDate,Budget,Owner,Password")] ProjectModel projectModel)
        {
            if (id != projectModel.ProjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(projectModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectModelExists(projectModel.ProjectId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(projectModel);
        }

        // GET: ProjectModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.projects == null)
            {
                return NotFound();
            }

            var projectModel = await _context.projects
                .FirstOrDefaultAsync(m => m.ProjectId == id);
            if (projectModel == null)
            {
                return NotFound();
            }

            return View(projectModel);
        }

        // POST: ProjectModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.projects == null)
            {
                return Problem("Entity set 'ApplicationDbContext.projects'  is null.");
            }
            var projectModel = await _context.projects.FindAsync(id);
            if (projectModel != null)
            {
                _context.projects.Remove(projectModel);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Users(Guid? ProjectId,string Projectname)
        {
            var projectse = await _context.userToProjects.Include(x=>x.User).Include(x=>x.role).Where(x => x.ProjectId == ProjectId).ToListAsync();
            List<Users> users = new List<Users>();
            foreach(var user in projectse)
            {
                if (user.role.Title != "Owner")
                {
                    users.Add(new Users { Id = user.UserId, Name = user.User.FirstName + " " + user.User.LastName, Role = user.role.Title });
                }
            }
            ViewData["ProjctName"] = Projectname;
            ViewData["Users"] = users;
            ViewData["ProjectId"] = ProjectId;
            ViewData["ProjectName"] = Projectname;
            return View();
        }

        [HttpPost, ActionName("UpdateToAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateToAdmin(Guid UserId, Guid? projectId, string projectname)
        {
            var user = await _context.userToProjects.FindAsync(UserId);
            if (user != null)
            {
                _context.userToProjects.Remove(user);
                var NewStatus = _context.roles.Where(x => x.Title == "Admin");
                ApplicationUserToProject userToProject = new ApplicationUserToProject();
                userToProject.ProjectId = projectId.Value;
                userToProject.UserId = UserId.ToString();
                userToProject.roleid = NewStatus.FirstOrDefault().id;
                _context.userToProjects.Add(userToProject);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Users), new { ProjectId = projectId, Projectname = projectname });
        }

        [HttpPost, ActionName("UpdateToNormalUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateToNormalUser(Guid UserId, Guid? projectId, string projectname)
        {
            var user = await _context.userToProjects.FindAsync(UserId);
            if (user != null)
            {
                _context.userToProjects.Remove(user);
                var NewStatus = _context.roles.Where(x => x.Title == "Normal User");
                ApplicationUserToProject userToProject = new ApplicationUserToProject();
                userToProject.ProjectId = projectId.Value;
                userToProject.UserId = UserId.ToString();
                userToProject.roleid = NewStatus.FirstOrDefault().id;
                _context.userToProjects.Add(userToProject);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Users), new { ProjectId = projectId, Projectname = projectname });
        }

        [HttpPost, ActionName("RemoveUserFromProject")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveUserFromProject(Guid UserId, Guid? projectId, string projectname)
        {
            var user = await _context.userToProjects.FindAsync(UserId);
            if (user != null)
            {
                _context.userToProjects.Remove(user);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Users), new { ProjectId = projectId , Projectname = projectname });
        }

        private bool ProjectModelExists(Guid id)
        {
          return (_context.projects?.Any(e => e.ProjectId == id)).GetValueOrDefault();
        }
    }
}
