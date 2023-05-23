using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ProFlow.Data;
using ProFlow.Models;

namespace ProFlow.Controllers
{
    public class AssignmentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AssignmentsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Assignments
        public async Task<IActionResult> Index(Guid projectId)
        {
            ViewData["projectId"] = projectId;
            var userid = _userManager.GetUserId(User);
            var users = await _context.userToProjects.Include(u=>u.User).Where(x=>x.ProjectId==projectId).ToListAsync();
            var name = new UsernameAndId();
            name.items = new List<SelectListItem>();
            UsernameAndId OpenUser = new UsernameAndId();
            name.items.Add(new SelectListItem { Text = "Åben", Value="0"});
            foreach (var user in users)
            {
                name.items.Add(new SelectListItem { Text = user.User.FirstName +" "+ user.User.LastName, Value = user.User.Id});
            }
            //ViewData["TimeTast"] = _context.projects.Where(x => x.ProjectId == projectId).FirstOrDefault().TimeEntry;

            List<Assignments> assignments = new List<Assignments>();
            if (projectId == Guid.Empty)
            {
                //var applicationDbContext = await _context.assignments.Include(x=>x.as).Where(x => x.ProjectId == projectId).ToListAsync();
                var userassigments = await _context.UserToAssignments.Include(x=>x.Assignments).Where(x=>x.UserId == userid).ToListAsync();
                foreach (var item in userassigments)
                {
                    assignments.Add(item.Assignments);
                }

            }
            else
            {
                var applicationDbContext = await _context.assignments.ToListAsync();
                foreach (var item in applicationDbContext)
                {
                    assignments.Add(item);
                }
            }
            ViewData["Users"] = name;
            ViewData["Data"] = assignments as IEnumerable<Assignments>;

            return View();
        }

        // GET: Assignments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.assignments == null)
            {
                return NotFound();
            }

            var assignments = await _context.assignments
                .Include(a => a.ProjectModel)
                .FirstOrDefaultAsync(m => m.AssigmentID == id);
            if (assignments == null)
            {
                return NotFound();
            }

            return View(assignments);
        }

        // GET: Assignments/Create
        public IActionResult Create()
        {
            ViewData["ProjectId"] = new SelectList(_context.projects, "ProjectId", "Password");
            return View();
        }

        // POST: Assignments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AssigmentID,Title,Beskrivelse,EstimatedTime,Userid,ProjectId")] Assignments assignments)
        {
            if (ModelState.IsValid)
            {
                if (assignments.Userid !="0")
                {
                    assignments.Assignt = true;
                    _context.Add(assignments);
                    await _context.SaveChangesAsync();
                    ApplicationUserToAssignments applicationUserToAssignments = new ApplicationUserToAssignments();
                    var userid = _userManager.GetUserId(User);
                    applicationUserToAssignments.AssignmentsId = assignments.AssigmentID;
                    applicationUserToAssignments.UserId = userid;
                    _context.Add(applicationUserToAssignments);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _context.Add(assignments);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index),new { projectId = assignments.ProjectId });
            }
            return View(assignments);
        }

        // GET: Assignments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.assignments == null)
            {
                return NotFound();
            }

            var assignments = await _context.assignments.FindAsync(id);
            if (assignments == null)
            {
                return NotFound();
            }
            ViewData["ProjectId"] = new SelectList(_context.projects, "ProjectId", "Password", assignments.ProjectId);
            return View(assignments);
        }

        // POST: Assignments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AssigmentID,Title,Beskrivelse,ProjectId")] Assignments assignments)
        {
            if (id != assignments.AssigmentID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignmentsExists(assignments.AssigmentID))
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
            ViewData["ProjectId"] = new SelectList(_context.projects, "ProjectId", "Password", assignments.ProjectId);
            return View(assignments);
        }

        // GET: Assignments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.assignments == null)
            {
                return NotFound();
            }

            var assignments = await _context.assignments
                .Include(a => a.ProjectModel)
                .FirstOrDefaultAsync(m => m.AssigmentID == id);
            if (assignments == null)
            {
                return NotFound();
            }

            return View(assignments);
        }

        // POST: Assignments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.assignments == null)
            {
                return Problem("Entity set 'ApplicationDbContext.assignments'  is null.");
            }
            var assignments = await _context.assignments.FindAsync(id);
            if (assignments != null)
            {
                _context.assignments.Remove(assignments);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


            private bool AssignmentsExists(int id)
        {
          return (_context.assignments?.Any(e => e.AssigmentID == id)).GetValueOrDefault();
        }
    }
}
