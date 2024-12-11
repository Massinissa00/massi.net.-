using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using mvc.Data;
using mvc.Models;

public class TeacherController : Controller
{
    private readonly UserManager<Teacher> _userManager;

    public TeacherController(UserManager<Teacher> userManager)
    {
        _userManager = userManager;
    }

    // Afficher tous les enseignants
    public async Task<IActionResult> Index()
    {
        // Récupérer tous les enseignants depuis la base
        var teachers = _userManager.Users.ToList();
        return View(teachers);
    }

    // Ajouter un Teacher (GET)
    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    // Ajouter un Teacher (POST)
    [HttpPost]
    public async Task<IActionResult> Add(Teacher teacher)
    {
        if (!ModelState.IsValid)
        {
            return View(teacher);
        }

        // Créer l'enseignant avec un mot de passe par défaut (modifiable ensuite)
        var result = await _userManager.CreateAsync(teacher, "DefaultPassword123!");

        if (result.Succeeded)
        {
            // Rediriger vers la liste des enseignants
            return RedirectToAction("Index");
        }

        // Si l'ajout échoue, ajouter les erreurs au modèle
        foreach (var error in result.Errors)
        {
            ModelState.AddModelError(string.Empty, error.Description);
        }

        return View(teacher);
    }
}
